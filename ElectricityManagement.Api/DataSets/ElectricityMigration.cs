using ElectricityManagement.Domain.ElectricityDataEntity;
using ElectricityManagement.Infrastructure.Data.DbContext;
using LINQtoCSV;

namespace ElectricityManagement.Api.DataSets;

public class ElectricityMigration
{
    public static void Migration(IServiceCollection services, string contentRootPath)
    {
        var buildServiceProvider = services.BuildServiceProvider();
        if ((buildServiceProvider.GetService(typeof(ElectricityManagementDbContext)) is ElectricityManagementDbContext electricityManagementDbContext))
        {
            if (electricityManagementDbContext.ElectricityDatas.Count() == 0)
            {
                contentRootPath = contentRootPath + "DataSets";
                var fileDirectory = new DirectoryInfo(contentRootPath);
                FileInfo[] Files = fileDirectory.GetFiles("*.csv");
                var electricityDtos = new List<ElectricityDto>();
                foreach (FileInfo file in Files)
                {
                    electricityDtos.AddRange(ReadCsvFile(file.FullName));
                }

                var electricityDataList = electricityDtos.GroupBy(x => x.Network).ToList();

                foreach (var electricityDataObjects in electricityDataList)
                {
                    foreach (var electricityData in electricityDataObjects)
                    {
                        if (electricityData.ObtTitle.Equals("Butas"))
                        {
                            ElectricityData electricityDataObject = new ElectricityData()
                            {
                                Network = electricityData.Network,
                                ObtTitle = electricityData.ObtTitle,
                                ObjGvType = electricityData.ObjGvType,
                                ObjNumbers = electricityData.ObjNumbers,
                                Pplus = electricityData.Pplus,
                                Plt = electricityData.Plt,
                                Pminus = electricityData.Pminus,
                            };

                            electricityManagementDbContext.ElectricityDatas.Add(electricityDataObject);
                        }
                    }
                }
                electricityManagementDbContext.SaveChanges();
            }
        }
    }
    private static List<ElectricityDto> ReadCsvFile(string filePath)
    {
        var csvFileDescription = new CsvFileDescription
        {
            FirstLineHasColumnNames = true,
            IgnoreTrailingSeparatorChar = true,
            SeparatorChar = ',',
            UseFieldIndexForReadingData = false,
        };

        var csvContext = new CsvContext();
        var electricityDtos = csvContext.Read<ElectricityDto>(filePath, csvFileDescription).ToList();

        return electricityDtos;
    }
}

[Serializable]
public record ElectricityDto
{
    [CsvColumn(Name = "TINKLAS", FieldIndex = 1)]
    public string Network { get; set; }

    [CsvColumn(Name = "OBT_PAVADINIMAS", FieldIndex = 2)]
    public string ObtTitle { get; set; }

    [CsvColumn(Name = "OBJ_GV_TIPAS", FieldIndex = 3)]
    public string ObjGvType { get; set; }

    [CsvColumn(Name = "OBJ_NUMERIS", FieldIndex = 4)]
    public string ObjNumbers { get; set; }

    [CsvColumn(Name = "P+", FieldIndex = 5)]
    public double Pplus { get; set; }

    [CsvColumn(Name = "PL_T", FieldIndex = 6, OutputFormat = "dd-MM-yyyy HH:mm:ss")]
    public DateTime Plt { get; set; }

    [CsvColumn(Name = "P-", FieldIndex = 7)]
    public double Pminus { get; set; }
}
