using System.Collections.ObjectModel;

namespace Vega.Controllers.Resources
{
    public partial class VehicleResource
    {
        public class MakeResources
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<KeyValuePairResource> Models { get; set; }

            public MakeResources()
            {
                Models = new Collection<KeyValuePairResource>();
            }
        }
    }
}
