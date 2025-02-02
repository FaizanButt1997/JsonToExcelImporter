using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonToExcelUtility.ViewModels
{
    [Serializable]
    public class Document
    {

        public static bool mappingStatus = false;
        public List<DocumentTypeInfo> docTypes { get; set; }
        public DocumentTitleDetails documentTitle { get; set; }
        public List<DocumentTypeSpecificInfo> documentTypeSpecificInfo { get; set; }
        public AgreementDateDetails agreementDate { get; set; }
        public List<GoverningAndJudrictionDetails> jurisdiction { get; set; }
        public GoverningAndJudrictionDetails governingLaw { get; set; }
        public List<Parties> parties { get; set; }
        public List<Sections> sections { get; set; }
        public ResponseHeader header { get; set; }

        //constructor

        public Document()
        {
            docTypes = new List<DocumentTypeInfo>();
            documentTitle = new DocumentTitleDetails();
            documentTypeSpecificInfo = new List<DocumentTypeSpecificInfo>();
            agreementDate = new AgreementDateDetails();
            jurisdiction = new List<GoverningAndJudrictionDetails>();
            governingLaw = new GoverningAndJudrictionDetails();
            parties = new List<Parties>();
            sections = new List<Sections>();
            header = new ResponseHeader();
        }
    }
}
