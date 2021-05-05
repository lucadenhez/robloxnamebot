using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace robloxnamebot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ROBLOX RARE NAME BOT (By Luca Denhez)";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string title = @"
                                     ██████╗  ██████╗ ██████╗ ██╗      ██████╗ ██╗  ██╗                         
                                     ██╔══██╗██╔═══██╗██╔══██╗██║     ██╔═══██╗╚██╗██╔╝                         
                                     ██████╔╝██║   ██║██████╔╝██║     ██║   ██║ ╚███╔╝                          
                                     ██╔══██╗██║   ██║██╔══██╗██║     ██║   ██║ ██╔██╗                          
                                     ██║  ██║╚██████╔╝██████╔╝███████╗╚██████╔╝██╔╝ ██╗                         
                By: Luca Denhez      ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═╝                         
                                                                                                       
         ██████╗  █████╗ ██████╗ ███████╗    ███╗   ██╗ █████╗ ███╗   ███╗███████╗    ██████╗  ██████╗ ████████╗
         ██╔══██╗██╔══██╗██╔══██╗██╔════╝    ████╗  ██║██╔══██╗████╗ ████║██╔════╝    ██╔══██╗██╔═══██╗╚══██╔══╝
         ██████╔╝███████║██████╔╝█████╗      ██╔██╗ ██║███████║██╔████╔██║█████╗      ██████╔╝██║   ██║   ██║   
         ██╔══██╗██╔══██║██╔══██╗██╔══╝      ██║╚██╗██║██╔══██║██║╚██╔╝██║██╔══╝      ██╔══██╗██║   ██║   ██║   
         ██║  ██║██║  ██║██║  ██║███████╗    ██║ ╚████║██║  ██║██║ ╚═╝ ██║███████╗    ██████╔╝╚██████╔╝   ██║   
         ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝    ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚═════╝  ╚═════╝    ╚═╝
            ";

            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" How many web requests do you want to send? (Max 1000) ");

            int tries;

            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                tries = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Sorry, exception occured:\n");
                Console.WriteLine(ex);
                tries = 1001;
            }
            while (tries > 1000)
            {
                Console.WriteLine("\n Sorry, cannot send more then 1,000 webrequests to prevent bandwith use. ");
                Console.WriteLine("");
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    tries = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n Sorry, exception occured:\n");
                    Console.WriteLine(ex);
                }
            }

            Console.Write("\n How many digits should the name be? ");
            int length;

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                length = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                if (length < 5)
                {
                    Console.WriteLine("\n Sorry, minimum username length is 5. (All 4 and under are taken) ");
                }
                else if (length > 20)
                {
                    Console.WriteLine("\n Sorry, maximum username length is 20. ");
                }
                else { }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nSorry, exception occured:\n");
                Console.WriteLine(ex);
                length = 4;
            }
            while (length < 5 || length > 20)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    length = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("\nSorry, exception occured:\n");
                    Console.WriteLine(ex);
                    length = 4;
                }
                if (length < 5)
                {
                    Console.WriteLine("\n Sorry, minimum username length is 5. (All 4 and under are taken) ");
                }
                else if (length > 20)
                {
                    Console.WriteLine("\n Sorry, maximum username length is 20. ");
                }
                else { }
            }

            // This section was giving me issues
            char mode;
            Console.WriteLine("\n Would you like to continue or stop when you find an available name? (c/s) \n");

            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                mode = Convert.ToChar(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nSorry, exception occured:\n");
                Console.WriteLine(ex);
                mode = 'z';
            }
            
            if (mode == 'c')
            {
                Console.WriteLine(@"\n Continue mode selected. Each username found will be saved to a .txt file in the 'C:\roblox' directory... ");
                
                if (Directory.Exists(@"C:\roblox")) { }
                else
                {
                    Directory.CreateDirectory(@"C:\roblox");
                }
                if (File.Exists(@"C:\roblox\usernames.txt"))
                {
                    File.Delete(@"C:\roblox\usernames.txt");
                }
                else
                {
                    File.CreateText(@"C:\roblox\usernames.txt");
                }

                StreamWriter file = new StreamWriter(@"C:\roblox\usernames.txt");
                file.WriteLine("Valid usernames: \n");
                Console.Clear();

                for (int i = 0; i < tries; i++)
                {
                    string name = generateName(length);
                    int code = makeWebRequest(name);

                    if (code == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" Valid!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" | Username: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(name);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n");
                        file.WriteLine(name);
                    }
                    else if (code == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Taken.. ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (code == 2 || code == 4 || code == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(" Regenerating.. ");
                        Console.ForegroundColor = ConsoleColor.White;
                        while (name.IndexOf('_') == 0 && name.IndexOf('_') == 4)
                        {
                            name = generateName(length);
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" Valid!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" | Username: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(name);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n");
                        file.WriteLine(name);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" Error, Retrying...\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n " + tries);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" name searches completed! All valid usernames can be found in 'C:\\roblox\\usernames.txt'\n");
                file.Close();
                Console.ReadLine();
            }
            else if (mode == 's')
            {
                Console.WriteLine("\n Sorry, stop mode has not been coded yet. Please select continue mode instead. ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nSorry, mode has to be either 'c' or 's'.");
            }
        }
        static string generateName(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static int makeWebRequest(string name)
        {
            HttpClient request = new HttpClient();
            HttpResponseMessage response = request.GetAsync("https://auth.roblox.com/v1/usernames/validate?request.username=" + name + "&request.birthday=10.20.1999&request.context=Unknown").Result;
            string responseString = response.Content.ReadAsStringAsync().Result;
            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            return Convert.ToInt32(responseJson.code);
        }
    }
}
                    