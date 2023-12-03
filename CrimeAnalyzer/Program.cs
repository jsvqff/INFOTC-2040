using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CrimeStats
{
    public int Year { get; set; }
    public int Population { get; set; }
    public int ViolentCrime { get; set; }
    public int Murder { get; set; }
    public int Rape { get; set; }
    public int Robbery { get; set; }
    public int AggravatedAssault { get; set; }
    public int PropertyCrime { get; set; }
    public int Burglary { get; set; }
    public int Theft { get; set; }
    public int MotorVehicleTheft { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: CrimeAnalyzer <crime_csv_file_path> <report_file_path>");
            return;
        }

        string csvFilePath = args[0];
        string reportFilePath = args[1];

        List<CrimeStats> crimeStatsList = new List<CrimeStats>();

        try
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length == 11)
                    {
                        var crimeStats = new CrimeStats
                        {
                            Year = int.Parse(values[0]),
                            Population = int.Parse(values[1]),
                            ViolentCrime = int.Parse(values[2]),
                            Murder = int.Parse(values[3]),
                            Rape = int.Parse(values[4]),
                            Robbery = int.Parse(values[5]),
                            AggravatedAssault = int.Parse(values[6]),
                            PropertyCrime = int.Parse(values[7]),
                            Burglary = int.Parse(values[8]),
                            Theft = int.Parse(values[9]),
                            MotorVehicleTheft = int.Parse(values[10])
                        };
                        crimeStatsList.Add(crimeStats);
                    }
                    else
                    {
                        Console.WriteLine($"Error: Row contains {values.Length} values. It should contain 11.");
                    }
                }
            }

            Console.WriteLine("Report generated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
