using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Mappers
{
    public class SaleMapper
    {
        public static SaleDto EntityToSaleDto(Sale sale)
        {
            return new SaleDto(sale);

        }

        public static Sale SaleDtoToEntity(SaleDto saleDto)
        {
            return new Sale
            {
                Id = saleDto.Id,
                CustomerId = saleDto.CustomerId,
                ProductId = saleDto.ProductId,
                StoreId = saleDto.StoreId,
                DateSold = saleDto.DateSold
            };
        }
    }
}
