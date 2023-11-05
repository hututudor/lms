using LMS.Domain.Common;

namespace LMS.Domain.Entities;

public class User: AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }    
    public string Email { get; private set; }
    public string Password { get; private set; }
    
    private User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }

    public static Result<User> Create(string name, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<User>.Failure("name is required");
        }        
        
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<User>.Failure("email is required");
        }
        
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result<User>.Failure("password is required");
        }
        
        return Result<User>.Success(new User(name, email, password));
    }
}