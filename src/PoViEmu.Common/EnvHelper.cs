using System;

namespace PoViEmu.Common
{
    public static class EnvHelper
    {
        public static string? GetHostName() => GetEnvVar("HOSTNAME");

        public static string? GetLocale() => GetEnvVar("LANGUAGE");

        public static string? GetUserName() => GetEnvVar("USER");

        public static string? GetHomeDir() => GetEnvVar("HOME");

        private static string? GetEnvVar(string name)
        {
            var txt = Environment.GetEnvironmentVariable(name);
            return txt?.TrimNull();
        }
    }
}