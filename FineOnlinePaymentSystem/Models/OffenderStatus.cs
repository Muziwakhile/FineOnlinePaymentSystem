using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FineOnlinePaymentSystem.Models
{
    public class OffenderStatus
    {
        [Key]
        public int StatusID { get; set; }

        [Required, Column(TypeName = "varchar(25)")]
        public string Name { get; set; }
    }
}