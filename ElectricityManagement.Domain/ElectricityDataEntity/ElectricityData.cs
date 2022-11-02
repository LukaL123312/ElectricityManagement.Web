namespace ElectricityManagement.Domain.ElectricityDataEntity;

public record ElectricityData
{
    public int Id { get; set; }
    public string Network { get; set; }
    public string ObtTitle { get; set; }
    public string ObjGvType { get; set; }
    public string ObjNumbers { get; set; }
    public double Pplus { get; set; }
    public DateTime Plt { get; set; }
    public double Pminus { get; set; }
}
