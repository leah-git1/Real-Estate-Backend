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
    public class ProductImageService : IProductImageService
    {
        IProductImageRepository _iProductImageRepository;
        IMapper _mapper;
        public ProductImageService(IProductImageRepository iProductImageRepository, IMapper mapper)
        {
            this._iProductImageRepository = iProductImageRepository;
            this._mapper = mapper;
        }

        public async Task<ProductImageDTO> getProductImageById(int id)
        {
            ProductImage productImage = await _iProductImageRepository.getProductImageById(id);
            ProductImageDTO imageDTO = _mapper.Map<ProductImage, ProductImageDTO>(productImage);
            return imageDTO;
        }

        public async Task<List<ProductImageDTO>> GetProductImagesByProductId(int productId)
        {
            List<ProductImage> images = await _iProductImageRepository.GetProductImagesByProductId(productId);
            return _mapper.Map<List<ProductImage>, List<ProductImageDTO>>(images);
        }

        public async Task<ProductImageDTO> AddProductImage(ProductImageCreateDTO createImage)
        {
            ProductImage image = _mapper.Map<ProductImageCreateDTO, ProductImage>(createImage);
            image = await _iProductImageRepository.AddProductImage(image);
            return _mapper.Map<ProductImage, ProductImageDTO>(image);
        }

        public async Task<ProductImageDTO> UpdateProductImage(int imageId, ProductImageUpdateDTO updateImage)
        {
            ProductImage image1 = _mapper.Map<ProductImageUpdateDTO, ProductImage>(updateImage);
            ProductImage image = await _iProductImageRepository.UpdateProductImage(imageId, image1);
            return _mapper.Map<ProductImage, ProductImageDTO>(image);
        }

        public async Task<bool> DeleteImage(int id)
        {
            ProductImage image = await _iProductImageRepository.getProductImageById(id);

            if (image == null) return false;
            await _iProductImageRepository.DeleteImage(image);
            return true;
        }
    }
}
