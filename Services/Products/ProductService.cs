using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork):IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);
            
            //manuel mapper yapmak en hızlı çalışır.
            //öncelik hız değilse mapper kütüphaneleri kullanılmalıdır.
            var productAsDto = products.Select(p=>new ProductDto(p.Id,p.Name,p.Price,p.Stock)).ToList();
            
            
            return new ServiceResult<List<ProductDto>>()
            {
                Data = productAsDto,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllList()
        {
            var products = await productRepository.GetAll().ToListAsync();

            var productAsDto = products.Select(p=> new ProductDto(p.Id , p.Name, p.Price,p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Success(productAsDto);

        }
 
        
        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) {
                ServiceResult<Product>.Fail("Product Not Found", HttpStatusCode.NotFound); ;
            }

            var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

            return ServiceResult<ProductDto>.Success(productAsDto)!; // ! product null olmaz demek.derleyiciyi bilgilendirmek.
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            };
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
        }

        public async Task<ServiceResult> UpdateAsync(UpdateProductRequest request,int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            //geriye veri dönmüyor şuan.
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);

            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
    }
}
