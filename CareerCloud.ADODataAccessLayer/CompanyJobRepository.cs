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
    public class CompanyJobRepository : BaseADO, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                        ([Id], [Company], [Profile_Created], [Is_Inactive], [Is_Company_Hidden])
                        VALUES(@Id, @Company, @Profile_Created, @Is_Inactive, @Is_Company_Hidden)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            CompanyJobPoco[] pocos = new CompanyJobPoco[2000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Company_Jobs]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CompanyJobPoco companyJobPoco = new CompanyJobPoco();

                    companyJobPoco.Id = reader.GetGuid(0);
                    companyJobPoco.Company = reader.GetGuid(1);
                    companyJobPoco.ProfileCreated = reader.GetDateTime(2);
                    companyJobPoco.IsInactive = reader.GetBoolean(3);
                    companyJobPoco.IsCompanyHidden = reader.GetBoolean(4);
                    companyJobPoco.TimeStamp = (byte[])reader[5];

                    pocos[position] = companyJobPoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyJobPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Jobs] WHERE [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                         SET Company= @Company, Profile_Created= @Profile_Created, Is_Inactive= @Is_Inactive, 
                                        Is_Company_Hidden= @Is_Company_Hidden WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
