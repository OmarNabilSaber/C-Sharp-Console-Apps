using CsvHelper;
using System.Globalization;
using System.Text;

namespace PasswordManager
{
    internal class Program
    {
        /*  [password manager]
            1. List all passwords
            2. Add or change password
            3. Get pssword
            4. Delete password
        */
        public static Dictionary<string, string> passwordBook = new Dictionary<string, string>() ;
        public const string passwoedsFilePath = "Passwords.CSV";
        public const string AdminInfoFilePath = "AdminInfo.text";
        public static bool IsUpdated = false ;
        static int Main(string[] args)
        {
            PrintWelcome();
            if (!File.Exists(AdminInfoFilePath))
            {
                string userName, password;
                while (true)
                {
                    Console.WriteLine("\nFrist Time To visit! let's set your user name and password to secure your passwords.");
                    Console.Write("User name: ");
                    userName = Console.ReadLine();
                    if ( userName.Contains(','))
                    {
                        Console.WriteLine("User name cannot contain ',' ... ");
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        continue;
                    }
                    Console.Write("Password : ");
                    password = Console.ReadLine();
                    break;
                }
                var encreptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
                var AdminInfo = userName + "," + encreptedPassword;
                File.WriteAllText(AdminInfoFilePath, AdminInfo);
            }
            else
            {
                var counter = 1;
                while(true)
                {
                    var adminInfo = File.ReadAllText(AdminInfoFilePath);
                    var userName = adminInfo.Substring(0, adminInfo.IndexOf(','));
                    var EcreptedTruePassword = Convert.FromBase64String(adminInfo.Substring(userName.Length + 1));
                    var truePassword = Encoding.UTF8.GetString(EcreptedTruePassword);
                    Console.Write($"Welcome back {userName}, please enter your password:  ");
                    var password = Console.ReadLine();
                    if (!truePassword.Equals(password))
                    {
                        if (counter == 3)
                        {
                            Console.WriteLine("maximum number of attempts reached. try again later");
                            return 0;
                        }
                        Console.WriteLine("Incorredt password try again ...");
                        counter++;
                    }
                    else
                        break;
                }

            }

            ReadPasswords();
            while (true)
            {
                PrintWelcome();
                ListOptions();
                Console.Write("\nPlease, enter the number of your choice: ");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    ListAllPasswords();
                }
                else if (choice == "2")
                {
                    Console.Write("\nPlease, enter the name: ");
                    string name = Console.ReadLine();
                    if (name != string.Empty && name != null)
                    {
                        Console.Write("Pleae, enter the password: ");
                        string password = Console.ReadLine();
                        if (!passwordBook.ContainsKey(name))
                            AddPassword(name, password);
                        else
                            ChangePassword(name, password);
                    }
                    else
                    {
                        Console.WriteLine("\nName cannot be empty ");
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                    }

                }
                else if (choice == "3")
                {
                    Console.Write("Please, enter the name: ");
                    string name = Console.ReadLine();

                    Console.WriteLine($"Password is: {GetPassword(name) ?? "not available ..."}");
                    Console.Write("\nPress any key to return to menu ....");
                    Console.ReadKey();
                }
                else if (choice == "4")
                {
                    Console.Write("Please, enter the name: ");
                    string name = Console.ReadLine();
                    DeletePassword(name);
                }
                else if (choice == "5")
                {
                    if (IsUpdated)
                        WriteToFile();
                    Console.WriteLine("Saved ...");
                    Console.ReadKey();
                }
                else if (choice == "6")
                {
                    if (IsUpdated)
                        WriteToFile();
                    break;
                }
                else
                {
                    Console.WriteLine("Please choose number from the list ");
                    Console.Write("\nPress any key to return to menu ....");
                    Console.ReadKey();
                }

            }
            return 0;
        }

        public static void ReadPasswords()
        {
            if (File.Exists(passwoedsFilePath))
            {
                using (var reader = new StreamReader(passwoedsFilePath)) 
                using (var csv = new CsvReader(reader , CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var name = csv.GetField(0);
                        var encodedPassword = Convert.FromBase64String(csv.GetField(1));
                        string decodedPassword = Encoding.UTF8.GetString(encodedPassword);
                        passwordBook.Add(name ?? "NA", decodedPassword);
                    }
                }
            }
        }

        public static void WriteToFile()
        {
            var encrebtedPasswordBook = new Dictionary<string, string>();
            foreach (var item in passwordBook)
            {
                encrebtedPasswordBook[item.Key] = Convert.ToBase64String(Encoding.UTF8.GetBytes(item.Value));
            }
            
            using (var writer = new StreamWriter(passwoedsFilePath)) 
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords<KeyValuePair<string, string>>(encrebtedPasswordBook);
            }
        }

        public static void ListAllPasswords()
        {
            Console.WriteLine();
            foreach (var item in  passwordBook)
            {
                Console.WriteLine($"Name: {item.Key,-15} ::: Password: {item.Value}");
            }
            Console.Write("\nPress any key to return to menu ....");
            Console.ReadKey();
        }

        public static void AddPassword(string name , string password)
        {
            passwordBook.Add(name , password);
            IsUpdated = true;
            Console.WriteLine("Done ...");
            Console.Write("\nPress any key to return to menu ....");
            Console.ReadKey();
        }
        public static void ChangePassword(string name , string newPassword)
        {
            Console.WriteLine($"{name} is already exist ");
            Console.Write($"Are you sure you wnat to change it (Y for ok/ N or any for discard): ");
            string choice = Console.ReadLine();
            if (choice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                passwordBook[name] = newPassword;
                IsUpdated = true;
                Console.WriteLine("Done ...");
                Console.Write("\nPress any key to return to menu ....");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("discarded ....");
                Console.Write("\nPress any key to return to menu ....");
                Console.ReadKey();
            }
        }

        public static string GetPassword(string name)
        {
            passwordBook.TryGetValue(name, out var password);
            return password;
        }

        public static void DeletePassword(string name)
        {
            if ( passwordBook.TryGetValue(name, out var password))
            {
                passwordBook.Remove(name);
                IsUpdated = true;
                Console.WriteLine("Password has been Deleted");
            }
            else
                Console.WriteLine("Not availble name");

            Console.Write("\nPress any key to return to menu ....");
            Console.ReadKey();
        }

        public static void ListOptions()
        {
            Console.WriteLine("" +
                "\t1. List all passwords\n" +
                "\t2. Add or change password\n" +
                "\t3. Get pssword\n" +
                "\t4. Delete password\n"+
                "\t5. Save\n" +
                "\t6. Save and Exit");
        }
        public static void PrintWelcome()
        {
            Console.Clear();
            Console.WriteLine(  "\t\t* * * * * * * * * * * * * * * * * * * * * * *\n" +
                                "\t\t*                                           *\n" +
                                "\t\t*                                           *\n" +
                                "\t\t*      Welcome to our password manager      *\n" +
                                "\t\t*                                           *\n" +
                                "\t\t*                                           *\n" +
                                "\t\t* * * * * * * * * * * * * * * * * * * * * * *\n");

        }
    }
}
