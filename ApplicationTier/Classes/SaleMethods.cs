using IndustryConnect_Week5_WebApi.ApplicationTier.Dtos;
using IndustryConnect_Week5_WebApi.ApplicationTier.Interfaces;
using IndustryConnect_Week5_WebApi.ApplicationTier.Mappers;
using IndustryConnect_Week5_WebApi.Models;
using IndustryConnectWeek5WebApi.ApplicationTier.Common;
using Microsoft.EntityFrameworkCore;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Classes
{
    public class SaleMethods : ISaleMethods
    {
        private readonly IndustryConnectWeek2Context _context;

        public SaleMethods(IndustryConnectWeek2Context context)
        {
            _context = context;
        }

        public async Task<PagedDtos<SaleDto>?> GetAllSalesAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Sales.CountAsync();

            if (totalCount < 0)
            {
                //Nothing in table
                return null;
            }

            var salesDto =  await _context.Sales.Include(p => p.Product)
                .Include(c => c.Customer)
                .Include(st => st.Store)
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(s => SaleMapper.EntityToSaleDto(s))
                .ToListAsync();

            //Pagination
            //add the SaleDto to the paged Dto
            return new PagedDtos<SaleDto>
            {
                Dtos = salesDto,
                TotalCount = totalCount
            };
        }

        public async Task<SaleDto?> GetSaleAsync(int id)
        {
            var sale = await _context.Sales
                .Include(p => p.Product)
                .Include(c => c.Customer)
                .Include(st => st.Store)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return null;
            }

            //detaching the entity so that it can be reused later
            _context.Entry(sale).State = EntityState.Detached;

            return SaleMapper.EntityToSaleDto(sale);
        }

        public async Task<SaleDto?> AddSaleAsync(SaleDto saleDto)
        {
            var sale = SaleMapper.SaleDtoToEntity(saleDto);

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            _context.Entry(sale).State = EntityState.Detached;

            var saleDtoNew = await GetSaleAsync(sale.Id);

            return saleDtoNew;
        }

        public async Task<SaleDto?> UpdateSaleAsync(int id, SaleDto saleDto)
        {
            var sale = SaleMapper.SaleDtoToEntity(saleDto);

            if (!SaleExists(id))
            {
                //Sale doesn't exist add it.

                saleDto = SaleMapper.EntityToSaleDto(sale);
                return await AddSaleAsync(saleDto);
            }
            else
            {
                sale.Id = id;
                _context.Entry(sale).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _context.Entry(sale).State = EntityState.Detached;

                var saleDtoNew = await GetSaleAsync(sale.Id);

                return saleDtoNew;
            }
        }

        public async Task<SaleDto> PatchSaleDetails(int id, SaleDto saleDto)
        {
            var sale = SaleMapper.SaleDtoToEntity(saleDto);

            _context.Entry(sale).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return saleDto;
        }

        public async Task<StatusEnum> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return StatusEnum.NotFound;
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return StatusEnum.NoContent;
        }

        public bool SaleExists(int id)
        {  
            return _context.Sales.Any(x => x.Id == id); 
        }
    }
}
