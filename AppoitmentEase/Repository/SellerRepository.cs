using AutoMapper;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommereAPI.Repository
{
    public class SellerRepository : ISellerRepository
    {
        private readonly UserManager<EcommerceUser> _userManager;
        private readonly ProjectEcommerceContext _projectEcommerceContext;
        private readonly SignInManager<EcommerceUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SellerRepository(
            UserManager<EcommerceUser> userManager,
            SignInManager<EcommerceUser> signInManager,
            IConfiguration configuration,
            ProjectEcommerceContext projectEcommerceContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _projectEcommerceContext = projectEcommerceContext;
            _mapper = mapper;
        }
        public async Task<List<UserSM>> GetAllUsers()
        {
            var res = _projectEcommerceContext.UserDM.ToList();
            
            return _mapper.Map<List<UserSM>>(res);
        }
    }
}
