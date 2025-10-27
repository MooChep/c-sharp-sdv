namespace CoursSupDeVinci;

public class Detail
{
    private string city;
    private string street;

    public Detail(string street, int zipCode, string city)
    {
        this.street = street;
        ZipCode = zipCode;
        this.city = city;
    }

    public string Street
    {
        get => street;
        set => street = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ZipCode { get; set; }

    public string City
    {
        get => city;
        set => city = value ?? throw new ArgumentNullException(nameof(value));
    }
}