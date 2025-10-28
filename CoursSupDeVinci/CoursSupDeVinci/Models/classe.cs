namespace CoursSupDeVinci;

public class Classe
{
    public string Ecole;
    public List<Person> ListeEtudiants;
    public string Niveau;
    public string Nom;

    public Classe()
    {
        ListeEtudiants = new List<Person>();
    }
}