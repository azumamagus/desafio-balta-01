using Balta.IBGE.Domain.Accounts.ValueObjects;

using Balta.IBGE.Domain.Core.Entities;

namespace Balta.IBGE.Domain.Accounts.Entities;

public class User : Entity
{
    public User(Email email, Password password)
    {
        Email = email;
        Password = password;
    }

    protected User()
    { }

    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
}