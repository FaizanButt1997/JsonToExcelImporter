using JSonToExcelUtility.Helpers;
using JSonToExcelUtility.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.JSonToSQL
{
    public class Utility
    {
        public static Dictionary<int, string> JurisidictionMapping = new Dictionary<int, string>()
        {
            {1,  "Alabama"},
            {2, "Alaska"},
            {3, "Arizona"},
            {4, "Arkansas"},
            {5, "California"},
            {6, "Colorado"},
            {7, "Connecticut"},
            {8, "Delaware"},
            {9, "Florida"},
            {10,    "Georgia"},
            {11,    "Hawaii"},
            {12,    "Idaho"},
            {13,    "Illinois"},
            {14,    "Indiana"},
            {15,    "Iowa"},
            {16,    "Kansas"},
            {17,    "Kentucky"},
            {18,    "Louisiana"},
            {19,    "Maine"},
            {20,    "Maryland"},
            {21,    "Massachusetts"},
            {22,    "Michigan"},
            {23,    "Minnesota"},
            {24,    "Mississippi"},
            {25,    "Missouri"},
            {26,    "Montana"},
            {27,    "Nebraska"},
            {28,    "Nevada"},
            {29,    "New Hampshire"},
            {30,    "New Jersey"},
            {31,    "New Mexico"},
            {32,    "New York"},
            {33,    "North Carolina"},
            {34,    "North Dakota"},
            {35,    "Ohio"},
            {36,    "Oklahoma"},
            {37,    "Oregon"},
            {38,    "Pennsylvania"},
            {39,    "Rhode Island"},
            {40,    "South Carolina"},
            {41,    "South Dakota"},
            {42,    "Tennessee"},
            {43,    "Texas"},
            {44,    "Utah"},
            {45,    "Vermont"},
            {46,    "Virginia"},
            {47,    "Washington"},
            {48,    "West Virginia"},
            {49,    "Wisconsin"},
            {50,    "Wyoming"},
            {51,    "Ontario"},
            {52,    "Québec"},
            {53,    "Nova Scotia"},
            {54,    "New Brunswick"},
            {55,    "Manitoba"},
            {56,    "British Columbia"},
            {57,    "Prince Edward Island"},
            {58,    "Newfoundland and Labrador"},
            {59,    "Saskatchewan"},
            {60,    "Alberta"},
            {61,    "Cayman Islands"},
            {63,    "Hong Kong"},
            {64,    "Canada"},
            {65,    "Thailand"},
            {66,    "China"},
            {67,    "England"},
            {69,    "Australia "},
            {70,    "Bermuda"},
            {71,    "Netherlands"},
            {72,    "Singapore"},
            {73,    "British Virgin Islands"},
            {74,    "Austria"},
            {76,    "Puerto Rico"},
            {77,    "Ireland"},
            {78,    "District of Columbia"},
            {79,    "Japan"},
            {80,    "France"},
            {81,    "Brazil"},
            {82,    "Israel"},
            {83,    "Czech Republic"},
            {84,    "Republic of Colombia"},
            {85,    "South Korea"},
            {86,    "Germany"},
            {87,    "Luxembourg"},
            {88,    "India"},
            {89,    "Liberia"},
            {90,    "Cyprus"},
            {91,    "Portugal"},
            {93,    "Marshall Islands"},
            {94,    "Canada"},
            {95,    "Bahamas"},
            {96,    "Spain"},
            {97,    "United States"},
            {98,    "Jersey"},
            {99,    "Malaysia"}

        };

        public static int InsertExhibitDoc(string fileName, string file)
        {
            string Query = $@"INSERT INTO `db_test`.`tbl_blackbox_documents` (`doc_id`, `doc_name`) VALUES('{fileName}', '{file}')";
            return SQLManager.StoreInDB(Query);
        }

        public static int GetExhibitDoc(string fileName, string format)
        {
            string Query = $@"select pk_id as id from db_test.tbl_blackbox_documents where doc_id = {fileName} and doc_name like '%{format}%'";
            return SQLManager.SelectInDB(Query);
        }

        private static int GetExhibitVersion(string version)
        {
            string Query = $@"select id from db_test.tbl_blackbox_ref_build where build_number = '{version}'";
            return SQLManager.SelectInDB(Query);
        }

        public static string InsertIntoExhibitSection(int docId, List<Sections> sections)
        {
            StringBuilder Query = new StringBuilder();
            var queryString = string.Empty;
            if (sections.Any())
            {
                sections.ForEach(s =>
                {
                    if (s.Level < 3)
                        Query.Append(sectionParsing(docId, s));
                });
                queryString = Query.ToString().Trim(',') + ";";
                if (!String.IsNullOrEmpty(queryString))
                {
                    queryString =
                        $@"insert into {APPCONFIG.TABLE_SECTION} (`document_id`,`clause_index`,`parent_index`,`heading_name`,`char_start_index`,`char_end_index`,`is_definition`,`clause_level`,`count`,`version_id`) values" +
                        queryString;
                }
            }
            return queryString;
        }

        public static string sectionParsing(int docId, Sections s)
        {
            var sectionstring = $@"({docId},{0},{0},'{s.Title.Replace("'", "")}',{s.StartIndex},{s.EndIndex},{s.IsDefinition},{s.Level},{s.sections.Count},{APPCONFIG.BUILD_ID}),";
            return sectionstring.ToString();
        }

        public static string InsertIntoExhibit(int docId, string docName, DocumentTitleDetails docTitleInfo, List<DocumentTypeInfo> docTypeInfo)
        {
            StringBuilder Query = new StringBuilder();
            var docTypes = String.Join(" , ", docTypeInfo.Select(x => x.DocumentTypeName));
            int? docTypeId = docTypeInfo.Select(x => x.DocumentTypeID)?.FirstOrDefault();

            Query.Append($@"INSERT INTO {APPCONFIG.TABLE_DOCUMENT} (`doc_id`,`doc_name`,`doc_title`,`doc_type`,`doc_type_id`,`version_id`) values ");
            Query.Append($@"({docId},'{docName}','{docTitleInfo?.DocumentTitle}','{docTypes}',{docTypeId}, {APPCONFIG.BUILD_ID});");
            return Query.ToString();
        }

        public static string InsertIntoExhibitPartyInvolved(int docId,string docName, List<Parties> parties)
        {
            StringBuilder Query = new StringBuilder();

            Query.Append($@"insert into {APPCONFIG.TABLE_PARTYINVOLVED} ( `document_id`,`party_startIndex`, `party_endIndex`,`ref_party_name`,`version_id` ) 
                        values");
            if (parties.Count == 0)
                return "";
            parties.ForEach(y =>
            {
                Query.Append($@"({docId},{y.StartIndex},{y.EndIndex},'{y.Value.Replace("'", "")}',{APPCONFIG.BUILD_ID}),");
            });
            var a = Query.ToString().TrimEnd(',') + ";";
            return a;
        }

        public static string InsertIntoExhibitLocationJurisdiction(int docId,List<GoverningAndJudrictionDetails> jurdiLawAndJurisdictionLocation)
        {

            StringBuilder Query = new StringBuilder();
            Query.Append($@"insert into {APPCONFIG.TABLE_JURISDICTION} (`document_id`, `jurisdiction_id`,`jurisdiction_start_index`, `jurisdiction_end_index`,`version_id`) values");

            if (jurdiLawAndJurisdictionLocation.Count == 0)
                return "";
            jurdiLawAndJurisdictionLocation.ToList().ForEach(J =>
            {
                Query.Append($@"({docId},{MapLoaction(J.Province)},{J.StartIndex},{J.EndIndex},{APPCONFIG.BUILD_ID}),");
            });
            var a = Query.ToString().TrimEnd(',') + ";";
            return a;
        }

        public static string InsertIntoExhibitGoverningLaw(int docId, GoverningAndJudrictionDetails jurdiLawAndJurisdiction)
        {

            StringBuilder Query = new StringBuilder();
            Query.Append($@"insert into {APPCONFIG.TABLE_GOVERNINGLAW} (`document_id`, `law_id`,`law_start_index`, `law_end_index`,`version_id`) values");

            if (jurdiLawAndJurisdiction == null)
                return "";
            Query.Append($@"({docId},{MapLoaction(jurdiLawAndJurisdiction.Province)},{jurdiLawAndJurisdiction.StartIndex},{jurdiLawAndJurisdiction.EndIndex},{APPCONFIG.BUILD_ID}),");
            var a = Query.ToString().TrimEnd(',') + ";";
            return a;
        }

        public static string InsertIntoExhibitAgreementSpecificValue(int docId, List<DocumentTypeSpecificInfo> documentTypeSpecificInfo)
        {
            StringBuilder Query = new StringBuilder();

            Query.Append($@"insert into {APPCONFIG.TABLE_DEALPOINT} ( `document_id`,`value_type_id`, `type`,`start_index`, `end_index` , `value` , `normalize_value` , `version_id` ) 
                        values");
            if (documentTypeSpecificInfo.Count == 0)
                return "";
            documentTypeSpecificInfo.ForEach(y =>
            {
                var field = string.IsNullOrEmpty(y.FieldValue) ? "" : y.FieldValue.Replace("'", "");
                var extractedValue = string.IsNullOrEmpty(y.ExtractedValue) ? "" : y.ExtractedValue.Replace("'", "");
                Query.Append($@"({docId},{y.FilterKey},'{y.Type}',{y.StartIndex},{y.EndIndex},'{y.FilterValue.Replace("'", "")}','{y.FilterValue.Replace("'", "")}',{APPCONFIG.BUILD_ID}),");
            });
            var a = Query.ToString().TrimEnd(',') + ";";
            return a;
        }

        public static string DeletePreviousRecord(List<JsonResult> resultList)
        {
            var docIds = String.Join(",", resultList.Select(x => x.FileId));

            string query = $@"DELETE FROM {APPCONFIG.TABLE_DOCUMENT} where doc_id IN ({docIds}) AND version_id IN ({APPCONFIG.BUILD_ID});";
            query += $@"DELETE FROM {APPCONFIG.TABLE_SECTION} where document_id IN ({docIds}) AND version_id IN ({APPCONFIG.BUILD_ID});";
            query += $@"DELETE FROM {APPCONFIG.TABLE_DEALPOINT} where document_id IN ({docIds}) AND version_id IN ({APPCONFIG.BUILD_ID});";
            query += $@"DELETE FROM {APPCONFIG.TABLE_GOVERNINGLAW} where document_id IN ({docIds}) AND version_id IN ({APPCONFIG.BUILD_ID});";
            query += $@"DELETE FROM {APPCONFIG.TABLE_JURISDICTION} where document_id IN ({docIds}) AND version_id IN ({APPCONFIG.BUILD_ID});";
            query += $@"DELETE FROM {APPCONFIG.TABLE_PARTYINVOLVED} where document_id IN ({docIds}) AND version_id IN ({APPCONFIG.BUILD_ID});";

            return query;
        }
        public static int MapLoaction(string Loacation)
        {
            int Loaction_id = 0;
            return Loaction_id = JurisidictionMapping.FirstOrDefault(value => value.Value.Equals(Loacation)).Key;
        }
    }
}
