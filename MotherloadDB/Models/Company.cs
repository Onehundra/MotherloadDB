using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotherloadDB.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string OrgNumber { get; set; }
        public string?Name { get; set; }
        public string?WebSite { get; set; }
        public string?City { get; set; }
        public string?Industry { get; set; }
        public int?AmountOfEmployees { get; set; }
        public static int?NumberOfLikes { get; set; }
        public List<TechTags>?ListOfTags { get; set; }
        public bool?IsBankrupt { get; set; }
    }

    public enum TechTags
    {
        Cloud,
        DevOps,
        NET,
        Java,
        Javascript
    }
}
