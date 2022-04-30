using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonarBd.Core.Domain.BloodDonars
{
    public class BloodDonar: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookURL { get; set; }
        public string Gender { get; set; }//dropdown in frontend
        public string BloodGroup { get; set; }//dropdown in frontend
        public string BloodStatus { get; set; }//dropdown in frontend
        public DateTime BirthDayDate { get; set; }
        public DateTime LastDonationDate { get; set; }
        public string PresentDistrict { get; set; }
        public string PresentCity { get; set; }
        public string PresentZone { get; set; }
        public long TotalBloodDonation { get; set; }
    }
}
