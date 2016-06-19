using Anatoli.PMC.DataAccess.Helpers.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.Helpers
{
    public class GeneralCommands
    {
        public static DateTime GetServerDateTime(DataContext context)
        {
            if (context == null) context = new DataContext();
            return context.GetValue<DateTime>("SELECT GETDATE() AS ServerDate");
        }

        public static string GetServerShamsiDateTime(DataContext context)
        {
            if (context == null) context = new DataContext();
            return context.GetValue<string>("SELECT dbo.ToShamsi(GETDATE()) AS ServerDate");
        }

        public static int GetId(DataContext context, string tableName)
        {
            if (context == null) context = new DataContext();
            return context.GetValue<int>("declare @id int EXEC GetId '" + tableName + "' , @Id output  select @id");
        }
        public static int GetFiscalYearId(DataContext context)
        {
            if (context == null) context = new DataContext();
            return context.GetValue<int>(DBQuery.Instance.GetFiscalYearId());
        }

        public static int GetCashSessionId(DataContext context)
        {
            if (context == null) context = new DataContext();
            return context.GetValue<int>(DBQuery.Instance.GetCashSessionId());
        }

        public static int GetRequestNo(DataContext context, int fiscalYearId)
        {
            if (context == null) context = new DataContext();
            return context.GetValue<int>(DBQuery.Instance.GetLatestRequestNo(fiscalYearId));
        }
    }
}
