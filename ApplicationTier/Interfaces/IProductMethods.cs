using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnectWeek5WebApi.ApplicationTier.Common;
using Microsoft.AspNetCore.JsonPatch;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Interfaces
{
    public interface IProductMethods
    {
        public Task<PagedDtos<ProductDto>?> GetAllProductsAsync(int pageNumber, int pageSize);
        public Task<ProductDto?> GetProductAsync(int id);

        public Task<ProductDto> AddProductAsync(ProductDto product);

        public Task<ProductDto> UpdateProductAsync(int id, ProductDto product);

        public Task<ProductDto> PatchProductDetails(int id, ProductDto product);
        public Task<StatusEnum> DeleteProductAsync(int id);
        public bool ProductExists(int id);
    }
}
