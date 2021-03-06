//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AkitoRentalsSample.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentalHistory
    {
        public int RentalHistoryId { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public Nullable<System.DateTime> RentalStartDate { get; set; }
        public Nullable<System.DateTime> RentalReturnDate { get; set; }
        public Nullable<decimal> RentalRate { get; set; }
        public int RentalMethodId { get; set; }
        public int IssuingEmployeeId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<bool> SendReminderAlert { get; set; }
        public Nullable<int> StatusId { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee IssuingEmployee { get; set; }
        public virtual RentalMethod RentalMethod { get; set; }
        public virtual RentalStatus RentalStatus { get; set; }
    }
}
