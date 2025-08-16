using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            //notnull kullanmak icin degisken tipi nullable olmalıdır.örneğin int ise int'i nullable yapmalıyız.
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün Gereklidir")
                .Length(1, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.")
                //yazdıgımız senkron metodu ekleyelim.
                .Must(MustUniqeProductName).WithMessage("Ürün ismi veritabanında bulunmaktadir.");

            //decimal ve int için notnull kontrolü yapmaya gerek yok.default değeri zaten 0 dır.null değildir.
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0 dan büyük olmalıdır.");

            //stock inclusive between
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok adetleri 1 ile 100 arasında olmalıdır");
        }

        //senktron method //küçük orta büyüklükte uygulamalarda.
        private bool MustUniqeProductName(string name)
        {
            return !_productRepository.Where(x=>x.Name == name).Any();

        }


    }
}
