using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giraffics.Utilities.Game
{
    /// <summary>
    /// These are events that occur during only one frame and 
    /// which can each be represented by one varient.
    /// </summary>
    enum SimpleInstantEvent
    {
        MouseWheel,
        MouseMove,
        MouseEnter,
        MouseLeave,
        WindowFocused,
        WindowUnfocused,
        WindowResized,
        WindowMoved,
        WindowClosed
    }


    /// <summary>
    /// Translates the input-related events of a Giraffic object into a form 
    /// digestible by a gameloop. Allows instantaneous events like a mouse
    /// click or a key press to be captured by a gameloop.
    /// </summary>
    public class EventListener
    {
        private Giraffic giraffic;

        // { isActive, isRegistered }
        private Dictionary<SimpleInstantEvent, bool[]> simpleInstantEventData;
        // { isActive, isRegistered }
        private Dictionary<MouseButtons, bool[]> mouseDoubleClickData;
        // { { isPressedActive, isPressedRegistered }, { isReleasedActive, isReleasedRegistered }, { isDown } }
        private Dictionary<MouseButtons, bool[,]> mouseButtonData;
        // { { isPressedActive, isPressedRegistered }, { isReleasedActive, isReleasedRegistered }, { isDown } }
        private Dictionary<string, bool[,]> keysData;

        // How much mouse wheel delta has been scrolled since the last frame.
        private int scrollDelta = 0;
        // { lastFramePos, thisFramePos }
        private Point[] mousePosData;
        // { lastFrameSize, thisFrameSize }
        private Size[] windowSizeData;
        // { lastFramePos, thisFramePos }
        private Point[] windowPosData;
        

        /// <summary>
        /// Translates the input-related events of a Giraffic object into a form 
        /// digestible by a gameloop. Allows instantaneous events like a mouse
        /// click or a key press to be captured by a gameloop.
        /// </summary>
        public EventListener(Giraffic giraffic)
        {
            this.giraffic = giraffic;
            
            // Instantiate simple event data with all dict keys
            simpleInstantEventData = new Dictionary<SimpleInstantEvent, bool[]>();
            foreach (SimpleInstantEvent eventVariant in Enum.GetValues(typeof(SimpleInstantEvent)))
            {
                simpleInstantEventData.Add(eventVariant, new bool[] { false, false });
            }

            // Instantiate mouse double click data with all dict keys
            mouseDoubleClickData = new Dictionary<MouseButtons, bool[]>();
            foreach(MouseButtons buttonVariant in Enum.GetValues(typeof(MouseButtons)))
            {
                mouseDoubleClickData.Add(buttonVariant, new bool[] { false, false });
            }

            // Instantiate mousebutton event data with all dict keys
            mouseButtonData = new Dictionary<MouseButtons, bool[,]>();
            foreach (MouseButtons buttonVariant in Enum.GetValues(typeof(MouseButtons)))
            {
                mouseButtonData.Add(buttonVariant, new bool[,] { { false, false }, { false, false }, { false, false } });
            }

            // Instantiate key event data with all dict keys
            keysData = new Dictionary<string, bool[,]>();
            foreach (string keyVariant in Enum.GetNames(typeof(Keys)))
            {
                keysData.Add(keyVariant, new bool[,] { { false, false }, { false, false }, { false, false } });
            }

            // Set default positions for mouse position data, window size data, and window position data
            mousePosData = new Point[] { giraffic.MousePosition, giraffic.MousePosition };
            windowSizeData = new Size[] { giraffic.Size, giraffic.Size };
            windowPosData = new Point[] { giraffic.Position, giraffic.Position };


            // Listen to the giraffic's input events.
            giraffic.KeyDown += Giraffic_KeyDown;
            giraffic.KeyUp += Giraffic_KeyUp;
            giraffic.MouseDown += Giraffic_MouseDown;
            giraffic.MouseUp += Giraffic_MouseUp;
            giraffic.MouseDoubleClick += Giraffic_MouseDoubleClick;
            giraffic.MouseWheel += Giraffic_MouseWheel;
            giraffic.MouseMove += Giraffic_MouseMove;
            giraffic.MouseEnter += Giraffic_MouseEnter;
            giraffic.MouseLeave += Giraffic_MouseLeave;
            giraffic.WindowFocused += Giraffic_WindowFocused;
            giraffic.WindowUnfocused += Giraffic_WindowUnfocused;
            giraffic.WindowResized += Giraffic_WindowResized;
            giraffic.WindowMoved += Giraffic_WindowMoved;
            giraffic.WindowClosed += Giraffic_WindowClosed;
        }

        /// <summary>Updates the EventListener with proper data. Call this in your gameloop 
        /// before or after it is used and in the same thread as the one where you are using the 
        /// event listener.</summary>
        public void Update()
        {
            // Unregister and reset registered simple events
            foreach (SimpleInstantEvent instantEvent in simpleInstantEventData.Keys.ToArray())
            {
                if (simpleInstantEventData[instantEvent][1])
                    simpleInstantEventData[instantEvent] = new bool[] { false, false };
            }
            // Register unregistered simple events
            foreach (SimpleInstantEvent instantEvent in simpleInstantEventData.Keys.ToArray())
            {
                if (simpleInstantEventData[instantEvent][0])
                    simpleInstantEventData[instantEvent][1] = true;
            }

            // Unregister and reset registered double click events
            foreach (MouseButtons mouseButton in mouseDoubleClickData.Keys.ToArray())
            {
                if (mouseDoubleClickData[mouseButton][1])
                    mouseDoubleClickData[mouseButton] = new bool[] { false, false };
            }
            // Register unregistered double click events
            foreach (MouseButtons mouseButton in mouseDoubleClickData.Keys.ToArray())
            {
                if (mouseDoubleClickData[mouseButton][0])
                    mouseDoubleClickData[mouseButton][1] = true;
            }

            // Unregister and reset registered mouse button events
            foreach (MouseButtons mouseButton in mouseButtonData.Keys.ToArray())
            {
                if (mouseButtonData[mouseButton][0, 1])
                {
                    mouseButtonData[mouseButton][0, 0] = false;
                    mouseButtonData[mouseButton][0, 1] = false;
                }
                if (mouseButtonData[mouseButton][1, 1])
                {
                    mouseButtonData[mouseButton][1, 0] = false;
                    mouseButtonData[mouseButton][1, 1] = false;
                }
            }
            // Register unregistered mouse button events
            foreach (MouseButtons mouseButton in mouseButtonData.Keys.ToArray())
            {
                if (mouseButtonData[mouseButton][0, 0])
                    mouseButtonData[mouseButton][0, 1] = true;
                if (mouseButtonData[mouseButton][1, 0])
                    mouseButtonData[mouseButton][1, 1] = true;
            }

            // Unregister and reset registered key events
            foreach (string key in keysData.Keys.ToArray())
            {
                if (keysData[key][0, 1])
                {
                    keysData[key][0, 0] = false;
                    keysData[key][0, 1] = false;
                }
                if (keysData[key][1, 1])
                {
                    keysData[key][1, 0] = false;
                    keysData[key][1, 1] = false;
                }
            }
            // Register unregistered key events
            foreach (string key in keysData.Keys.ToArray())
            {
                if (keysData[key][0, 0])
                    keysData[key][0, 1] = true;
                if (keysData[key][1, 0])
                    keysData[key][1, 1] = true;
            }

            // Clear tracked mouse wheel delta from the last update
            if (simpleInstantEventData[SimpleInstantEvent.MouseWheel][1])
                scrollDelta = 0;

            // Update mouse position data, window size data, and window position data
            mousePosData = new Point[] { mousePosData[1], giraffic.MousePosition };
            windowSizeData = new Size[] { windowSizeData[1], giraffic.Size };
            windowPosData = new Point[] { windowPosData[1], giraffic.Position };
        }

        /// <summary>Determines if the key was first pressed down on this update.</summary>
        public bool IsKeyPressed(Keys key)
        {
            return keysData[Enum.GetName(typeof(Keys), key)][0, 1];
        }

        /// <summary>Determines if the key was released on this update.</summary>
        public bool IsKeyReleased(Keys key)
        {
            return keysData[Enum.GetName(typeof(Keys), key)][1, 1];
        }

        /// <summary>Determines if the key is currently down.</summary>
        public bool IsKeyDown(Keys key)
        {
            return keysData[Enum.GetName(typeof(Keys), key)][2, 0];
        }

        /// <summary>Determines if the mouse button was first pressed on this update.</summary>
        public bool IsMouseButtonPressed(MouseButtons button)
        {
            return mouseButtonData[button][0, 1];
        }

        /// <summary>Determines if the mouse button was released on this update.</summary>
        public bool IsMouseButtonReleased(MouseButtons button)
        {
            return mouseButtonData[button][1, 1];
        }

        /// <summary>Determines if the mouse button is currently down.</summary>
        public bool IsMouseButtonDown(MouseButtons button)
        {
            return mouseButtonData[button][2, 0];
        }

        /// <summary>Determines if the given mouse button was double clicked on this update.</summary>
        public bool IsDoubleClick(MouseButtons mouseButton)
        {
            return mouseDoubleClickData[mouseButton][1];
        }

        /// <summary>Gets the total amount of scroll between this update and the last.</summary>
        public int GetScrollDelta()
        {
            return scrollDelta;
        }

        /// <summary>Gets the current mouse position in window-space.</summary>
        public Point GetMousePos()
        {
            return mousePosData[1];
        }

        /// <summary>Gets the mouse position in window-space recorded from the last update.</summary>
        public Point GetLastMousePos()
        {
            return mousePosData[0];
        }

        /// <summary>Gets the current size of the Giraffic window.</summary>
        public Size GetWindowSize()
        {
            return windowSizeData[1];
        }

        /// <summary>Gets the size of the Giaffic window recorded on the last update.</summary>
        public Size GetLastWindowSize()
        {
            return windowSizeData[0];
        }

        /// <summary>Gets the position of the window in screen-space.</summary>
        public Point GetWindowPos()
        {
            return windowPosData[1];
        }

        /// <summary>Gets the position of the window in screen-space recorded on the last update.</summary>
        public Point GetLastWindowPos()
        {
            return windowPosData[0];
        }

        /// <summary>Determines if the window was put into focus between this update and the last.</summary>
        public bool IsWindowFocusing()
        {
            return simpleInstantEventData[SimpleInstantEvent.WindowFocused][1];
        }

        /// <summary>Determines if the window was put out of focus between this update and the last.</summary>
        public bool IsWindowUnfocusing()
        {
            return simpleInstantEventData[SimpleInstantEvent.WindowUnfocused][1];
        }

        /// <summary>Determines if the user's mouse entered the window's bounds between this frame and the last.</summary>
        public bool IsMouseEntering()
        {
            return simpleInstantEventData[SimpleInstantEvent.MouseEnter][1];
        }

        /// <summary>Determines if the use's mouse exited the window's bounds between this frame and the last.</summary>
        public bool IsMouseExiting()
        {
            return simpleInstantEventData[SimpleInstantEvent.MouseLeave][1];
        }

        /// <summary>Determines if the window just closed.</summary>
        public bool IsWindowClose()
        {
            return simpleInstantEventData[SimpleInstantEvent.WindowClosed][1];
        }





        #region Giraffic Event Listeners
        private void Giraffic_KeyDown(object sender, KeyEventArgs e)
        {
            string key = Enum.GetName(typeof(Keys), e.KeyCode);
            keysData[key][0, 0] = true;
            keysData[key][2, 0] = true;
        }

        private void Giraffic_KeyUp(object sender, KeyEventArgs e)
        {
            string key = Enum.GetName(typeof(Keys), e.KeyCode);
            keysData[key][1, 0] = true;
            keysData[key][2, 0] = false;
        }

        private void Giraffic_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtonData[e.Button][0, 0] = true;
            mouseButtonData[e.Button][2, 0] = true;
        }

        private void Giraffic_MouseUp(object sender, MouseEventArgs e)
        {
            mouseButtonData[e.Button][1, 0] = true;
            mouseButtonData[e.Button][2, 0] = false;
        }

        private void Giraffic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mouseDoubleClickData[e.Button][0] = true;
        }

        private void Giraffic_MouseWheel(object sender, MouseEventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.MouseWheel][0] = true;
            scrollDelta += e.Delta;
        }

        private void Giraffic_MouseMove(object sender, MouseEventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.MouseMove][0] = true;
        }

        private void Giraffic_MouseEnter(object sender, EventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.MouseEnter][0] = true;
        }

        private void Giraffic_MouseLeave(object sender, EventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.MouseLeave][0] = true;
        }

        private void Giraffic_WindowFocused(object sender, EventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.WindowFocused][0] = true;
        }

        private void Giraffic_WindowUnfocused(object sender, EventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.WindowUnfocused][0] = true;
        }

        private void Giraffic_WindowResized(object sender, ResizeArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.WindowResized][0] = true;
        }

        private void Giraffic_WindowMoved(object sender, MoveArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.WindowMoved][0] = true;
        }

        private void Giraffic_WindowClosed(object sender, FormClosedEventArgs e)
        {
            simpleInstantEventData[SimpleInstantEvent.WindowClosed][0] = true;
        }
        #endregion
    }
}
