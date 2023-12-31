﻿using System;
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

    public class StudentModel
    {
        public int ChildID { get; set; } // ФИО студента
        public string FIO { get; set; } // ФИО студента
        public string Birth { get; set; } // ФИО студента
        public string MomName { get; set; } // ФИО студента
        public string NumbMom { get; set; }
        public string Group { get; set; }
        public string  DadName { get; set; } // ФИО студента
        public string NumbDad { get; set; } // ФИО студента
        public string Allergy { get; set; } // ФИО студента
        public bool IsHere { get; set; } // Состояние присутствия (True/False)
        public int? MedID { get; set; } // ФИО студента
        public int? ContarctID { get; set; } // ФИО студента


    }


}
