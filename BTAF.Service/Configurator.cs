using System;
using System.Collections.Generic;
using System.Linq;
using BTAF.Lib;

namespace BTAF.Service
{
    internal static class Configurator
    {
        private class MenuOption
        {

            public string Key { get; }
            public string Label { get; }

            public MenuOption(string key, string label)
            {
                Key = key;
                Label = label;
            }

            public MenuOption(string label) : this(label.Split('=').First(), label.Split("=".ToCharArray(), 2).Last())
            {
                //NOOP
            }

            public static IEnumerable<MenuOption> Numbered(IEnumerable<string> items)
            {
                int i = 0;
                foreach (var item in items)
                {
                    yield return new MenuOption($"{++i}", item);
                }
            }

            public static IEnumerable<MenuOption> Numbered(params string[] items)
            {
                return Numbered(items.AsEnumerable());
            }
        }

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (Menu("Main Menu", "C=Configure audio device", "M=Manage BTAF service", "R=Reset audio gateway service", "Q=Quit").Key)
                {
                    case "C":
                        AudioConfig();
                        break;
                    case "M":
                        ManageBTAF();
                        break;
                    case "R":
                        Reset();
                        break;
                    default:
                        return;
                }
            }
        }

        private static void AudioConfig()
        {
            var defaultItems = new MenuOption[]
            {
                    new MenuOption("R=Refresh list"),
                    new MenuOption("C=Cancel")
            };
            while (true)
            {
                Console.Clear();
                var devices = AudioDeviceEnumerator.EnumerateDevices(true).ToArray();
                if (devices.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No audio device present and ready. Make sure your bluetooth device is functioning");
                    Console.ResetColor();
                    WaitForKey();
                    return;
                }
                var devId = ConfigFile.Load().AudioDeviceId;
                if (!string.IsNullOrEmpty(devId))
                {
                    var dev = devices.FirstOrDefault(m => m.Id == devId);
                    if (dev != null)
                    {
                        dev.Name += " (Current selection)";
                    }
                }
                var item = Menu("Select audio device to monitor", devices.Select(m => new MenuOption(m.Id, m.Name)).Concat(defaultItems)).Key;
                var audioDevice = devices.FirstOrDefault(m => m.Id == item);
                if (audioDevice != null)
                {
                    WriteConfig(audioDevice);
                    Console.WriteLine("Settings saved. Restart the BTAF service from the main menu for the changes to take effect");
                    WaitForKey();
                    return;
                }
                else if (item == "C") //Cancel
                {
                    return;
                }
            }
        }

        private static void ManageBTAF()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Service installed: {0}", ServiceInstallHelper.IsInstalled ? "Yes" : "No");
                Console.WriteLine("Service enabled:   {0}", ServiceInstallHelper.IsEnabled ? "Yes" : "No");
                Console.WriteLine("Service running:   {0}", ServiceInstallHelper.IsRunning ? "Yes" : "No");
                Console.WriteLine();
                var items = new List<MenuOption>();
                if (!ServiceInstallHelper.IsInstalled)
                {
                    items.Add(new MenuOption("I=Install"));
                }
                else
                {
                    items.Add(new MenuOption("U=Uninstall"));
                    if (!ServiceInstallHelper.IsEnabled)
                    {
                        items.Add(new MenuOption("E=Enable service (also enables automatic start on next boot)"));
                    }
                    else
                    {
                        items.Add(new MenuOption("D=Disable service (also disables automatic start on next boot)"));
                        if (!ServiceInstallHelper.IsRunning)
                        {
                            items.Add(new MenuOption("R=Run service"));
                        }
                        else
                        {
                            items.Add(new MenuOption("S=Stop service"));
                        }
                    }
                }
                items.Add(new MenuOption("M=Main Menu"));
                var key = Menu("Select service option", items).Key;
                switch (key)
                {
                    case "I":
                        if (!ServiceInstallHelper.IsInstalled)
                        {
                            Console.WriteLine("Installing...");
                            ServiceInstallHelper.Install();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Service installed");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Service already installed");
                        }
                        break;
                    case "U":
                        if (ServiceInstallHelper.IsInstalled)
                        {
                            ServiceInstallHelper.Uninstall();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Service uninstalled");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Service is not installed");
                        }
                        break;
                    case "E":
                        if (ServiceInstallHelper.IsInstalled)
                        {
                            if (!ServiceInstallHelper.IsEnabled)
                            {
                                ServiceInstallHelper.Enable();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Service is aleady enabled");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Service is not installed");
                        }
                        break;
                    case "D":
                        if (ServiceInstallHelper.IsInstalled)
                        {
                            if (ServiceInstallHelper.IsEnabled)
                            {
                                ServiceInstallHelper.Disable();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Service is aleady disabled");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Service is not installed");
                        }
                        break;
                    case "M":
                        return;
                }
                Console.ResetColor();
                WaitForKey();
            }
        }

        private static void WriteConfig(AudioDevice audioDevice)
        {
            var c = ConfigFile.Load();
            c.AudioDeviceId = audioDevice.Id;
            c.Save();
        }

        private static void Reset()
        {
            if (ServiceControl.IsRunning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Service already enabled and running");
                Console.ResetColor();
                WaitForKey();
                return;
            }
            if (ServiceInstallHelper.IsRunning)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The BTAF service is currently running. Stop it first using /STOP argument");
                Console.ResetColor();
                WaitForKey();
                return;
            }
            else if (ServiceInstallHelper.IsEnabled)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The BTAF service is installed but not running.");
                Console.WriteLine("Resetting the bluetooth audio gateway service will only persist until the BTAF service runs again");
                Console.ResetColor();
            }
            if (Menu("Reset Bluetooth Audio Gateway Service", new MenuOption("Y=Yes"), new MenuOption("N=No")).Key == "Y")
            {
                if (!ServiceControl.IsEnabled)
                {
                    Console.WriteLine("Enabling service...");
                    ServiceControl.Enable();
                }
                if (!ServiceControl.IsRunning)
                {
                    Console.WriteLine("Starting service...");
                    ServiceControl.Start();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Operation complete");
                Console.ResetColor();
                WaitForKey();
            }
        }

        private static MenuOption Menu(string title, IEnumerable<MenuOption> options)
        {
            var opt = 0;
            var items = options?.ToArray();
            if (items == null || items.Length == 0)
            {
                return null;
            }
            var longest = items.Max(m => m.Label.Length);
            var pos = new { X = Console.CursorLeft, Y = Console.CursorTop };
            while (true)
            {
                Console.SetCursorPosition(pos.X, pos.Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" {0}", title);
                Console.WriteLine();
                Console.ResetColor();
                for (var i = 0; i < items.Length; i++)
                {
                    if (i == opt)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    Console.Write(" {0}", items[i].Label.PadRight(longest + 1));
                    if (i == opt)
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine(string.Empty.PadRight(Console.BufferWidth - 2 - longest));
                }
                Console.WriteLine();
                Console.WriteLine("[ARROW] Select | [ENTER] Accept");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        return items[opt];
                    case ConsoleKey.UpArrow:
                        opt = Math.Max(0, opt - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        opt = Math.Min(items.Length - 1, opt + 1);
                        break;
                }
            }
        }

        private static MenuOption Menu(string title, params MenuOption[] items)
        {
            return Menu(title, items.AsEnumerable());
        }

        private static MenuOption Menu(string title, IEnumerable<string> items)
        {
            return Menu(title, items.Select(m => new MenuOption(m)));
        }

        private static MenuOption Menu(string title, params string[] items)
        {
            return Menu(title, items.AsEnumerable());
        }

        private static void WaitForKey()
        {
            const string msg = "Press any key to continue";
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            Console.Write(msg.PadRight(msg.Length * 2, '\b'));
            Console.ReadKey(true);
            Console.Write(string.Empty.PadRight(msg.Length).PadRight(msg.Length * 2, '\b'));
        }
    }
}
