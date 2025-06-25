using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDocs.Models
{
    [Table("USER_CREDENTIAL")]
    public class UserCredentials
    {
        [Key]
        [Required]
        [ForeignKey("User")]
        [Column("ID_USER", TypeName = "int")]
        public int IdUser { get; set; }
        [Required]
        [Column("EMAIL", TypeName = "varchar(200)")]
        public string Email { get; set; }
        [Required]
        [Column("PASSWORD", TypeName = "varchar(50)")]
        public string Password { get; set; }

        #region Virtual Properties
        public virtual User User { get; set; }
        #endregion
    }
}
