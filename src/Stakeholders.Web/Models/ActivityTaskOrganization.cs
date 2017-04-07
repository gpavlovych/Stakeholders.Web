namespace Stakeholders.Web.Models
{
    public class ActivityTaskOrganization
    {
        public long TaskId { get; set; }

        public ActivityTask Task { get; set; }
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}