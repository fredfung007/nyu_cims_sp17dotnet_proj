
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
    
public partial class Address
{

    public int Id { get; set; }

    public string FirstLine { get; set; }

    public string SecondLine { get; set; }

    public DataAccessLayer.Constants.US_STATE State { get; set; }

    public string ZipCode { get; set; }



    public virtual Profile BillingInfo { get; set; }

}

}
