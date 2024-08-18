using System;

namespace PoViEmu.Core.Meta
{
    public sealed class Metronome
    {
        /*
         * TODO
         *
         * "CLOCK": "25"
         * "LCDC_FREQ": "70"
         */

        public void Step()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            // TODO Reset ?!
            
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }

    public static class X
    {
        public static void Do()
        {
            Metronome m = new Metronome();
            m.Start();
            m.Step();
            m.Pause();
            m.Stop();
            
            // Open / Close log file
        }
    }
}