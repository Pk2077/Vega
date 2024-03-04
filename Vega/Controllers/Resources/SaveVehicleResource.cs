using System.Collections.ObjectModel;

namespace Vega.Controllers.Resources
{
    public partial class VehicleResource
    {
        public class SaveVehicleResource
        {
            public int Id { get; set; }
            public int ModelId { get; set; }
            public bool IsRegistered { get; set; }
            public ICollection<int> Features { get; set; }
            public ContactResource Contact { get; set; }
            public DateTime LastUpdate { get; set; }
            public SaveVehicleResource()
            {
                Features = new Collection<int>();
            }
        }
    }
}
