using System.IO;
using System.Collections.Generic;

var salesFile = FindFiles("stores");

foreach (var file in salesFile)
{
    Console.WriteLine(file);
}

IEnumerable<string> FindFiles(string folder)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}