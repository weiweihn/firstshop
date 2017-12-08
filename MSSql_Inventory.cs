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
    public class MSSql_Inventory:Inventory
    {
            //public int ProductItemId
            //public int? Qty
            //public int? OrderQty
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable
        /// <summary>
        /// 使用id获取库存
        /// </summary>
        /// <param name="id">库存id</param>
        /// <returns>成功返回一个Inventory 否则返回null </returns>
        public Inventory GetInventoryById(int id)
        {
            if (id <= 0) return null;
            string sqlstr = @"select * from dbo.Inventory where ProductItemId=@id";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            Inventory at = new Inventory();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {
                    at.ProductItemId = (int)dr["ProductItemId"];
                    at.Qty = (int)dr["Qty"];
                    at.OrderQty = (int)dr["OrderQty"];
                    at.Cdate = (DateTime)dr["Cdate"];
                    at.Udate = (DateTime)dr["Udate"];
                    at.IsEnable = (Boolean)dr["IsEnable"];
                    at.IsVisable = (Boolean)dr["IsVisable"];
                }
            }
            if (at.ProductItemId == 0 || at.ProductItemId == null)
            {
                return null;

            }
            return at;
        }
        /// <summary>
        /// 使用产品项目id获取库存
        /// </summary>
        /// <param name="itemid">库存名</param>
        /// <returns>成功返回一个Inventory 否则返回null </returns>
        public Inventory GetInventoryByItemId(int itemid)
        {
            if (itemid <= 0) return null;
            string sqlstr = @"select * from dbo.Inventory where ProductItemId=@id";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = itemid;
            Inventory at = new Inventory();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {
                    at.ProductItemId = (int)dr["ProductItemId"];
                    at.Qty = (int)dr["Qty"];
                    at.OrderQty = (int)dr["OrderQty"];
                    at.Cdate = (DateTime)dr["Cdate"];
                    at.Udate = (DateTime)dr["Udate"];
                    at.IsEnable = (Boolean)dr["IsEnable"];
                    at.IsVisable = (Boolean)dr["IsVisable"];
                }
            }
            if (at.ProductItemId == 0 || at.ProductItemId == null)
            {
                return null;

            }
            return at;
        }

        /// <summary>
        /// 获取所有有效库存
        /// </summary>
        /// <returns>Inventory列表</returns>
        public IList<Inventory> GetInventory ()
        {
            string sqlstr = @"select * from dbo.Inventory where  IsEnable=1 and IsVisable=1";

           
            IList<Inventory> al = new List<Inventory>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
            {
                while (dr.Read())
                {
                    Inventory at = new Inventory();
                    at.ProductItemId = (int)dr["id"];
                    at.Qty = (int)dr["ParentId"];
                    at.OrderQty =(int) dr["Name"];
                    at.Cdate = (DateTime)dr["Cdate"];
                    at.Udate = (DateTime)dr["Udate"];
                    at.IsEnable = (Boolean)dr["IsEnable"];
                    at.IsVisable = (Boolean)dr["IsVisable"];
                    al.Add(at);

                    //public int ProductItemId
                    //public int? Qty
                    //public int? OrderQty
                    //public DateTime Cdate
                    //public DateTime Udate
                    //public Boolean IsEnable
                    //public Boolean IsVisable
                }



            }


            return al;
        
        
        
        
        }

        /// <summary>
        /// 新增一个库存
        /// </summary>
        /// <param name="Inventory">库存类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败</returns>
        public int InsertInventory(Inventory Inventory)
        {

            string sqlstr = @"insert into dbo.category(ProductItemId,Qty,OrderQty) 
            values(@ProductItemId,@Qty,@OrderQty)";

            //public int ProductItemId
            //public int? Qty
            //public int? OrderQty
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable
          
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@ProductItemId", SqlDbType.Int);
            sqlpara[1] = new SqlParameter("@Qty", SqlDbType.Int);
            sqlpara[2] = new SqlParameter("@OrderQty", SqlDbType.Int);



            sqlpara[0].Value = Inventory.ProductItemId;
            sqlpara[1].Value = Inventory.Qty;
            sqlpara[2].Value = Inventory.OrderQty;
           

            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            
            return retval;
        
        
        }


        /// <summary>
        /// 新增一批库存
        /// </summary>
        /// <param name="Inventorys">库存类列表 如果成功回填每个Inventory数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败</returns>
        public int InsertInventory(IList<Inventory> Inventorys)
        {
            int retval = 0;
            foreach (Inventory cg in Inventorys)
            {
                retval += InsertInventory(cg);
            }
            return retval;
        
        
        }


        /// <summary>
        /// 使用id删除库存
        /// </summary>
        /// <param name="id">库存id</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败 </returns>
        public int DelInventoryById(int id)
        {

            string sqlstr = @"delete dbo.Inventory where ProductItemId=@id ";


            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;

            int retval = 0;

            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

            return retval;
        
        
        
        }


        /// <summary>
        /// 使用id列表删除一批库存
        /// </summary>
        /// <param name="lid">库存id列表</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败 </returns>
        public int DelInventoryById(List<int> lid)
        {

            int retval = 0;
            foreach (int cg in lid)
            {
                retval += DelInventoryById(cg);
            }
            return retval;
        
        
        
        }

        /// <summary>
        /// 使用产品项目id删除库存
        /// </summary>
        /// <param name="itemid">产品项目id</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败</returns>
        public int DelInventoryByItemId(string itemid)
        {

            string sqlstr = @"delete dbo.Inventory where ProductItemId=@id ";


            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = itemid;

            int retval = 0;

            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);

            return retval;
        
        
        
        }

        /// <summary>
        /// 使用库存类删除库存
        /// </summary>
        /// <param name="Inventory">库存类</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败</returns>
        public int DelInventory(Inventory Inventory)
        { 
        
            int retval = 0;
            retval = DelInventoryById(Inventory.ProductItemId);

            return retval;
        
        
        }

        /// <summary>
        /// 使用库存类列表,删除一批库存
        /// </summary>
        /// <param name="Inventorys">库存类列表</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败</returns>
        public int DelInventory(IList<Inventory> Inventorys)
        {

            int retval = 0;
            foreach (Inventory cg in Inventorys)
            {
                retval += DelInventory(cg);
            }
            return retval;
        
        
        }

        /// <summary>
        /// 使用库存类更新库存
        /// </summary>
        /// <param name="Inventory">库存类</param>
        /// <returns>是否成功 大于等于1;0没有此库存;小于0失败</returns>
        public int UpInventory(Inventory Inventory)
        {
            //public int ProductItemId
            //public int? Qty
            //public int? OrderQty
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable


            string sqlstr = @"update dbo.Inventory set  Qty=@Qty,OrderQty=@OrderQty,
            udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where ProductItemId=@ProductItemId ";

            SqlParameter[] sqlpara = new SqlParameter[4];
            sqlpara[0] = new SqlParameter("@Qty", SqlDbType .Int);
            sqlpara[1] = new SqlParameter("@OrderQty", SqlDbType.Int);
            sqlpara[2] = new SqlParameter("@IsEnable", SqlDbType.Bit);
            sqlpara[3] = new SqlParameter("@IsVisable", SqlDbType.Bit);

            sqlpara[0].Value = Inventory.Qty;
            sqlpara[1].Value = Inventory.OrderQty;
            sqlpara[2].Value = Inventory.IsEnable;
            sqlpara[3].Value = Inventory.IsVisable;
           
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        }

        /// <summary>
        /// 使用库存类列表,更新一批库存
        /// </summary>
        /// <param name="Inventorys">库存类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
        public int UpInventory(IList<Inventory> Inventorys)
        {

            int retval = 0;
            foreach (Inventory cg in Inventorys)
            {
                retval += UpInventory(cg);
            }
            return retval;
        
        
        }





    }
}
