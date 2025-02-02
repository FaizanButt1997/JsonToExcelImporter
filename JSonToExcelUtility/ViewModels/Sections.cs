using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.ViewModels
{
    public class Sections
    {
        public string Title { get; set; } // The clause title
        public string SectionNumber { get; set; }
        public string TypeName { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public int Level { get; set; }

        public int TypeID { get; set; }

        public List<Sections> sections { get; set; }

        public bool IsDefinition { get; set; }

        public Sections()
        {
            sections = new List<Sections>();
        }
    }
}
