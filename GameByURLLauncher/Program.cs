﻿using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
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
            Log.Information("----------------------------------------------------------------------------------");
            Log.Information("------------------------Lancement d'un jeu grâce au Launcher----------------------");
            Log.Information("----------------------------------------------------------------------------------");
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
                while (!process.HasExited)
                {
                    watcher.Update();
                    if (
                        watcher.gamepad.Buttons == (SharpDX.XInput.GamepadButtonFlags.LeftShoulder | SharpDX.XInput.GamepadButtonFlags.RightShoulder| SharpDX.XInput.GamepadButtonFlags.Start | SharpDX.XInput.GamepadButtonFlags.Back)
                      )
                    {
                        process.Kill(true);
                        Log.Information($"Game Closed by Gamepad.");
                        return;
                    }
                }
            });

        }
        static async Task RunAsync(string args)
        {

            try
            {
                var GameID = Path.GetFileNameWithoutExtension(args);
                var game = await APIService.GetGameRomByID(GameID);
                if (game == null)
                {
                    Log.Error($"ERROR: Impossible de trouver un jeux avec cette ID {GameID}"); return;
                }
                else
                {
                    Log.Information($"ID Game {GameID} pour {game.Name}");
                }
                var emu = await APIService.GetEmulatorByID(game.EmulatorID.ToString());
                if(emu == null)
                {
                    Log.Error($"ERROR: Impossible de trouver l'émulateur pour le jeu {game.Name}"); return;
                }
                else
                {
                    Log.Information($"ID Emulateur {emu.EmulatorID} pour {emu.Name}");
                }
                var sys = await APIService.GetSystemeByID(emu.SystemeID.ToString());
                if(sys == null)
                {
                    Log.Error($"ERROR: Impossible de trouver le systeme pour l'émulateur {sys.Name}"); return;
                }
                else
                {
                    Log.Information($"ID Systeme {sys.SystemeID} pour {sys.Name}");
                }
                string appexe = string.Empty;
                string argsexe = string.Empty;
                string gamepath = string.Empty;
                string gameexe = string.Empty ;
                
                if(sys.Type == Models.SysType.GameStore)
                {
                    var gameargs = game.Path.Split(" ").ToArray();
                    appexe = gameargs[0].Trim();
                    gameexe = gameargs[1].Trim();
                }
                else if(sys.Type == Models.SysType.Standalone)
                {
                    appexe = game.Path;
                    gameexe = Path.GetFileNameWithoutExtension(appexe);
                }
                else
                {
                    appexe = emu.Chemin;
                    argsexe = emu.Command.Replace("{ImagePath}",$"{game.Path}");
                    gameexe = Path.GetFileNameWithoutExtension(appexe);
                }

                var ps = new ProcessStartInfo(appexe)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                if (!string.IsNullOrEmpty(argsexe))
                {
                    var arglist = argsexe.Split('\"', StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries);

                    var extlist = emu.Extension.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                    var argslist = new List<string>();
                    foreach (var arg in arglist)
                    {
                        if (arg != " " && arg !="\"\"" && arg != "\" \"" && !string.IsNullOrEmpty(arg) && !string.IsNullOrWhiteSpace(arg))
                        {
                            if (argslist.Any() && argslist.LastOrDefault().EndsWith("="))
                            {
                                var argfull = argslist.Last() + arg.Trim();
                                argslist.Remove(ps.ArgumentList.Last());
                                argslist.Add(argfull);
                            }
                            else if (arg.Trim().Contains(" ") /*|| arg.Trim().Contains("cores")*/)
                            {
                                argslist.Add($" \"{arg.Trim()}\"");
                            }
                            else
                            {
                                argslist.Add(arg.Trim());
                            }
                        }
                    }
                    //ps.Arguments = argsexe;
                    ps.Arguments = String.Join(" ", argslist);
                }
                Log.Information($"appexe : {appexe}");
                Log.Information($"argsexe : {argsexe}");
                Log.Information($"gamepath : {gamepath}");
                Log.Information($"gameexe : {gameexe}");

                Log.Information($"Starting : {ps.FileName} with args {ps.Arguments}");
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
                        Thread.Sleep(10000);
                        gameProcesses = Process.GetProcessesByName(gameexe);
                        if (gameProcesses.Length != 1)
                        {
                            Log.Error($"Could not find a single process with name: {gameexe} In third try");
                            return;
                        }
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
