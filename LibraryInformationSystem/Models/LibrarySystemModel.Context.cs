﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryInformationSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DemoEntities : DbContext
    {
        public DemoEntities()
            : base("name=DemoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Book_Image> Book_Image { get; set; }
        public virtual DbSet<Book_Recommendation> Book_Recommendation { get; set; }
        public virtual DbSet<Book_Reservation> Book_Reservation { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Customer_favorite> Customer_favorite { get; set; }
        public virtual DbSet<Message_Board> Message_Board { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<Recommendation_Status> Recommendation_Status { get; set; }
        public virtual DbSet<Record> Record { get; set; }
        public virtual DbSet<Rent_Item> Rent_Item { get; set; }
        public virtual DbSet<Reply_Message_Board> Reply_Message_Board { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}