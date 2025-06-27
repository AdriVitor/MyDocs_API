using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyDocs.Models
{
    [Table("ALERT")]
    public class Alert
    {
        [Key]
        [Column("ID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [ForeignKey("USER")]
        [Column("ID_USER", TypeName = "int")]
        [Required]
        public int IdUser { get; set; }
        [Column("NAME", TypeName = "varchar(70)")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres atingido")]
        [MinLength(2, ErrorMessage = "Preencha no mínimo 2 caracteres no nome do alerta")]
        [Required]
        public string Name { get; set; }
        [Column("DESCRIPTION", TypeName = "varchar(200)")]
        public string Description { get; set; }
        [Column("NR_RECURRENCE", TypeName = "int")]
        [Required]
        public int RecurrenceOfSending { get; set; }
        [Column("CREATION_DATE", TypeName = "date")]
        [Required]
        public DateTime CreationDate { get; set; }
        [Column("END_DATE", TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [Column("FIRST_DAY_SEND", TypeName = "date")]
        public DateTime FirstDaySend { get; set; }
        [Column("JOB_ID", TypeName = "varchar(100)")]
        public string JobId { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}
