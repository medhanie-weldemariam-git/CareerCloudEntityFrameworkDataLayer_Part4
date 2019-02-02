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
    public class CompanyLocationRepository : BaseADO, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                        ([Id], [Company], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code])
                        VALUES(@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    command.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    command.Parameters.AddWithValue("@Street_Address", poco.Street);
                    command.Parameters.AddWithValue("@City_Town", poco.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[50000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Company_Locations]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CompanyLocationPoco companyLocationPoco = new CompanyLocationPoco();

                    companyLocationPoco.Id = reader.GetGuid(0);
                    companyLocationPoco.Company = reader.GetGuid(1);
                    companyLocationPoco.CountryCode = reader.GetString(2);
                    companyLocationPoco.Province = reader.GetString(3);
                    companyLocationPoco.Street = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                    {
                        companyLocationPoco.City = reader.GetString(5);
                    }
                    else
                    {
                        companyLocationPoco.City = null;
                    }

                    if (!reader.IsDBNull(6))
                    {
                        companyLocationPoco.PostalCode = reader.GetString(6);
                    }
                    else
                    {
                        companyLocationPoco.PostalCode = null;
                    }

                    companyLocationPoco.TimeStamp = (byte[])reader[7];

                    pocos[position] = companyLocationPoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyLocationPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Locations] WHERE [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Locations]
                                         SET Company= @Company, Country_Code= @Country_Code, State_Province_Code= @State_Province_Code, 
                                        Street_Address= @Street_Address, City_Town=@City_Town, Zip_Postal_Code=@Zip_Postal_Code WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    command.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    command.Parameters.AddWithValue("@Street_Address", poco.Street);
                    command.Parameters.AddWithValue("@City_Town", poco.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
