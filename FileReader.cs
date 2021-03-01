using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Labb_3__Alexander_Bartha
{
    class FileReader
    {


        static void Main(string[] args)
        {
            string pngSignature = "89-50-4E-47-0D";
            string bmpSignature = "42-4D";
            string targetDirectory = @"../../../";

            string[] fileArray = Directory.GetFiles(targetDirectory, "");//.Select(Path.GetFileNameWithoutExtension).Select(p => p.Substring(0)).ToArray();
            Console.WriteLine("List of files to check if BMP or PNG.");
            foreach (string fileName in fileArray)
            {

                Console.WriteLine(fileName);

            }
            Console.WriteLine("Enter filename(dont't forget to add file extension):");
            string input = Console.ReadLine();
            string directoryInput = targetDirectory + input;
            Console.WriteLine(directoryInput);
            byte[] byteArray = new byte[32];
            byte[] pictureWidth = new byte[4];
            byte[] pictureHeight = new byte[4];
            try
            {
                using (FileStream fs = new FileStream(directoryInput, FileMode.Open, FileAccess.Read))
                {

                    fs.Read(byteArray, 0, byteArray.Length);
                    Buffer.BlockCopy(byteArray, 16, pictureWidth, 0, pictureWidth.Length);
                    Buffer.BlockCopy(byteArray, 20, pictureHeight, 0, pictureHeight.Length);


                    string stringByte = BitConverter.ToString(byteArray);
                   
                    if (stringByte.Substring(0, pngSignature.Length) == pngSignature)
                    {
                        Array.Reverse(pictureWidth);
                        Array.Reverse(pictureHeight);
                        var width = BitConverter.ToInt32(pictureWidth[..4]);
                        var height = BitConverter.ToInt32(pictureHeight[..4]);


                        Console.WriteLine($"It's a .png file with width: {width} and height: {height}.");

                    }
                    else if (stringByte.Substring(0, bmpSignature.Length) == bmpSignature)
                    {
                        
                        (pictureHeight[2], pictureHeight[3]) = (pictureHeight[3], pictureHeight[2]);
                        (pictureWidth[2], pictureWidth[3]) = (pictureWidth[3], pictureWidth[2]);
                        
                        Array.Reverse(pictureWidth);
                        Array.Reverse(pictureHeight);                  
                        
                        var width = BitConverter.ToInt32(pictureWidth[..4]);
                        var height = BitConverter.ToInt32(pictureHeight[..4]);
                        Console.WriteLine($"It's .bmp file with width: {width} and height: {height}.");
                       
                    }
                    else
                    {
                        Console.WriteLine("It's not a .bmp or .png file.");
                    }

                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();
            Console.ReadKey();
        }
       
            



        }

    

}
