using System.ComponentModel.DataAnnotations;
namespace Tazzker.Client.Data.Models
{
    public class SimpleModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
