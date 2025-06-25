using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDocs.Models
{
    [Table("USER")]
    public class User
    {
        [Key]
        [Column("ID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Tamanho máximo de 150 caracteres atingido")]
        [MinLength(2, ErrorMessage = "Preencha o nome corretamente")]
        [Column("NAME", TypeName = "varchar(150)")]
        public string Name { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "Tamanho máximo de 14 caracteres atingido")]
        [MinLength(11, ErrorMessage = "Preencha o CPF corretamente")]
        [Column("CPF", TypeName = "varchar(14)")]
        public string CPF { get; set; }
        [Column("DATE_OF_BIRTH", TypeName = "DATE")]
        public DateTime DateOfBirth { get; set; }
        [Column("PHONE", TypeName = "varchar(14)")]
        [StringLength(14, ErrorMessage = "Tamanho máximo de 14 caracteres atingido")]
        public string Phone { get; set; }

        #region Virtual Properties
        public virtual UserCredentials UserCredentials { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        #endregion
    }
}
