using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface ITransactionService
	{
        public HasilPaging<List<TransactionData>> GetList(Paging paging);
        public Response<bool> Add(TransactionAdd req);
        public Response<bool> Edit(TransactionEdit req);
        public Response<bool> Delete(TransactionDelete req);
    }
}
