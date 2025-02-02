using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.Helpers
{
    public static class APPCONFIG
    {
       public static readonly string FOLDER_PATH =  ConfigurationManager.AppSettings["folderPath"];
       public static readonly int BUILD_ID =  Convert.ToInt32(ConfigurationManager.AppSettings["buildId-Integer"]);
       public static readonly string TABLE_DOCUMENT =  ConfigurationManager.AppSettings["tableDocument"];
       public static readonly string TABLE_SECTION =  ConfigurationManager.AppSettings["tableSection"];
       public static readonly string TABLE_DEALPOINT =  ConfigurationManager.AppSettings["tableDealpoint"];
       public static readonly string TABLE_GOVERNINGLAW =  ConfigurationManager.AppSettings["tableGoverningLaw"];
       public static readonly string TABLE_JURISDICTION =  ConfigurationManager.AppSettings["tableJurisdiction"];
       public static readonly string TABLE_PARTYINVOLVED =  ConfigurationManager.AppSettings["tablePartyInvolved"];
       public static readonly bool IS_CSV = Convert.ToBoolean(ConfigurationManager.AppSettings["ImportDataIntoCSV"]);
       public static readonly bool IS_SQL = Convert.ToBoolean(ConfigurationManager.AppSettings["ImportDataIntoSQL"]);
    }
}
