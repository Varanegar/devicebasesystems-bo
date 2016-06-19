using Anatoli.Rastak.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.Helpers
{
    public  class RastakDBQuery
    {
        private static List<RastakDBQueryViewModel> queryList = new List<RastakDBQueryViewModel>();
        private static  RastakDBQuery instance = null;
        public static RastakDBQuery Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakDBQuery();
                    using (var context = new DataContext())
                    {
                        var data = context.All<RastakDBQueryViewModel>(@"select UniqueId, QueryName, QueryTSQL from AnatoliQuery");
                        queryList = data.ToList();
                    }
                }
                return instance;
            }
        }
        RastakDBQuery() { }
        public string GetFiscalYearQuery()
        {
            return @"select FiscalYearId as id, convert(uniqueidentifier,uniqueid) as UniqueId, dbo.ToMiladi(StartDate) as FromDate, StartDate as fromPDate, dbo.ToMiladi(EndDate) as toDate, EndDate as ToPdate  from fiscalyear ";
        }
        public  string GetStoreQuery()
        {
            return @"SELECT Convert(Uniqueidentifier, UniqueID) as UniqueID,  CenterId as ID, CenterId CenterId, CenterCode as StoreCode, CenterName as StoreName, Address, Lat, Lng, Hasdelivery, HasCourier, SupportAppOrder, SupportWebOrder, SupportCallCenterOrder FROM Center ";//where centertypeid=3 ";
        }
        public  string GetStockQuery()
        {
            return @"SELECT Convert(Uniqueidentifier, stock.UniqueID) as UniqueID,  stock.StockId as ID, Convert(Uniqueidentifier, center.UniqueId) as StoreId, stock.Stockid as StockCode, StockName , Center.Address
	                    FROM stock, Center where stock.centerid=Center.centerid
                    ";
        }
        public  string GetStoreCalendarQuery(int storeId) 
        {
            return @"select Convert(uniqueidentifier, uniqueId) as uniqueId, case DayTimeWorkingTypeId when 1 then convert(uniqueidentifier, 'E4A73D47-8AC7-41D1-8EEA-21EDFBA90424') when 2 then convert(uniqueidentifier, 'D5C5E5BF-9235-48D8-B026-B7EB8DB14100') end as CalendarTypeValueId, DayTimeWorkingId as ID, WorkingDate as PDate, dbo.ToMiladi(WorkingDate) as Date, FromHour as FromTimeString, '23:59' as ToTimeString from DayTimeWorking where CenterId = " + storeId;
        }
        public  string GetStoreDeliveryRegion(int storeId) 
        {
            return @"select DeliveryRegionTreeId as ID, CityRegionID as UniqueIdString from (
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=CityId and  center.CenterId =  CenterDeliveryRegion.CenterId and ZoneId is null and RegionId is null and AreaId is null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=RegionId and  center.CenterId =  CenterDeliveryRegion.CenterId and ZoneId is null and  RegionId is not null and AreaId is null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=AreaId and  center.CenterId =  CenterDeliveryRegion.CenterId and ZoneId is null and  AreaId is not null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=ZoneId and  center.CenterId =  CenterDeliveryRegion.CenterId and ZoneId is not null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                ) as tt ";
        }
        public  string GetStorePriceList() 
        {
            return @"IF OBJECT_ID('tempdb..#CenterPrice') IS NOT NULL drop table  #CenterPrice
                        select ROW_NUMBER() over (order by p.ProductId) as rowNo, p.ProductId, c.CenterId, c.UniqueId as StoreGuidString,  p.SellPrice as price, Product.UniqueId as ProductGuidString, p.Modifieddate 
	                        into #CenterPrice
	                        from Price p, Center c, Product 
	                        where p.CenterId is null and p.CustomerId is null and p.CustomerGroupId is null and p.CenterGroupId is null and product.ProductId = p.ProductId
		                        and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 

                        delete #CenterPrice where rowNo in (
	                        select rowNo from price p, #CenterPrice 
		                        where p.CenterId = #CenterPrice.CenterId and p.ProductId = #CenterPrice.ProductId 
			                        and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 
			                        and p.CustomerId is null and p.CustomerGroupId is null and p.CenterGroupId is null 
	                        )

                        insert into #CenterPrice
	                        select 1, p.ProductId, p.CenterId, c.UniqueId as StoreGuidString,  p.SellPrice as price, Product.UniqueId as ProductGuidString , p.Modifieddate 
		                        from Price p, Center c, Product
			                         where p.CenterId is not null and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 
				                        and p.CenterId = c.CenterId
				                        and p.ProductId = Product.ProductId
				                        and p.CustomerId is null and p.CustomerGroupId is null and p.CenterGroupId is null 

                        select  price, convert(uniqueidentifier, StoreGuidString) as StoreGuid, convert(uniqueidentifier, ProductGuidString) as ProductGuid from #CenterPrice
                            where StoreGuidString in (select uniqueid FROM Center where centertypeid=3) 
                        ";
        }
        public  string GetFiscalYearId()
        {
            return @"select FiscalYearId from FiscalYear where StartDate <= dbo.ToShamsi(getdate()) and enddate>= dbo.ToShamsi(getdate())";
        }
        public  string GetCashSessionId()
        {
            return @"SELECT top 1 c.CashSessionId FROM CashSession c where c.CashSessionTypeId = 2 and c.CashSessionStatusId = 1";
        }
        public  string GetLatestRequestNo(int fiscalYear)
        {
            return @"SELECT isnull(MAX(s.RequestNo),0)+1 FROM Sell s where s.SellCategoryId = 2 and s.FiscalYearId = " + fiscalYear;
        }
        public  string GetStockProducts(int fiscalYear)
        {
            return
            @" select convert(uniqueidentifier, stockproduct.uniqueId) as UniqueId,convert(uniqueidentifier, stock.uniqueId) as StockGuid,
	                convert(uniqueidentifier, product.uniqueId) as ProductGuid,
	                convert(uniqueidentifier, FiscalYear.uniqueId) as FiscalYearId,
	                ActiveInStock as isEnable from stockproduct, product, stock, FiscalYear
	                where stockproduct.productid = product.productid and  stock.stockid = stockproduct.stockid and FiscalYear.FiscalYearId = stockproduct.fiscalyearid
                    

                ";

        }   
        public  string GetStockOnHand(int fiscalYear)
        {
            return
            @"select convert(uniqueidentifier, Product.UniqueId) as ProductGuid, Qty, convert(uniqueidentifier, Stock.UniqueId) as StockGuid from (      
                      SELECT  ProductId,sum(Qty) as  Qty,StockId
                      FROM         
                      (        
                        SELECT iv.FiscalYearId,iv.StockId,id.ProductId ,   
                       (CASE WHEN vt.IsInput=1 THEN 1 ELSE -1 END * id.Qty) Qty          
                       ,iv.CenterId    
                        FROM InvVoucher iv           
                       INNER JOIN InvVoucherDetail id ON iv.InvVoucherId=id.InvVoucherId          
                       INNER JOIN InvVoucherType vt ON vt.InvVoucherTypeId=iv.InvVoucherTypeId         
                       where fiscalYearId = " + fiscalYear + @"   
                      UNION ALL          
             
                      SELECT s.FiscalYearId,s.StockId,sd.ProductId     
                       ,-SUM(Qty) Qty ,   CenterId    
                      FROM SellM s           
                       INNER JOIN SellDetail sd ON s.SellId=sd.SellId          
                      WHERE s.IsCanceled=0       
                       AND s.InvVoucherId IS NULL      
                       and fiscalYearId = " + fiscalYear + @"   
                      GROUP BY s.FiscalYearId ,s.CenterId ,s.StockId,sd.ProductId,s.InvoiceDate     
             
                
                      UNION ALL        
             
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty)*(-1) AS Qty ,         
                        s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =5     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
      
                      UNION ALL     
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty) AS Qty ,    s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =4     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
                      ) A 
                    group by FiscalYearId, ProductId,StockId
                    ) as onhand, Product, Stock where onhand.ProductId = Product.ProductId and Stock.StockId = onhand.StockId";

        }
        public  string GetStockOnHandByStockId(int fiscalYear, string stockId)
        {
            return
            @"select convert(uniqueidentifier, Product.UniqueId) as ProductGuid, Qty, convert(uniqueidentifier, Stock.UniqueId) as StockGuid from (      
                      SELECT  ProductId,sum(Qty) as  Qty,StockId, CenterId
                      FROM         
                      (        
                        SELECT iv.FiscalYearId,iv.StockId,id.ProductId ,   
                       (CASE WHEN vt.IsInput=1 THEN 1 ELSE -1 END * id.Qty) Qty          
                       ,iv.CenterId    
                        FROM InvVoucher iv           
                       INNER JOIN InvVoucherDetail id ON iv.InvVoucherId=id.InvVoucherId          
                       INNER JOIN InvVoucherType vt ON vt.InvVoucherTypeId=iv.InvVoucherTypeId         
                       where fiscalYearId = " + fiscalYear + @" and iv.stockid = '" + stockId + @"       
                      UNION ALL          
             
                      SELECT s.FiscalYearId,s.StockId,sd.ProductId     
                       ,-SUM(Qty) Qty ,   CenterId    
                      FROM SellM s           
                       INNER JOIN SellDetail sd ON s.SellId=sd.SellId          
                      WHERE s.IsCanceled=0       
                       AND s.InvVoucherId IS NULL      
                       and fiscalYearId = " + fiscalYear + @" and s.stockid = '" + stockId + @"       
                      GROUP BY s.FiscalYearId ,s.CenterId ,s.StockId,sd.ProductId,s.InvoiceDate     
             
                
                      UNION ALL        
             
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty)*(-1) AS Qty ,         
                        s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =5     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" and srd.stockid = '" + stockId + @"       
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
      
                      UNION ALL     
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty) AS Qty ,    s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =4     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" and srd.stockid = '" + stockId + @"       
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
                      ) A 
                    group by FiscalYearId, ProductId,StockId
                    ) as onhand, Product, Stock where onhand.ProductId = Product.ProductId and Stock.StockId = onhand.StockId";

        }
        public  string GetStoreStockOnHand(int fiscalYear)
        {
            return
            @"select convert(uniqueidentifier, Product.UniqueId) as ProductGuid, Qty, convert(uniqueidentifier, Center.UniqueId) as StoreGuid from (      
                      SELECT  ProductId,sum(Qty) as  Qty,CenterId    
                      FROM         
                      (        
                        SELECT iv.FiscalYearId,iv.StockId,id.ProductId ,   
                       (CASE WHEN vt.IsInput=1 THEN 1 ELSE -1 END * id.Qty) Qty          
                       ,iv.CenterId    
                        FROM InvVoucher iv           
                       INNER JOIN InvVoucherDetail id ON iv.InvVoucherId=id.InvVoucherId          
                       INNER JOIN InvVoucherType vt ON vt.InvVoucherTypeId=iv.InvVoucherTypeId         
                       where fiscalYearId = " + fiscalYear + @"        
                      UNION ALL          
             
                      SELECT s.FiscalYearId,s.StockId,sd.ProductId     
                       ,-SUM(Qty) Qty ,   CenterId    
                      FROM SellM s           
                       INNER JOIN SellDetail sd ON s.SellId=sd.SellId          
                      WHERE s.IsCanceled=0       
                       AND s.InvVoucherId IS NULL      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,s.CenterId ,s.StockId,sd.ProductId,s.InvoiceDate     
             
                
                      UNION ALL        
             
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty)*(-1) AS Qty ,         
                        s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =5     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
      
                      UNION ALL     
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty) AS Qty ,    s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =4     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
                      ) A 
                    group by FiscalYearId, ProductId,CenterId
                    ) as onhand, Product, Center where onhand.ProductId = Product.ProductId and Center.centerid = onhand.CenterId and centertypeid = 3";

        }
        public  string GetProduct()
        {
            return @"SELECT p.QtyPerPack, p.ProductId AS id, CONVERT(uniqueidentifier, p.UniqueId) AS uniqueid, p.ProductCode, p.ProductName, p.StoreProductName, p.PackVolume, 
                         p.PackWeight, p.Description, NULL AS PackUnitId, CASE ProductTypeId WHEN 1 THEN CONVERT(uniqueidentifier, '21B7F88F-42B2-40F6-83C9-EF20943440B9') 
                         WHEN 2 THEN CONVERT(uniqueidentifier, '594120A7-1312-45B2-883B-605000D33D0F') WHEN 3 THEN CONVERT(uniqueidentifier, 
                         '9DA7C343-CE14-4CBB-81AE-709E075D4E10') END AS ProductTypeId, pg.UniqueId AS ProductGroupIdString, m.UniqueId AS ManufactureIdString, 
                          mpg.UniqueId AS MainProductGroupIdString
                    FROM            ProductGroupTreeSite AS pg RIGHT OUTER JOIN
                         Product AS p LEFT OUTER JOIN
                         ProductGroupTree AS mpg ON p.ProductGroupTreeId = mpg.ProductGroupTreeId LEFT OUTER JOIN
                         Manufacturer AS m ON p.ManufacturerId = m.ManufacturerId ON pg.ProductGroupTreeSiteId = p.ProductGroupTreeSiteId
                    ";
        }
        public  string GetProductSupplier(int productId)
        {
            return @"select CONVERT(uniqueidentifier, s.uniqueId) as uniqueId from SupplierProduct as sp, supplier as s, product as p 
	                        where sp.SupplierId = s.SupplierId and p.ProductId = sp.ProductId and sp.ProductId='" + productId + "'";
        }
        public  string GetProductCharValue(int productId)
        {
            return @"select CONVERT(uniqueidentifier, pv.uniqueId) as uniqueId from ProductSpecificRel as ps , product as p, ProductSpecificityValue as pv
	                            where ps.ProductSpecificityValueId = pv.ProductSpecificityValueId and p.productId = ps.ProductID and p.productId ='" + productId + "'";
        }
        public  string GetProductGroupData()
        {
            return @"IF OBJECT_ID('tempdb..#GroupRec') IS NOT NULL drop table  #GroupRec
                    select p.ProductGroupTreeSiteId as ProductGroupTreeId, p.ParentId, p.Title, p.uniqueid, p2.uniqueid as Parentuniqueid, null  as CharGroupId
		                     into #GroupRec
		                     from ProductGroupTreeSite AS p INNER JOIN
                                             ProductGroupTreeSite AS p2 ON p.ParentId = p2.ProductGroupTreeSiteId 
		                     where p.parentid = p2.ProductGroupTreeSiteId";
        }
        public  string GetProductGroupTree()
        {
            return @"
                    WITH ProductGroupTreeLevels AS
                    (
                        SELECT
                            p.ProductGroupTreeSiteId as ProductGroupTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.uniqueid,
		                    convert(varchar(66),null) as Parentuniqueid,
		                    null as CharGroupId,
                            CONVERT(VARCHAR(MAX), p.ProductGroupTreeSiteId) AS thePath,
                            1 AS Level
	                    FROM            ProductGroupTreeSite AS p 
                        WHERE p.ParentId IS NULL 

                        UNION ALL

                        SELECT
                            e.ProductGroupTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.uniqueid,
		                    e.Parentuniqueid,
		                    e.CharGroupId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.ProductGroupTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM ProductGroupTreeLevels x
                        JOIN #GroupRec e on e.ParentId = x.ProductGroupTreeId
                    ),
                    ProductGroupTreeRows AS
                    (
                        SELECT
                             ProductGroupTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM ProductGroupTreeLevels
                    )
                    SELECT
	                    Er.UniqueId as UniqueIdString,
	                    Er.ParentUniqueId as ParentUniqueIdString,
                         ER.ProductGroupTreeId as Row,
                         ER.ParentId as ParentId,
	                     ER.Title as GroupName,
	                     ER.CharGroupId as CharGroupIdString,
                         --ER.thePath,
                         ER.Level as NLevel,
                         ER.Row as Id,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM ProductGroupTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM ProductGroupTreeRows ER
                    ORDER BY thePath";
        }
        public  string GetMainProductGroupData()
        {
            return @"IF OBJECT_ID('tempdb..#GroupRec') IS NOT NULL drop table  #GroupRec
                    select p.ProductGroupTreeId as ProductGroupTreeId, p.ParentId, p.Title, p.uniqueid, p2.uniqueid as Parentuniqueid, null  as CharGroupId
		                     into #GroupRec
		                     from ProductGroupTree AS p INNER JOIN
                                             ProductGroupTree AS p2 ON p.ParentId = p2.ProductGroupTreeId 
		                     where p.parentid = p2.ProductGroupTreeId";
        }
        public  string GetMainProductGroupTree()
        {
            return @"
                    WITH ProductGroupTreeLevels AS
                    (
                        SELECT
                            p.ProductGroupTreeId as ProductGroupTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.uniqueid,
		                    convert(varchar(66),null) as Parentuniqueid,
		                    null as CharGroupId,
                            CONVERT(VARCHAR(MAX), p.ProductGroupTreeId) AS thePath,
                            1 AS Level
	                    FROM            ProductGroupTree AS p 
                        WHERE p.ParentId IS NULL 

                        UNION ALL

                        SELECT
                            e.ProductGroupTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.uniqueid,
		                    e.Parentuniqueid,
		                    e.CharGroupId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.ProductGroupTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM ProductGroupTreeLevels x
                        JOIN #GroupRec e on e.ParentId = x.ProductGroupTreeId
                    ),
                    ProductGroupTreeRows AS
                    (
                        SELECT
                             ProductGroupTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM ProductGroupTreeLevels
                    )
                    SELECT
	                    Er.UniqueId as UniqueIdString,
	                    Er.ParentUniqueId as ParentUniqueIdString,
                         ER.ProductGroupTreeId as Row,
                         ER.ParentId as ParentId,
	                     ER.Title as GroupName,
	                     ER.CharGroupId as CharGroupIdString,
                         --ER.thePath,
                         ER.Level as NLevel,
                         ER.Row as ID,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM ProductGroupTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM ProductGroupTreeRows ER
                    ORDER BY thePath";
        }
        public  string GetManufacture()
        {
            return "select ManufacturerId as Id, convert(uniqueidentifier, uniqueid) as uniqueid, ManufacturerName as ManufactureName from Manufacturer";
        }
        public  string GetCharType()
        {
            return @"SELECT ProductSpecificityId as Id, ProductSpecificityName as CharTypeDesc, Convert(uniqueidentifier, UniqueId)  as UniqueId
                        FROM            ProductSpecificity";
        }
        public  string GetCharGroupCharType(int charGroupId )
        {
            return @"select Convert(uniqueidentifier,UniqueId) as UniqueId from ProductSpecificityGDetail g,  ProductSpecificity p 
                                    where p.ProductSpecificityId = g.ProductSpecificityId and ProductSpecificityGId=" + charGroupId;
        }
        public  string GetCharValue(int charTypeId)
        {
            return @"SELECT ProductSpecificityValueName as CharValueText, Convert(uniqueidentifier,UniqueId) as UniqueId
                                FROM ProductSpecificityValue where ProductSpecificityId=" + charTypeId;
        }
        public  string GetCharGroup()
        {
            return @"SELECT ProductSpecificityGId as Id, ProductSpecificityGroupName as CharGroupName, Convert(uniqueidentifier, UniqueId)  as UniqueId
                        FROM ProductSpecificityG";
        }
        public  string GetSupplir()
        {
            return "select SupplierId as Id, convert(uniqueidentifier, uniqueid) as uniqueid, SupplierName from Supplier ";
        }
        public  string GetCityRegion()
        {
            return @"WITH DeliveryRegionTreeLevels AS
                    (
                        SELECT
                            p.DeliveryRegionTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.UniqueId,
		                    CONVERT(varchar(66),'') as ParentUniqueId,
                            CONVERT(VARCHAR(MAX), p.DeliveryRegionTreeId) AS thePath,
                            1 AS Level
                        FROM DeliveryRegionTree p--, DeliveryRegionTree p2 
                        WHERE p.ParentId IS NULL --and p.ParentId = p2.DeliveryRegionTreeId

                        UNION ALL

                        SELECT
                            e.DeliveryRegionTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.UniqueId,
		                    e.ParentUniqueId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.DeliveryRegionTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM DeliveryRegionTreeLevels x
                        JOIN (select p.DeliveryRegionTreeId, p.ParentId, p.Title, p.UniqueId, p2.UniqueId as ParentUniqueId from  DeliveryRegionTree as p , DeliveryRegionTree as p2
			                    where p.ParentId = p2.DeliveryRegionTreeId)  e on e.ParentId = x.DeliveryRegionTreeId
                    ),
                    DeliveryRegionTreeRows AS
                    (
                        SELECT
                             DeliveryRegionTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM DeliveryRegionTreeLevels
                    )
                    SELECT 
                         ER.DeliveryRegionTreeId,
                         ER.ParentId,
	                     ER.UniqueId as uniqueIdString,
	                     ER.parentUniqueId as ParentUniqueIdString,
	                     ER.Title as GroupName,
                         --ER.thePath,
                         ER.Level as NLevel,
                         --ER.Row,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM DeliveryRegionTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM DeliveryRegionTreeRows ER
                    ";
        }
        public  string GetCenterPicture()
        {
            return @"select center.UniqueId as baseDataId, centerimageId as ID,  convert(uniqueidentifier, CenterImage.uniqueid) as uniqueid, CenterImage as image, CenterImageName as ImageName,
                                                    '9CED6F7E-D08E-40D7-94BF-A6950EE23915' as ImageType from CenterImage, Center
													where Center.CenterId = CenterImage.CenterId and CenterTypeId = 3";
        }
        public  string GetProductPicture()
        {
            return @"select Product.UniqueId as baseDataId, ProductimageId as ID,  convert(uniqueidentifier, ProductImage.uniqueid) as uniqueid, ProductImage as image, ProductImageName as ImageName,
                                                    '635126C3-D648-4575-A27C-F96C595CDAC5' as ImageType from ProductImage, Product
													where Product.ProductId = ProductImage.ProductId ";
        }
        public  string GetProductSiteGroupPicture()
        {
            return @"select ProductGroupTreeSite.UniqueId as baseDataId, ProductGroupTreeSiteimageId as ID,  convert(uniqueidentifier, ProductGroupTreeSiteImage.uniqueid) as uniqueid, ProductGroupTreeSiteImage as image, ProductGroupTreeSiteImageName as ImageName,
                                                    '149E61EF-C4DC-437D-8BC9-F6037C0A1ED1' as ImageType from ProductGroupTreeSiteImage, ProductGroupTreeSite
													where ProductGroupTreeSite.ProductGroupTreeSiteId = ProductGroupTreeSiteImage.ProductGroupTreeSiteId  ";
        }
        public  string GetBaseValue()
        {
            return @"select paytypename as BaseValueName, uniqueid, PayTypeId as id, convert(uniqueidentifier, 'F17B8898-D39F-4955-9757-A6B31767F5C7') as BaseTypeId from PayType
                    union all
                    select producttypename as BaseValueName, uniqueid, ProductTypeId as id, convert(uniqueidentifier, 'CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7') as BaseTypeId from ProductType
                    union all
                    select 'Checkout' as BaseValueName, convert(uniqueidentifier, 'F6CE03E2-8A2A-4996-8739-DA9C21EAD787') as uniqueid, 1 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Checkout
                    union all
                    select 'Favorite' as BaseValueName, convert(uniqueidentifier, 'AE5DE00D-3391-49FE-985B-9DA7045CDB13') as uniqueid, 2 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Favorite
                    union all
                    select 'سفارش ناقص' as BaseValueName, convert(uniqueidentifier, '194CA845-2E34-4A06-9A89-DCAFF956FE4D') as uniqueid, 3 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Incomplete
                    union all
                    select 'سایر' as BaseValueName, convert(uniqueidentifier, '41581E50-9928-4A3C-A513-A32DBB3B3D0D') as uniqueid, 4 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Others
                    union all
                    select 'دریافت از محل' as BaseValueName, convert(uniqueidentifier, 'BE2919AB-5564-447A-BE49-65A81E6AF712') as uniqueid, 1 as id, convert(uniqueidentifier, 'F5FFAD55-6E39-40BD-A95D-12A34BA4D005') as BaseTypeId  --DeliveryType : Pickup
                    union all
                    select 'تحویل درب منزل' as BaseValueName, convert(uniqueidentifier, 'CE4AEE25-F8A7-404F-8DBA-80340F7339CC') as uniqueid, 2 as id, convert(uniqueidentifier, 'F5FFAD55-6E39-40BD-A95D-12A34BA4D005') as BaseTypeId  --DeliveryType : Delivery
                    union all
                    select 'سایت' as BaseValueName, convert(uniqueidentifier, '0410F5BD-0C01-4E32-A4D9-D2F4DCC46003') as uniqueid, 2 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as BaseTypeId  --OrderSource : Site
                    union all
                    select 'App' as BaseValueName, convert(uniqueidentifier, '65DEC223-059E-48BA-8281-E4FAAFF6E32D') as uniqueid, 2 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as BaseTypeId  --OrderSource : App
                    union all
                    select 'سایر' as BaseValueName,  convert(uniqueidentifier, '6CF27F09-E162-4802-A451-9BC3304A8130') as uniqueid, 2 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as BaseTypeId  --OrderSource : Others
                    ";
        }
        public  string GetBaseType()
        {
            return @"select 1 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as uniqueid, 'OrderSource' as BaseTypeDesc
                        union all 
                        select 2 as id, convert(uniqueidentifier, 'F5FFAD55-6E39-40BD-A95D-12A34BA4D005') as uniqueid, 'DeliveryType' as BaseTypeDesc
                        union all 
                        select 3 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as uniqueid, 'BasketType' as BaseTypeDesc
                        union all 
                        select 5 as id, convert(uniqueidentifier, 'CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7') as uniqueid, 'ProductType' as BaseTypeDesc
                        union all 
                        select 6 as id, convert(uniqueidentifier, 'F17B8898-D39F-4955-9757-A6B31767F5C7') as uniqueid, 'PayType' as BaseTypeDesc
                        ";
        }
        public  string GetSellInfo()
        {
            return @"select * from sell ";
        }
        public  string GetSellDetailInfo()
        {
            return @"select * from sell ";
        }
        public  string GetSellActionInfo()
        {
            return @"select * from sell ";
        }
        public  string GetNewCustomers()
        {
            return "select UniqueId, Mobile as username, Mobile, Mobile as Password, Mobile as ConfirmPassword, Email, CustomerName as FullName from Customer where CustomerSiteUserId is null and Mobile is not null ";
        }
    }
}
