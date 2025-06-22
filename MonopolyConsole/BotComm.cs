using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;

namespace MonopolyConsole
{
    public class BotManager
    {
        // Bot level 1
        //- bad absolute path
        private string JavaBotJarPath = @"C:\Users\HP\source\repos\MonopolyConsole\MonopolyConsole\jbot.jar";
        private List<PlayerBot> Bots;

        public BotManager()
        {
            Bots = new List<PlayerBot>();  

            if (!File.Exists(JavaBotJarPath))
            {
                Console.WriteLine("ERROR: jbot.jar not found at " + JavaBotJarPath);
                return;
            }
        }

        public int Spawn(int startingBalance)
        {
            Process proc = new Process();
            StreamWriter inputWriter;
            StreamReader outputReader;
            StreamReader errorReader;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\Program Files\Java\jdk-24\bin\java.exe",
                Arguments = $"-jar {JavaBotJarPath}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            proc.StartInfo = startInfo;

            try
            {
                proc.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not start Java bot.");
                Console.WriteLine(ex.Message);
                return -1;
                //- ??
            }

            //- Need checks, probably just communicate directly
            // For now using int and string to communicate
            string initInfo = $"{{\"startingBalance\": {startingBalance}}}";
            PlayerBot bot = new PlayerBot(proc, initInfo);
            Bots.Add(bot);
            return Bots.Count - 1;
        }

        public string SendMessage(int bot, string message)
        {
            return Bots[bot].SendMessage(message);
        }

        public void KillAll()
        {
            foreach (PlayerBot bot in Bots)
                bot.Dispose();
        }
    }

    //- might merge some into bot manager
    internal class PlayerBot : Participant
    {
        private Process Process;
        private StreamWriter inputWriter;
        private StreamReader outputReader;
        private StreamReader errorReader;

        //- Just in case so I can remove them all later
        private static PlayerBot[] instances;

        public PlayerBot(Process proc, string initInfo)
        {
            Process = proc;
            inputWriter = proc.StandardInput;
            outputReader = proc.StandardOutput;
            errorReader = proc.StandardError;

            SendMessage(initInfo);
        }

        public string SendMessage(string command)
        {
            inputWriter.WriteLine(command);
            inputWriter.Flush();

            //- probably need cancelation token so it doesnt go on forever, given bot is quicker, maybe 3 seconds or so
            // or async reader with timeout.
            string line;
            string total = "";
            while ((line = outputReader.ReadLine()) != null)
            {
                Console.WriteLine("Bot says: " + line);
                total += line;
                if (line.Contains(":stop")) break; 
            }

            //- revisit
            //string stderr = errorReader.ReadToEnd();
            //if (!string.IsNullOrWhiteSpace(stderr))
            //{
            //    Console.WriteLine("Java bot stderr:");
            //    Console.WriteLine(stderr);
            //}
            return total; 
        }


        public override void Play()
        {

        }
        public override void Move(int steps)
        {

        }
        public override void MoveTo(int pos)
        {

        }

        public void Dispose()
        {
            try
            {
                if (!Process.HasExited)
                {
                    Process.Kill();
                }
                Process.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR disposing bot process: " + ex.Message);
            }

        }

    }

    //- Probably general bot but decoupled from processbot, would decouple and put in player class
    //- not might have to move all seperate
    //internal class EasyPlayerBot : PlayerBot
    //{

    //}
}



