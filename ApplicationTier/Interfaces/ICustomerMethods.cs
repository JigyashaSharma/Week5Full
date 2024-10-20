using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnectWeek5WebApi.ApplicationTier.Common;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Interfaces
{
    public interface ICustomerMethods
    {
        public Task<CustomerDto?> GetCustomerAsync(int id);

        public Task<PagedDtos<CustomerDto>?> GetAllCustomersAsync(int pageNumber, int pageSize);

        public Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto);
        public Task<CustomerDto> UpdateCustomerAsync(int id, CustomerDto customerDto);
        public Task<CustomerDto> PatchCustomerDetails(int id, CustomerDto customerDto);

        public Task<StatusEnum> DeleteCustomerAsync(int id);
        public bool CustomerExists(int id);
    }
}
