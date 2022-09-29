using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GameByURLLauncher
{
    internal class Program
    {
        static XInputWatcher watcher = new XInputWatcher();
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"Log/{DateTime.Now.ToString("dd.MM.yyyy")}.txt")
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
              .CreateLogger();
            if (args.Length != 1)
            {
                Log.Error("ERROR: Pas d'Id de jeux renseigner");
                //System.Console.WriteLine("ERROR: Needs launch URL and EXE Name");
                return;
            }
            RunAsync(args[0]).GetAwaiter().GetResult();
        }
        static async Task IsEscapeCombinationSend(Process process)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    watcher.Update();
                    if (
                        watcher.gamepad.Buttons == (SharpDX.XInput.GamepadButtonFlags.LeftShoulder | SharpDX.XInput.GamepadButtonFlags.RightShoulder| SharpDX.XInput.GamepadButtonFlags.Start | SharpDX.XInput.GamepadButtonFlags.Back)
                        //&&
                        //watcher.gamepad.Buttons == SharpDX.XInput.GamepadButtonFlags.RightShoulder
                        //&&
                        //watcher.gamepad.Buttons == SharpDX.XInput.GamepadButtonFlags.Start
                        //&&
                        //watcher.gamepad.Buttons == SharpDX.XInput.GamepadButtonFlags.Back

                      )
                    {
                        process.Kill(true);
                        return;
                    }
                }
            });

        }
        static async Task RunAsync(string args)
        {

            try
            {
                var GameID = args;
                var game = await APIService.GetGameRomByID(GameID);
                if (game == null)
                {
                    Log.Error("ERROR: Impossible de trouver un jeux avec cette ID"); return;
                }
                var emu = await APIService.GetEmulatorByID(game.EmulatorID.ToString());
                if(emu == null)
                {
                    Log.Error($"ERROR: Impossible de trouver l'émulateur pour le jeu {game.Name}"); return;
                }
                var sys = await APIService.GetSystemeByID(emu.SystemeID.ToString());
                if(sys == null)
                {
                    Log.Error($"ERROR: Impossible de trouver le systeme pour l'émulateur {emu.Name}"); return;
                }
                string appexe = string.Empty;
                string argsexe = string.Empty;
                string gamepath = string.Empty;
                string gameexe = string.Empty ;
                
                if(sys.Type == Models.SysType.GameStore)
                {
                    var gameargs = game.Path.Split(" ").ToArray();
                    appexe = gameargs[0];
                    gameexe = gameargs[1];
                }
                else if(sys.Type == Models.SysType.Standalone)
                {
                    appexe = game.Path;
                    gameexe = Path.GetFileNameWithoutExtension(appexe);
                }
                else
                {
                    appexe = emu.Chemin;
                    argsexe = emu.Command.Replace("{ImagePath}",$"\"{game.Path}\"");
                    gameexe = Path.GetFileNameWithoutExtension(appexe);
                }

                var ps = new ProcessStartInfo(appexe)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                if(!string.IsNullOrEmpty(argsexe))
                {
                    ps.Arguments = argsexe;
                }
                Log.Information($"Starting : {appexe} with args {argsexe}");
                Process.Start(ps);

                Thread.Sleep(5000);

                var gameProcesses = Process.GetProcessesByName(gameexe);
                Process gameprocess;
                if (gameProcesses.Length != 1)
                {
                    Log.Error($"Could not find a single process with name: {gameexe}");
                    Thread.Sleep(10000);
                    gameProcesses = Process.GetProcessesByName(gameexe);
                    if (gameProcesses.Length != 1)
                    {
                        Log.Error($"Could not find a single process with name: {gameexe} In second try");
                        return;
                    }
                    
                }
                gameprocess = gameProcesses[0];
                Log.Information($"Game started.");
                await IsEscapeCombinationSend(gameprocess);

                gameprocess.WaitForExit();
                Log.Information($"Game Closed.");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

        }

    }
}
