using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactEmail { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }
}
