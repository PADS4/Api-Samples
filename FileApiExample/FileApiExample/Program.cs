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
            var newFolderObject = fai.CreateFolder("TestFolder").Result;
            Console.WriteLine(newFolderObject);
            Console.WriteLine("FOLDER CREATED");
            var newFolder2Object = fai.CreateFolder("TestFolder2").Result;
            Console.WriteLine(newFolder2Object);
            Console.WriteLine("FOLDER CREATED");
            var folderObject = fai.GetFolder("TestFolder").Result;
            Console.WriteLine(folderObject);
            Console.WriteLine("FOLDER GOTTEN");
            var moveFolderObject = fai.MoveFolder("TestFolder2").Result;
            Console.WriteLine(moveFolderObject);
            Console.WriteLine("FOLDER MOVED");
            var uploadFileObject = fai.UploadFile(".\\Assets\\slipper.png").Result;
            Console.WriteLine(uploadFileObject);
            Console.WriteLine("FILE UPLOADED");
            var moveFileObject = fai.MoveFile("TestFolder2").Result;
            Console.WriteLine(moveFileObject);
            Console.WriteLine("FILE MOVED");
            var deleteFileObject = fai.DeleteFile("slipper.png").Result;
            Console.WriteLine(deleteFileObject);
            Console.WriteLine("FILE DELETED");
            var deleteFolderObject = fai.DeleteFolder("TestFolder2").Result;
            Console.WriteLine(deleteFolderObject);
            Console.WriteLine("FOLDER DELETED");
            Console.WriteLine("Done");
        }
    }
}
