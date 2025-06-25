using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyDocs.Models
{
    [Table("DOCUMENT")]
    public class Document
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
        [Column("FILE_NAME", TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres atingido")]
        [MinLength(2, ErrorMessage = "Preencha no mínimo 2 caracteres no nome do documento")]
        [Required]
        public string FileName { get; set; }
        [Column("UNIQUE_FILE_NAME", TypeName = "varchar(100)")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 100 caracteres atingido")]
        [MinLength(5, ErrorMessage = "Preencha no mínimo 2 caracteres no nome do documento")]
        [Required]
        public string UniqueFileName { get; set; }
        [Column("FILE_TYPE", TypeName = "varchar(50)")]
        [Required]
        public string FileType { get; set; }
        [Column("FILE_SIZE", TypeName = "BIGINT")]
        [Required]
        public long FileSize { get; set; }
        //public string? DocumentTypeId { get; set; }
        [Column("UPLOAD_DATE", TypeName = "DATETIME")]
        [Required]
        public DateTime UploadDate { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}
