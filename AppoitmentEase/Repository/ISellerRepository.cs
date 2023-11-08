using EcommereAPI.DomainModels;
using EcommereAPI.ServiceModels;

namespace EcommereAPI.Repository
{
    public interface ISellerRepository
    {
        Task<List<UserSM>> GetAllUsers();
    }
}
