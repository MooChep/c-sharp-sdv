namespace CoursSupDeVinci;

public static class DetailHelper
{
    public static List<Detail> ParseDetails(string input)
    {
        // On vérifie que l'entrée n'est pas vide
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("L'adresse ne peut pas être vide.", nameof(input));

        // On découpe les éléments : Rue ; Code Postal ; Ville
        var parts = input.Split(';');

        var street = parts[0].Trim();
        int zipCode;
        var city = parts[2].Trim();

        // On tente de convertir le code postal en entier
        if (!int.TryParse(parts[1].Trim(), out zipCode))
            // Sinon on ecrit un message d'erreur
            throw new FormatException($"Code postal invalide : {parts[1]}");

        // Création et retour d'une liste contenant un objet Detail
        return new List<Detail> { new(street, zipCode, city) };
    }
}