using CoursSupDeVinci;

public static class ClasseHelper
{
    public static (string Ecole, string Niveau, string Nom) GetClasseInfo()
    {
        Console.WriteLine("La classe fait partie de quelle école ?");
        string? ecole = Console.ReadLine();
        Console.WriteLine("Quel est le niveau d'étude de cette classe ?");
        string? niveau = Console.ReadLine();
        Console.WriteLine("Donnez un nom à la classe ?");
        string? nom = Console.ReadLine();

        return (ecole ?? "", niveau ?? "", nom ?? "");
    }

    public static Classe CreateClasse(List<Person> personnes, (string Ecole, string Niveau, string Nom) info)
    {
        return new Classe
        {
            ListeEtudiants = personnes,
            Ecole = info.Ecole,
            Niveau = info.Niveau,
            Nom = info.Nom
        };
    }
}