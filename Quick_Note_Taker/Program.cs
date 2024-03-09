using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_Note_Taker
{
    internal class Program
    {
        static string fileName = "Notizen.txt"; 

        static void Main(string[] args)
        {
            while (true)
            {
                string notiz = Console.ReadLine();

                if (!notiz.Contains("/") && !notiz.Contains("|")  && !string.IsNullOrWhiteSpace(notiz))
                {
                    string dateTime = DateTime.Now.ToString("dd-MM-yy HH:mm");
                    string format = $"{dateTime} : {notiz} |";

                    File.AppendAllText(fileName, format);
                }
                else
                {
                    string[] cmd = notiz.Split(' ');

                    switch (cmd[0])
                    {
                        default:
                            Console.WriteLine("Sie haben nichts eingegeben");
                            break;

                        case string n when n.Contains("|"):
                            Console.WriteLine("Die Notiz enthält ein ungültiges Zeichen: '|'.");
                            break;

                        case "/delete":
                            if (cmd[1] == "all")
                                File.Delete(fileName);
                            else
                                Delete(cmd[1]);
                                break;

                        case "/read":
                            Read();
                            break;

                        case "/clear":
                            Console.Clear();
                            break;
                    }
                }
            }

        }

        static void Read()
        {
            string file = File.ReadAllText(fileName);

            string[] fileSplit = file.Split('|');

            foreach(string line in fileSplit)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine($"Sie haben {fileSplit.Length - 1} Notizen" + Environment.NewLine);
        }

        static void Delete(string StrID)
        {
            int id = Convert.ToInt32(StrID);

        }
    }
}
