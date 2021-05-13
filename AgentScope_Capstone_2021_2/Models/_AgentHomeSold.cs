using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgentScope_Capstone_2021_2.Models {

    [MetadataType(typeof(agentHomeSold_MetaData))]
    public partial class AgentHomeSold {
    }

    public class agentHomeSold_MetaData {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string ZipCode { get; set; }
        public int Price { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime DateSold { get; set; }
        public string ImageFile { get; set; }
    }
}