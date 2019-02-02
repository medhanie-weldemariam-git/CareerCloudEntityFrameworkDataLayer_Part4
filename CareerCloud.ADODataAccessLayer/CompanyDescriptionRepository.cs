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
    public class CompanyDescriptionRepository : BaseADO, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                        ([Id], [Company], [LanguageID], [Company_Name], [Company_Description])
                        VALUES(@Id, @Company, @LanguageID, @Company_Name, @Company_Description)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    command.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    command.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Company_Descriptions]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CompanyDescriptionPoco companyDescriptionPoco = new CompanyDescriptionPoco();

                    companyDescriptionPoco.Id = reader.GetGuid(0);
                    companyDescriptionPoco.Company = reader.GetGuid(1);
                    companyDescriptionPoco.LanguageId = reader.GetString(2);
                    companyDescriptionPoco.CompanyName = reader.GetString(3);
                    companyDescriptionPoco.CompanyDescription = reader.GetString(4);
                    companyDescriptionPoco.TimeStamp = (byte[])reader[5];

                    pocos[position] = companyDescriptionPoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyDescriptionPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyDescriptionPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Descriptions] WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                         SET Company= @Company, LanguageID= @LanguageID, Company_Name= @Company_Name, 
                                        Company_Description= @Company_Description WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    command.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    command.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                    command.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
