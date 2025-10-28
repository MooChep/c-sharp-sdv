using CoursSupDeVinci;

public static class DisplayHelper
{
    public static void DisplayTallStudents(Classe nouvelleClasse)
    {
        var averageHeight = nouvelleClasse.ListeEtudiants.Average(p => p.Height);

        var tallerStudents = nouvelleClasse.ListeEtudiants
            .Where(p => p.Height > averageHeight && p.AdressDetails[0].City == "Nantes")
            .OrderByDescending(p => p.Height)
            .ToList();

        Console.WriteLine(
            $"Voici la liste des étudiants de la classe {nouvelleClasse.Nom} triée du plus grand au plus petit.");

        var counter = 1;
        foreach (var student in tallerStudents)
            Console.WriteLine($"{counter++} - {student.Firstname} - {student.Height}");
    }
}