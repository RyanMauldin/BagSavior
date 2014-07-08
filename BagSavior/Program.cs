using System;

namespace BagSavior
{
    class Program
    {
        /// <summary>
        /// Basic program to read in json files and process the number of bags needed.
        /// </summary>
        /// <param name="args">The args array.</param>
        static void Main(string[] args)
        {
            // Display program title.
            Console.WriteLine("****    Top Grocier - Bag Savior    ****");
            Console.WriteLine();

            // Read in json files from the main project directory and process the
            // number of bags needed in each case.
            BagSaviorUtils.ProcessImportFile("BagSaviorImport1.json");
            BagSaviorUtils.ProcessImportFile("BagSaviorImport2.json");
            BagSaviorUtils.ProcessImportFile("BagSaviorImport3.json");
            BagSaviorUtils.ProcessImportFile("BagSaviorImport4.json");
            BagSaviorUtils.ProcessImportFile("BagSaviorImport5.json");

            // Display the footer message.
            Console.WriteLine();
            Console.WriteLine("Press any key to quit: ");

            // Exit program on next key press.
            Console.ReadKey(true);
        }
    }
}
