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
    public class MSSql_Supplier:ISupplier
    {
        //public int Id
        //public string Name
        //public int Status
        //public string Addr1
        //public string Addr2
        //public string City
        //public int? State
        //public string Zip
        //public string Phone
        //public string Tel
        //public DateTime Cdate
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable
        /// <summary>
        /// 使用id获取供应商
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>成功返回一个Supplier 否则返回null </returns>
        public Supplier GetSupplierById(int id)
        {
            if (id <= 0) return null;
            string sqlstr = @"select * from dbo.Supplier where Id=@id";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            Supplier at = new Supplier();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {
                    at.Id = (int)dr["Id"];
                    at.Name = dr["Name"].ToString();
                    at.Status = (int)dr["Status"];
                    at.Addr1 = dr["Addr1"].ToString();
                    at.Addr2 = dr["Addr2"].ToString();
                    at.City = dr["City"].ToString();
                    at.State = (int)dr["State"];
                    at.Zip = dr["Zip"].ToString();
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
        /// 获取所有有效供应商
        /// </summary>
        /// <returns>Supplier列表</returns>
        public IList<Supplier> GetSupplier()
        {
            string sqlstr = @"select * from dbo.Supplier where  IsVisable=1 and IsEnable=1";
          
            List<Supplier> ladd = new List<Supplier>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
            {
                while (dr.Read())
                {
                    Supplier at = new Supplier();
                    at.Id = (int)dr["Id"];
                    at.Name = dr["Name"].ToString();
                    at.Status = (int)dr["Status"];
                    at.Addr1 = dr["Addr1"].ToString();
                    at.Addr2 = dr["Addr2"].ToString();
                    at.City = dr["City"].ToString();
                    at.State = (int)dr["State"];
                    at.Zip = dr["Zip"].ToString();
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
        /// 新增一个供应商
        /// </summary>
        /// <param name="Supplier">供应商类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败</returns>
        public int InsertSupplier(Supplier Supplier)
        {


            string sqlstr = @"insert into dbo.Supplier(OrderBillId,State,status,desn) 
            values(@OrderBillId,@State,@status,@desn);
            Select @Id=scope_identity()";
            //public int Id
            //public string Name
            //public int Status
            //public string Addr1
            //public string Addr2
            //public string City
            //public int? State
            //public string Zip
            //public string Phone
            //public string Tel
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable
            SqlParameter[] sqlpara = new SqlParameter[10];
            sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
            sqlpara[1] = new SqlParameter("@Name", SqlDbType.VarChar);
            sqlpara[2] = new SqlParameter("@Status", SqlDbType.Int);
            sqlpara[3] = new SqlParameter("@Addr1", SqlDbType.VarChar);
            sqlpara[4] = new SqlParameter("@Addr2", SqlDbType.VarChar);

            sqlpara[5] = new SqlParameter("@City", SqlDbType.VarChar);
            sqlpara[6] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[7] = new SqlParameter("@Zip", SqlDbType.VarChar);
            sqlpara[8] = new SqlParameter("@Phone", SqlDbType.VarChar);
            sqlpara[9] = new SqlParameter("@Tel", SqlDbType.VarChar);

            sqlpara[1].Value = Supplier.Name;
            sqlpara[2].Value = Supplier.Status;
            sqlpara[3].Value = Supplier.Addr1;
            sqlpara[4].Value = Supplier.Addr2;
            sqlpara[5].Value = Supplier.City;
            sqlpara[6].Value = Supplier.State;
            sqlpara[7].Value = Supplier.Zip;
            sqlpara[8].Value = Supplier.Phone;
            sqlpara[9].Value = Supplier.Tel;

            sqlpara[0].Direction = ParameterDirection.Output;

            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            Supplier.Id = (int)sqlpara[0].Value;

            return retval;
        
        
        
        
        
        }


        /// <summary>
        /// 新增一批供应商
        /// </summary>
        /// <param name="Suppliers">供应商类列表 如果成功回填每个Supplier数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败</returns>
        public int InsertSupplier(IList<Supplier> Suppliers)
        {
            int retval = 0;
            foreach (Supplier pr in Suppliers)
            {

                retval += InsertSupplier(pr);


            }
            return retval;
        
        
        
        }


        /// <summary>
        /// 使用id删除供应商
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败 </returns>
        public int DelSupplierById(int id)
        {

            string sqlstr = @"delete dbo.Supplier where id=@id ";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        
        
        }


        /// <summary>
        /// 使用id列表删除一批供应商
        /// </summary>
        /// <param name="lid">供应商id列表</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败 </returns>
       public int DelSupplierById(List<int> lid)
        {
            int retval = 0;
            foreach (int pr in lid)
            {

                retval += DelSupplierById(pr);


            }
            return retval;
       
       
       
       
       }


        /// <summary>
        /// 使用供应商类删除供应商
        /// </summary>
        /// <param name="Supplier">供应商类</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败</returns>
       public int DelSupplier(Supplier Supplier)
       {

           int retval = 0;

           retval += DelSupplierById(Supplier.Id);



           return retval;
       
       }

        /// <summary>
        /// 使用供应商类列表,删除一批供应商
        /// </summary>
        /// <param name="Suppliers">供应商类列表</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败</returns>
       public int DelSupplier(IList<Supplier> Suppliers)
       {
           int retval = 0;
           foreach (Supplier pr in Suppliers)
           {

               retval += DelSupplier(pr);


           }
           return retval;
       
       
       
       }

        /// <summary>
        /// 使用供应商类更新供应商
        /// </summary>
        /// <param name="Supplier">供应商类</param>
        /// <returns>是否成功 大于等于1;0没有此供应商;小于0失败</returns>
       public int UpSupplier(Supplier Supplier)
       {
           string sqlstr = @"update dbo.Supplier set  Name=@Name,Status=@Status,
            Addr1=@Addr1,Addr2=@Addr2,City=@City,State=@State,Zip=@Zip,Phone=@Phone,Tel=@Tel,
            udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";

           //public int Id
           //public string Name
           //public int Status
           //public string Addr1
           //public string Addr2
           //public string City
           //public int? State
           //public string Zip
           //public string Phone
           //public string Tel
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable

           SqlParameter[] sqlpara = new SqlParameter[12];
           sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Status", SqlDbType.Int);
           sqlpara[3] = new SqlParameter("@Addr1", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@Addr2", SqlDbType.VarChar);
           sqlpara[5] = new SqlParameter("@City", SqlDbType.VarChar);
           sqlpara[6] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[7] = new SqlParameter("@Zip", SqlDbType.VarChar);
           sqlpara[8] = new SqlParameter("@Phone", SqlDbType.VarChar);
           sqlpara[9] = new SqlParameter("@Tel", SqlDbType.VarChar);
           sqlpara[10] = new SqlParameter("@IsEnable", SqlDbType.Bit);
           sqlpara[11] = new SqlParameter("@IsVisable", SqlDbType.Bit);

           sqlpara[0].Value = Supplier.Id;
           sqlpara[1].Value = Supplier.Name;
           sqlpara[2].Value = Supplier.Status;
           sqlpara[3].Value = Supplier.Addr1;
           sqlpara[4].Value = Supplier.Addr2;
           sqlpara[2].Value = Supplier.City;
           sqlpara[3].Value = Supplier.State;
           sqlpara[4].Value = Supplier.Zip;
           sqlpara[5].Value = Supplier.Phone;
           sqlpara[6].Value = Supplier.Tel;
           sqlpara[5].Value = Supplier.IsEnable;
           sqlpara[6].Value = Supplier.IsVisable;


           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       
       
       }

        /// <summary>
        /// 使用供应商类列表,更新一批供应商
        /// </summary>
        /// <param name="Suppliers">供应商类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int UpSupplier(IList<Supplier> Suppliers)
       {

           int retval = 0;
           foreach (Supplier pr in Suppliers)
           {

               retval += UpSupplier(pr);


           }
           return retval;
       
       }

    }
}
