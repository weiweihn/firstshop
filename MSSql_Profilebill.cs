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
   public class MSSql_Profilebill:IProfilebill
   {
        //public int Id
        //public string UserName
        //public int ItemId
        //public string Name
        //public decimal Price
        //public int Billclass
        //public int Quantity
        //public DateTime Cdate
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable
       /// <summary>
       /// 使用id获取个性单据
       /// </summary>
       /// <param name="id">个性单据id</param>
       /// <returns>成功返回一个Profilebill 否则返回null </returns>
       public Profilebill GetProfilebillById(int id)
       {
           if (id <= 0) return null;
           string sqlstr = @"select * from dbo.Profilebill where Id=@id";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           Profilebill at = new Profilebill();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               if (dr.Read())
               {
                   at.Id = (int)dr["Id"];
                   at.UserName = dr["UserName"].ToString();
                   at.ItemId = (int)dr["ItemId"];
                   at.Name = dr["Name"].ToString();
                   at.Price = (decimal)dr["Price"];
                   at.Billclass =(int) dr["Billclass"];
                   at.Quantity = (int)dr["Quantity"];
                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];

                   //public int Id
                   //public string UserName
                   //public int ItemId
                   //public string Name
                   //public decimal Price
                   //public int Billclass
                   //public int Quantity
                   //public DateTime Cdate
                   //public DateTime Udate
                   //public Boolean IsEnable
                   //public Boolean IsVisable
               }
           }
           if (at.Id == 0 || at.Id == null)
           {
               return null;

           }
           return at;
       
       
       
       }
       /// <summary>
       /// 使用用户名获取个性单据
       /// </summary>
       /// <param name="username">用户名</param>
       /// <returns>成功返回一个Profilebill列表 否则返回null </returns>
       public IList<Profilebill> GetProfilebillByUserName(string username)
       {

           string sqlstr = @"select * from dbo.Profilebill where username=@username and IsEnable=1 and IsVisable=1";
           SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.Int);
           sqlpara.Value = username;
           List<Profilebill> ladd = new List<Profilebill>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr,sqlpara))
           {
               while (dr.Read())
               {
                   Profilebill at = new Profilebill();
                   at.Id = (int)dr["Id"];
                   at.UserName = dr["UserName"].ToString();
                   at.ItemId = (int)dr["ItemId"];
                   at.Name = dr["Name"].ToString();
                   at.Price = (decimal)dr["Price"];
                   at.Billclass = (int)dr["Billclass"];
                   at.Quantity = (int)dr["Quantity"];
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
       /// 获取所有有效个性单据
       /// </summary>
       /// <returns>Profilebill列表</returns>
       public IList<Profilebill> GetProfilebill()
       {
           string sqlstr = @"select * from dbo.Profilebill where  IsVisable=1 and IsEnable=1";
         
           List<Profilebill> ladd = new List<Profilebill>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Profilebill at = new Profilebill();
                   at.Id = (int)dr["Id"];
                   at.UserName = dr["UserName"].ToString();
                   at.ItemId = (int)dr["ItemId"];
                   at.Name = dr["Name"].ToString();
                   at.Price = (decimal)dr["Price"];
                   at.Billclass = (int)dr["Billclass"];
                   at.Quantity = (int)dr["Quantity"];
                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];
                   ladd.Add(at);
               }

           }
           return ladd;
       
       
       
       }

      public IList<Profilebill> GetProfilebill(string wherestr)
       {

           string sqlstr = @"select * from dbo.Profilebill where  IsVisable=1 and IsEnable=1";

           if (!string.IsNullOrEmpty(wherestr))
           {
               sqlstr += " and " + wherestr;
           
           }

           List<Profilebill> ladd = new List<Profilebill>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Profilebill at = new Profilebill();
                   at.Id = (int)dr["Id"];
                   at.UserName = dr["UserName"].ToString();
                   at.ItemId = (int)dr["ItemId"];
                   at.Name = dr["Name"].ToString();
                   at.Price = (decimal)dr["Price"];
                   at.Billclass = (int)dr["Billclass"];
                   at.Quantity = (int)dr["Quantity"];
                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];
                   ladd.Add(at);
               }

           }
           return ladd;



       }

       public int DelProfilebill(string strwhere)
       {
           if (string.IsNullOrEmpty(strwhere))
               return -1;
           
           string sqlstr = @"delete dbo.Profilebill where " + strwhere;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, null);
           return retval;
       }


       /// <summary>
       /// 新增一个个性单据
       /// </summary>
       /// <param name="Profilebill">个性单据类,如果成功回填id</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败</returns>
       public int InsertProfilebill(Profilebill Profilebill)
       {
           string sqlstr = @"insert into dbo.Profilebill(UserName,ItemId,Name,Price,Billclass,Image,Quantity) 
            values(@Productid,@Spec,@Color,@Profilebill,@Descn,@Image,@State);
            Select @Id=scope_identity()";
           //public int Id
           //public string UserName
           //public int ItemId
           //public string Name
           //public decimal Price
           //public int Billclass
           //public int Quantity
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           SqlParameter[] sqlpara = new SqlParameter[8];
           sqlpara[0] = new SqlParameter("@UserName", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@ItemId", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Price", SqlDbType.Float);
           sqlpara[4] = new SqlParameter("@Billclass", SqlDbType.VarChar);
           sqlpara[5] = new SqlParameter("@Quantity", SqlDbType.VarChar);
           sqlpara[6] = new SqlParameter("@Id", SqlDbType.Int);

           sqlpara[0].Value = Profilebill.UserName;
           sqlpara[1].Value = Profilebill.ItemId;
           sqlpara[2].Value = Profilebill.Name;
           sqlpara[3].Value = Profilebill.Price;
           sqlpara[4].Value = Profilebill.Billclass;
           sqlpara[5].Value = Profilebill.Quantity;
          
           sqlpara[6].Direction = ParameterDirection.Output;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           Profilebill.Id = (int)sqlpara[6].Value;

           return retval;
       
       
       
       
       
       }



       /// <summary>
       /// 新增一批个性单据
       /// </summary>
       /// <param name="Profilebills">个性单据类列表 如果成功回填每个Profilebill数据类id</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败</returns>
       public int InsertProfilebill(IList<Profilebill> Profilebills)
       {
           int retval = 0;
           foreach (Profilebill pr in Profilebills)
           {

               retval += InsertProfilebill(pr);


           }
           return retval;
       
       
       }


       /// <summary>
       /// 使用id删除个性单据
       /// </summary>
       /// <param name="id">个性单据id</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败 </returns>
       public int DelProfilebillById(int id)
       {
           string sqlstr = @"delete dbo.Profilebill where id=@id ";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       }


       /// <summary>
       /// 使用id列表删除一批个性单据
       /// </summary>
       /// <param name="lid">个性单据id列表</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败 </returns>
       public int DelProfilebillById(List<int> lid)
       {
           int retval = 0;
           foreach (int pr in lid)
           {

               retval += DelProfilebillById(pr);


           }
           return retval;
       
       
       }

       /// <summary>
       /// 使用用户名删除个性单据
       /// </summary>
       /// <param name="username">用户名</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败</returns>
       public int DelProfilebillByUserName(string username)
       {
           string sqlstr = @"delete dbo.Profilebill where username=@username ";
           SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.VarChar);
           sqlpara.Value = username;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       }

       /// <summary>
       /// 使用个性单据类删除个性单据
       /// </summary>
       /// <param name="Profilebill">个性单据类</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败</returns>
       public int DelProfilebill(Profilebill Profilebill)
       {
           int retval = 0;
          
            retval += DelProfilebillById(Profilebill.Id);


           
           return retval;
       
       
       
       }

       /// <summary>
       /// 使用个性单据类列表,删除一批个性单据
       /// </summary>
       /// <param name="Profilebills">个性单据类列表</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败</returns>
       public int DelProfilebill(IList<Profilebill> Profilebills)
       {

           int retval = 0;
           foreach (Profilebill pr in Profilebills)
           {

               retval += DelProfilebill(pr);


           }
           return retval;
       
       }

       /// <summary>
       /// 使用个性单据类更新个性单据
       /// </summary>
       /// <param name="Profilebill">个性单据类</param>
       /// <returns>是否成功 大于等于1;0没有此个性单据;小于0失败</returns>
       public int UpProfilebill(Profilebill Profilebill)
       {    //public int Id
           //public string UserName
           //public int ItemId
           //public string Name
           //public decimal Price
           //public int Billclass
           //public int Quantity
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable

           string sqlstr = @"update dbo.Profilebill set  UserName=@UserName,ItemId=@ItemId,
            Name=@Name,Price=@Price,Billclass=@Billclass,Quantity=@Quantity,udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";
           
           SqlParameter[] sqlpara = new SqlParameter[9];
           sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@UserName", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@ItemId", SqlDbType.Int);
           sqlpara[3] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@Price", SqlDbType.Decimal);
           sqlpara[5] = new SqlParameter("@Billclass", SqlDbType.Int);
           sqlpara[6] = new SqlParameter("@Quantity", SqlDbType.Int);
           sqlpara[7] = new SqlParameter("@IsEnable", SqlDbType.Bit);
           sqlpara[8] = new SqlParameter("@IsVisable", SqlDbType.Bit);

           sqlpara[0].Value = Profilebill.Id;
           sqlpara[1].Value = Profilebill.UserName;
           sqlpara[2].Value = Profilebill.ItemId;
           sqlpara[3].Value = Profilebill.Name;
           sqlpara[4].Value = Profilebill.Price;
           sqlpara[5].Value = Profilebill.Billclass;
           sqlpara[6].Value = Profilebill.Quantity;
           sqlpara[7].Value = Profilebill.IsEnable;
           sqlpara[8].Value = Profilebill.IsVisable;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       }

       /// <summary>
       /// 使用个性单据类列表,更新一批个性单据
       /// </summary>
       /// <param name="Profilebills">个性单据类列表</param>
       /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int UpProfilebill(IList<Profilebill> Profilebills)
       {

           int retval = 0;
           foreach (Profilebill pr in Profilebills)
           {

               retval += UpProfilebill(pr);


           }
           return retval;
       }





   }
}
