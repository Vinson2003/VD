using VD.EF.Models;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
	public class BrandService : IBrandService
	{
		public HasilPaging<List<BrandData>> GetList(Paging paging)
		{
			var list = new HasilPaging<List<BrandData>>();
			using (var context = new VddbContext())
			{
				var getlist = (from t in context.MtBrands
							   where t.FlgDeleted != true
							   select new BrandData()
							   {
								   Id = t.Id,
								   Name = t.Name,
								   Created = t.Created,
								   Createdby = t.CreatedBy,
								   Updated = t.Updated,
								   Updatedby = t.UpdatedBy,
							   });

				list.Total = getlist.Count();
				if (paging.Col.ToLower() == "name")
				{
					if (paging.Dir == "asc")
					{
						getlist = getlist.OrderBy(x => x.Name);
					}
					else
					{
						getlist = getlist.OrderByDescending(x => x.Name);
					}
				}
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

        public Response<bool> Add(BrandAdd model)
		{
			var Response = new Response<bool>();
			using var context = new VddbContext();
			{
				var entitybrand = (from w in context.MtBrands
								  where w.Name == model.Name
								  select w).FirstOrDefault();
				if (entitybrand != null)
				{
					Response.Message = "Brand Already Exists";
					return Response;
				}

				MtBrand a = new MtBrand();
				a.Name = model.Name.Trim().ToLower();
				a.Created = DateTime.UtcNow;
				a.CreatedBy = model.RequestBy;

				context.MtBrands.Add(a);
				context.SaveChanges();

				Response.Result = true;
				Response.Sts = true;
			}
			return Response;
		}

		public Response<bool> Update(BrandEdit model)
		{
			var Response = new Response<bool>();
			Response.Result = false;

			using var context = new VddbContext();
			{
				var entity = (from w in context.MtBrands
							  where w.Id == model.Id
							  select w).FirstOrDefault();
				if (entity == null)
				{
					Response.Message = "InvalidBrand";
					return Response;
				}
				 
				entity.Name = model.Name.Trim().ToLower();
				entity.Updated = DateTime.UtcNow;
				entity.UpdatedBy = model.RequestBy;

				context.SaveChanges();
				Response.Result = true;
				Response.Sts = true;
			}
			return Response;
		}

        public Response<bool> Delete(int id, string RequestBy)
        {
            var Response = new Response<bool>();
            Response.Result = false;

            using (var context = new VddbContext())
            {
                var entity = (from d in context.MtBrands
                               where d.Id == id
                               select d).FirstOrDefault();

                if (entity == null)
                {
                    Response.Message = "InvalidData";
                    return Response;
                }
				
				entity.FlgDeleted = true;
				entity.Updated = DateTime.UtcNow;
				entity.UpdatedBy = RequestBy;

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
