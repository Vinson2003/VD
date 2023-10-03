using System.Configuration;
using System.Globalization;
using VD.EF.Data;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
	public class TransactionService : ITransactionService
	{
        public HasilPaging<List<TransactionData>> GetList(Paging paging, string TracDateStart, string TracDateEnd, string Brand, string Result)
        {
            var list = new HasilPaging<List<TransactionData>>();
            using (var context = new VddbContext())
            {
                var GMT = Convert.ToInt32(ConfigurationManager.AppSettings["GMT"]);
                DateTime DateFilter = DateTime.UtcNow.AddDays(-30);
                var getlist = from t in context.PTransactions
                              where t.FlgDeleted == false && t.Brand.FlgDeleted != true
                              && (string.IsNullOrEmpty(TracDateStart) || t.Date >= DateTime.ParseExact(TracDateStart, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                              && (string.IsNullOrEmpty(TracDateEnd) || t.Date <= DateTime.ParseExact(TracDateEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                              && (string.IsNullOrEmpty(Brand) || t.Brand.Name != null && t.Brand.Name.Contains(Brand))
                              && (string.IsNullOrEmpty(Result) || t.Result != null && t.Result.Contains(Result))
                              select new TransactionData()
                              {
                                  Id = t.Id,
                                  BrandId = t.BrandId,
                                  Brand = t.Brand.Name,
                                  Date = t.Date,
                                  DateText = t.Date.ToString("dd/MM/yyyy"),
                                  Result = t.Result,
                                  Updated = t.Updated,
                                  UpdatedText = t.Updated.HasValue ? t.Updated.Value.AddHours(GMT).ToString("dd/MM/yyyy HH:mm:ss") : "",
                                  Updatedby = t.UpdatedBy,
                                  Created = t.Created,
                                  CreatedText = t.Created.HasValue ? t.Created.Value.AddHours(GMT).ToString("dd/MM/yyyy HH:mm:ss") : "", 
                                  Createdby = t.CreatedBy,
                              };

                if (paging.Col.ToLower() == "brand") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Brand); } else { getlist = getlist.OrderByDescending(x => x.Brand); } }
                if (paging.Col.ToLower() == "datetext") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Date); } else { getlist = getlist.OrderByDescending(x => x.Date); } }
				if (paging.Col.ToLower() == "result") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Result); } else { getlist = getlist.OrderByDescending(x => x.Result); } }
                if (paging.Col.ToLower() == "createdtext") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Created); } else { getlist = getlist.OrderByDescending(x => x.Created); } }
                if (paging.Col.ToLower() == "updatedtext") { if (paging.Dir == "asc") { getlist = getlist.OrderBy(x => x.Updated); } else { getlist = getlist.OrderByDescending(x => x.Updated); } }
                
                list.Total = getlist.Count();
                list.Result = getlist.Skip(paging.Start).Take(paging.Length).ToList();
            }
            return list;
        }

        public Response<bool>Create(TransactionAdd Req)
        {
            var Response = new Response<bool>();
            using var context = new VddbContext();
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    var entitytrac = (from w in context.PTransactions
                                      where w.FlgDeleted == false && w.BrandId == Req.BrandId && w.Id == Req.Id
                                      select w).FirstOrDefault();
                    if (entitytrac != null)
                    {
                        Response.Message = "TransactionExists";
                        return Response;
                    }

                    DateTime dateconvert = DateTime.MinValue;
                    try
                    {
                        if (!string.IsNullOrEmpty(Req.Date))
                        {
                            string[] dte = Req.Date.Split('/');//dd/MM/yyyy;
                            dateconvert = new DateTime(int.Parse(dte[2]), int.Parse(dte[1]), int.Parse(dte[0]));
                        }
                    }
                    catch (Exception) { Response.Message = "InvalidDate"; return Response; }

                    var Resulttrac = (from w in context.PTransactions
                                      where w.Date == dateconvert && w.Result == Req.Result
                                      select w).FirstOrDefault();
                    if (Resulttrac != null)
                    {
                        Response.Message = "InvalidResult";
                        return Response;
                    }

                    PTransaction a = new PTransaction();
                    a.BrandId = Req.BrandId;
                    a.Result = Req.Result;
                    a.Date = dateconvert;
                    a.CreatedBy = Req.RequestBy;
                    a.Created = DateTime.UtcNow;

                    context.PTransactions.Add(a);
                    context.SaveChanges();

                    Response.Result = true;
                    Response.Sts = true;

                    scope.Complete();
                }    
            }
            return Response;
        }

        public Response<bool>Update(TransactionEdit Req)
        {
            var Response = new Response<bool>();
            using var context = new VddbContext();
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    var entity = (from w in context.PTransactions
                                  where w.Id == Req.Id
                                  select w).FirstOrDefault();
                    if (entity == null)
                    {
                        Response.Message = "InvalidTransaction";
                        return Response;
                    }

                    DateTime dateconvert = DateTime.MinValue;
                    try
                    {
                        if (!string.IsNullOrEmpty(Req.Date))
                        {
                            string[] dte = Req.Date.Split('/');//dd/MM/yyyy;
                            dateconvert = new DateTime(int.Parse(dte[2]), int.Parse(dte[1]), int.Parse(dte[0]));
                        }
                    }
                    catch (Exception) { Response.Message = "InvalidDate"; return Response; }

                    var entitydate = (from w in context.PTransactions
                                      where w.Date == dateconvert && w.Result == Req.Result
                                      select w).FirstOrDefault();
                    if (entitydate != null)
                    {
                        Response.Message = "Invalid Date or Result";
                        return Response;
                    }

                    entity.BrandId = Req.BrandId;
                    entity.Date = dateconvert;
                    entity.Result = Req.Result;
                    entity.UpdatedBy = Req.RequestBy;
                    entity.Updated = DateTime.UtcNow;

                    context.SaveChanges();
                    Response.Result = true;
                    Response.Sts = true;

                    scope.Complete();
                }
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

                entity.FlgDeleted = true;
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
