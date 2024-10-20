using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnectWeek5WebApi.ApplicationTier.Common;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Interfaces
{
    public interface IStoreMethods
    {
        public Task<PagedDtos<StoreDto>?> GetAllStoresAsync(int pageNumber, int pageSize);
        public Task<StoreDto?> GetStoreAsync(int id);

        public Task<StoreDto> AddStoreAsync(StoreDto storeDto);

        public Task<StoreDto> UpdateStoreAsync(int id, StoreDto storeDto);

        public Task<StoreDto> PatchStoreDetails(int id, StoreDto storeDto);
        public Task<StatusEnum> DeleteStoreAsync(int id);
        public bool StoreExists(int id);
    }
}
