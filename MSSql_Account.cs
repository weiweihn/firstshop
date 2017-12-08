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
    public  class MSSql_Account:IAccount
    {
        /// <summary>
        /// 使用id获取用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>成功返回一个Account 否则返回null </returns>
        public  Account GetAccountById(int id) 
        {
            if (id <= 0) return null;
               string sqlstr=@"select * from dbo.Account where id=@id";
               SqlParameter sqlpara=new SqlParameter("@id",SqlDbType.Int);
               sqlpara.Value = id;
               Account at =new Account();
               using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
               {   
                   if (dr.Read())
                   {

                       at.Id =(int)dr["id"];
                       at.UserName=dr["UserName"].ToString();
                       at.Password=dr["Password"].ToString();
                       at.Email=dr["Email"].ToString();
                       at.Name=dr["Name"].ToString();
                       at.NickName=dr["NickName"].ToString();
                       at.State = (int)dr["State"];
                       at.Status = (int)dr["Status"];
                       at.Phone=dr["Phone"].ToString();
                       at.Tel=dr["Tel"].ToString();
                       at.Cdate=(DateTime)dr["Cdate"];
                       at.Udate = (DateTime)dr["Udate"];
                       at.IsEnable=(Boolean)dr["IsEnable"];
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
        /// 使用用户名获取用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>成功返回一个Account 否则返回null </returns>
        public   Account GetAccountByUserName(string username)
        {

            if (username == null || username==string.Empty) return null;
            string sqlstr = @"select * from dbo.Account where username=@username";
            SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.Int);
            sqlpara.Value = username;
            Account at = new Account();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {

                    at.Id = (int)dr["id"];
                    at.UserName = dr["UserName"].ToString();
                    at.Password = dr["Password"].ToString();
                    at.Email = dr["Email"].ToString();
                    at.Name = dr["Name"].ToString();
                    at.NickName = dr["NickName"].ToString();
                    at.State = (int)dr["State"];
                    at.Status = (int)dr["Status"];
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
        /// 获取所有有效用户
        /// </summary>
        /// <returns>Account列表</returns>
        public   IList<Account> GetAccount()
        {


            string sqlstr = @"select * from dbo.Account where  IsEnable=1 and IsVisable=1";

            SqlParameter sqlpara = new SqlParameter();
            IList<Account> al = new List<Account>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Account at = new Account();
                    at.Id = (int)dr["id"];
                    at.UserName = dr["UserName"].ToString();
                    at.Password = dr["Password"].ToString();
                    at.Email = dr["Email"].ToString();
                    at.Name = dr["Name"].ToString();
                    at.NickName = dr["NickName"].ToString();
                    at.State = (int)dr["State"];
                    at.Status = (int)dr["Status"];
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
        /// 新增一个用户
        /// </summary>
        /// <param name="account">用户类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败</returns>
        public   int InsertAccount(Account account) 
        {

            string sqlstr = @"insert into dbo.Account(UserName,Password,Email,Name,NickName,State,Status,Phone,Tel) 
            values(@UserName,@Password,@Email,@Name,@NickName,@State,@Status,@Phone,@Tel);
            Select @Id=scope_identity()";

            SqlParameter[] sqlpara = new SqlParameter[10];
            sqlpara[0] = new SqlParameter("@UserName",SqlDbType.VarChar);
            sqlpara[1] = new SqlParameter("@Password", SqlDbType.VarChar);
            sqlpara[2] = new SqlParameter("@Email", SqlDbType.VarChar);
            sqlpara[3] = new SqlParameter("@Name", SqlDbType.VarChar);
            sqlpara[4] = new SqlParameter("@NickName", SqlDbType.VarChar);
            sqlpara[5] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[6] = new SqlParameter("@Status", SqlDbType.Int);
            sqlpara[7] = new SqlParameter("@Phone", SqlDbType.VarChar);
            sqlpara[8] = new SqlParameter("@Tel", SqlDbType.VarChar);
            sqlpara[9] = new SqlParameter("@Id", SqlDbType.Int);

            sqlpara[0].Value =account.UserName;
            sqlpara[1].Value =account.Password;
            sqlpara[2].Value =account.Email;
            sqlpara[3].Value =account.Name;

            sqlpara[4].Value =account.NickName;
            sqlpara[5].Value =account.State;
            sqlpara[6].Value =account.Status;
            sqlpara[7].Value =account.Phone;
            sqlpara[8].Value = account.Tel;
            sqlpara[9].Direction = ParameterDirection.Output;

            int retval = 0;
            retval=(int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            account.Id = (int)sqlpara[9].Value;

            return retval;

        
        
        
        
        }


        /// <summary>
        /// 新增一批用户
        /// </summary>
        /// <param name="accounts">用户类列表 如果成功回填每个account数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败</returns>
        public   int InsertAccount(IList<Account> accounts) 
        {


            string sqlstr = @"insert into dbo.Account(UserName,Password,Email,Name,NickName,State,Status,Phone,Tel) 
            values(@UserName,@Password,@Email,@Name,@NickName,@State,@Status,@Phone,@Tel);
            Select @Id=scope_identity()";

            SqlParameter[] sqlpara = new SqlParameter[10];
            sqlpara[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
            sqlpara[1] = new SqlParameter("@Password", SqlDbType.VarChar);
            sqlpara[2] = new SqlParameter("@Email", SqlDbType.VarChar);
            sqlpara[3] = new SqlParameter("@Name", SqlDbType.VarChar);
            sqlpara[4] = new SqlParameter("@NickName", SqlDbType.VarChar);
            sqlpara[5] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[6] = new SqlParameter("@Status", SqlDbType.Int);
            sqlpara[7] = new SqlParameter("@Phone", SqlDbType.VarChar);
            sqlpara[8] = new SqlParameter("@Tel", SqlDbType.VarChar);
            sqlpara[9] = new SqlParameter("@Id", SqlDbType.Int);
            sqlpara[9].Direction = ParameterDirection.Output;
         
            int retval = 0;

            foreach(Account account in accounts)
            {
                sqlpara[0].Value = account.UserName;
                sqlpara[1].Value = account.Password;
                sqlpara[2].Value = account.Email;
                sqlpara[3].Value = account.Name;

                sqlpara[4].Value = account.NickName;
                sqlpara[5].Value = account.State;
                sqlpara[6].Value = account.Status;
                sqlpara[7].Value = account.Phone;
                sqlpara[8].Value = account.Tel;
                sqlpara[9].Value = 0;


                try
                {
                   retval += (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
                   account.Id = (int)sqlpara[9].Value;
                }
                catch
                { 
                    
                }


            }
 
            return retval;
        }


        /// <summary>
        /// 使用id删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败 </returns>
        public   int DelAccountById(int id) 
        {

            string sqlstr = @"delete dbo.Account where id=@id ";

           
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;

            int retval = 0;

            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

            return retval;
        }
        /// <summary>
        /// 使用用户名删除用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败</returns>
        public   int DelAccountByUserName(string username) 
        {
            string sqlstr = @"delete dbo.Account where username=@username ";

            
            SqlParameter sqlpara = new SqlParameter("@username", SqlDbType.Int);
            sqlpara.Value = username;

            int retval = 0;

            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

            return retval;
        
        
        
        }

        /// <summary>
        /// 使用用户类删除用户
        /// </summary>
        /// <param name="account">用户类</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败</returns>
        public   int DelAccount(Account account)
        {

            int retval = 0;
            retval = DelAccountById(account.Id);
           
            return retval;
        
        
        
        }


        /// <summary>
        /// 使用用户类列表,删除一批用户
        /// </summary>
        /// <param name="accounts">用户类列表</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败</returns>
        public   int DelAccount(IList<Account> accounts)
        {
            int retval = 0;

            foreach (Account at in accounts)
            {
                retval+=DelAccount(at);
            
            }
            return retval;
        
        }

        /// <summary>
        /// 使用用户类更新用户
        /// </summary>
        /// <param name="account">用户类</param>
        /// <returns>是否成功 大于等于1;0没有此用户;小于0失败</returns>
        public   int UpAccount(Account account)
        {
            string sqlstr = @"update dbo.Account set UserName=@UserName,Password=@Password,Email=@Email,Name=@Name,
            NickName=@NickName,State=@State,Status=@Status,Phone=@Phone,Tel=@Tel,Udate=getdate(),
            ,IsEnable=@IsEnable,IsVisable=@IsVisable
            where id=@id";
     
            SqlParameter[] sqlpara = new SqlParameter[12];
            sqlpara[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
            sqlpara[1] = new SqlParameter("@Password", SqlDbType.VarChar);
            sqlpara[2] = new SqlParameter("@Email", SqlDbType.VarChar);
            sqlpara[3] = new SqlParameter("@Name", SqlDbType.VarChar);
            sqlpara[4] = new SqlParameter("@NickName", SqlDbType.VarChar);
            sqlpara[5] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[6] = new SqlParameter("@Status", SqlDbType.Int);
            sqlpara[7] = new SqlParameter("@Phone", SqlDbType.VarChar);
            sqlpara[8] = new SqlParameter("@Tel", SqlDbType.VarChar);
            sqlpara[9] = new SqlParameter("@Id", SqlDbType.Int);
            sqlpara[10] = new SqlParameter("@IsEnable", SqlDbType.Bit);
            sqlpara[11] = new SqlParameter("@IsVisable", SqlDbType.Bit);

            sqlpara[0].Value = account.UserName;
            sqlpara[1].Value = account.Password;
            sqlpara[2].Value = account.Email;
            sqlpara[3].Value = account.Name;

            sqlpara[4].Value = account.NickName;
            sqlpara[5].Value = account.State;
            sqlpara[6].Value = account.Status;
            sqlpara[7].Value = account.Phone;
            sqlpara[8].Value = account.Tel;
            sqlpara[9].Value = account.Id;
            sqlpara[10].Value = account.IsEnable;
            sqlpara[11].Value = account.IsVisable;

            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        }


        /// <summary>
        /// 使用用户类列表,更新一批用户
        /// </summary>
        /// <param name="accounts">用户类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
        public   int UpAccount(IList<Account> accounts)
        {

            int retval = 0;

            foreach (Account at in accounts)
            {
                retval += UpAccount(at);

            }
            return retval;
        
        
        
        
        }


    }
}
