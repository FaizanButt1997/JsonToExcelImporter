using JSonToExcelUtility.Helpers;
using JSonToExcelUtility.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility
{
    public class Program
    {
      public static async Task Main(string[] args)
        {
            var csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Constant.CSV_FILE_NAME}.csv");

            if (!File.Exists(csvFilePath))
            {
                using (File.CreateText(csvFilePath))
                {
                }
            }

            Console.WriteLine("Starting extraction of JSON files from the folder path: {0}", APPCONFIG.FOLDER_PATH);

            var jsonResults = new List<JsonResult>();
            await Task.Run(() => Helper.LoadJson(APPCONFIG.FOLDER_PATH, jsonResults));

            if (APPCONFIG.IS_CSV)
            {
                Console.WriteLine("------------- Starting JSON to CSV Processing -----------------");
                await Task.Run(() => Helper.SaveJsonInCSV(csvFilePath, jsonResults));
                Console.WriteLine("CSV file successfully generated at: {0}", csvFilePath);
            }

            if (APPCONFIG.IS_SQL)
            {
                Console.WriteLine("------------- Starting JSON to Database Processing ------------");
                await Task.Run(() => Helper.SaveInSQL(jsonResults));
                Console.WriteLine("JSON data has been added to the database");
            }

            Console.WriteLine("");
            Console.WriteLine("Processing Has Been Ccompleted");
            Console.ReadKey();
        }
    }

}
