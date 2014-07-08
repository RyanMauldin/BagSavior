using System;
using System.IO;
using System.Text;
using BagSavior.Configuration;
using BagSavior.Library;
using Newtonsoft.Json;

namespace BagSavior
{
    public static class BagSaviorUtils
    {
        /// <summary>
        /// Grabs the fileName specified from the main project directory of the currently executing program,
        /// or it uses the currently executing directory if two parent directories do not exist.
        /// </summary>
        /// <param name="fileName">The name of the file to check for.</param>
        /// <returns>
        /// Returns the full path for the file name given.
        /// </returns>
        private static string GetFullImportFilePath(string fileName)
        {
            // Going back to project directory to find input file.
            var builder = new StringBuilder(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));
            builder.Remove(0, 6); // removes file:\ from the start.
            var executingDirectoryInfo = new DirectoryInfo(builder.ToString());
            if (!executingDirectoryInfo.Exists)
                throw new Exception(string.Format("Unable to fetch working directory: {0}", builder));

            // If the directory structure of executing program is not like:
            // BagSavior/bin/Debug, just try to find file in the bin folder.
            if (executingDirectoryInfo.Parent != null)
            {
                var buildDirectoryInfo = executingDirectoryInfo.Parent;
                if (buildDirectoryInfo.Parent != null)
                {
                    var projectDirectoryInfo = buildDirectoryInfo.Parent;
                    builder.Clear();
                    builder.Append(projectDirectoryInfo.FullName);
                }
            }

            builder.Append("\\").Append(fileName);
            return builder.ToString();
        }

        /// <summary>
        /// Processes the import of a specified file name
        /// and outputs the calculation of the number of bags needed from the json input.
        /// </summary>
        /// <param name="fileName">The name of the file to fetch json from.</param>
        public static void ProcessImportFile(string fileName)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Importing File: {0}", fileName);
                Console.WriteLine();
                var bagCalculator = new BagCalculator(new AppSettingsManager());
                var filePath = GetFullImportFilePath(fileName);

                var builder = new StringBuilder();
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    builder.Append(streamReader.ReadToEnd());
                    streamReader.Close();
                    fileStream.Close();
                }

                var jsonImport = builder.ToString();
                if (string.IsNullOrEmpty(jsonImport))
                    throw new Exception(string.Format("Json Import file was empty: {0}", filePath));

                var productModel = JsonConvert.DeserializeObject<ProductModel>(jsonImport);
                if (productModel == null)
                    throw new Exception(string.Format("Unable to parse json object from file: {0}", filePath));

                var numberOfBags = bagCalculator.GetNumberOfBags(
                    productModel.ItemTypes, productModel.BagStrength);
                Console.WriteLine("Number of Bags Needed: {0}", numberOfBags);
            }
            catch (Exception e)
            {
                var builder = new StringBuilder("ERROR: ").AppendLine(e.Message);
                if (e.InnerException != null)
                    builder.AppendLine().AppendLine("INNER EXCEPTION: ").AppendLine(e.InnerException.Message);
                Console.WriteLine(builder.ToString());
            }
        }
    }
}
