namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Activity task status
    /// </summary>
    public class ActivityTaskStatus
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        /// <value>
        /// The name en.
        /// </value>
        public string NameEn { get; set; }
    }
}