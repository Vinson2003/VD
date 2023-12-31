﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface ITransactionService
	{
        public HasilPaging<List<TransactionData>> GetList(Paging paging, string TracDateStart, string TracDateEnd, string SBrand, string SResult);
        public Response<bool> Create(TransactionAdd req);
        public Response<bool> Update(TransactionEdit req);
        public Response<bool> Delete(TransactionDelete req);
    }
}
