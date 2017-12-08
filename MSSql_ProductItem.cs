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
   public class MSSql_ProductItem:IProductItem
    {
        //public int Id
        //public int Productid
        //public string Spec
        //public string Color
        //public decimal? Price
        //public string Descn
        //public string Image
        //public int? State
        //public DateTime Cdate
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable
        /// <summary>
        /// 使用id获取产品项目
        /// </summary>
        /// <param name="id">产品项目id</param>
        /// <returns>成功返回一个ProductItem 否则返回null </returns>
       public ProductItem GetProductItemById(int id)
       {
           if (id <= 0) return null;
           string sqlstr = @"select * from dbo.ProductItem where Id=@id";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           ProductItem at = new ProductItem();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               if (dr.Read())
               {
                   at.Id = (int)dr["Id"];
                   at.Productid = (int)dr["CategoryId"];
                   at.Spec = dr["SupplierId"].ToString();
                   at.Color = dr["Name"].ToString();
                   at.Price = (decimal)dr["LowPrice"];
                   at.Descn = dr["Version"].ToString();
                   at.Image = dr["Image"].ToString();
                   at.State = (int)dr["State"];
                   at.Cdate = (DateTime)dr["Cdate"];
                   at.Udate = (DateTime)dr["Udate"];
                   at.IsEnable = (Boolean)dr["IsEnable"];
                   at.IsVisable = (Boolean)dr["IsVisable"];

                   //public int Id
                   //public int Productid
                   //public string Spec
                   //public string Color
                   //public decimal? Price
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
        /// 使用产品id获取产品项目
        /// </summary>
        /// <param name="productid">产品id</param>
        /// <returns>成功返回一个ProductItem列表 否则返回null </returns>
       public IList<ProductItem> GetProductItemByProductId(int productid)
       {
           string sqlstr = @"select * from dbo.ProductItem where productid=@productid";
           SqlParameter sqlpara = new SqlParameter("@productid", SqlDbType.Int);
           sqlpara.Value = productid;
           List<ProductItem> ladd = new List<ProductItem>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
           {
               while (dr.Read())
               {
                   ProductItem at = new ProductItem();
                   at.Id = (int)dr["Id"];
                   at.Productid = (int)dr["CategoryId"];
                   at.Spec = dr["SupplierId"].ToString();
                   at.Color = dr["Name"].ToString();
                   at.Price = (decimal)dr["LowPrice"];
                   at.Descn = dr["Version"].ToString();
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
        /// 获取所有有效产品项目
        /// </summary>
        /// <returns>ProductItem列表</returns>
       public IList<ProductItem> GetProductItem()
       {

           string sqlstr = @"select * from dbo.ProductItem ";
           List<ProductItem> ladd = new List<ProductItem>();
           using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
           {
               while (dr.Read())
               {
                   ProductItem at = new ProductItem();
                   at.Id = (int)dr["Id"];
                   at.Productid = (int)dr["CategoryId"];
                   at.Spec = dr["SupplierId"].ToString();
                   at.Color = dr["Name"].ToString();
                   at.Price = (decimal)dr["LowPrice"];
                   at.Descn = dr["Version"].ToString();
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
        /// 新增一个产品项目
        /// </summary>
        /// <param name="ProductItem">产品项目类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败</returns>
       public int InsertProductItem(ProductItem ProductItem)
       {
           string sqlstr = @"insert into dbo.Orders(Productid,Spec,Color,Price,Descn,Image,State) 
            values(@Productid,@Spec,@Color,@Price,@Descn,@Image,@State);
            Select @Id=scope_identity()";
           //public int Id
           //public int Productid
           //public string Spec
           //public string Color
           //public decimal? Price
           //public string Descn
           //public string Image
           //public int? State
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           SqlParameter[] sqlpara = new SqlParameter[8];
           sqlpara[0] = new SqlParameter("@Productid", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@Spec", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Color", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Price", SqlDbType.Float);
           sqlpara[4] = new SqlParameter("@Descn", SqlDbType.VarChar);
           sqlpara[5] = new SqlParameter("@Image", SqlDbType.VarChar);
           sqlpara[6] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[7] = new SqlParameter("@Id", SqlDbType.Int);

           sqlpara[0].Value = ProductItem.Productid;
           sqlpara[1].Value = ProductItem.Spec;
           sqlpara[2].Value = ProductItem.Color;
           sqlpara[3].Value = ProductItem.Price;
           sqlpara[4].Value = ProductItem.Descn;
           sqlpara[5].Value = ProductItem.Image;
           sqlpara[6].Value = ProductItem.State;
           sqlpara[7].Direction = ParameterDirection.Output;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           ProductItem.Id = (int)sqlpara[7].Value;

           return retval;
       
       
       }


        /// <summary>
        /// 新增一批产品项目
        /// </summary>
        /// <param name="ProductItems">产品项目类列表 如果成功回填每个ProductItem数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败</returns>
       public int InsertProductItem(IList<ProductItem> ProductItems)
       {
           int retval = 0;
           foreach (ProductItem pr in ProductItems)
           {

               retval += InsertProductItem(pr);


           }
           return retval;
       
       
       }


        /// <summary>
        /// 使用id删除产品项目
        /// </summary>
        /// <param name="id">产品项目id</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败 </returns>
       public int DelProductItemById(int id)
       {

           string sqlstr = @"delete dbo.ProductItem where id=@id ";
           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       }


        /// <summary>
        /// 使用id列表删除一批产品项目
        /// </summary>
        /// <param name="lid">产品项目id列表</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败 </returns>
       public int DelProductItemById(List<int> lid)
       {
           int retval = 0;
           foreach (int sid in lid)
           {

               retval += DelProductItemById(sid);


           }
           return retval;
       
       
       }

        /// <summary>
        /// 使用分类id删除产品项目
        /// </summary>
        /// <param name="productid">产品id</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败</returns>
       public int DelProductItemByProductId(int productid)
       {

           string sqlstr = @"delete dbo.ProductItem where Productid=@productid ";
           SqlParameter sqlpara = new SqlParameter("@productid", SqlDbType.Int);
           sqlpara.Value = productid;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       }




        /// <summary>
        /// 使用产品项目类删除产品项目
        /// </summary>
        /// <param name="ProductItem">产品项目类</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败</returns>
       public int DelProductItem(ProductItem ProductItem)
       {

           int retval = 0;


           retval += DelProductItemById(ProductItem.Id);


           
           return retval;
       
       
       }

        /// <summary>
        /// 使用产品项目类列表,删除一批产品项目
        /// </summary>
        /// <param name="ProductItems">产品项目类列表</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败</returns>
       public int DelProductItem(IList<ProductItem> ProductItems)
       {

           int retval = 0;
           foreach (ProductItem sid in ProductItems)
           {

               retval += DelProductItem(sid);


           }
           return retval;
       
       }

        /// <summary>
        /// 使用产品项目类更新产品项目
        /// </summary>
        /// <param name="ProductItem">产品项目类</param>
        /// <returns>是否成功 大于等于1;0没有此产品项目;小于0失败</returns>
       public int UpProductItem(ProductItem ProductItem)
       {
           string sqlstr = @"update dbo.Orders set  Productid=@Productid,Spec=@Spec,
            Color=@Color,Price=@Price,Descn=@Descn,Image=@Image,State=@State,udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";
           //public int Id
           //public int Productid
           //public string Spec
           //public string Color
           //public decimal? Price
           //public string Descn
           //public string Image
           //public int? State
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable
           SqlParameter[] sqlpara = new SqlParameter[10];
           sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@Productid", SqlDbType.Int);
           sqlpara[2] = new SqlParameter("@Spec", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Color", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@Price", SqlDbType.Float);
           sqlpara[5] = new SqlParameter("@Descn", SqlDbType.VarChar);
           sqlpara[6] = new SqlParameter("@Image", SqlDbType.VarChar);
           sqlpara[7] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[8] = new SqlParameter("@IsEnable", SqlDbType.Bit);
           sqlpara[9] = new SqlParameter("@IsVisable", SqlDbType.Bit);

           sqlpara[0].Value = ProductItem.Id;
           sqlpara[1].Value = ProductItem.Productid;
           sqlpara[2].Value = ProductItem.Spec;
           sqlpara[3].Value = ProductItem.Color;
           sqlpara[4].Value = ProductItem.Price;
          
           sqlpara[5].Value = ProductItem.Descn;
           sqlpara[6].Value = ProductItem.Image;
           sqlpara[7].Value = ProductItem.State;
           sqlpara[8].Value = ProductItem.IsEnable;
           sqlpara[9].Value = ProductItem.IsVisable;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
       
       
       
       }

        /// <summary>
        /// 使用产品项目类列表,更新一批产品项目
        /// </summary>
        /// <param name="ProductItems">产品项目类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int UpProductItem(IList<ProductItem> ProductItems)
       {
           int retval = 0;
           foreach (ProductItem sid in ProductItems)
           {

               retval += UpProductItem(sid);


           }
           return retval;
       
       
       }



    }
}
