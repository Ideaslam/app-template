using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 

namespace Domain.Entities
{
   public class ExceptionHandling
    {


        public void  main()
        {
            string path =  Console.ReadLine();
            Console.WriteLine(path);
            StreamReader streamReader = new StreamReader(@"D:\SC\Final Sync\islah\IslahProject\Domain\Entities\Errors\data.txt");
            Console.WriteLine(streamReader.ReadToEnd());

            StreamWriter sw = new StreamWriter(path);
            sw.Write("Message");
            sw.Close();
            streamReader.Close();
        }

    }
}
