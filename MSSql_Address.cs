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
   public class MSSql_Address:IAddress
    {

        /// <summary>
        /// 使用id获取地址
        /// </summary>
        /// <param name="id">地址id</param>
        /// <returns>成功返回一个Address 否则返回null </returns>
       public Address GetAddressById(int id) 
       {

           if (id <= 0) return null;
           string sqlstr = @"select * from dbo.Address where id=@id";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           Address at = new Address();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               if (dr.Read())
               {
//public int Id
//public string UserName
//public string Addr
//public string Contacts
//public string Phone
//public string Tel
//public DateTime Cdate
//public DateTime Udate
//public Boolean IsEnable
//public Boolean IsVisable

                   at.Id = (int)dr["id"];
                   at.UserName = dr["UserName"].ToString();
                   at.Addr = dr["Addr"].ToString();
                   at.Contacts = dr["Contacts"].ToString();
                   at.Phone = dr["Phone"].ToString();
                   at.Tel = dr["Tel"].ToString();
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
        /// 使用用户名获取地址
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>成功返回一个Address列表 否则返回null </returns>
       public IList<Address> GetAddressByUserName(string username)
       { 
       
         if (username == null || username==string.Empty) return null;
         string sqlstr = @"select * from dbo.Address where username=@username";
            SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.Int);
            sqlpara.Value = username;
            List<Address> ladd = new List<Address>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Address at = new Address();
                    at.Id = (int)dr["id"];
                    at.UserName = dr["UserName"].ToString();
                    at.Addr = dr["Addr"].ToString();
                    at.Contacts = dr["Contacts"].ToString();
                    at.Phone = dr["Phone"].ToString();
                    at.Tel = dr["Tel"].ToString();
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
        /// 获取所有有效地址
        /// </summary>
        /// <returns>Address列表</returns>
       public  IList<Address> GetAddress()
       {

           string sqlstr = @"select * from dbo.Address where  IsEnable=1 and IsVisable=1";

           
           IList<Address> al = new List<Address>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Address at = new Address();
                   at.Id = (int)dr["id"];
                   at.UserName = dr["UserName"].ToString();
                   at.Addr = dr["Addr"].ToString();
                   at.Contacts = dr["Contacts"].ToString();
                   at.Phone = dr["Phone"].ToString();
                   at.Tel = dr["Tel"].ToString();
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
        /// 新增一个地址
        /// </summary>
        /// <param name="address">地址类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int InsertAddress(Address address) 
       {
           string sqlstr = @"insert into dbo.Address(UserName,Addr,Contacts,Phone,Tel) 
            values(@UserName,@Addr,@Contacts,@Phone,@Tel);
            Select @Id=scope_identity()";

           SqlParameter[] sqlpara = new SqlParameter[6];
           sqlpara[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
           sqlpara[1] = new SqlParameter("@Addr", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Contacts", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Phone", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@Tel", SqlDbType.VarChar);
           sqlpara[5] = new SqlParameter("@Id", SqlDbType.Int);


           sqlpara[0].Value = address.UserName;
           sqlpara[1].Value = address.Addr;
           sqlpara[2].Value = address.Contacts;
           sqlpara[3].Value = address.Phone;
           sqlpara[4].Value = address.Tel;
           sqlpara[5].Direction = ParameterDirection.Output;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           address.Id = (int)sqlpara[5].Value;

           return retval;
       }


        /// <summary>
        /// 新增一批地址
        /// </summary>
        /// <param name="laddress">用户类列表 如果成功回填每个address数据类id</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int InsertAddress(IList<Address> laddress)
       {
           int retval = 0;
           foreach (Address ar in laddress)
           {
               retval += InsertAddress(ar);
           }
           return retval;
       }


        /// <summary>
        /// 使用id删除地址
        /// </summary>
        /// <param name="id">地址id</param>
        /// <returns>是否成功 大于等于1;0没有此地址;小于0失败 </returns>
       public int DelAddressById(int id)
       {
           string sqlstr = @"delete dbo.Address where id=@id ";

           
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;

           int retval = 0;

           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

           return retval;
       
       
       
       }
        /// <summary>
        /// 使用用户名删除地址
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是否成功 大于等于1;0没有此地址;小于0失败</returns>
       public int DelAddressByUserName(string username)
       {

           string sqlstr = @"delete dbo.Address where username=@username ";


           SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.VarChar);
           sqlpara.Value = username;

           int retval = 0;

           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

           return retval;
       
       
       
       
       
       }

        /// <summary>
        /// 使用id列表,删除一批地址
        /// </summary>
        /// <param name="lid">id列表</param>
        /// <returns>是否成功 大于等于1;0没有此地址;小于0失败</returns>
       public int DelAddress(IList<int> lid)
       {

           int retval = 0;
           foreach (int ino in lid)
           {
               retval += DelAddressById(ino);
           }
           return retval;
       }

        /// <summary>
        /// 使用地址类更新地址
        /// </summary>
        /// <param name="address">地址类</param>
        /// <returns>是否成功 大于等于1;0没有此地址;小于0失败</returns>
       public int UpAddress(Address address)
       {
           string sqlstr = @"update dbo.Address set  UserName=@UserName,Addr=@Addr,
            Contacts=@Contacts,Phone=@Phone,Tel=@Tel,Udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable
            where id=@Id ";

           SqlParameter[] sqlpara = new SqlParameter[8];
           sqlpara[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
           sqlpara[1] = new SqlParameter("@Addr", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Contacts", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Phone", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@Tel", SqlDbType.VarChar);
           sqlpara[5] = new SqlParameter("@Id", SqlDbType.Int);

           sqlpara[6] = new SqlParameter("@IsEnable", DbType.Boolean);
           sqlpara[7] = new SqlParameter("@IsVisable", DbType.Boolean);

           sqlpara[0].Value = address.UserName;
           sqlpara[1].Value = address.Addr;
           sqlpara[2].Value = address.Contacts;
           sqlpara[3].Value = address.Phone;
           sqlpara[4].Value = address.Tel;
           sqlpara[5].Value =address.Id;
           sqlpara[6].Value = address.IsEnable;
           sqlpara[7].Value = address.IsVisable;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       }

        /// <summary>
        /// 使用地址类列表,更新一批地址
        /// </summary>
        /// <param name="laddress">用户类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public  int UpAddress(IList<Address> laddress)
       {

           int retval = 0;
           foreach (Address ar in laddress)
           {
               retval += UpAddress(ar);
           }
           return retval;
       
       
       
       
       }
           



    }
}
