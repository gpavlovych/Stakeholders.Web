
namespace Stakeholders.Web.Models
{
    public class ActivityTaskContact : BaseEntity
    {
        public ActivityTask Task { get; set; }
        public Contact Contact { get; set; }
    }
}