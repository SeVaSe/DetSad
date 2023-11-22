using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetSad.Classes
{
    public class Event
    {
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string EventName { get; set; }
    }

    public class MedSpravkaModel
    {
        public int ChildID { get; set; }
        public string FIO { get; set; }
        public string Birth { get; set; }
        public string Allergy { get; set; }
        public string NameMom { get; set; }
        public string NumbMom { get; set; }
        public string NameSpravka { get; set; }
        public int NameGroup { get; set; }
        public int? MedicalCertificateID { get; set; } // Добавьте свойство ID для справки
    }

    public class DogovorModel
    {
        public int ChildID { get; set; }
        public string FIO { get; set; }
        public string Birth { get; set; }
        public string Allergy { get; set; }
        public string NameMom { get; set; }
        public string NumbMom { get; set; }
        public string NameDogovor { get; set; }
        public int NameGroup { get; set; }
        public int? DogovorCertificateID { get; set; } // Добавьте свойство ID для справки
    }

}
