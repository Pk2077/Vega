namespace Vega.Models
{
    public class PhotoSettings
    {
        public int Max_Bytes { get; set; }
        public string[] AcceptedTypes { get; set; }

        public bool IsSupported(string FileName)
        {
            return AcceptedTypes.Any(s => s == Path.GetExtension(FileName).ToLower());
        }
    }
}
