using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChanceGame.Database.Entities;

[Table("Users")]
public class User
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("username")]
    public string Username { get; set; }
    
    [Column("Password")]
    public string Password { get; set; }

    [Column("Balance")] public long Balance { get; set; } = 10000;

}