using CoursSupDeVinci;

public class Person
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime Birthdate { get; set; }

    public int Height { get; set; }
    public required List<Detail> AdressDetails { get; set; }

    public int GetYearsOld()
    {
        DateTime today = DateTime.Today;

        int years = today.Year - Birthdate.Year;

        if (today.Month < Birthdate.Month || (today.Month == Birthdate.Month && today.Day < Birthdate.Day)) years--;

        return years;
    }
}