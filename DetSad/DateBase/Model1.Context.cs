﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KindergartenDBEntities : DbContext
    {
        private static KindergartenDBEntities _context;

        public KindergartenDBEntities()
            : base("name=KindergartenDBEntities")
        {
        }

        public static KindergartenDBEntities GetEntities1()
        {
            if (_context == null)
            {
                _context = new KindergartenDBEntities();
            }
            return _context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Children> Children { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<EventsSchedule> EventsSchedule { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<MedicalRecords> MedicalRecords { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
