using System;
using System.IO.Compression;
using System.IO;

namespace CodeDuplicateApplication
{
    class RepositoryDownloader
    {
        // Seetting the directory path to be in My Documents and naming the
        // new directory
        private static string directoryPath = 
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) 
            + @"\Code_Duplicate_Checker_Program_Github_Code";
        // Setting the paths for the zip files that is downloaded from 
        // GitHub. The paths are based on the path of new directory
        private static string ZipFilePath1= directoryPath
            + @"\Repository 1.zip";
        private static string ZipFilePath2 = directoryPath
            + @"\Repository 2.zip";

        // Cosntructor
        public RepositoryDownloader()
        {

        }
        // Creates the new folder if it already does not exists
        public void FolderCreator ()
        {
            Directory.CreateDirectory(directoryPath);
        }
        // Empties the folder
        public void EmptyFolder ()
        {
            if (Directory.Exists(directoryPath))
            {
                // Delete all files
                foreach (var file in Directory.GetFiles
                    (directoryPath))
                {
                    File.Delete(file);
                }

                // Delete all folders
                foreach (var directory in Directory.GetDirectories
                    (directoryPath))
                {
                    Directory.Delete(directory, true);
                }
            }
        }
        // Deletes the zip files that were downloaded from GitHub
        public void ZipFileDeleter()
        {
            File.Delete(ZipFilePath1);
            File.Delete(ZipFilePath2);
        }
        // Creates two empty zip files that are used to contain 
        // the GitHub repositories 
        public void ZipFileCreator()
        {
            using (FileStream zipToCreate =
                new FileStream(ZipFilePath1, FileMode.CreateNew))
            {
                ZipArchive archive =
                    new ZipArchive(zipToCreate, ZipArchiveMode.Create);
            }
            using (FileStream zipToCreate =
                new FileStream(ZipFilePath2, FileMode.CreateNew))
            {
                ZipArchive archive =
                    new ZipArchive(zipToCreate, ZipArchiveMode.Create);
            }
        }
        // Extracts the zip files into two seperate directories within
        // the directory that was created at the beginning
        public void ZipFileExtractor()
        {
            ZipFile.ExtractToDirectory(ZipFilePath1, directoryPath
                + @"\Repository 1");
            ZipFile.ExtractToDirectory(ZipFilePath2, directoryPath 
                + @"\Repository 2");
        }
        // Downloads the two GitHub repositories based on the user input
        public void Downlaod(string gitHubUrl1, string gitHubUrl2)
        {
            // Token taken from GitHub
            // This token only allows to use my own account not any account
            // The token only allows the use of repositories and nothing else
            var githubToken = "7192d8abfc7fe0436bc133c1de9293956a078525";
            var url1 = gitHubUrl1 + @"/archive/master.zip";
            var url2 = gitHubUrl2 + @"/archive/master.zip";
            var path = ZipFilePath1;
            // This downlaods the GitHub repositories based on first address
            using (var client = new System.Net.Http.HttpClient())
            {
                var credentials = string.Format
                    (System.Globalization.CultureInfo.InvariantCulture,
                    "{0}:", githubToken);
                credentials = Convert.ToBase64String
                    (System.Text.Encoding.ASCII.GetBytes(credentials));
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue
                    ("Basic", credentials);
                var contents = client.GetByteArrayAsync(url1).Result;
                System.IO.File.WriteAllBytes(path, contents);
            }
            // This downlaods the GitHub repositories based on second address
            var secondPath = ZipFilePath2;
            using (var client = new System.Net.Http.HttpClient())
            {
                var credentials = string.Format
                    (System.Globalization.CultureInfo.InvariantCulture,
                    "{0}:", githubToken);
                credentials = Convert.ToBase64String
                    (System.Text.Encoding.ASCII.GetBytes(credentials));
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue
                    ("Basic", credentials);
                var contents = client.GetByteArrayAsync(url2).Result;
                System.IO.File.WriteAllBytes(secondPath, contents);
            }
        }
    }
}
