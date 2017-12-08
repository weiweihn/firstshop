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
    public class MSSql_Orders:IOrders
    {
        //public int Id
        //public string UserName
        //int State
        //public int? status
        //public decimal TotalPrice
        //public string Addr
        //public DateTime Cdate
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable

        /// <summary>
        /// 使用id获取订单
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns>成功返回一个Orders 否则返回null </returns>
        public Orders GetOrdersById(int id)
        {
            if (id <= 0) return null;
            string sqlstr = @"select * from dbo.Orders where Id=@id";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            Orders at = new Orders();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {
                    at.Id = (int)dr["Id"];
                    at.UserName =dr["UserName"].ToString();
                    at.status = (int)dr["status"]; 
                    at.State = (int)dr["State"];
                    at.TotalPrice = (decimal)dr["TotalPrice"];
                    at.Addr = dr["Addr"].ToString();
                    at.Cdate = (DateTime)dr["Cdate"];
                    at.Udate = (DateTime)dr["Udate"];
                    at.IsEnable = (Boolean)dr["IsEnable"];
                    at.IsVisable = (Boolean)dr["IsVisable"];
                }
            }
            if (at.Id == 0 || at.Id == null)
            {
                return null;

            }
            return at;
        }

        /// <summary>
        /// 使用用户名获取订单
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>成功返回一个Orders列表 否则返回null </returns>
        public List<Orders> GetOrdersByUserName(string username)
        {

            if (username == null || username == string.Empty) return null;
            string sqlstr = @"select * from dbo.Orders where username=@username";
            SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.VarChar);
            sqlpara.Value = username;
            List<Orders> ladd = new List<Orders>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Orders at = new Orders();
                    at.Id = (int)dr["Id"];
                    at.UserName = dr["UserName"].ToString();
                    at.status = (int)dr["status"];
                    at.State = (int)dr["State"];
                    at.TotalPrice = (decimal)dr["TotalPrice"];
                    at.Addr = dr["Addr"].ToString();
                    at.Cdate = (DateTime)dr["Cdate"];
                    at.Udate = (DateTime)dr["Udate"];
                    at.IsEnable = (Boolean)dr["IsEnable"];
                    at.IsVisable = (Boolean)dr["IsVisable"];
                    ladd.Add(at);
                }

            }
            return ladd;
        }
        /// <summary>
        /// 获取所有有效订单
        /// </summary>
        /// <returns>Orders列表</returns>
        public IList<Orders> GetOrders()
        {

            string sqlstr = @"select * from dbo.Orders where  IsEnable=1 and IsVisable=1";

            SqlParameter sqlpara = new SqlParameter();
            IList<Orders> al = new List<Orders>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Orders at = new Orders();
                    at.Id = (int)dr["Id"];
                    at.UserName = dr["UserName"].ToString();
                    at.status = (int)dr["status"];
                    at.State = (int)dr["State"];
                    at.TotalPrice = (decimal)dr["TotalPrice"];
                    at.Addr = dr["Addr"].ToString();
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
        /// 新增一个订单
        /// </summary>
        /// <param name="Orders">订单类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败</returns>
        public int InsertOrders(Orders Orders)
        {

            //public int Id
            //public string UserName
            //int State
            //public int? status
            //public decimal TotalPrice
            //public string Addr
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable
            string sqlstr = @"insert into dbo.Orders(UserName,State,status,TotalPrice,Addr) 
            values(@UserName,@State,@status,@TotalPrice,@Addr);
            Select @Id=scope_identity()";
        
            SqlParameter[] sqlpara = new SqlParameter[6];
            sqlpara[0] = new SqlParameter("@UserName",SqlDbType.VarChar);
            sqlpara[1] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[2] = new SqlParameter("@status", SqlDbType.Int);
            sqlpara[3] = new SqlParameter("@TotalPrice", SqlDbType.Float);
            sqlpara[4] = new SqlParameter("@Addr", SqlDbType.VarChar);
            sqlpara[5] = new SqlParameter("@Id",SqlDbType.Int);

            sqlpara[0].Value = Orders.UserName;
            sqlpara[1].Value = Orders.State;
            sqlpara[2].Value = Orders.status;
            sqlpara[3].Value = Orders.TotalPrice;
            sqlpara[4].Value = Orders.Addr;
            sqlpara[5].Direction = ParameterDirection.Output;

            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            Orders.Id = (int)sqlpara[5].Value;

            return retval;
        }


        /// <summary>
        /// 新增一批订单
        /// </summary>
        /// <param name="Orderss">订单类列表 如果成功回填每个Orders数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败</returns>
        public int InsertOrders(IList<Orders> Orderss)
        {

            int retval = 0;
            foreach (Orders cg in Orderss)
            {
                retval += InsertOrders(cg);
            }
            return retval;
        
        
        }


        /// <summary>
        /// 使用id删除订单
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败 </returns>
        public int DelOrdersById(int id)
        {
            string sqlstr = @"delete dbo.Orders where id=@id ";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        }


        /// <summary>
        /// 使用id列表删除一批订单
        /// </summary>
        /// <param name="lid">订单id列表</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败 </returns>
        public int DelOrdersById(List<int> lid)
        {
            int retval = 0;
            foreach (int cg in lid)
            {
                retval += DelOrdersById(cg);
            }
            return retval;
        
        }

        /// <summary>
        /// 使用用户名删除订单
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败</returns>
        public int DelOrdersByUserName(string username)
        {

            string sqlstr = @"delete dbo.Orders where username=@username ";
            SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.VarChar);
            sqlpara.Value = username;
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        }

        /// <summary>
        /// 使用订单类删除订单
        /// </summary>
        /// <param name="Orders">订单类</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败</returns>
        public int DelOrders(Orders Orders)
        {

            return DelOrdersById(Orders.Id);
        
        }

        /// <summary>
        /// 使用订单类列表,删除一批订单
        /// </summary>
        /// <param name="Orderss">订单类列表</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败</returns>
        public int DelOrders(IList<Orders> Orderss)
        {
            int retval = 0;
            foreach (Orders cg in Orderss)
            {
                retval += DelOrders(cg);
            }
            return retval;
        
        
        }

        /// <summary>
        /// 使用订单类更新订单
        /// </summary>
        /// <param name="Orders">订单类</param>
        /// <returns>是否成功 大于等于1;0没有此订单;小于0失败</returns>
        public int UpOrders(Orders Orders)
        {
            //public int Id
            //public string UserName
            //int State
            //public int? status
            //public decimal TotalPrice
            //public string Addr
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable
            string sqlstr = @"update dbo.Orders set  UserName=@UserName,State=@State,
            status=@status,TotalPrice=@TotalPrice,Addr=@Addr,udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";

            SqlParameter[] sqlpara = new SqlParameter[8];
            sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
            sqlpara[1] = new SqlParameter("@UserName", SqlDbType.VarChar);
            sqlpara[2] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[3] = new SqlParameter("@status", SqlDbType.Int);
            sqlpara[4] = new SqlParameter("@TotalPrice", SqlDbType.Float);
            sqlpara[5] = new SqlParameter("@Addr", SqlDbType.VarChar);
            sqlpara[6] = new SqlParameter("@IsEnable", SqlDbType.Bit);
            sqlpara[7] = new SqlParameter("@IsVisable", SqlDbType.Bit);

            sqlpara[0].Value = Orders.Id;
            sqlpara[1].Value = Orders.UserName;
            sqlpara[2].Value = Orders.State;
            sqlpara[3].Value = Orders.status;
            sqlpara[4].Value = Orders.TotalPrice;
            sqlpara[5].Value = Orders.Addr;
            sqlpara[6].Value = Orders.IsEnable;
            sqlpara[7].Value = Orders.IsVisable;
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
            
        
        }

        /// <summary>
        /// 使用订单类列表,更新一批订单
        /// </summary>
        /// <param name="Orderss">订单类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
        public int UpOrders(IList<Orders> Orderss)
        {
            int retval = 0;
            foreach (Orders cg in Orderss)
            {
                retval += UpOrders(cg);
            }
            return retval;
        
        
        }



    }
}
