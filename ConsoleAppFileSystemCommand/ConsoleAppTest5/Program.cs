namespace FileSystemCommand;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write(">> ");
            var input = Console.ReadLine().Trim();
            var whiteSpaceIndex = input.IndexOf(' ');
            String? command = default;
            String? path = default;
            if (whiteSpaceIndex == -1 )
            {
                command = input; 
            }
            else
            {
                command = input.Substring(0, whiteSpaceIndex).Trim();
                path = input.Substring(whiteSpaceIndex + 1).Trim();
            }

            if (command == "list")
            {
                if (Directory.Exists(path))
                {
                    foreach (var entry in Directory.GetDirectories(path))
                        Console.WriteLine($"\t[Dir] {entry}");
                    foreach (var entry in Directory.GetFiles(path))
                        Console.WriteLine($"\t[file] {entry}");
                }
                else
                    Console.WriteLine("Cannot find the path");
            }
            else if (command == "info")
            {
                if (Directory.Exists(path))
                {
                    var dirInfo = new DirectoryInfo(path);
                    Console.WriteLine("Type: Directory");
                    Console.WriteLine($"Created At: {dirInfo.CreationTime}");
                    Console.WriteLine($"Last Modified At: {dirInfo.LastWriteTime}");
                }
                else if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    Console.WriteLine("Type: File");
                    Console.WriteLine($"Created At: {fileInfo.CreationTime}");
                    Console.WriteLine($"Last Modified At: {fileInfo.LastWriteTime}");
                    Console.WriteLine($"Size: {fileInfo.Length / (1024 * 1024)}MB");
                }
                else
                    Console.WriteLine("Cannot find the path");
            }

            else if (command == "mkdir")
            {
                if (path != null)
                    Directory.CreateDirectory(path);
                else
                    Console.WriteLine("Cannot find the path");
            }
            else if (command == "read")
            {
                if (File.Exists(path))
                {
                    string content = File.ReadAllText(path);
                }
                else
                {
                    Console.WriteLine("File is not exist");
                }
            }
            else if (command == "write")
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Enter your string below: ");
                    string str = Console.ReadLine() ?? "";
                    File.WriteAllText(path, str);
                }
                else
                    Console.WriteLine("File is not exist");
            }
            else if (command == "append")
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Enter your string below: ");
                    string str = Console.ReadLine() ?? "";
                    File.AppendAllText(path, str);
                }
                else
                    Console.WriteLine("File is not exist");
            }
            else if (command == "remove")
            {
                if (Directory.Exists(path))
                    Directory.Delete(path);

                else if (File.Exists(path))
                    File.Delete(path);
                else
                    Console.WriteLine("File is not exist");
            }
            else if (command == "help")
            {
                Console.WriteLine("\n" +
                                    "\tlist   => Show all files in directory\n" +
                                    "\tinfo   => Display information about the file or folder\n" +
                                    "\tmkdir  => Create a new dirctory\n" +
                                    "\tread   => Read the content form file\n" +
                                    "\twrite  => Write string in file\n" +
                                    "\tappend => Append string to file\n" +
                                    "\tremove => Remove file or directory\n" +
                                    "\tclear  => Clearing CMD\n" +
                                    "\texit   => Close the app\n");

            }
            else if (command == "clear")
                Console.Clear();
            else if (command.ToLower() == "exit")
                break;
            else
                Console.WriteLine("Not available command");

        }
    }
}

