using CoursSupDeVinci;

public static class CsvHandler
{
    public static List<Person> GetPeopleFromCsv(string csvFilePath)
    {
        var csvData = ReadCsv.ReadCsvFile(csvFilePath);
        List<Person> personnes = new();

        foreach (var row in csvData)
        {
            if (row.Length < 5) continue;
            if (!DateTime.TryParse(row[3], out var birthdate)) continue;

            Person person = new()
            {
                Lastname = row[1],
                Firstname = row[2],
                Birthdate = birthdate,
                AdressDetails = DetailHelper.ParseDetails(row[4]),
                Height = int.Parse(row[5])
            };

            personnes.Add(person);
        }

        return personnes;
    }
}