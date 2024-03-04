using System.Collections.ObjectModel;

namespace Vega.Controllers.Resources
{
    public partial class VehicleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public MakeResources Make { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }

        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }

        public VehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}
