﻿using System.Configuration;
using VD.EF.Data;
using VD.LIB;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
	public class BrandService : IBrandService
	{
		public HasilPaging<List<BrandData>> GetList(Paging paging, string qBrand)
		{
			var list = new HasilPaging<List<BrandData>>();
			using (var context = new VddbContext())
			{
                var GMT = Convert.ToInt32(ConfigurationManager.AppSettings["GMT"]);
                var getlist = (from t in context.MtBrands
							   where t.FlgDeleted != true
                               && (string.IsNullOrEmpty(qBrand) || t.Name != null && t.Name.Contains(qBrand))
                               select new BrandData()
							   {
								   Id = t.Id,
								   Name = t.Name,
								   Created = t.Created,
                                   CreatedText = t.Created.HasValue ? t.Created.Value.AddHours(GMT).ToString("dd/MM/yyyy HH:mm:ss") : "",
                                   Createdby = t.CreatedBy,
								   Updated = t.Updated,
                                   UpdatedText = t.Updated.HasValue ? t.Updated.Value.AddHours(GMT).ToString("dd/MM/yyyy HH:mm:ss") : "",
								   Updatedby = t.UpdatedBy,
							   });

                if (paging.Col.ToLower() == "name") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Name); } else { getlist = getlist.OrderByDescending(x => x.Name); } }
                if (paging.Col.ToLower() == "createdtext") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Created); } else { getlist = getlist.OrderByDescending(x => x.Created); } }
                if (paging.Col.ToLower() == "updatedtext") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Updated); } else { getlist = getlist.OrderByDescending(x => x.Updated); } }

                list.Total = getlist.Count();
				list.Result = getlist.Skip(paging.Start).Take(paging.Length).ToList();
			}
			return list;
		}

        public List<BrandList> GetBrandList(/*string Name*/)
        {
            using (var context = new VddbContext())
            {
                var m = new List<BrandList>();
                using (var ctx = new VddbContext())
                {
                    var getRole = from n in ctx.MtBrands
								  where n.FlgDeleted == false
                                  select new BrandList()
                                  {
                                      Id = n.Id,
                                      Name = n.Name,
                                  };

                    m = getRole.ToList();
                }
                return m;
            }
        }

        public Response<bool> Create(BrandAdd req)
		{
			var Response = new Response<bool>();
			using var context = new VddbContext();
			{
				var entitybrand = (from w in context.MtBrands
								  where w.Name == req.Name
								  select w).FirstOrDefault();
				if (entitybrand != null)
				{
					Response.Message = "Brand Already Exists";
					return Response;
				}

				MtBrand a = new MtBrand();
				a.Name = req.Name.Trim().ToLower();
				a.BrandPicture = SaveImage.SaveBrandImage(req.BrandPicture);
				a.Created = DateTime.UtcNow;
				a.CreatedBy = req.RequestBy;

				context.MtBrands.Add(a);
				context.SaveChanges();

				Response.Result = true;
				Response.Sts = true;
			}
			return Response;
		}

		public Response<bool> Update(BrandEdit req)
		{
			var Response = new Response<bool>();
			Response.Result = false;

			using var context = new VddbContext();
			{
				var entity = (from w in context.MtBrands
							  where w.Id == req.Id
							  select w).FirstOrDefault();
				if (entity == null)
				{
					Response.Message = "InvalidBrand";
					return Response;
				}
				 
				entity.Name = req.Name.Trim().ToLower();
				entity.BrandPicture = SaveImage.SaveBrandImage(req.BrandPicture);
				entity.Updated = DateTime.UtcNow;
				entity.UpdatedBy = req.RequestBy;

				context.SaveChanges();
				Response.Result = true;
				Response.Sts = true;
			}
			return Response;
		}

        public Response<bool> Delete(BrandDelete req)
        {
            var Response = new Response<bool>();
            Response.Result = false;

            using (var context = new VddbContext())
            {
                var entity = (from d in context.MtBrands
                               where d.Id == req.Id
                               select d).FirstOrDefault();

                if (entity == null)
                {
                    Response.Message = "InvalidData";
                    return Response;
                }
				
				entity.FlgDeleted = true;
				entity.Updated = DateTime.UtcNow;
				entity.UpdatedBy = req.RequestBy;

				context.SaveChanges();
				Response.Result = true;
				Response.Sts = true;
            }
            return Response;
        }
        //public Response<bool> Delete(BrandDelete Model)
        //{
        //	var Response = new Response<bool>();
        //	Response.Result = false;

        //	using var context = new VddbContext();
        //	{
        //		var entity = (from w in context.MtBrands
        //					  where w.Id == Model.Id
        //					  select w).FirstOrDefault();
        //		if (entity == null)
        //		{
        //			Response.Message = "InvalidData";
        //			return Response;
        //		}

        //		entity.FlgDeleted = true;
        //		entity.Updated = DateTime.UtcNow;
        //		entity.UpdatedBy = Model.RequestBy;

        //		context.SaveChanges();
        //		Response.Result = true;
        //		Response.Sts = true;
        //	}
        //	return Response;
        //}
    }
}
