using PetHotel.Core.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Contracts
{
    public interface IHomeService
    {
        Task<ICollection<PricesViewModel>> GetPricesAsync();
    }
}
