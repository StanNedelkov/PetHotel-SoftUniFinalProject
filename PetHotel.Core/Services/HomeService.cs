using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HomeModels;
using PetHotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Services
{
    public class HomeService : IHomeService
    {
        private readonly PetHotelDbContext context;
        public HomeService(PetHotelDbContext _context)
        {
            this.context = _context;
        }
        public async Task<ICollection<PricesViewModel>> GetPricesAsync()
        {
            return await context
                .PetTypes
                .Select(x => new PricesViewModel() 
                { 
                    Price = x.CostPerDay,
                    Title = x.Name
                })
                .ToListAsync();
        }
    }
}
