using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWords
{
    static class Program
    {
        public static Dictionary<string, string> Words = new Dictionary<string, string>();

        static void Help()
        {
            Console.WriteLine("Usage: ReplaceWords <text file> [words file]");
            Console.WriteLine("Words file should contain from,to pairs");
            Console.WriteLine("Comma separated, one pair per line");
            Console.WriteLine("Default name is words.csv");
        }

        static void Version()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine("{0} {1}", assemblyName.Name, assemblyName.Version.ToString(2));
        }

        static void Main(string[] args)
        {
            try
            {
                // Command line
                var filenames = new List<string>();
                for (int i = 0; i < args.Length; i++)
                {
                    var s = args[i];

                    // On Windows, options can start with /
                    if (Path.DirectorySeparatorChar == '\\' && s.StartsWith("/"))
                        s = "-" + s.Substring(1);

                    // Not an option
                    if (!s.StartsWith("-"))
                    {
                        filenames.Add(s);
                        continue;
                    }

                    // Option
                    s = s.TrimStart('-');
                    switch (s)
                    {
                        case "?":
                        case "h":
                        case "help":
                            Help();
                            return;
                        case "V":
                        case "v":
                        case "version":
                            Version();
                            return;
                        default:
                            throw new IOException(args[i] + ": Unknown option");
                    }
                }
                if (!(1 <= filenames.Count && filenames.Count <= 2))
                {
                    Help();
                    return;
                }

                // Filenames
                var textfile = filenames[0];
                var wordsfile = "words.csv";
                if (filenames.Count == 2)
                    wordsfile = filenames[1];

                // Read text
                List<Token> tokens;
                using (var reader = new StreamReader(textfile))
                    tokens = Token.Read(reader);

                // Read words
                int j = 0;
                foreach (var line in File.ReadAllLines(wordsfile))
                {
                    j++;
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    var line1 = line.Split(',');
                    if (line1.Length != 2)
                    {
                        throw new IOException(string.Format("{0}:{1}: Expected pair", wordsfile, j));
                    }
                    Words.Add(line1[0], line1[1]);
                }

                // Replace
                foreach (var token in tokens)
                    token.Replace();

                // Write text
                try
                {
                    File.Delete(textfile + ".bak");
                    File.Move(textfile, textfile + ".bak");
                }
                catch (IOException e) { }
                using (var writer = new StreamWriter(textfile))
                    foreach (var token in tokens)
                        writer.Write(token.Value);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            if (Debugger.IsAttached)
                Console.ReadKey();
        }
    }
}
