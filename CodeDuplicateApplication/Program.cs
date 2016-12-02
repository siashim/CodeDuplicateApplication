using System;
using System.Diagnostics;


namespace CodeDuplicateApplication
{
    class Program
    {
        // Our directory path which will be created when the function is called
        // this is used by the Simian
        private static string directoryPath = 
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            + @"\Code_Duplicate_Checker_Program_Github_Code";

        static void Main(string[] args)
        {
            string languageExtension = "";
            bool languageCheck = true;
            // Getting user input for two repositories
            Console.WriteLine
                ("Please type the address for a public GitHub repository.");
            string url1 = Console.ReadLine();
            Console.WriteLine("Please type the address for another"
                + " public GitHub repository.");
            string url2 = Console.ReadLine();
            // Asks the user for language of the repositories
            // This is used to give the right extension to Simian
            while (languageCheck)
            {
                Console.WriteLine("Please type the language of the"
                    + " repositories (Java, C#, C, C++, COBOL, Ruby," 
                    + " JSP, ASP, HTML, XML, Visual Basic, Groovy).");
                string language = Console.ReadLine();
                switch (language)
                {
                    case "Java":
                        languageExtension = ".java";
                        languageCheck = false;
                        break;
                    case "C#":
                        languageExtension = ".cs";
                        languageCheck = false;
                        break;
                    case "C":
                        languageExtension = ".c";
                        languageCheck = false;
                        break;
                    case "C++":
                        languageExtension = ".cpp";
                        languageCheck = false;
                        break;
                    case "COBOL":
                        languageExtension = ".cob";
                        languageCheck = false;
                        break;
                    case "Ruby":
                        languageExtension = ".rb";
                        languageCheck = false;
                        break;
                    case "JSP":
                        languageExtension = ".jsp";
                        languageCheck = false;
                        break;
                    case "ASP":
                        languageExtension = ".asp";
                        languageCheck = false;
                        break;
                    case "HTML":
                        languageExtension = ".htm";
                        languageCheck = false;
                        break;
                    case "XML":
                        languageExtension = ".xml";
                        languageCheck = false;
                        break;
                    case "Visual Basic":
                        languageExtension = ".vbp";
                        languageCheck = false;
                        break;
                    case "Groovy":
                        languageExtension = ".groovy";
                        languageCheck = false;
                        break;
                    default:
                        Console.WriteLine
                            ("Please input one of the given options.");
                        break;
                }
            }
            // Calling all the functions for creating the folder
            // and downloading and unpacking the GitHub repositories
            RepositoryDownloader repDownloader = new RepositoryDownloader();
            repDownloader.FolderCreator();
            repDownloader.EmptyFolder();
            repDownloader.ZipFileCreator();
            repDownloader.Downlaod(url1, url2);
            repDownloader.ZipFileExtractor();
            repDownloader.ZipFileDeleter();
            // Notifying user about the creation of new folder in My Documents
            // Which is used to contain the unpacked GitHub repositories
            Console.WriteLine("A new folder containing the GitHub"
                 + " repositories has been created in My Documents.");
            Console.WriteLine("Running Simian to check for duplicate code.");
            System.Threading.Thread.Sleep(2500);
    
            // Openning cmd to run Simian to check for duplication
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            // Initializing the string that is used to 
            // the directory that Simian is located
            string changeToDirectory = @"cd C:\simian-2.4.0\bin\";
            const string quote = "\"";
            // Initializing the string that is used to give
            // Simian the path to where the GitHub repositories are located
            string runApplicationCommand = "simian-2.4.0.exe " + quote +
                @"/" + directoryPath + @"**/*" + languageExtension + quote;

            // Giving the string that changes the directory to Simian to cmd
            cmd.StandardInput.WriteLine(changeToDirectory);
            // Giving the string that runs Simian to cmd
            cmd.StandardInput.WriteLine(runApplicationCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());

        }
    }
}

