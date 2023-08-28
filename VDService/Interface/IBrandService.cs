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
		public HasilPaging<List<BrandData>> GetList(Paging paging);
		public Response<bool> Add(BrandAdd model);
		public Response<bool> Edit(BrandEdit model);
		public Response<bool> Delete(BrandDelete Model);
	}
}
