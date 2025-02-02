using JSonToExcelUtility.JSonToSQL;
using JSonToExcelUtility.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JSonToExcelUtility.Helpers
{
    public static class Helper
    {
        public static void LoadJson(string folderPath, List<JsonResult> resultList)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    throw new DirectoryNotFoundException($"Directory not found: {folderPath}");
                }
                var jsonFiles = Directory.GetFiles(folderPath, "*.json");
                if (jsonFiles.Length == 0)
                {
                    throw new FileNotFoundException("No JSON files found in directory.", folderPath);
                }
                foreach (string file in jsonFiles)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        string fileId = string.Empty;
                        if (fileInfo.Name.EndsWith(".json"))
                        {
                            if (fileInfo.Name.Length < 30)
                            {

                                if (fileInfo.Name.Contains("-"))
                                    fileId = fileInfo.Name.Split('-')[0].Trim();
                                else
                                    fileId = fileInfo.Name.Split('.')[0].Trim();
                            }
                            else
                            {
                                var match = Regex.Matches(fileInfo.Name, @"_(\d+)_");
                                if (match.Count > 0)
                                {
                                    fileId = match[match.Count - 1].Groups[1].Value;
                                }
                                else
                                {
                                    fileId = string.Empty;
                                }
                            }
                            resultList.Add(new JsonResult()
                            {
                                FileName = fileInfo.Name,
                                FileId = fileId,
                                document = JsonConvert.DeserializeObject<Document>(LongFile.ReadAllText(fileInfo.FullName))
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deserializing JSON from file '{file}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SaveJsonInCSV(string filePath, List<JsonResult> resultList)
        {
            try
            {
                System.IO.File.WriteAllText(filePath, string.Empty);
                var listHeader = GetAllListHeaders(resultList);
                string header = $"Document Id,Document Name,Document Type,Title,Parties Involved,Governing Law,Jurisdiction,Agreement Date,{String.Join(",", listHeader)}";
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(header);
                    foreach (var reusltObj in resultList)
                    {
                        var filename = reusltObj.FileName;
                        var fileId = reusltObj.FileId;
                        var documentTypeName = string.Join(" , ", reusltObj.document.docTypes.Select(x => x.DocumentTypeName));
                        var documentTitle = reusltObj?.document?.documentTitle?.DocumentTitle ?? String.Empty;
                        var partiesInvolved = string.Join(" || ", reusltObj.document.parties.Select(x => x.Value));
                        var governingLaw = reusltObj.document.governingLaw.Code;
                        var jurisdiction = string.Join(" || ", reusltObj.document.jurisdiction.Select(x => x.Code));
                        var agreementDate = reusltObj.document.agreementDate.Value;

                        var dealpointList = reusltObj?.document?.documentTypeSpecificInfo?.Select(x => x)?.GroupBy(x => x.Type)?.Select(group => new DocumentTypeSpecificInfo
                                        {
                                            Type = group.Key,
                                            Value = string.Join(" || ", group.Select(item => item.Value)),
                                        })
                                        ?.ToList() ?? Enumerable.Empty<DocumentTypeSpecificInfo>();

                        IEnumerable<string> _matchValuesWithHeader = listHeader.Select(h =>
                        {
                            var match = dealpointList.FirstOrDefault(x => x.Type.Equals(h, StringComparison.CurrentCultureIgnoreCase));
                            return match != null ? match?.Value?.Replace(",", string.Empty) ?? string.Empty : string.Empty;
                        });

                        var matchDealpointValues = String.Join(",", _matchValuesWithHeader);
                        string values = $"{fileId},\"{filename?.Replace("\"", "\"\"")?.Replace("\n", string.Empty)?.Replace(",", string.Empty)}\",\"{documentTypeName?.Replace("\"", "\"\"")?.Replace("\n", string.Empty)}\",\"{documentTitle?.Replace("\"", "\"\"")?.Replace("\n", string.Empty)?.Replace(",", string.Empty)}\",\"{partiesInvolved?.Replace("\"", "\"\"").Replace("\n", string.Empty)}\",\"{governingLaw?.Replace("\"", "\"\"")}\",\"{jurisdiction.Replace("\"", "\"\"").Replace("\n", string.Empty)}\",{agreementDate},{matchDealpointValues.Replace("\n", string.Empty)}";
                        sw.WriteLine(values);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error occurred while saving CSV file: {ex.Message}");
            }
        }
        public static void SaveInSQL(List<JsonResult> resultList)
        {
            string log = string.Empty;
            string query = string.Empty;
            query += Utility.DeletePreviousRecord(resultList);
            if (!string.IsNullOrEmpty(query))
                SQLManager.StoreInDB(query);

            foreach (var item in resultList)
            {
                query = string.Empty;
                var fileName = item.FileName;
                var docId = Convert.ToInt32(item.FileId);
                try
                {
                    if (docId > 0 && fileName.Contains(".json"))
                    {
                        query += Utility.InsertIntoExhibit(docId, fileName, item.document.documentTitle, item.document.docTypes);
                        query += Utility.InsertIntoExhibitSection(docId, item.document.sections);
                        query += Utility.InsertIntoExhibitAgreementSpecificValue(docId, item.document.documentTypeSpecificInfo);
                        query += Utility.InsertIntoExhibitGoverningLaw(docId, item.document.governingLaw);
                        query += Utility.InsertIntoExhibitLocationJurisdiction(docId, item.document.jurisdiction);
                        query += Utility.InsertIntoExhibitPartyInvolved(docId, fileName, item.document.parties);

                        if (!string.IsNullOrEmpty(query))
                            SQLManager.StoreInDB(query);

                        log += "Processing completed for docId : " + docId;
                        log += "\r\n";
                    }
                }
                catch (Exception ex)
                {
                    log += "Exception for docId : " + docId + " ---- " + ex.ToString();
                    log += "\r\n";
                }

            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", log);

        }
        public static HashSet<string> GetAllListHeaders(List<JsonResult> resultList)
        {

            HashSet<string> mySet = new HashSet<string>();
            var documents = resultList.Select(d => d.document);
            var documentTypeInfos = documents.SelectMany(d => d.documentTypeSpecificInfo);
            var documentTypes = documentTypeInfos.Select(dti => dti.Type);
            return new HashSet<string>(documentTypes);
        }
    }
}
