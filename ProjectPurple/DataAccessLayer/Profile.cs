
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
    
public partial class Profile
{

    public System.Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }



    public virtual PhoneNumber PhoneNumber { get; set; }

    public virtual Email Email { get; set; }

    public virtual Address Addresse { get; set; }

    public virtual Reservation Reservation { get; set; }

    public virtual User User { get; set; }

}

}
