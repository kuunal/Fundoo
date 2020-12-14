using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ImagesCloud
{
    public class CloudService : ICloudService
    {
        private readonly CloudConfiguration _config;
        private Cloudinary _cloudinary;

        public CloudService(CloudConfiguration config)
        {
            _config = config;
            Account account = new Account(_config.Name
                                          , _config.ApiKey
                                          , _config.Secret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UpdloadToCloud(IFormFile image, string email)
        {
            var uploadResult = new ImageUploadResult();
            if (image.Length > 0)
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(email+DateTime.Now.ToString(), stream)
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);  
                }
            }
            return uploadResult.Url.ToString();
        }
    }
}
