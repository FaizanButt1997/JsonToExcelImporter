using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.ViewModels
{
    public class ResponseHeader
    {
        public string DocumentHash { get; set; }
        public List<ParserVersion> BasicParsers { get; set; }
        public List<ParserVersion> AgreementSpecificParsers { get; set; }
        public string BuildVersion { get; set; }
        public string DateUpdated { get; set; }
        public string MachineName { get; set; }
        public bool IsTextContent { get; set; }
    }

    public class ParserVersion
    {
        public int ParserId { set; get; }
        public int Version { set; get; }
        public ParserTypeEnum ParserType { set; get; }
        public int DocumentTypeId { get; set; }
    }

    public enum ParserTypeEnum
    {
        Generic = 1,
        Legacy = 2
    }
}
