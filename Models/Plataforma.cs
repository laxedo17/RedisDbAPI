using System.ComponentModel.DataAnnotations;

namespace RedisDbAPI.Models
{
    public class Plataforma
    {
        [Required]
        public string Id { get; set; } = $"plataforma:{Guid.NewGuid().ToString()}";
        [Required]
        public string Nome { get; set; } = String.Empty;
    }
}
