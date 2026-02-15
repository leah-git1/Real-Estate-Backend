using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _ShopContext;

        public ProductRepository(ShopContext ShopContext)
        {
            this._ShopContext = ShopContext;
        }

        public async Task<(List<Product>, int total)> GetProducts(int?[] categoryIds, string? city, decimal? minPrice, decimal? maxPrice, int? rooms, int? beds, int position = 1, int skip = 10)
        {
            //return await _ShopContext.Products
            //    .Include(p => p.Category)
            //    .Include(p => p.ProductImages)
            //    .Where(p =>
            //        (p.IsAvailable == true) &&
            //        (categoryIds == null || categoryIds.Length == 0 || categoryIds.Contains(p.CategoryId)) &&
            //        (city == null || p.City.Contains(city)) &&
            //        (minPrice == null || p.Price >= minPrice) &&
            //        (maxPrice == null || p.Price <= maxPrice) &&
            //        (rooms == null || p.Rooms == rooms) &&
            //        (beds == null || p.Beds == beds)
            //    )
            //    .OrderByDescending(p => p.CreatedDate)
            //    .AsNoTracking()
            //    .ToListAsync();
            var query = _ShopContext.Products.Include(p => p.ProductImages).Where(product =>
            //(desc == null ? (true) : (product.Description.Contains(desc)))
            ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))
             && (rooms == null || product.Rooms == rooms) &&
                    (beds == null || product.Beds == beds) &&
                    (city == null || product.City.Contains(city)) &&
                    (product.IsAvailable == true)))
            .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Skip(((position - 1) * skip))
            .Take(skip).Include(product => product.Category).ToListAsync();
            var total = await query.CountAsync();
            return (products, total);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _ShopContext.Products
                .Include(p => p.Category)
                .Include(p => p.Owner)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<List<Product>> GetProductsByOwnerId(int ownerId)
        {
            return await _ShopContext.Products
                .Include(p => p.Category)
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _ShopContext.Products.AddAsync(product);
            await _ShopContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product productToUpdate)
        {
            Product existingProduct = await _ShopContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }
            productToUpdate.ProductId = id;
            _ShopContext.Entry(existingProduct).CurrentValues.SetValues(productToUpdate);
            await _ShopContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            Product product = await _ShopContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _ShopContext.Products.Remove(product);
            await _ShopContext.SaveChangesAsync();
            return true;
        }

    }
}