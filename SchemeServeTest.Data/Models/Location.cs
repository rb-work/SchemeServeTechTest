using System.ComponentModel.DataAnnotations.Schema;

namespace SchemeServeTest.Data.Models
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LocationId { get; set; }
        public int ProviderId { get; set; }
    }
}
