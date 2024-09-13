namespace SampleProto.Core.Models;

public class Person
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Address { get; private set; }
    public string ZipCode { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }

    public Person(string name, string surname, string address, string zipCode, string state, string country, string email, DateTime birthDate)
    {
        Name = name;
        Surname = surname;
        Address = address;
        ZipCode = zipCode;
        State = state;
        Country = country;
        Email = email;
        BirthDate = birthDate;
    }
}
