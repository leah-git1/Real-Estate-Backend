using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository _iProductRepository;
        IMapper _mapper;

        public ProductService(IProductRepository iProductRepository, IMapper mapper)
        {
            this._iProductRepository = iProductRepository;
            this._mapper = mapper;
        }

        public async Task<PageResponseDTO<ProductSummaryDTO>> GetProducts(int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds, int position, int skip)
        {
            if (skip <= 0) skip = 10;
            if (position <= 0) position = 1;
            (List<Product>, int) response = await _iProductRepository.GetProducts(categoryIds, city, minPrice, maxPrice, rooms, beds, position, skip);
            List<ProductSummaryDTO> data = _mapper.Map<List<Product>, List<ProductSummaryDTO>>(response.Item1);
            PageResponseDTO<ProductSummaryDTO> pageResponse = new();
            pageResponse.Data = data;
            pageResponse.TotalItems = response.Item2;
            pageResponse.CurrentPage = position;
            pageResponse.PageSize = skip;
            pageResponse.HasPreviousPage = position > 1;
            int numOfPages = pageResponse.TotalItems / skip;
            if (pageResponse.TotalItems % skip != 0)
                numOfPages++;
            pageResponse.HasNextPage = position < numOfPages;
            return pageResponse;
        }

        public async Task<ProductDetailsDTO> GetProductById(int id)
        {
            Product product = await _iProductRepository.GetProductById(id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<Product, ProductDetailsDTO>(product);
        }

        public async Task<List<ProductSummaryDTO>> GetProductsByOwnerId(int ownerId)
        {
            List<Product> ownerProducts = await _iProductRepository.GetProductsByOwnerId(ownerId);
            return _mapper.Map<List<Product>, List<ProductSummaryDTO>>(ownerProducts);
        }

        public async Task<ProductDetailsDTO> AddProduct(ProductCreateDTO productCreateDto)
        {
            Product product = _mapper.Map<ProductCreateDTO, Product>(productCreateDto);
            Product newProduct = await _iProductRepository.AddProduct(product);
            return _mapper.Map<Product, ProductDetailsDTO>(newProduct);
        }

        public async Task<ProductDetailsDTO> UpdateProduct(int id, ProductUpdateDTO productUpdateDto)
        {
            Product productToUpdate = _mapper.Map<ProductUpdateDTO, Product>(productUpdateDto);
            Product updatedProduct = await _iProductRepository.UpdateProduct(id, productToUpdate);
            if (updatedProduct == null)
            {
                return null;
            }
            if (productUpdateDto.CategoryId <= 0)
            {
                return null;
            }
            return _mapper.Map<Product, ProductDetailsDTO>(updatedProduct);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _iProductRepository.DeleteProduct(id);
        }
    }
}