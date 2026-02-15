using AutoMapper;
using DTOs;
using Entities;

namespace Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // ================= Users =================
            CreateMap<User, UserProfileDTO>();
            CreateMap<UserRegisterDTO, User>();

            // ================= Products =================
            CreateMap<Product, ProductSummaryDTO>();
            CreateMap<Product, ProductDetailsDTO>();
            CreateMap<ProductDetailsDTO, Product>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
            CreateMap<Product, ProductViewDTO>();

            // ================= Product Images =================
            CreateMap<ProductImage, ProductImageDTO>();
            CreateMap<ProductImageDTO, ProductImage>();
            CreateMap<ProductImageCreateDTO, ProductImage>();
            CreateMap<ProductImageUpdateDTO, ProductImage>();
            CreateMap<ProductImage, ProductImageUrlDTO>();
            CreateMap<ProductImageUrlDTO, ProductImage>();

            // ================= Categories =================
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            // ================= Orders =================
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<Order, OrderHistoryDTO>();
            CreateMap<Order, OrderHistoryAdminDTO>();

            // ================= Order Items =================
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderItem, OrderItemViewDTO>();
        }
    }
}
