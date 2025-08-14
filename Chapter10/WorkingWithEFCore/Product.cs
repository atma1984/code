

using System.ComponentModel.DataAnnotations; // [Required], [StringLength]
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // [Column]
namespace Packt.Shared;
public class Product
{
    public int ProductId { get; set; } // первичный ключ
    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!;
    [Column("UnitPrice", TypeName = "money")]
    public decimal? Cost { get; set; } // имя свойства != имя столбца
    [Column("UnitsInStock")]
    public short? Stock { get; set; }
    public bool Discontinued { get; set; }

    // эти два параметра определяют отношение внешнего ключа
    // к таблице Categories
    // 🔑 Внешний ключ
    public int CategoryId { get; set; }
    // 🔄 Навигационное свойство к категории
    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;
}