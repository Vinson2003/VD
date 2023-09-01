using System.Configuration;
using VD.EF.Models;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
	public class TransactionService : ITransactionService
	{
        public HasilPaging<List<TransactionData>> GetList(Paging paging)
        {
            var list = new HasilPaging<List<TransactionData>>();
            using (var context = new VddbContext())
            {
                var GMT = Convert.ToInt32(ConfigurationManager.AppSettings["GMT"]);
                var getlist = from t in context.PTransactions
                              where t.FlgDeleted != true
                              select new TransactionData()
                              {
                                  Id = t.Id,
                                  Brand = t.Brand.Name,
                                  Date = t.Date.ToString("dd/MM/yyyy"),
                                  Result = t.Result,
                                  Updated = t.Updated,
                                  Updatedby = t.UpdatedBy,
                                  Created = t.Created,
                                  Createdby = t.CreatedBy,
                              };

                list.Total = getlist.Count();
                if (paging.Col.ToLower() == "Name")
                {
                    if (paging.Dir == "asc")
                    {
                        getlist = getlist.OrderBy(x => x.Brand);
                    }
                    else
                    {
                        getlist = getlist.OrderByDescending(x => x.Brand);
                    }
                }
                list.Result = getlist.Skip(paging.Start).Take(paging.Length).ToList();
            }
            return list;
        }

        public Response<bool>Add(TransactionAdd req)
        {
            var Response = new Response<bool>();
            using var context = new VddbContext();
            {
                var entitytrac = (from w in context.PTransactions
                                  where w.BrandId == req.BrandId && w.Id == req.Id
                                  select w).FirstOrDefault();
                if (entitytrac != null)
                {
                    Response.Message = "TransactionExists";
                    return Response;
                }

                PTransaction a = new PTransaction();
                a.BrandId = req.BrandId;
                a.Result = req.Result;
                a.Date = req.Date;
                a.CreatedBy = req.RequestBy;
                a.Created = DateTime.UtcNow;

                context.PTransactions.Add(a);
                context.SaveChanges();

                Response.Result = true;
                Response.Sts = true;
            }
            return Response;
        }

        public Response<bool>Edit(TransactionEdit req)
        {
            var Response = new Response<bool>();
            using var context = new VddbContext();
            {
                var entity = (from w in context.PTransactions
                              where w.Id == req.Id
                              select w).FirstOrDefault();
                if (entity == null)
                {
                    Response.Message = "InvalidTransaction";
                    return Response;
                }

                entity.Result = req.Result;
                entity.CreatedBy = req.RequestBy;
                entity.Created = DateTime.UtcNow;

                context.SaveChanges();
                Response.Result = true;
                Response.Sts = true;
            }
            return Response;
        }

        public Response<bool> Delete(TransactionDelete req)
        {
            var Response = new Response<bool>();
            using (var context = new VddbContext())
            {
                var entity = (from t in context.PTransactions
                              where t.FlgDeleted == false && t.Id == req.Id
                              select t).FirstOrDefault();
                if (entity == null)
                {
                    Response.Message = "InvalidData";
                    return Response;
                }

                entity.Updated = DateTime.UtcNow;
                entity.UpdatedBy = req.RequestBy;

                context.SaveChanges();
                Response.Result = true;
                Response.Sts = true;
            }
            return Response;
        }
    }
}
