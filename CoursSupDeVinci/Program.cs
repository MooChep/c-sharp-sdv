using CoursSupDeVinci;

public class Program
{
    private static void Main()
    {
        #region récupération du csv et création de la liste de personnes

        string csvFilePath = @"C:\Users\ilanh\Downloads\CoursSupDeVinci_C#.csv";
        List<string[]> csvData = ReadCsv.ReadCsvFile(csvFilePath);

        List<Person> personnes = new List<Person>();

        for (int i = 0; i < csvData.Count; i++)
        {
            string[] row = csvData[i];

            if (row.Length < 5) continue;
            // Tente de convertir la valeur en DateTime
            // Si la conversion échoue on passe à la personne suivante
            // Si la conversion réussit, on renvoie birthdate.
            if (!DateTime.TryParse(row[3], out DateTime birthdate)) continue;

                Person person = new Person
                {
                    Lastname = row[1],
                    Firstname = row[2],
                    Birthdate = birthdate,
                    AdressDetails = DetailHelper.ParseDetails(row[4]),
                    Height = int.Parse(row[5])
                };
                // Ajoute la personne créée à la liste de personnes.
                personnes.Add(person);
        }

        #endregion

        #region Récupération des informations de la classe.
        // L'utilisateur renseigne les informations relatives à la classe.
        Console.WriteLine("La classe fait partie de quelle école ?");
        string ecoleInput = Console.ReadLine();        
        Console.WriteLine("Quel est le niveau d'étude de cette classe ?");
        string niveauInput = Console.ReadLine();        
        Console.WriteLine("Donnez un nom à la classe ?");
        string nomInput = Console.ReadLine();

        #endregion
        #region Affectation de la liste de personnes à la classe.

        Classe nouvelleClasse = new Classe
        {
            ListeEtudiants = personnes,
            Ecole = ecoleInput,
            Niveau = niveauInput,
            Nom = nomInput
        };
        #endregion

        #region Calcul et écriture des élèves les plus grands de la classe.
        
        // Taille moyenne de la classe.
        double averageHeight = nouvelleClasse.ListeEtudiants.Average(p => p.Height);
        
        // Les étudiants plus grands que la moyenne de taille de la classe.
        IEnumerable<Person> tallerStudents = nouvelleClasse.ListeEtudiants.Where(p => p.Height > averageHeight);
        
        // Parmis ces étudiants on garde ceux qui habitent à Nantes.
        tallerStudents = tallerStudents.Where(p => p.AdressDetails[0].City == "Nantes");
        // Même liste mais triée par ordre décroissant (du plus grand au plus petit).
        tallerStudents = tallerStudents.OrderByDescending(person => person.Height);
        
        int counter = 0;
        Console.WriteLine($"Voici la liste des étudiants de la classe {nouvelleClasse.Nom} triée du plus grand au plus petit.");
        foreach (Person student in tallerStudents)
        {
            counter++;
            Console.WriteLine($"{counter} - {student.Firstname} - {student.Height}");
        }
        #endregion
        
    }
}
