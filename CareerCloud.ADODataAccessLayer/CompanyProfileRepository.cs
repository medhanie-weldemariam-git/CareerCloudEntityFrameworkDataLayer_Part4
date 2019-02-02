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
    public class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                        ([Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo])
                        VALUES(@Id, @Registration_Date, @Company_Website, @Contact_Phone, @Contact_Name, @Company_Logo)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    command.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    command.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    command.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    command.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[50000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Company_Profiles]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CompanyProfilePoco companyProfilePoco = new CompanyProfilePoco();

                    companyProfilePoco.Id = reader.GetGuid(0);
                    
                    companyProfilePoco.RegistrationDate = reader.GetDateTime(1);
                    if (!reader.IsDBNull(2))
                    {
                        companyProfilePoco.CompanyWebsite = reader.GetString(2);
                    }
                    else
                    {
                        companyProfilePoco.CompanyWebsite = null;
                    }
                    companyProfilePoco.ContactPhone = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                    {
                        companyProfilePoco.ContactName = reader.GetString(4);
                    }
                    else
                    {
                        companyProfilePoco.ContactName = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        companyProfilePoco.CompanyLogo = (byte[])reader[5];
                    }
                    else
                    {
                        companyProfilePoco.CompanyLogo = null;
                    }
                    if (!reader.IsDBNull(6))
                    {
                        companyProfilePoco.TimeStamp = (byte[])reader[6];
                    }
                    else
                    {
                        companyProfilePoco.TimeStamp = null;
                    }
                    

                    pocos[position] = companyProfilePoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyProfilePoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Profiles] WHERE [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                         SET Registration_Date= @Registration_Date, Company_Website= @Company_Website, Contact_Phone= @Contact_Phone, 
                                        Contact_Name= @Contact_Name, Company_Logo=@Company_Logo WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    command.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    command.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    command.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    command.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
