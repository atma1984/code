
using System.ComponentModel.DataAnnotations; // [Required], [StringLength]
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // [Column]
namespace Packt.Shared;
public class Customer
{
    public string CustomerId { get; set; }
    [Required]
    [StringLength(40)]
    public string CompanyName { get; set; } = null!;
    public string City { get; set; } = null!;
}
