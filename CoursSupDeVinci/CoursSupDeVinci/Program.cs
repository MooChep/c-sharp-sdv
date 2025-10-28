
public class Program
{
    private static void Main()
    {
        string csvFilePath =
            @"C:\Users\ilanh\RiderProjects\TP1-SDV\CoursSupDeVinci\CoursSupDeVinci\Data\CoursSupDeVinci_Csharp.csv";

        List<Person> personnes = CsvHandler.GetPeopleFromCsv(csvFilePath);
        var classeInfo = ClasseHelper.GetClasseInfo();
        var nouvelleClasse = ClasseHelper.CreateClasse(personnes, classeInfo);

        DatabaseHandler.AskForDatabaseInsert(nouvelleClasse, personnes);
        DisplayHelper.DisplayTallStudents(nouvelleClasse);
    }
}