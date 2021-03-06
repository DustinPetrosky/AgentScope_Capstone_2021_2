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
    
    public partial class AgentAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgentAccount()
        {
            this.AgentHomeSolds = new HashSet<AgentHomeSold>();
            this.AgentReviews = new HashSet<AgentReview>();
        }
    
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneOffice { get; set; }
        public string Company { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string SuiteNumber { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string ZipCode { get; set; }
        public string AboutMeText { get; set; }
        public short YearsOfExp { get; set; }
        public string WebsiteLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }
        public string RealEstateLicense { get; set; }
        public string LicensedState { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentHomeSold> AgentHomeSolds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentReview> AgentReviews { get; set; }
    }
}
