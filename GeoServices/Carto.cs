namespace GeoServices
{
    public class Carto
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

    public class Attributes
    {
        public Name name { get; set; }
    }

    public class Geometry
    {
        public List<List<double>> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Instruction
    {
        public string type { get; set; }
        public string modifier { get; set; }

        public override string ToString()
        {
            return $"{nameof(type)}: {type}, {nameof(modifier)}: {modifier}";
        }
    }

    public class Name
    {
        public string nom_1_gauche { get; set; }
        public string nom_1_droite { get; set; }
        public string cpx_numero { get; set; }
        public string cpx_toponyme { get; set; }

        public override string ToString()
        {
            return $"{nameof(nom_1_gauche)}: {nom_1_gauche}, {nameof(nom_1_droite)}: {nom_1_droite}, {nameof(cpx_numero)}: {cpx_numero}, {nameof(cpx_toponyme)}: {cpx_toponyme}";
        }
    }

    public class Portion
    {
        public string start { get; set; }
        public string end { get; set; }
        public double distance { get; set; }
        public double duration { get; set; }
        public List<double> bbox { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Root
    {
        public string resource { get; set; }
        public string resourceVersion { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string profile { get; set; }
        public string optimization { get; set; }
        public Geometry geometry { get; set; }
        public string crs { get; set; }
        public string distanceUnit { get; set; }
        public string timeUnit { get; set; }
        public List<double> bbox { get; set; }
        public double distance { get; set; }
        public double duration { get; set; }
        public List<object> constraints { get; set; }
        public List<Portion> portions { get; set; }
    }

    public class Step
    {
        public Geometry geometry { get; set; }
        public Attributes attributes { get; set; }
        public double distance { get; set; }
        public double duration { get; set; }
        public Instruction instruction { get; set; }
    }
}
