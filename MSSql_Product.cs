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
   public  class MSSql_Product:IProduct
    {

        //public int Id
        //public int? CategoryId
        //public int? SupplierId
        //public string Name
        //public decimal? LowPrice
        //public string Version
        //public string Descn  
        //public string Image
        //public int? State
        //public DateTime Cdate  
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable

        /// <summary>
        /// 使用id获取产品
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>成功返回一个Product 否则返回null </returns>
		/// 第一次  提交到  dev ，修改product.cs
        /// 第二次  提交到  dev ，修改product.cs
	   public Product GetProductById(int id)
       {
           if (id <= 0) return null;
           string sqlstr = @"select * from dbo.Product where Id=@id";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           Product at = new Product();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               if (dr.Read())
               {
                   at.Id = (int)dr["Id"];
                   at.CategoryId = (int)dr["CategoryId"];
                   at.SupplierId = (int)dr["SupplierId"];
                   at.Name = dr["Name"].ToString();
                   at.LowPrice = (decimal)dr["LowPrice"];
                   at.Version = dr["Version"].ToString();
                   at.Descn = dr["Descn"].ToString();
                   at.Image = dr["Image"].ToString();
                   at.State = (int)dr["State"];
                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];

                   //public int Id
                   //public int? CategoryId
                   //public int? SupplierId
                   //public string Name
                   //public decimal? LowPrice
                   //public string Version
                   //public string Descn  
                   //public string Image
                   //public int? State
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
        /// 使用分类id获取产品
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <returns>成功返回一个Product列表 否则返回null </returns>
       public IList<Product> GetProductByCategoryId(int categoryId)
       { 
             
            string sqlstr = @"select * from dbo.Product where CategoryId=@categoryId";
            SqlParameter sqlpara = new SqlParameter("@categoryId", SqlDbType.Int);
            sqlpara.Value = categoryId;
            List<Product> ladd = new List<Product>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Product at = new Product();
                    at.Id = (int)dr["Id"];
                   at.CategoryId = (int)dr["CategoryId"];
                   at.SupplierId = (int)dr["SupplierId"];
                   at.Name = dr["Name"].ToString();
                   at.LowPrice = (decimal)dr["LowPrice"];
                   at.Version = dr["Version"].ToString();
                   at.Descn = dr["Descn"].ToString();
                   at.Image = dr["Image"].ToString();
                   at.State = (int)dr["State"];
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
        /// 使用供应商id获取产品
        /// </summary>
        /// <param name="supplierid">供应商id</param>
        /// <returns>成功返回一个Product列表 否则返回null </returns>
       public IList<Product> GetProductBySupplierId(int supplierid)
        {

            string sqlstr = @"select * from dbo.Product where SupplierId=@supplierid";
            SqlParameter sqlpara = new SqlParameter("@supplierid", SqlDbType.Int);
            sqlpara.Value = supplierid;
            List<Product> ladd = new List<Product>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                   Product at = new Product();
                   at.Id = (int)dr["Id"];
                   at.CategoryId = (int)dr["CategoryId"];
                   at.SupplierId = (int)dr["SupplierId"];
                   at.Name = dr["Name"].ToString();
                   at.LowPrice = (decimal)dr["LowPrice"];
                   at.Version = dr["Version"].ToString();
                   at.Descn = dr["Descn"].ToString();
                   at.Image = dr["Image"].ToString();
                   at.State = (int)dr["State"];
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
        /// 使用分类id与供应商id获取产品
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <param name="supplierid">供应商id</param>
        /// <returns>成功返回一个Product列表 否则返回null </returns>
       public IList<Product> GetProductByCIdAndSId(int categoryId, int supplierid)
       {
           string sqlstr = @"select * from dbo.Product where CategoryId=@categoryId and SupplierId=@supplierid";
           SqlParameter sqlpara = new SqlParameter("@supplierid", SqlDbType.Int);
           SqlParameter sqlpara1 = new SqlParameter("@categoryId", SqlDbType.Int);
           sqlpara.Value = supplierid;
           sqlpara1.Value = categoryId;
           IList<Product> ladd = new List<Product>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara, sqlpara1))
           {
               while (dr.Read())
               {
                   Product at = new Product();
                   at.Id = (int)dr["Id"];
                   at.CategoryId = (int)dr["CategoryId"];
                   at.SupplierId = (int)dr["SupplierId"];
                   at.Name = dr["Name"].ToString();
                   at.LowPrice = (decimal)dr["LowPrice"];
                   at.Version = dr["Version"].ToString();
                   at.Descn = dr["Descn"].ToString();
                   at.Image = dr["Image"].ToString();
                   at.State = (int)dr["State"];
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
        /// 获取所有有效产品
        /// </summary>
        /// <returns>Product列表</returns>
       public IList<Product> GetProduct()
       {

           string sqlstr = @"select * from dbo.Product where IsEnable=1 and IsVisable=1";
           
           IList<Product> ladd = new List<Product>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   Product at = new Product();
                   at.Id = (int)dr["Id"];
                   at.CategoryId = (int)dr["CategoryId"];
                   at.SupplierId = (int)dr["SupplierId"];
                   at.Name = dr["Name"].ToString();
                   at.LowPrice = (decimal)dr["LowPrice"];
                   at.Version = dr["Version"].ToString();
                   at.Descn = dr["Descn"].ToString();
                   at.Image = dr["Image"].ToString();
                   at.State = (int)dr["State"];
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
        /// 新增一个产品
        /// </summary>
        /// <param name="Product">产品类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int InsertProduct(Product Product)
       {

           //public int Id
           //public int? CategoryId
           //public int? SupplierId
           //public string Name
           //public decimal? LowPrice
           //public string Version
           //public string Descn  
           //public string Image
           //public int? State
           //public DateTime Cdate  
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           string sqlstr = @"insert into dbo.Orders(CategoryId,SupplierId,Name,LowPrice,Version,Descn,Image,State) 
            values(@CategoryId,@SupplierId,@Name,@LowPrice,@Version,@Descn,@Image,@State);
            Select @Id=scope_identity()";

           SqlParameter[] sqlpara = new SqlParameter[9];
           sqlpara[0] = new SqlParameter("@CategoryId", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@SupplierId", SqlDbType.Int);
           sqlpara[2] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@LowPrice", SqlDbType.Float);
           sqlpara[4] = new SqlParameter("@Version", SqlDbType.VarChar);
           sqlpara[5] = new SqlParameter("@Descn", SqlDbType.VarChar);
           sqlpara[6] = new SqlParameter("@Image", SqlDbType.VarChar);
           sqlpara[7] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[8] = new SqlParameter("@Id", SqlDbType.Int);

           sqlpara[0].Value = Product.CategoryId;
           sqlpara[1].Value = Product.SupplierId;
           sqlpara[2].Value = Product.Name;
           sqlpara[3].Value = Product.LowPrice;
           sqlpara[4].Value = Product.Version;
           sqlpara[5].Value = Product.Descn;
           sqlpara[6].Value = Product.Image;
           sqlpara[7].Value = Product.State;
           sqlpara[8].Direction = ParameterDirection.Output;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           Product.Id = (int)sqlpara[5].Value;

           return retval;
       
       
       
       
       }


        /// <summary>
        /// 新增一批产品
        /// </summary>
        /// <param name="Products">产品类列表 如果成功回填每个Product数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int InsertProduct(IList<Product> Products)
        {
           int retval = 0;
           foreach (Product pr in Products)
           {

               retval += InsertProduct(pr);
           
           
           }
           return retval;
       }

        /// <summary>
        /// 使用id删除产品
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败 </returns>
       public int DelProductById(int id)
       {

           string sqlstr = @"delete dbo.Products where id=@id ";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       }


        /// <summary>
        /// 使用id列表删除一批产品
        /// </summary>
        /// <param name="lid">产品id列表</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败 </returns>
       public int DelProductById(List<int> lid)
       {
           int retval = 0;
           foreach (int pr in lid)
           {

               retval += DelProductById(pr);


           }
           return retval;
       
       
       }

        /// <summary>
        /// 使用分类id删除产品
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int DelProductByCategoryId(int categoryid)
       {
           string sqlstr = @"delete dbo.Products where categoryid=@categoryid ";
           SqlParameter sqlpara = new SqlParameter("@categoryid", SqlDbType.Int);
           sqlpara.Value = categoryid;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       }


        /// <summary>
        /// 使用供应商id删除产品
        /// </summary>
        /// <param name="supplierid">供应商id</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int DelProductBySupplierId(int supplierid)
       {
           string sqlstr = @"delete dbo.Products where supplierid=@supplierid ";
           SqlParameter sqlpara = new SqlParameter("@supplierid", SqlDbType.Int);
           sqlpara.Value = supplierid;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       }
        /// <summary>
        /// 使用分类id与供应商id删除产品
        /// </summary>
        /// <param name="categoryId">分类id</param>
        /// <param name="supplierid">供应商id</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int DelProductByCIdAndSId(int categoryId, int supplierid)
       {

           string sqlstr = @"delete dbo.Products where supplierid=@supplierid and categoryid=@categoryid";
           SqlParameter sqlpara = new SqlParameter("@supplierid", SqlDbType.Int);
           SqlParameter sqlpara1 = new SqlParameter("@categoryid", SqlDbType.Int);
           sqlpara.Value = supplierid;
           sqlpara1.Value = categoryId;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara, sqlpara1);
           return retval;
       
       
       
       }

        /// <summary>
        /// 使用产品类删除产品
        /// </summary>
        /// <param name="Product">产品类</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int DelProduct(Product Product) 
       {
           return DelProductById(Product.Id);
       
       
       }

        /// <summary>
        /// 使用产品类列表,删除一批产品
        /// </summary>
        /// <param name="Products">产品类列表</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int DelProduct(IList<Product> Products)
       {
           int retval = 0;
           foreach (Product pr in Products)
           {

               retval += DelProduct(pr);


           }
           return retval;
       
       
       
       
       }

        /// <summary>
        /// 使用产品类更新产品
        /// </summary>
        /// <param name="Product">产品类</param>
        /// <returns>是否成功 大于等于1;0没有此产品;小于0失败</returns>
       public int UpProduct(Product Product)
       {
           string sqlstr = @"update dbo.Orders set  CategoryId=@CategoryId,SupplierId=@SupplierId,
            Name=@Name,LowPrice=@LowPrice,Version=@Version,Descn=@Descn,Image=@Image,State=@State,udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";
           //public int Id
           //public int? CategoryId
           //public int? SupplierId
           //public string Name
           //public decimal? LowPrice
           //public string Version
           //public string Descn  
           //public string Image
           //public int? State
           //public DateTime Cdate  
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           SqlParameter[] sqlpara = new SqlParameter[11];
           sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@CategoryId", SqlDbType.Int);
           sqlpara[2] = new SqlParameter("@SupplierId", SqlDbType.Int);
           sqlpara[3] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@LowPrice", SqlDbType.Float);
           sqlpara[5] = new SqlParameter("@Version", SqlDbType.VarChar);
           sqlpara[6] = new SqlParameter("@Descn", SqlDbType.VarChar);
           sqlpara[7] = new SqlParameter("@Image", SqlDbType.VarChar);
           sqlpara[8] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[9] = new SqlParameter("@IsEnable", SqlDbType.Bit);
           sqlpara[10] = new SqlParameter("@IsVisable", SqlDbType.Bit);

           sqlpara[0].Value = Product.Id;
           sqlpara[1].Value = Product.CategoryId;
           sqlpara[2].Value = Product.SupplierId;
           sqlpara[3].Value = Product.Name;
           sqlpara[4].Value = Product.LowPrice;
           sqlpara[5].Value = Product.Version;
           sqlpara[6].Value = Product.Descn;
           sqlpara[7].Value = Product.Image;
           sqlpara[8].Value = Product.State;
           sqlpara[9].Value = Product.IsEnable;
           sqlpara[10].Value = Product.IsVisable;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       
       }

        /// <summary>
        /// 使用产品类列表,更新一批产品
        /// </summary>
        /// <param name="Products">产品类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int UpProduct(IList<Product> Products)
       {
           int retval = 0;
           foreach (Product pr in Products)
           {

               retval += UpProduct(pr);


           }
           return retval;
       
       
       
       }








    }
}
