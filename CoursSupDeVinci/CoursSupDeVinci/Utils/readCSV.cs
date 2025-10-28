namespace CoursSupDeVinci;

public static class ReadCsv
{
    public static List<string[]> ReadCsvFile(string filePath)
    {
        var rows = new List<string[]>();
        var lines = File.ReadAllLines(filePath);
        if (lines.Length <= 1) return rows;

        for (var i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            rows.Add(values);
        }

        return rows;
    }
}