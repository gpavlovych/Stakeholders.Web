using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.CompanyViewModels
{
    public class CompanyViewModel: ViewModelBase
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string CompanyCode { get; set; }
        public string Name { get; set; }
        public string Influencing { get; set; }
        public string InfluencedBy { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long[] ObserverActivityIds { get; set; }
        public string LogoUrl { get; set; }
    }
}
