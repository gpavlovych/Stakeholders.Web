namespace Stakeholders.Web.Models
{
    public class ActivityObserverUserCompany : BaseEntity
    {
        public Activity Activity { get; set; }

        public ApplicationUser User { get; set; }

        public Company Company { get; set; }
    }
}