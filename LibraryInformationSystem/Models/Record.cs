//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Record
    {
        public int BookID { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<int> AdminID { get; set; }
        public int CardID { get; set; }
        public Nullable<System.DateTime> Update_Time { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Card Card { get; set; }
    }
}
