using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotDir = Path.Combine(currentDirectory, "SalesTotalDirectory");
Directory.CreateDirectory(salesTotDir);

var salesFile = FindFiles(storesDirectory);
var salesTotal = CalculateSalesTotal(salesFile);

File.AppendAllText(Path.Combine(salesTotDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

IEnumerable<string> FindFiles(string folder)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);

        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesfiles)
{
    double salesTotal = 0;

    foreach (var file in salesfiles)
    {
        string salesJson = File.ReadAllText(file);

        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        salesTotal += data?.total ?? 0;
    }

    return salesTotal;
}

record SalesData (double total);