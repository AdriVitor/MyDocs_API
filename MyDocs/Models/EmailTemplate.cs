using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyDocs.Models
{
    [Table("EMAIL_TEMPLATE")]
    public class EmailTemplate
    {
        [Key]
        [Column("ID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("NAME", TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres atingido")]
        [MinLength(2, ErrorMessage = "Preencha no mínimo 2 caracteres no nome do template")]
        [Required]
        public string Name { get; set; }

        [Column("SUBJECT", TypeName = "varchar(150)")]
        [StringLength(150, ErrorMessage = "Tamanho máximo de 150 caracteres atingido")]
        public string Subject { get; set; }

        [Column("BODY", TypeName = "varchar(max)")]
        [Required]
        public string Body { get; set; }
    }
}
