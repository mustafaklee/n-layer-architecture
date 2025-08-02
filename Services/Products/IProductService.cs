using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);

        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
        Task<ServiceResult<List<ProductDto>>> GetAllList();

        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateAsync(UpdateProductRequest request, int id);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
