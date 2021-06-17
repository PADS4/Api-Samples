﻿using System;

namespace FileApiExample
{
    class Program
    {
        static void Main()
        {
            FileApiExample fai = new FileApiExample();
            var authObject = fai.Authenticate("user1", "user1", "pads", "http://localhost:81/").Result;
            Console.WriteLine(authObject);
            Console.WriteLine("AUTHENTICATED");
            var newFolderObject = fai.CreateFolder("TestFolder", "\\", false).Result;
            Console.WriteLine(newFolderObject);
            Console.WriteLine("FOLDER CREATED");
            var newFolder2Object = fai.CreateFolder("TestFolder2", "\\", false).Result;
            Console.WriteLine(newFolder2Object);
            Console.WriteLine("FOLDER CREATED");
            var folderObject = fai.GetFolder("TestFolder", 0, 10, "").Result;
            Console.WriteLine(folderObject);
            Console.WriteLine("FOLDER GOTTEN");
            var moveFolderObject = fai.MoveFolder("TestFolder", "TestFolder2").Result;
            Console.WriteLine(moveFolderObject);
            Console.WriteLine("FOLDER MOVED");
            var uploadFileObject = fai.UploadFile(".\\Assets\\slipper.png", "slipper", "slipper.png").Result;
            Console.WriteLine(uploadFileObject);
            Console.WriteLine("FILE UPLOADED");
            var moveFileObject = fai.MoveFile("slipper.png", "", "TestFolder2", "slipper.png").Result;
            Console.WriteLine(moveFileObject);
            Console.WriteLine("FILE MOVED");
            var deleteFileObject = fai.DeleteFile("slipper.png", "TestFolder2").Result;
            Console.WriteLine(deleteFileObject);
            Console.WriteLine("FILE DELETED");
            var deleteFolderObject = fai.DeleteFolder("TestFolder2").Result;
            Console.WriteLine(deleteFolderObject);
            Console.WriteLine("FOLDER DELETED");
            Console.WriteLine("Done");
        }
    }
}
