using PetHotel.Core.Models.GalleryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Contracts
{
    public interface ICloudinaryImageUpload
    {
        Task<string> UploadPicture(UploadModel model);

    }
}
