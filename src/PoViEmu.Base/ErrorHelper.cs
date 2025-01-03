using System;

namespace PoViEmu.Base
{
    public static class ErrorHelper
    {
        public static TOutput? Try<TOutput>(Func<TOutput> func, out Exception? error)
        {
            try
            {
                error = null;
                return func();
            }
            catch (Exception ex)
            {
                error = ex;
                return default;
            }
        }
    }
}