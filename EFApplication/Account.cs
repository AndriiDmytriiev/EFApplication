using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    [Column("id")]
    
    public int Id { get; set; }
    [Column("accountnumber")]
    public string AccountNumber { get; set; }
    [Column("balance")]
    public decimal Balance { get; set; }
}
