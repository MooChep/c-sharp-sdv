using CoursSupDeVinci;
using Microsoft.Extensions.Configuration;

using Npgsql;

public static class DatabaseHandler
{
    public static void AskForDatabaseInsert(Classe nouvelleClasse, List<Person> personnes)
    {
        Console.WriteLine("\nSouhaitez-vous insérer les données du CSV dans la base PostgreSQL ? (O/N)");
        string choix = Console.ReadLine()?.Trim().ToUpper() ?? "N";

        if (choix != "O")
        {
            Console.WriteLine("Insertion annulée, aucune donnée n'a été ajoutée à la base.");
            return;
        }
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile(@"C:\Users\ilanh\RiderProjects\TP1-SDV\CoursSupDeVinci\CoursSupDeVinci\appsettings.json")
            .AddEnvironmentVariables()
            .Build();
 
        string? connectionString = config.GetConnectionString("Default");

        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        
        string insertClasseQuery = @"
            INSERT INTO ""Classe"" (nom, niveau, ecole)
            VALUES (@nom, @niveau, @ecole)
            ON CONFLICT DO NOTHING
            RETURNING id;
        ";
        Guid? idClasse;
        using (NpgsqlCommand cmdClasse = new(insertClasseQuery, connection))
        {
            cmdClasse.Parameters.AddWithValue("nom", (object?)nouvelleClasse.Nom ?? DBNull.Value);
            cmdClasse.Parameters.AddWithValue("niveau", (object?)nouvelleClasse.Niveau ?? DBNull.Value);
            cmdClasse.Parameters.AddWithValue("ecole", (object?)nouvelleClasse.Ecole ?? DBNull.Value);
            cmdClasse.ExecuteNonQuery();
            object? existingId = cmdClasse.ExecuteScalar();
            idClasse = existingId != null ? (Guid?)existingId : null;
        }

        foreach (Person person in personnes)
        {
            string insertPersonQuery = @"
                INSERT INTO ""Person"" (birthdate, firstname, lastname, size, classe_id)
                VALUES (@birthdate, @firstname, @lastname, @size, @class_id)
                ON CONFLICT DO NOTHING
                RETURNING id;
            ";
            Guid? personId;
            using (NpgsqlCommand cmdPerson = new(insertPersonQuery, connection))
            {   
                cmdPerson.Parameters.AddWithValue("birthdate", (object?)person.Birthdate ?? DBNull.Value);
                cmdPerson.Parameters.AddWithValue("firstname", person.Firstname);
                cmdPerson.Parameters.AddWithValue("lastname", person.Lastname);
                cmdPerson.Parameters.AddWithValue("size", person.Height);
                cmdPerson.Parameters.AddWithValue("class_id", (object?)idClasse ?? DBNull.Value);
                cmdPerson.ExecuteNonQuery();
                object? existingId = cmdPerson.ExecuteScalar();
                personId = existingId != null ? (Guid?)existingId : null;
            }


            if (person.AdressDetails.Count > 0)
            {
                var adressDetail = person.AdressDetails[0];
                
                string insertDetailQuery = @"
                    INSERT INTO ""Detail"" (person_id, street, zipcode, city)
                    VALUES (@person_id, @street, @zipcode, @city)
                    ON CONFLICT DO NOTHING;
                ";

                using (NpgsqlCommand cmdDetail = new(insertDetailQuery, connection))
                {
                    cmdDetail.Parameters.AddWithValue("person_id", (object?)personId ?? DBNull.Value);
                    cmdDetail.Parameters.AddWithValue("street", (object?)adressDetail.Street ?? DBNull.Value);
                    cmdDetail.Parameters.AddWithValue("zipcode", (object?)adressDetail.ZipCode ?? DBNull.Value);
                    cmdDetail.Parameters.AddWithValue("city", (object?)adressDetail.City ?? DBNull.Value);
                    cmdDetail.ExecuteNonQuery();
                }
            }
        }

        Console.WriteLine("Données insérées avec succès dans la base de données !");
    }
}