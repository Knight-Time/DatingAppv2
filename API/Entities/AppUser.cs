using API.Extensions;

namespace API.Entities;


public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];

    public required DateOnly DateOfBirth { get; set; }

    public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public required string Alias { get; set; }

    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    

    public string? Gender { get; set; }

    public string? Introduction { get; set; }

    public string? Interests { get; set; }

    public string? LookingFor { get; set; }

    public required string City { get; set; }

    public required string Country { get; set; }

    public List<Photo> Photos { get; set; }


    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }
    
        
    




}


