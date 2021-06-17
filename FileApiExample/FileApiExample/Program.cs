using System;

namespace FileApiExample
{
    class Program
    {
        static void Main()
        {
            FileApiExample fai = new FileApiExample();
            var authObject = fai.Authenticate().Result;
            Console.WriteLine(authObject);
            Console.WriteLine("AUTHENTICATED");
            var folderObject = fai.GetFolder("upload_files").Result;
            Console.WriteLine(folderObject);
            Console.WriteLine("FOLDER GOTTEN");
            var newFolderObject = fai.CreateFolder("TestFolder").Result;
            Console.WriteLine(newFolderObject);
            Console.WriteLine("FOLDER CREATED");
            var moveFolderObject = fai.MoveFolder("\\").Result;
            Console.WriteLine(moveFolderObject);
            Console.WriteLine("FOLDER MOVED");
            var uploadFileObject = fai.UploadFile(".\\Assets\\slipper.png").Result;
            Console.WriteLine(uploadFileObject);
            Console.WriteLine("FILE UPLOADED");
            Console.WriteLine("Done");
        }
    }
}
