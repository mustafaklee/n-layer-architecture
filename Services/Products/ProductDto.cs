namespace App.Services.Products
{
    //controller => services => repositories arasında nesneler ilerlerken
    //bir kere üretildiyse birdaha değişmemesi gerekir.bunun için set yerine *init* keyword'ü kullanılır.
    //çünkü class'lar referans tipli oldukları için bir yerde değişirse her yerde değişir.
    //isterse nesneden örnek üretip öyle değiştirebilir.



    //bu şekilde uzun uzadıya da bir ProductResponse Tanımlanabilirdi.
    //public record ProductResponse
    //{
    //    public int Id { get; init; }
    //    public string? Name { get; init; }
    //    public decimal Price { get; init; }
    //    public int Stock { get; init; }
    //}

    public record ProductDto(int Id,string Name, decimal Price,int Stock);



}
