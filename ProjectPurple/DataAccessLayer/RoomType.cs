//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomType()
        {
            this.RoomOccupancies = new HashSet<RoomOccupancy>();
        }
    
        public System.Guid Id { get; set; }
        public int BaseRate { get; set; }
        public int Inventory { get; set; }
        public DataAccessLayer.Constants.ROOM_TYPE Type { get; set; }
        public string Ameneties { get; set; }
        public string Description { get; set; }
    
        public virtual Reservation Reservation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomOccupancy> RoomOccupancies { get; set; }
    }
}
