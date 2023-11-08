using AutoMapper;
using EcommereAPI.DomainModels;
using EcommereAPI.ServiceModels;

namespace EcommereAPI.Helpers
{
    public class EcommerceMappingProfile : Profile
    {
        public EcommerceMappingProfile() 
        {
            CreateMap<UserDM, UserSM>().ReverseMap();
            CreateMap<UserDM, UserDTO>().ReverseMap();
            CreateMap<UserDM, EcommerceUser>().ReverseMap();
            CreateMap<ProductDM, ProductSM>().ReverseMap();
            CreateMap<ProductDM, ProductDTO>().ReverseMap();
            /*   .ForMember(dest => dest.UserProductOrders, opt => opt.Ignore()) // Ignore UserProductOrders mapping
               .ReverseMap();*/
            CreateMap<ProductCategoryDM, ProductCategorySM>().ReverseMap();
            CreateMap<ProductCategoryDTO, ProductCategoryDM>().ReverseMap();
            CreateMap<UserProductOrderDM,UserProductOrderDTO>().ReverseMap();
            CreateMap<AddressDM, AddressSM>().ReverseMap();
            CreateMap<AddressDM, AddressDTO>().ReverseMap();
            CreateMap<OrderDetailDM, OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDM,OrderDTO>().ReverseMap();
            CreateMap<TrackingDetailsDM, TrackingDetailsDTO>().ReverseMap();
            CreateMap<BuyerDM, BuyerDTO>().ReverseMap();
            CreateMap<SellerDM, SellerDTO>().ReverseMap();
            
            

        }
        
    }
}
