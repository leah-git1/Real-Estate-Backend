using AutoMapper;
using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AutoMapping:Profile
    {
        public AutoMapping() 
        {
            CreateMap<User, UserProfileDTO>();
            CreateMap<UserRegisterDTO, User>();

            CreateMap<Product, ProductSummaryDTO>();

            CreateMap<Product, ProductDetailsDTO>();

            CreateMap<ProductCreateDTO, Product>();

            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<ProductImage,ProductImageDTO>();
            CreateMap<ProductImageDTO, ProductImage>();
            CreateMap<ProductImageUpdateDTO, ProductImage>();
            CreateMap<ProductImageCreateDTO, ProductImage>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<CategoryCreateDTO, Category > ();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<Order, OrderHistoryDTO>();
            CreateMap<Order, OrderHistoryAdminDTO>();
        }
    }
}
