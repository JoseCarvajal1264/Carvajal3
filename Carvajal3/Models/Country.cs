namespace Carvajal3.Models
{
    public class Country
    {
        public name name { get; set; }
        public string region { get; set; }
        public maps maps { get; set; }
    }

    public class name
    {
        public string Common { get; set; }
    }

    public class maps
    {
        public string GoogleMaps { get; set; }
    }
}
