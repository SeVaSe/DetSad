//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DetSad.DateBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Children
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Children()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int ChildID { get; set; }
        public int GroupID { get; set; }
        public string ChildName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string MotherName { get; set; }
        public string MotherNumber { get; set; }
        public string FatherName { get; set; }
        public string FatherNumber { get; set; }
        public string Allergy { get; set; }
        public Nullable<int> MedicalCertificateID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<bool> IsHere { get; set; }
    
        public virtual Groups Groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
        public virtual Contracts Contracts { get; set; }
        public virtual MedicalRecords MedicalRecords { get; set; }
    }
}
