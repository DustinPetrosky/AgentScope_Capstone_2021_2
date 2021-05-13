using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgentScope_Capstone_2021_2.Models {
    [MetadataType(typeof(AgentAccount_Metadata))]
    public partial class AgentAccount {
        public double? avgRating() {
            if (AgentReviews.Any()) {
                return AgentReviews.Average(r => r.Rating);
            }
            return null;
        }
    }

    public class AgentAccount_Metadata {
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
    }
}