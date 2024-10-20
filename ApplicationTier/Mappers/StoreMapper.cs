using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Mappers
{
    public static class StoreMapper
    {
        public static Store StoreDtoToEntity(StoreDto storeDto)
        {
            return new Store
            {
                Id = storeDto.Id,
                Name = storeDto.Name,
                Location = storeDto.Location
            };
        }

        public static StoreDto EntityToStoreDto(Store store)
        {
            return new StoreDto(store);
        }
    }
}
