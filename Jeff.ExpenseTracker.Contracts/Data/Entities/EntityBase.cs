using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jeff.ExpenseTracker.Contracts.Data.Entities
{
    public class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UpdatedBy { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime UpdatedOn { get; set; }
    }
}
