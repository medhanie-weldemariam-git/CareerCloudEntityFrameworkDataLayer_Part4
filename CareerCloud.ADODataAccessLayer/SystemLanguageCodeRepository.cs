using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : BaseADO, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                        ([LanguageID], [Name], [Native_Name])
                        VALUES(@LanguageID, @Name, @Native_Name)";

                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    command.Parameters.AddWithValue("@Name", poco.Name);
                    command.Parameters.AddWithValue("@Native_Name", poco.NativeName);
                    try
                    {
                        conn.Open();
                        int rowAffected = command.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception e) { }
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            //throw new NotImplementedException();
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(name, con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e) { }

            }
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[50000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[System_Language_Codes]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SystemLanguageCodePoco systemLanguageCodePoco = new SystemLanguageCodePoco();

                    systemLanguageCodePoco.LanguageID = reader.GetString(0);
                    systemLanguageCodePoco.Name = reader.GetString(1);
                    systemLanguageCodePoco.NativeName = reader.GetString(2);

                    pocos[position] = systemLanguageCodePoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (SystemLanguageCodePoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[System_Language_Codes] WHERE [LanguageID]= @LanguageID";
                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                         SET Name= @Name, Native_Name=@Native_Name WHERE LanguageID=@LanguageID";

                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    command.Parameters.AddWithValue("@Name", poco.Name);
                    command.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
