using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ImagesCloud
{
    public interface ICloudService
    {
        Task<string> UpdloadToCloud(IFormFile image, string email);
    }
}
