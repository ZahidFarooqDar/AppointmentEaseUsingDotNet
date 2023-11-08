using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Identity;

namespace EcommereAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
    }
}
