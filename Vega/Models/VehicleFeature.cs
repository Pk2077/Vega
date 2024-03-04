using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    public class VehicleFeature
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public int FeatureId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}
