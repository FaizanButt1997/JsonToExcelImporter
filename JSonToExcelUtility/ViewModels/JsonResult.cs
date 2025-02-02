using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.ViewModels
{
    [Serializable]
    public class JsonResult
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public Document document { get; set; }
    }
}
