using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Quick_Note_Taker
{
    class NotizObjekt
    {
        public string DatumZeit { get; set; }
        public string Notiz { get; set; }
    }

    internal class Program
    {
        static List<string> notizenList = new List<string>();
        static string file = "Notizen.txt";

        static void Main(string[] args)
        {
            notizenList = File.ReadAllLines(file).ToList();

            while (true)
            {
                string notiz = Console.ReadLine();

                if (IsValid(notiz))
                {
                    var dateTime = DateTime.Now.ToString("dd-MM-yy HH:mm");

                    NotizObjekt obj = new NotizObjekt()
                    {
                        DatumZeit = dateTime,
                        Notiz = notiz
                    };

                    List<string> list = new List<string>();

                    string jsonString = JsonConvert.SerializeObject(obj);
                    list.Add(jsonString);
                    File.AppendAllLines(file, list);
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
                            {
                                File.Delete(file);
                                Console.WriteLine("Notizen wurden gelöscht" + Environment.NewLine);
                            }
                            else
                                Delete(cmd[1]);
                                break;

                        case "/read":
                            Read();
                            break;

                        case "/clear":
                            Console.Clear();
                            break;

                        case "/close":
                            Environment.Exit(0);
                            break;
                    }
                }
            }

        }

        static bool IsValid(string notiz)
        {
            return !notiz.StartsWith("/") && !notiz.Contains("|") && !string.IsNullOrWhiteSpace(notiz);
        }

        static void Read()
        {
            int anzahl = 0;

            try
            {
                string[] fileSplit = File.ReadAllLines(file);

                for (int i = 0; i < fileSplit.Length; i++)
                {
                    NotizObjekt notiz = JsonConvert.DeserializeObject<NotizObjekt>(fileSplit[i]);
                    Console.WriteLine(notiz.DatumZeit + ": " + notiz.Notiz);
                    anzahl++;
                }

                Console.WriteLine($"Sie haben {anzahl} Notizen" + Environment.NewLine);
            }
            catch
            {
                Console.WriteLine($"Kein Dokument unter {file} gefunden");
            }           
        }

        static void Delete(string StrID)
        {
            int id = Convert.ToInt32(StrID) - 1;

            string[] fileSplit = File.ReadAllLines(file);
            var list = fileSplit.ToList();
            list.Remove(fileSplit[id]);

            File.Delete(file);
            File.AppendAllLines(file, list);
            Console.WriteLine($"Notiz {id} wurde gelöscht");
        }
    }
}
