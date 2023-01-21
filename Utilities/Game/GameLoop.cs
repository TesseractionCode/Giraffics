using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giraffics.Utilities.Game
{
    public delegate void UpdateCall(EventListener events, float dT, float elapsedTime);
    public delegate void RenderCall(Giraffic giraffic);

    /// <summary>
    /// A simple gameloop that updates and renders on the same thread. Automatically updates the 
    /// event listener given upon instantiation. Must use the Start function of the instance to start.
    /// </summary>
    public class GameLoop
    {
        private Giraffic giraffic;
        private EventListener events;
        private UpdateCall[] updateCalls;
        private RenderCall[] renderCalls;
        private DateTime startTime;

        public bool isRunning { get; private set; }

        public GameLoop(Giraffic giraffic, EventListener events, UpdateCall[] updateCalls, RenderCall[] renderCalls)
        {
            this.giraffic = giraffic;
            this.events = events;
            this.updateCalls = updateCalls;
            this.renderCalls = renderCalls;

            isRunning = false;
        }

        public GameLoop(Giraffic giraffic, EventListener events, UpdateCall updateCall, RenderCall renderCall)
        {
            this.giraffic = giraffic;
            this.events = events;
            updateCalls = new UpdateCall[] { updateCall };
            renderCalls = new RenderCall[] { renderCall };

            isRunning = false;
        }

        /// <summary>Starts the gameloop.</summary>
        public void Start()
        {
            float dT = 0;
            DateTime frameStart;
            isRunning = true;
            startTime = DateTime.Now;
            while (giraffic.IsRunning)
            {
                frameStart = DateTime.Now;

                events.Update();
                Update(dT);
                Render();
                giraffic.Refresh();

                dT = (float)(DateTime.Now - frameStart).TotalSeconds;
            }
        }

        private void Update(float dT)
        {
            foreach (UpdateCall updateCall in updateCalls)
                updateCall(events, dT, (float)(DateTime.Now - startTime).TotalSeconds);
        }

        private void Render()
        {
            foreach (RenderCall renderCall in renderCalls)
                renderCall(giraffic);
        }
    }
}
