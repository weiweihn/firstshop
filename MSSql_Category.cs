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
    public  class MSSql_Category:ICategory
    {

        /// <summary>
        /// 使用id获取分类
        /// </summary>
        /// <param name="id">分类id</param>
        /// <returns>成功返回一个Category 否则返回null </returns>
        public Category GetCategoryById(int id) 
        {
            if (id <= 0) return null;
            string sqlstr = @"select * from dbo.Category where id=@id";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            Category at = new Category();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {
                    // public int Id
                    //public int? ParentId
                    //public string Name
                    //public string Descn
                    //public string Image
                    //public int? State
                    //public DateTime Cdate
                    //public DateTime Udate
                    //public Boolean IsEnable
                    //public Boolean IsVisable
                    at.Id = (int)dr["id"];
                    at.ParentId = (int)dr["ParentId"];
                    at.Name = dr["Name"].ToString();
                    at.Descn = dr["Descn"].ToString();
                    at.Image = dr["Image"].ToString();
                    at.State = (int)dr["State"];
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
        /// 使用父分类获取子分类
        /// </summary>
        /// <param name="category">用户名</param>
        /// <returns>成功返回一个Category列表 否则返回null </returns>
        public IList<Category> GetChildCategory(Category category) 
        {

            string sqlstr = @"select * from dbo.Category where ParentId=@Id and IsEnable=1 and IsVisable=1";

            SqlParameter sqlpara = new SqlParameter("@Id",SqlDbType.Int);
            sqlpara.Value = category.Id;

            IList<Category> al = new List<Category>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                { // public int Id
                    //public int? ParentId
                    //public string Name
                    //public string Descn
                    //public string Image
                    //public int? State
                    //public DateTime Cdate
                    //public DateTime Udate
                    //public Boolean IsEnable
                    //public Boolean IsVisable
                    Category at = new Category();
                    at.Id = (int)dr["id"];
                    at.ParentId = (int)dr["ParentId"];
                    at.Name = dr["Name"].ToString();
                    at.Descn = dr["Descn"].ToString();
                    at.Image = dr["Image"].ToString();
                    at.State =(int) dr["State"];
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
        /// 获取所有有效分类
        /// </summary>
        /// <returns>Category列表</returns>
        public IList<Category> GetCategory()
        {

            string sqlstr = @"select * from dbo.Category where  IsEnable=1 and IsVisable=1";

           
            IList<Category> al = new List<Category>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
            {
                while (dr.Read())
                { // public int Id
                    //public int? ParentId
                    //public string Name
                    //public string Descn
                    //public string Image
                    //public int? State
                    //public DateTime Cdate
                    //public DateTime Udate
                    //public Boolean IsEnable
                    //public Boolean IsVisable
                    Category at = new Category();
                    at.Id = (int)dr["id"];
                    at.ParentId = (int)dr["ParentId"];
                    at.Name = dr["Name"].ToString();
                    at.Descn = dr["Descn"].ToString();
                    at.Image = dr["Image"].ToString();
                    at.State = (int)dr["State"];
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
        /// 新增一个分类
        /// </summary>
        /// <param name="category">分类类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int InsertCategory(Category category)
       {
           string sqlstr = @"insert into dbo.category(ParentId,Name,Descn,Image,State) 
            values(@ParentId,@Name,@Descn,@Image,@State);
            Select @Id=scope_identity()";

           // public int Id
           //public int? ParentId
           //public string Name
           //public string Descn
           //public string Image
           //public int? State
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable


           SqlParameter[] sqlpara = new SqlParameter[6];
           sqlpara[0] = new SqlParameter("@ParentId",SqlDbType.Int);
           sqlpara[1] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Descn", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Image", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[5] = new SqlParameter("@Id", SqlDbType.Int);


           sqlpara[0].Value = category.ParentId;
           sqlpara[1].Value = category.Name;
           sqlpara[2].Value = category.Descn;
           sqlpara[3].Value = category.Image;
           sqlpara[4].Value = category.State;
           sqlpara[5].Direction = ParameterDirection.Output;

           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           category.Id = (int)sqlpara[5].Value;

           return retval;
       }

        /// <summary>
        /// 新增一批分类
        /// </summary>
        /// <param name="lcategory">用户类列表 如果成功回填每个Category数据类id</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int InsertCategory(IList<Category> lcategory)
       {
           int retval = 0;
           foreach (Category cg in lcategory)
           {
               retval += InsertCategory(cg);
           }
           return retval;
       }


        /// <summary>
        /// 使用id删除分类
        /// </summary>
        /// <param name="id">分类id</param>
        /// <returns>是否成功 大于等于1;0没有此分类;小于0失败 </returns>
       public int DelCategoryById(int id)
       {
           string sqlstr = @"delete dbo.Category where id=@id ";


           SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
           sqlpara.Value = id;

           int retval = 0;

           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

           return retval;
       
       
       
       
       }
        /// <summary>
        /// 使用父分类删除子分类
        /// </summary>
        /// <param name="category">父分类</param>
        /// <returns>是否成功 大于等于1;0没有此分类;小于0失败</returns>
       public int DelCategoryByParent(Category category)
       {
           string sqlstr = @"delete dbo.Category where ParentId=@Id ";
           SqlParameter sqlpara = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara.Value = category.Id;

           int retval = 0;

           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

           return retval;
       
       
       
       }

        /// <summary>
        /// 使用id列表,删除一批分类
        /// </summary>
        /// <param name="lid">id列表</param>
        /// <returns>是否成功 大于等于1;0没有此分类;小于0失败</returns>
       public int DelCategory(IList<int> lid)
       {
           int retval = 0;
           foreach (int cg in lid)
           {
               retval += DelCategoryById(cg);
           }
           return retval;
       
       
       
       }


        /// <summary>
        /// 使用分类类更新分类
        /// </summary>
        /// <param name="Category">分类类</param>
        /// <returns>是否成功 大于等于1;0没有此分类;小于0失败</returns>
       public int UpCategory(Category Category)
       {
           // public int Id
           //public int? ParentId
           //public string Name
           //public string Descn
           //public string Image
           //public int? State
           //public DateTime Cdate
           //public DateTime Udate
           //public Boolean IsEnable
           //public Boolean IsVisable

           string sqlstr = @"update dbo.Category set  ParentId=@ParentId,Name=@Name,
            Descn=@Descn,Image=@Image,State=@State,udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";

           SqlParameter[] sqlpara = new SqlParameter[8];
           sqlpara[0] = new SqlParameter("@ParentId", SqlDbType.VarChar);
           sqlpara[1] = new SqlParameter("@Name", SqlDbType.VarChar);
           sqlpara[2] = new SqlParameter("@Descn", SqlDbType.VarChar);
           sqlpara[3] = new SqlParameter("@Image", SqlDbType.VarChar);
           sqlpara[4] = new SqlParameter("@State", SqlDbType.Int);
           sqlpara[5] = new SqlParameter("@Id", SqlDbType.Int);
           sqlpara[6] = new SqlParameter("@IsEnable", SqlDbType.Bit);
           sqlpara[7] = new SqlParameter("@IsVisable", SqlDbType.Bit);

           sqlpara[0].Value = Category.ParentId;
           sqlpara[1].Value = Category.Name;
           sqlpara[2].Value = Category.Descn;
           sqlpara[3].Value = Category.Image;
           sqlpara[4].Value = Category.State;
           sqlpara[5].Value = Category.Id;
           sqlpara[6].Value = Category.IsEnable;
           sqlpara[7].Value = Category.IsVisable;
           int retval = 0;
           retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
           return retval;
     
       }

        /// <summary>
        /// 使用分类类列表,更新一批分类
        /// </summary>
        /// <param name="lCategory">用户类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
       public int UpCategory(IList<Category> lCategory)
       {

           int retval = 0;
           foreach (Category cg in lCategory)
           {
               retval += UpCategory(cg);
           }
           return retval;
       
       
       
       
       }






    }
}
