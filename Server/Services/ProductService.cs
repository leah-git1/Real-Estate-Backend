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

        public async Task<IEnumerable<ProductSummaryDTO>> getProducts(int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds)
        {
            IEnumerable<Product> products = await _iProductRepository.getProducts(categoryIds, city, minPrice, maxPrice, rooms, beds);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductSummaryDTO>>(products);
        }

        public async Task<ProductDetailsDTO> getProductById(int id)
        {
            Product product = await _iProductRepository.getProductById(id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<Product, ProductDetailsDTO>(product);
        }

        public async Task<IEnumerable<ProductSummaryDTO>> getProductsByOwnerId(int ownerId)
        {
            IEnumerable<Product> ownerProducts = await _iProductRepository.getProductsByOwnerId(ownerId);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductSummaryDTO>>(ownerProducts);
        }

        public async Task<ProductDetailsDTO> addProduct(ProductCreateDTO productCreateDto)
        {
            Product product = _mapper.Map<ProductCreateDTO, Product>(productCreateDto);
            Product newProduct = await _iProductRepository.addProduct(product);
            return _mapper.Map<Product, ProductDetailsDTO>(newProduct);
        }

        public async Task<ProductDetailsDTO> updateProduct(int id, ProductUpdateDTO productUpdateDto)
        {
            Product productToUpdate = _mapper.Map<ProductUpdateDTO, Product>(productUpdateDto);
            Product updatedProduct = await _iProductRepository.updateProduct(id, productToUpdate);
            if (updatedProduct == null)
            {
                return null;
            }
            return _mapper.Map<Product, ProductDetailsDTO>(updatedProduct);
        }

        public async Task<bool> deleteProduct(int id)
        {
            return await _iProductRepository.deleteProduct(id);
        }
    }
}