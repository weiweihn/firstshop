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
   public class MSSql_Profiles:IProfiles
    {
       public Profiles GetProfilesById(int id) 
       {

           if (id <= 0) return null;
           string sqlstr = @"select * from dbo.Profiles where Id=@id";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           Profiles at = new Profiles();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               if (dr.Read())
               {
                   at.ID = (int)dr["Id"];
                   at.Username = dr["Username"].ToString();
                   at.IsAnonymous = (Boolean)dr["IsAnonymous"];
                   at.LastActivityDate = (DateTime)dr["LastActivityDate"];
                   at.LastUpdatedDate = (DateTime)dr["LastUpdatedDate"];
               }
           }

           if (at.ID == 0 || at.ID == null)
           {
               return null;

           }
           return at;
       
       
       
       
       }



       public Profiles GetProfilesByUserName(string UserName)
       {

           if (string.IsNullOrEmpty(UserName)) return null;
           string sqlstr = @"select * from dbo.Profiles where UserName=@UserName";
           SqlParameter sqlpara = new SqlParameter("@UserName", SqlDbType.VarChar);
           sqlpara.Value = UserName;
           Profiles at = new Profiles();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               if (dr.Read())
               {
                   at.ID = (int)dr["Id"];
                   at.Username = dr["Username"].ToString();
                   at.IsAnonymous = (Boolean)dr["IsAnonymous"];
                   at.LastActivityDate = (DateTime)dr["LastActivityDate"];
                   at.LastUpdatedDate = (DateTime)dr["LastUpdatedDate"];
               }
           }

           if (at.ID == 0 || at.ID == null)
           {
               return null;

           }
           return at;
       
       
       
       }





       public IList<Profiles> GetProfiles()
       {

           string sqlstr = @"select * from dbo.Profiles ";

           List<Profiles> ladd = new List<Profiles>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Profiles at = new Profiles();
                   at.ID = (int)dr["Id"];
                   at.Username = dr["Username"].ToString();
                   at.IsAnonymous = (Boolean)dr["IsAnonymous"];
                   at.LastActivityDate = (DateTime)dr["LastActivityDate"];
                   at.LastUpdatedDate = (DateTime)dr["LastUpdatedDate"];
                   ladd.Add(at);
               }

           }
           return ladd;
       }


       public IList<Profiles> GetProfiles(string  wherestr)
       {

           string sqlstr = @"select * from dbo.Profiles ";
           if (!string.IsNullOrEmpty(wherestr))
               sqlstr += wherestr;

           List<Profiles> ladd = new List<Profiles>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Profiles at = new Profiles();
                   at.ID = (int)dr["Id"];
                   at.Username = dr["Username"].ToString();
                   at.IsAnonymous = (Boolean)dr["IsAnonymous"];
                   at.LastActivityDate = (DateTime)dr["LastActivityDate"];
                   at.LastUpdatedDate = (DateTime)dr["LastUpdatedDate"];
                   ladd.Add(at);
               }

           }
           return ladd;
       }


       public int InsertProfiles(Profiles Profile)
       {
           string sqlstr = @"insert into dbo.Supplier(Username,IsAnonymous,LastActivityDate,LastUpdatedDate) 
            values(@Username,@IsAnonymous,getdate(),getdate());
            Select @Id=scope_identity()";
           
           SqlParameter[] sqlpara = new SqlParameter[10];
           sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@Username", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@IsAnonymous", SqlDbType.Bit);
           sqlpara[0].Direction = ParameterDirection.Output;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           Profile.ID = (int)sqlpara[0].Value;

           return retval;
       
       }


        /// <summary>
        /// 新增一批供应商
        /// </summary>
        /// <param name="Suppliers">供应商类列表 如果成功回填每个Supplier数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败</returns>
       public int InsertProfiles(IList<Profiles> Profile)
       {
           int retval = 0;
           foreach (Profiles pr in Profile)
           {

               retval += InsertProfiles(pr);


           }
           return retval;
            
       
       
       }



       public int DelProfilesById(int id)
       {

           string sqlstr = @"delete dbo.Profiles where id=@id ";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;


       }


       public int DelProfilesByWhere(string wherestr)
       {
           if (string.IsNullOrEmpty(wherestr))
               return -1;


           string sqlstr = @"delete dbo.Profiles where  "+wherestr;
           
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, null);
           return retval;


       }


        /// <summary>
        /// 使用id列表删除一批供应商
        /// </summary>
        /// <param name="lid">供应商id列表</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败 </returns>
       public int DelProfilesById(List<int> lid)
       {

           int retval = 0;
           foreach (int pr in lid)
           {

               retval += DelProfilesById(pr);


           }
           return retval;
       
       
       }



      
       public int DelProfiles(Profiles Profiles)
       {
           int retval = 0;

           retval += DelProfilesById(Profiles.ID);



           return retval;
       
       
       }


       public int DelProfiles(IList<Profiles> Profiles)
       {
           int retval = 0;
           foreach (Profiles pr in Profiles)
           {

               retval += DelProfiles(pr);


           }
           return retval;



       }


       public int UpProfiles(Profiles Profiles)
       {
           int parac = 3;
           string sqlstr = @"update dbo.Profiles set  Username=@Username,IsAnonymous=@IsAnonymous";
           if (Profiles.LastActivityDate == null)
               sqlstr += ",LastActivityDate=getdate()";
           else
           {
               parac++;
               sqlstr += ",LastActivityDate=@LastActivityDate";
           }
           if (Profiles.LastActivityDate == null)
               sqlstr += ",LastUpdatedDate=getdate()";
           else
           {
               parac++;
               sqlstr += ",LastUpdatedDate=@LastUpdatedDate";

           }
           sqlstr += " where id=@Id ";

           SqlParameter[] sqlpara = new SqlParameter[parac];
           sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@Username", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@IsAnonymous", SqlDbType.Bit);
           if(parac>3)
               sqlpara[3] = new SqlParameter("@LastActivityDate", SqlDbType.DateTime);

           if (parac>4)
               sqlpara[3] = new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime);

           sqlpara[0].Value = Profiles.ID;
           sqlpara[1].Value = Profiles.Username;
           sqlpara[2].Value = Profiles.IsAnonymous;
           if(parac>3)
           sqlpara[3].Value = Profiles.LastActivityDate;
           if(parac>4)
           sqlpara[4].Value = Profiles.LastActivityDate;
           


           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       }


       public int UpProfiles(IList<Profiles> Profiles)
       {
           int retval = 0;
           foreach (Profiles pr in Profiles)
           {

               retval += UpProfiles(pr);


           }
           return retval;

       }

    }
}
