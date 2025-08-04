using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork,IMapper mapper):IProductService
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

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();

            //var productAsDto = products.Select(p=> new ProductDto(p.Id , p.Name, p.Price,p.Stock)).ToList();

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);

        }
 
        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber,int pageSize)
        {
            var products = await productRepository.GetAll().Skip((pageNumber-1)*pageSize).ToListAsync();

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }


        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) {
                return ServiceResult<ProductDto?>.Fail("Product Not Found", HttpStatusCode.NotFound);
            }

            var productAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productAsDto)!; // ! product null olmaz demek.derleyiciyi bilgilendirmek.
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {

            //asenkron olarak Service business validation yapalım.
            //var anyProduct = await productRepository.Where(x => x.Name  == request.Name).AnyAsync();
            //if (anyProduct)
            //{
            //    return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır.",HttpStatusCode.BadRequest);
            //}

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


        //delete ve update metodlarında geriye sadece 204 kodu dönülür.Bir içerik dönmeye gerek yoktur.No content dönülür.
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
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        //HttpPatch'i denemek icin.
        public async Task<ServiceResult> UpdateStockAsync(int id, int quantity)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
            }

            product.Stock = quantity;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            //geriye veri dönmüyor şuan.
            return ServiceResult.Success(HttpStatusCode.NoContent);
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
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
