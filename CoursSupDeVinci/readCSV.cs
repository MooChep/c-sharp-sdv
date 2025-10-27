namespace CoursSupDeVinci;

public class ReadCsv
{
    public static List<string[]> ReadCsvFile(string filePath)
    {
        List<string[]> rows = new List<string[]>();
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length <= 1)
            {
                return rows;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                rows.Add(values);
            }
            return rows;
    }
}