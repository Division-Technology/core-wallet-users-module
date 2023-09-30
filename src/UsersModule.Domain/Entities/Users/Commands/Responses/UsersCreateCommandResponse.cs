namespace UsersModule.Domain.Entities.Users.Commands.Responses;

public class UsersCreateCommandResponse
{
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}