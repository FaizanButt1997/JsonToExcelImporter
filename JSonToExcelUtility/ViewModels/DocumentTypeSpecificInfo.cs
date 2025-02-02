using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.ViewModels
{
    public class DocumentTypeSpecificInfo
    {
        public string Type { set; get; }
        public int FilterKey { set; get; }
        public int ControlType { set; get; }
        public string FilterValue { set; get; }
        public string FieldValue { set; get; }
        public string Currency { set; get; }
        public int StartIndex { set; get; }
        public int EndIndex { set; get; }
        public string Value { set; get; }

        public string ExtractedValue { set; get; }

    }
}
