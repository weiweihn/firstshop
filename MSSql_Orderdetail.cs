using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using model;
using SqlHelp;
using IDAL;
using System.Data.SqlClient;
using System.Data;

namespace MSSqlDal
{
    public  class MSSql_Orderdetail: IOrderdetail
    {
      
        //public int OrderId
        //public int LineNum
        //public int ItemId
        //public int Qty
        //public decimal UnitPrice
        //public DateTime Cdate
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable
        /// <summary>
        /// 使用订单id获取订单明细
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns>成功返回一个Orderdetail列表，否则返回null </returns>
       public List<Orderdetail> GetOrderdetailById(int orderid)
       {
         if (orderid <= 0) return null;
         string sqlstr = @"select * from dbo.Orderdetail where OrderId=@orderid";
         SqlParameter sqlpara = new SqlParameter("@orderid", SqlDbType.Int);
            sqlpara.Value = orderid;
            List<Orderdetail> ol = new List<Orderdetail>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Orderdetail at = new Orderdetail();
                    at.OrderId = (int)dr["OrderId"];
                    at.LineNum = (int)dr["LineNum"];
                    at.ItemId = (int)dr["ItemId"];
                    at.Qty = (int)dr["Qty"];
                    at.UnitPrice = (decimal)dr["UnitPrice"];
                    at.Cdate = (DateTime)dr["Cdate"];
                    at.Udate = (DateTime)dr["Udate"];
                    at.IsEnable = (Boolean)dr["IsEnable"];
                    at.IsVisable = (Boolean)dr["IsVisable"];
                    ol.Add(at);

                }
            }

