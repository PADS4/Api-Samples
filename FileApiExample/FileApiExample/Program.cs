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
            Console.WriteLine("FOLDER GOTTEN");
            var newFolderObject = fai.CreateFolder("TestFolder").Result;
            Console.WriteLine(newFolderObject);
            Console.WriteLine("FOLDER CREATED");
            var folderObject = fai.GetFolder("TestFolder").Result;
            Console.WriteLine(folderObject);
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
