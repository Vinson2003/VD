using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.LIB
{
    public class SaveImage
    {
        //public string SaveBrandImage(IFormFile image)
        //{
        //    if (image != null && image.Length > 0)
        //    {
        //        var Filename = Guid.NewGuid().ToString();
        //        var Fileextension = Path.GetExtension(image.FileName);
        //        var Filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BrandPicture", Filename + Fileextension);

        //        using (var Filestream = new FileStream(Filepath, FileMode.Create))
        //        {
        //            image.CopyTo(Filestream);
        //        }

        //        return Filename + Fileextension;
        //    }
        //    return null;
        //}
    }
}
