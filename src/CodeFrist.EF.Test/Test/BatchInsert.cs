using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using CodeFrist.EF.Test.Domain.Context;
using CodeFrist.EF.Test.Domain.Model;
using NUnit.Framework;

namespace CodeFrist.EF.Test.Test
{
    public class BatchInsert
    {

        public static string BatchAdd(Role entity)
        {
            SqlParameter parameter = new SqlParameter("@RName", SqlDbType.NVarChar, 100);
            parameter.Value = entity.RName;
            var sb = new StringBuilder();
            sb.Append("insert into Role (RName) ");
            sb.AppendFormat("values ('{0}')", parameter.Value);
            return sb.ToString();
        }

        [Test]
        public void Test()
        {
            var sw = new Stopwatch();
            sw.Start();
            using (var db = new CMSDbContext())
            {
                var sql = new StringBuilder();
                for (int i = 0; i < 10000; i++)
                {
                    var role = new Role()
                    {
                        RName = i.ToString()
                    };
                    sql.Append(BatchAdd(role));
                }
                //一次性执行SQL
                db.Database.ExecuteSqlCommand(sql.ToString());
            }
            sw.Stop();
            var date = sw.ElapsedMilliseconds;
            Console.WriteLine("时间：{0}",date);
        }

        [Test()]
        public void SqlBluck()
        {
            var sw = new Stopwatch();
            sw.Start();
            string conStr = ConfigurationManager.ConnectionStrings["openmall"].ConnectionString;
            SqlBulkCopy copy=new SqlBulkCopy(conStr);
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof (Guid));
            table.Columns.Add("RName", typeof(string));
            for (int i = 0; i < 10000; i++)
            {
                DataRow row = table.NewRow();
                row["Id"] = Guid.NewGuid();
                row["RName"] = i;
                table.Rows.Add(row);
            }
            copy.DestinationTableName = "Role";
            copy.WriteToServer(table);

            sw.Stop();
            var date = sw.ElapsedMilliseconds;
            Console.WriteLine("时间：{0}", date);
        }

    }
}