            return ol;
       
       
       
       
       
       }
        /// <summary>
        /// 使用订单id与行号获取订单明细
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="linenum">行号</param>
        /// <returns>成功返回一个Orderdetail，否则返回null </returns>
       public Orderdetail GetOrderdetailById(int orderid, int linenum)
       {

           if (orderid <= 0) return null;
           string sqlstr = @"select * from dbo.Orderdetail where OrderId=@orderid and LineNum=@LineNum";
           SqlParameter sqlpara = new SqlParameter("@orderid", SqlDbType.Int);
           SqlParameter sqlpara1 = new SqlParameter("@LineNum", SqlDbType.Int);
           sqlpara.Value = orderid; sqlpara1.Value = linenum;
           Orderdetail at = new Orderdetail();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara,sqlpara1))
           {
               if (dr.Read())
               {
                   
                   at.OrderId = (int)dr["OrderId"];
                   at.LineNum = (int)dr["LineNum"];
                   at.ItemId = (int)dr["ItemId"];
                   at.Qty = (int)dr["Qty"];
                   at.UnitPrice = (decimal)dr["UnitPrice"];
                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];
                 

               }
           }

           return at;
       }
        /// <summary>
        /// 获取所有有效订单明细
        /// </summary>
        /// <returns>Orderdetail列表</returns>
       public IList<Orderdetail> GetOrderdetail()
       {

           string sqlstr = @"select * from dbo.Orderdetail where  IsEnable=1 and IsVisable=1";

           
           IList<Orderdetail> al = new List<Orderdetail>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Orderdetail at = new Orderdetail();
                   at.OrderId = (int)dr["OrderId"];
                   at.LineNum = (int)dr["LineNum"];
                   at.ItemId = (int)dr["ItemId"];
                   at.Qty = (int)dr["Qty"];
                   at.UnitPrice = (decimal)dr["UnitPrice"];

                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];
                   al.Add(at);

                  
               }



           }


           return al;
       
       
       
       
       
       }

        /// <summary>
        /// 新增一个订单明细
        /// </summary>
        /// <param name="Orderdetail">订单明细类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败</returns>
       public int InsertOrderdetail(Orderdetail Orderdetail)
       {
           string sqlstr = @"insert into dbo.Orderdetail(OrderId,LineNum,ItemId,Qty,UnitPrice) 
            values(@OrderId,@LineNum,@ItemId,@Qty,@UnitPrice)";

           //public int OrderId
           //public int LineNum
           //public int ItemId
           //public int Qty
           //public decimal UnitPrice
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           SqlParameter[] sqlpara = new SqlParameter[5];
           sqlpara[0] = new SqlParameter("@OrderId", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@LineNum", SqlDbType.Int);
           sqlpara[2] = new SqlParameter("@ItemId", SqlDbType.Int);
           sqlpara[3] = new SqlParameter("@Qty", SqlDbType.Int);
           sqlpara[4] = new SqlParameter("@UnitPrice", SqlDbType.Decimal);


           sqlpara[0].Value = Orderdetail.OrderId;
           sqlpara[1].Value = Orderdetail.LineNum;
           sqlpara[2].Value = Orderdetail.ItemId;
           sqlpara[3].Value = Orderdetail.Qty;
           sqlpara[4].Value = Orderdetail.UnitPrice;


           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

           return retval;
        
       
       
       
       
       }


        /// <summary>
        /// 新增一批订单明细
        /// </summary>
        /// <param name="Orderdetails">订单明细类列表 如果成功回填每个Orderdetail数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败</returns>
       public int InsertOrderdetail(IList<Orderdetail> Orderdetails)
       {
           int retval = 0;
           foreach (Orderdetail cg in Orderdetails)
           {
               retval += InsertOrderdetail(cg);
           }
           return retval;
       
       
       
       
       
       
       }


        /// <summary>
        /// 使用订单id删除订单明细
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败 </returns>
       public int DelOrderdetailByOrderId(int orderid)
       {
           string sqlstr = @"delete dbo.Orderdetail where OrderId=@OrderId ";


           SqlParameter sqlpara = new SqlParameter("@OrderId", SqlDbType.Int);
           sqlpara.Value = orderid;

           int retval = 0;

           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

           return retval;
       
       
       
       }

        /// <summary>
        /// 使用订单id与行号删除订单明细
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="linenum">行号</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败 </returns>
       public int DelOrderdetailByIdAndLn(int orderid, int linenum)
       {
           string sqlstr = @"delete dbo.Orderdetail where OrderId=@OrderId and linenum=@linenum ";


           SqlParameter sqlpara = new SqlParameter("@OrderId", SqlDbType.Int);
           sqlpara.Value = orderid;
           SqlParameter sqlpara1 = new SqlParameter("@linenum", SqlDbType.Int);
           sqlpara1.Value = linenum;

           int retval = 0;

           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara, sqlpara1);

           return retval;
       
       
       
       
       
       }



        /// <summary>
        /// 使用订单明细类删除订单明细
        /// </summary>
        /// <param name="Orderdetail">订单明细类</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败</returns>
       public int DelOrderdetail(Orderdetail Orderdetail)
       {
           

           

           int retval = 0;

           retval = DelOrderdetailByIdAndLn(Orderdetail.OrderId, Orderdetail.LineNum);

           return retval;
       
       
       }

        /// <summary>
        /// 使用订单明细类列表,删除一批订单明细
        /// </summary>
        /// <param name="Orderdetails">订单明细类列表</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败</returns>
       public int DelOrderdetail(IList<Orderdetail> Orderdetails)
       {
           int retval = 0;
           foreach (Orderdetail cg in Orderdetails)
           {
               retval += DelOrderdetail(cg);
           }
           return retval;
       
       
       
       }
        /// <summary>
        /// 使用订单明细类更新订单明细
        /// </summary>
        /// <param name="Orderdetail">订单明细类</param>
        /// <returns>是否成功 大于等于1;0没有此订单明细;小于0失败</returns>
       public int UpOrderdetail(Orderdetail Orderdetail)
       {

           string sqlstr = @"update dbo.Orderdetail set  OrderId=@OrderId,LineNum=@LineNum,ItemId=@ItemId,
            Qty=@Qty,UnitPrice=@UnitPrice,
            udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where ProductItemId=@ProductItemId ";

           SqlParameter[] sqlpara = new SqlParameter[7];
           sqlpara[0] = new SqlParameter("@OrderId", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@LineNum", SqlDbType.Int);
           sqlpara[2] = new SqlParameter("@ItemId", SqlDbType.Int);
           sqlpara[3] = new SqlParameter("@Qty", SqlDbType.Int);
           sqlpara[4] = new SqlParameter("@UnitPrice", SqlDbType.Decimal);
           sqlpara[5] = new SqlParameter("@IsEnable", SqlDbType.Bit);
           sqlpara[6] = new SqlParameter("@IsVisable", SqlDbType.Bit);

           sqlpara[0].Value = Orderdetail.OrderId;
           sqlpara[1].Value = Orderdetail.LineNum;
           sqlpara[2].Value = Orderdetail.ItemId;
           sqlpara[3].Value = Orderdetail.Qty;

           sqlpara[4].Value = Orderdetail.UnitPrice;
           sqlpara[5].Value = Orderdetail.IsEnable;
           sqlpara[6].Value = Orderdetail.IsVisable;
           //public int OrderId
           //public int LineNum
           //public int ItemId
           //public int Qty
           //public decimal UnitPrice
           //public DateTime z
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       
       
       }

        /// <summary>
        /// 使用订单明细类列表,更新一批订单明细
        /// </summary>
        /// <param name="Orderdetails">订单明细类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int UpOrderdetail(IList<Orderdetail> Orderdetails)
       {

           int retval = 0;
           foreach (Orderdetail cg in Orderdetails)
           {
               retval += UpOrderdetail(cg);
           }
           return retval;
       
       
       }



    }
}
