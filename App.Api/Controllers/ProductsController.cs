using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return CreateActionResult(await productService.GetAllListAsync());
        }

        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetById(int id){
            return CreateActionResult(await productService.GetByIdAsync(id));
        }



        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            return CreateActionResult(await productService.GetPagedAllListAsync(pageNumber,pageSize));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            return CreateActionResult(await productService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateProductRequest request)
        {
            return CreateActionResult(await productService.UpdateAsync(request,id));
        }

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(int id,int quantity)
        {
            return CreateActionResult(await productService.UpdateStockAsync(id, quantity));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await productService.DeleteAsync(id));
        }
    }
}
