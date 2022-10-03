namespace GeoServices.Model;
public class ItineraryRequest
{
    public string resource { get; set; } = "bdtopo-osrm"; // bdtopo-pgr but removes getSteps
    public string start { get; set; }
    public string end { get; set; }
    public string profile { get; set; } = "car";

    public string optimization { get; set; } = "fastest";

    //public bool getSteps { get; set; } = true;
    //public bool getBbox { get; set; } = true;
    public string distanceUnit { get; set; } = "kilometer";
    public string timeUnit { get; set; } = "hour";
}