namespace GeoServices.Model;

public class Feature
{
    public string type { get; set; }
    public AddressGeometry geometry { get; set; }
    public Properties properties { get; set; }
}

public class AddressGeometry
{
    public string type { get; set; }
    public List<double> coordinates { get; set; }
}

public class Properties
{
    public string label { get; set; }
    public double score { get; set; }
    public string housenumber { get; set; }
    public string id { get; set; }
    /// <summary>
    /// Type Of Result
    /// housenumber
    /// street
    /// locality
    /// municipality
    /// </summary>
    public string type { get; set; }
    public string name { get; set; }
    public string postcode { get; set; }
    public string citycode { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public string city { get; set; }
    public string context { get; set; }
    public double importance { get; set; }
    public string street { get; set; }
}

public class GeoAddress
{
    public string type { get; set; }
    public string version { get; set; }
    public List<Feature> features { get; set; }
    public string attribution { get; set; }
    public string licence { get; set; }
    public string query { get; set; }
    public int limit { get; set; }
}