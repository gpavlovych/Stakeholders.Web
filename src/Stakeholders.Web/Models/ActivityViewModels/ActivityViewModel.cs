using System;
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.ActivityViewModels
{
    public class ActivityViewModel: ViewModelBase
    {
        public long? TaskId { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public DateTime? DateActivity { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Description { get; set; }
        public long[] ObserverCompanyIds { get; set; }
        public long[] ObserverUserIds { get; set; }
        public string Subject { get; set; }
        public long? TypeId { get; set; }
        public long? UserId { get; set; }
    }
}