using HrHarmony.Data.Models.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Database.SeedData.StartDataSeeders.Dictionary;

public class AddressDataSeeder : ISeeder
{
    public int Order => 5;

    private readonly List<Address> _addresses = new()
    {
        new Address { Id = 1, Street = "ul. Marszałkowska", City = "Warszawa", PostalCode = "00-001" },
        new Address { Id = 2, Street = "ul. Długa", City = "Gdańsk", PostalCode = "80-001" },
        new Address { Id = 3, Street = "ul. Floriańska", City = "Kraków", PostalCode = "30-001" },
        new Address { Id = 4, Street = "ul. Wojska Polskiego", City = "Poznań", PostalCode = "60-001" },
        new Address { Id = 5, Street = "ul. Piłsudskiego", City = "Lublin", PostalCode = "20-001" },
        new Address { Id = 6, Street = "ul. 3 Maja", City = "Katowice", PostalCode = "40-001" },
        new Address { Id = 7, Street = "ul. Mickiewicza", City = "Łódź", PostalCode = "90-001" },
        new Address { Id = 8, Street = "ul. Zamkowa", City = "Wrocław", PostalCode = "50-001" },
        new Address { Id = 9, Street = "ul. Słowackiego", City = "Szczecin", PostalCode = "70-001" },
        new Address { Id = 10, Street = "ul. Kościuszki", City = "Bydgoszcz", PostalCode = "85-001" },
        new Address { Id = 11, Street = "ul. Jana III Sobieskiego", City = "Gdynia", PostalCode = "81-001" },
        new Address { Id = 12, Street = "ul. Świętojańska", City = "Częstochowa", PostalCode = "42-001" },
        new Address { Id = 13, Street = "ul. Kopernika", City = "Radom", PostalCode = "26-001" },
        new Address { Id = 14, Street = "ul. 1 Maja", City = "Kielce", PostalCode = "25-001" },
        new Address { Id = 15, Street = "ul. Sienkiewicza", City = "Olsztyn", PostalCode = "10-001" },
        new Address { Id = 16, Street = "ul. Mickiewicza", City = "Rzeszów", PostalCode = "35-001" },
        new Address { Id = 17, Street = "ul. Piastowska", City = "Białystok", PostalCode = "15-001" },
        new Address { Id = 18, Street = "ul. 11 Listopada", City = "Opole", PostalCode = "45-001" },
        new Address { Id = 19, Street = "ul. Malczewskiego", City = "Gorzów Wielkopolski", PostalCode = "66-001" },
        new Address { Id = 20, Street = "ul. Wyzwolenia", City = "Tarnów", PostalCode = "33-001" }
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var address in _addresses)
            modelBuilder.Entity<Address>().HasData(address);
    }
}