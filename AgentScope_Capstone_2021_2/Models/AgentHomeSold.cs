//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgentScope_Capstone_2021_2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AgentHomeSold
    {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string ZipCode { get; set; }
        public int Price { get; set; }
        public System.DateTime DateSold { get; set; }
        public string ImageFile { get; set; }
    
        public virtual AgentAccount AgentAccount { get; set; }
    }
}
