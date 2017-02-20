using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stakeholders.Web.Models.ActivityTypeViewModels
{
    public class ActivityTypeViewModel
    {
        public string Name { get; set; }
    }
    public class ActivityTypeInfoViewModel: ActivityTypeViewModel
    {
        public long Id { get; set; }
    }
}
