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

        public int Spawn()
        {
            //- Need checks, probably just communicate directly
            // For now using int and string to communicate
            PlayerBot bot = new EasyPlayerBot(JavaBotJarPath);
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

    internal abstract class PlayerBot : Participant
    {
        private Process Process;
        private StreamWriter inputWriter;
        private StreamReader outputReader;
        private StreamReader errorReader;
        //- Just in case so I can remove them all later
        private static PlayerBot[] instances;

        public PlayerBot(string botPath)
        {
            Process = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\Program Files\Java\jdk-24\bin\java.exe",
                Arguments = $"-jar {botPath}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true

            };

            Process.StartInfo = startInfo;

            try
            {
                Process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Could not start Java bot.");
                Console.WriteLine(ex.Message);
                return;
            }
            inputWriter = Process.StandardInput;
            outputReader = Process.StandardOutput;
            errorReader = Process.StandardError;
        }

        public string SendMessage(string command)
        {
            inputWriter.WriteLine(command);
            inputWriter.Flush();

            //- probably need cancelation token so it doesnt go on forever, given bot is quicker, maybe 3 seconds or so.
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

        public void Dispose()
        {
            Process.Kill();
            Process.Dispose();
        }

        public override void Play()
        {

        }
    }

    internal class EasyPlayerBot : PlayerBot
    {
        public EasyPlayerBot(string source) : base(source)
        {

        }
    }
}



