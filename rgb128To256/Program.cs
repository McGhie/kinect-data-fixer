using System;


namespace KinnectDataToHexAndXYZ
{
    public class Program
    {

        
        static void Main(string[] args)
        {
            Console.Title = "Bespaced data to xyz and hex, Argyll McGhie";
            Console.WriteLine(@"Author: Argyll McGhie

For Bespaced data to xyz and hex.
This formats the data and places it into json file format.
Removes all bloat, including zero and infinity.\n
Removes the UV
Reduces the decimal format from.^ 15 to.^ 3");

            while (true)
            {
                string FileName = Console.ReadLine();

                if (FileName.Trim(' ') != "")
                {
                    Data D = new Data(0, "Data.txt");
                    break;
                }

            }
            while (true) { }



        }


        //remove not being used
    }

}
