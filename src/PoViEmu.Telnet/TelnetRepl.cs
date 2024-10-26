using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using ConsoleServer;
using Serilog;
using Serilog.Events;
using Spectre.Console;

namespace PoViEmu.Telnet
{
    public sealed class TelnetRepl<T> where T : struct, Enum
    {
        private readonly IDictionary<string, ManualResetEvent> _wait;
        public readonly IDictionary<T, CommandDlgt> Handlers;

        public TelnetRepl()
        {
            _wait = new ConcurrentDictionary<string, ManualResetEvent>();
            Enum.TryParse<T>("quit", ignoreCase: true, out var quit);
            Handlers = new ConcurrentDictionary<T, CommandDlgt>
            {
                [quit] = QuitSession
            };
        }

        private TelnetServer? _server;

        public void Start()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.ConsoleTelnetServer()
                .CreateLogger();

            _server = new TelnetServer(3333);
            _server.StartListen(OnNewSession);
        }

        public IPEndPoint? EndPoint => _server?.LocalEndPoint;

        public void Stop()
        {
            _server?.Dispose();
            Handlers.Clear();
            _wait.Clear();
        }

        private bool QuitSession(TelnetSession session, string? _ = null, string? __ = null)
        {
            var key = session.GetSessionKey();
            _wait[key].Set();
            session.WriteOneLine("Good bye!");
            return false;
        }

        public string Prompt { get; set; } = ">";

        public string? HelloMessage { get; set; }
            = "[underline blue]Welcome to the server![/]";

        private void OnNewSession(TelnetSession session)
        {
            var key = session.GetSessionKey();
            _wait[key] = new ManualResetEvent(false);

            session.LogLevel = LogEventLevel.Debug;
            if (!string.IsNullOrWhiteSpace(HelloMessage))
                session.WriteOneLine(HelloMessage);

            var bld = new StringBuilder();
            session.WritePrompt(Prompt);

            session.AnsiConsole.Input.KeyPressed +=
                (_, e) => OnKeyPressed(session, e, bld);

            _wait[key].WaitOne();
        }

        private void OnKeyPressed(TelnetSession session, ConsoleKeyInfoExt e, StringBuilder bld)
        {
            if (e is { Control: true, KeyChar: 'c' or 'C' })
            {
                QuitSession(session);
                return;
            }
            var input = $"{e.KeyChar}";
            if (e.Key == ConsoleKey.Enter)
            {
                session.AnsiConsole.WriteLine();
                var line = bld.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(line))
                    try
                    {
                        if (!ExecuteCommand(session, line))
                            return;
                    }
                    catch (Exception ex)
                    {
                        const ExceptionFormats fmt = ExceptionFormats.NoStackTrace;
                        session.AnsiConsole.WriteException(ex, fmt);
                    }
                bld.Clear();
                session.WritePrompt(Prompt);
                return;
            }
            session.AnsiConsole.Write(input);
            bld.Append(input);
        }

        private bool ExecuteCommand(TelnetSession session, string line)
        {
            var parts = line.Trim().Split(' ', 2);
            var cmd = parts[0].Trim();

            if (cmd == "?")
                cmd = "help";

            if (!Enum.TryParse<T>(cmd, ignoreCase: true, out var kind))
                throw new InvalidOperationException($"Unknown command '{cmd}'!");

            if (!Handlers.TryGetValue(kind, out var dlgt))
                throw new NotImplementedException($"Missing delegate '{cmd}'!");

            var arg = parts.Length == 2 ? parts[1].Trim() : null;
            var @continue = dlgt(session, cmd, arg);
            return @continue;
        }
    }
}