using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface IBrandService
	{
		public HasilPaging<List<BrandData>> GetList(Paging paging, string Brands);
		public Response<bool> Create(BrandAdd model);
		public Response<bool> Update(BrandEdit model); 
		public Response<bool> Delete(BrandDelete req);
        public List<BrandList> GetBrandList(/*string Name*/);
    }
}
