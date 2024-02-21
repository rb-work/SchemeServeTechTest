using System.ComponentModel.DataAnnotations.Schema;

namespace SchemeServeTest.Data.Models
{
    public class LastInspection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
