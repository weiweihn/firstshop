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
    public class MSSql_Remark:IRemark
    {
        //public int Id
        //public int OrderBillId
        //public int State
        //public int status
        //public string desn
        //public DateTime Cdate
        //public DateTime Udate
        //public Boolean IsEnable
        //public Boolean IsVisable
        /// <summary>
        /// 使用id获取评论
        /// </summary>
        /// <param name="id">评论id</param>
        /// <returns>成功返回一个Remark 否则返回null </returns>
        public Remark GetRemarkById(int id) 
        {
            if (id <= 0) return null;
            string sqlstr = @"select * from dbo.Remark where Id=@id";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            Remark at = new Remark();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                if (dr.Read())
                {
                    at.Id = (int)dr["Id"];
                    at.OrderBillId = (int)dr["UserName"];
                    at.State = (int)dr["ItemId"];
                    at.status = (int)dr["Name"];
                    at.desn = dr["Price"].ToString();
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
        /// 使用订单id获取评论
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns>成功返回一个Remark列表 否则返回null </returns>
        public IList<Remark> GetRemarkByOrderId(int orderid)
        {
            string sqlstr = @"select * from dbo.Remark where OrderBillId=@orderid and IsVisable=1 and IsEnable=1";
            SqlParameter sqlpara = new SqlParameter("@orderid", SqlDbType.Int);
            sqlpara.Value = orderid;
            List<Remark> ladd = new List<Remark>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara))
            {
                while (dr.Read())
                {
                    Remark at = new Remark();
                    at.Id = (int)dr["Id"];
                    at.OrderBillId = (int)dr["UserName"];
                    at.State = (int)dr["ItemId"];
                    at.status = (int)dr["Name"];
                    at.desn = dr["Price"].ToString();
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
        /// 获取所有有效评论
        /// </summary>
        /// <returns>Remark列表</returns>
        public IList<Remark> GetRemark()
        {
            string sqlstr = @"select * from dbo.Remark where IsEnable=1 and IsVisable=1";

            List<Remark> ladd = new List<Remark>();
            using (SqlDataReader dr = MSSqlHelp.ExecuteReader(MSSqlHelp.DataserverString, CommandType.Text, sqlstr))
            {
                while (dr.Read())
                {
                   
                    Remark at = new Remark();
                    at.Id = (int)dr["Id"];
                    at.OrderBillId = (int)dr["UserName"];
                    at.State = (int)dr["ItemId"];
                    at.status = (int)dr["Name"];
                    at.desn = dr["Price"].ToString();
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
        /// 新增一个评论
        /// </summary>
        /// <param name="Remark">评论类,如果成功回填id</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败</returns>
        public int InsertRemark(Remark Remark)
        {
            string sqlstr = @"insert into dbo.Remark(OrderBillId,State,status,desn) 
            values(@OrderBillId,@State,@status,@desn);
            Select @Id=scope_identity()";
            //public int Id
            //public int OrderBillId
            //public int State
            //public int status
            //public string desn
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable
            SqlParameter[] sqlpara = new SqlParameter[5];
            sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
            sqlpara[1] = new SqlParameter("@OrderBillId", SqlDbType.VarChar);
            sqlpara[2] = new SqlParameter("@State", SqlDbType.VarChar);
            sqlpara[3] = new SqlParameter("@status", SqlDbType.Float);
            sqlpara[4] = new SqlParameter("@desn", SqlDbType.VarChar);



            sqlpara[1].Value = Remark.OrderBillId;
            sqlpara[2].Value = Remark.State;
            sqlpara[3].Value = Remark.status;
            sqlpara[4].Value = Remark.desn;
           

            sqlpara[0].Direction = ParameterDirection.Output;

            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            Remark.Id = (int)sqlpara[0].Value;

            return retval;
       
        
        
        
        }


        /// <summary>
        /// 新增一批评论
        /// </summary>
        /// <param name="Remarks">评论类列表 如果成功回填每个Remark数据类id</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败</returns>
        public int InsertRemark(IList<Remark> Remarks)
        {
            int retval = 0;
            foreach (Remark pr in Remarks)
            {

                retval += InsertRemark(pr);


            }
            return retval;
        
        
        
        }


        /// <summary>
        /// 使用id删除评论
        /// </summary>
        /// <param name="id">评论id</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败 </returns>
        public int DelRemarkById(int id)
        {

            string sqlstr = @"delete dbo.Remark where id=@id ";
            SqlParameter sqlpara = new SqlParameter("@id", SqlDbType.Int);
            sqlpara.Value = id;
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        }


        /// <summary>
        /// 使用id列表删除一批评论
        /// </summary>
        /// <param name="lid">评论id列表</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败 </returns>
        public int DelRemarkById(List<int> lid)
        {
            int retval = 0;
            foreach (int pr in lid)
            {

                retval += DelRemarkById(pr);


            }
            return retval;
       
        
        
        }

        /// <summary>
        /// 使用订单id删除评论
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败</returns>
        public int DelRemarkByOrderId(int orderid)
        {

            string sqlstr = @"delete dbo.Remark where OrderBillId=@orderid ";
            SqlParameter sqlpara = new SqlParameter("@orderid", SqlDbType.Int);
            sqlpara.Value = orderid;
            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        
        }


        /// <summary>
        /// 使用评论类删除评论
        /// </summary>
        /// <param name="Remark">评论类</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败</returns>
        public int DelRemark(Remark Remark)
        {
            int retval = 0;

            retval += DelRemarkById(Remark.Id);



            return retval;
        
        }

        /// <summary>
        /// 使用评论类列表,删除一批评论
        /// </summary>
        /// <param name="Remarks">评论类列表</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败</returns>
        public int DelRemark(IList<Remark> Remarks)
        {
            int retval = 0;
            foreach (Remark pr in Remarks)
            {

                retval += DelRemark(pr);


            }
            return retval;
        
        
        }

        /// <summary>
        /// 使用评论类更新评论
        /// </summary>
        /// <param name="Remark">评论类</param>
        /// <returns>是否成功 大于等于1;0没有此评论;小于0失败</returns>
        public int UpRemark(Remark Remark)
        {
            string sqlstr = @"update dbo.Profilebill set  OrderBillId=@OrderBillId,State=@State,
            status=@status,desn=@desn,udate=getdate(),IsEnable=@IsEnable,IsVisable=@IsVisable  
            where id=@Id ";

            //public int Id
            //public int OrderBillId
            //public int State
            //public int status
            //public string desn
            //public DateTime Cdate
            //public DateTime Udate
            //public Boolean IsEnable
            //public Boolean IsVisable

            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@Id", SqlDbType.Int);
            sqlpara[1] = new SqlParameter("@OrderBillId", SqlDbType.Int);
            sqlpara[2] = new SqlParameter("@State", SqlDbType.Int);
            sqlpara[3] = new SqlParameter("@status", SqlDbType.Int);
            sqlpara[4] = new SqlParameter("@desn", SqlDbType.VarChar);
            sqlpara[5] = new SqlParameter("@IsEnable", SqlDbType.Bit);
            sqlpara[6] = new SqlParameter("@IsVisable", SqlDbType.Bit);

            sqlpara[0].Value = Remark.Id;
            sqlpara[1].Value = Remark.OrderBillId;
            sqlpara[2].Value = Remark.State;
            sqlpara[3].Value = Remark.status;
            sqlpara[4].Value = Remark.desn;
            sqlpara[5].Value = Remark.IsEnable;
            sqlpara[6].Value = Remark.IsVisable;

            int retval = 0;
            retval = (int)MSSqlHelp.ExecuteNonQuery(MSSqlHelp.DataserverString, CommandType.Text, sqlstr, sqlpara);
            return retval;
        
        
        
        
        
        }


        /// <summary>
        /// 使用评论类列表,更新一批评论
        /// </summary>
        /// <param name="Remarks">评论类列表</param>
        /// <returns>是否成功 大于等于1;小于0失败</returns>
        public int UpRemark(IList<Remark> Remarks)
        {

            int retval = 0;
            foreach (Remark pr in Remarks)
            {

                retval += UpRemark(pr);


            }
            return retval;
        
        
        
        }


    }
}
