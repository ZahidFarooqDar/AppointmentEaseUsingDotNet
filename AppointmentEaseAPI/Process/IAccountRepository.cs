using AppointmentEaseAPI.ServiceModels;
using Microsoft.AspNetCore.Identity;

namespace AppointmentEaseAPI.Process
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
    }
}
