using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stakeholders.Web.Models.ActivityTaskViewModels
{
    public class ActivityTaskViewModel
    {
        public DateTime? DateCreated { get; set; }
        public long? GoalId { get; set; }
        public long? AssignToId { get; set; }
        public long[] ContactIds { get; set; }
        public long? CreatedById { get; set; }
        public long[] ObserverUserIds { get; set; }
        public string Subject { get; set; }
        public DateTime DateDeadline { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
        public bool IsImportant { get; set; }
        public long? StatusId { get; set; }
        public string SuccessFactor { get; set; }
    }
}