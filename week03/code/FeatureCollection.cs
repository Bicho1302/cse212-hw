public class FeatureCollection
{
    public Feature[] Features { get; set; } = Array.Empty<Feature>();
}

public class Feature
{
    public FeatureProperties Properties { get; set; } = new FeatureProperties();
}

public class FeatureProperties
{
    public string? Place { get; set; }
    public double? Mag { get; set; }
}
