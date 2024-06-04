using Microsoft.AspNetCore.Identity;

namespace eSiafApiN4.Servicios;

public class UsuarioStore: IUserStore<IdentityUser> ,IUserEmailStore<IdentityUser>
    ,IUserPasswordStore<IdentityUser>
{
    
    //::Begin IUserStore
    public void Dispose()
    {
    }

    public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(IdentityUser user, string? userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(IdentityUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    //::End IUserStore

    //::Begin IUserEmailStore
    public Task SetEmailAsync(IdentityUser user, string? email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedEmailAsync(IdentityUser user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    //::End IUserEmailStore

    //::Begin IUserPasswordStore
    public Task SetPasswordHashAsync(IdentityUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    //::End IUserPasswordStore

}