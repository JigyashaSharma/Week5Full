using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Mappers
{
    public static class ProductMapper
    {
        public static Product ProductDtoToEntity(ProductDto productDto)
        {
            var entity = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Active = productDto.Active,
                Price = productDto.Price
            };

            return entity;
        }

        public static ProductDto EntityToProductDto(Product product)
        {
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Active = product.Active,
                Price = product.Price
            };

            return dto;
        }
    }
}
