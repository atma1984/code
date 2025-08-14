
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // [Column]
namespace Packt.Shared;
public class Category
{
    // эти свойства сопоставляются со столбцами в базе данных
    public int CategoryId { get; set; }
    [Required]
    [StringLength(15)]
    public string? CategoryName { get; set; } = null!;
    [Column(TypeName = "ntext")]
    public string? Description { get; set; }
}