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
    public class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                        ([Id], [Login], [Current_Salary], [Current_Rate], [Currency], [Country_Code], [State_Province_Code], 
                        [Street_Address], [City_Town], [Zip_Postal_Code])
                        VALUES(@Id, @Login, @Current_Salary, @Current_Rate, @Currency, 
                        @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", poco.Currency);
                    command.Parameters.AddWithValue("@Country_Code", poco.Country);
                    command.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    command.Parameters.AddWithValue("@Street_Address", poco.Street);
                    command.Parameters.AddWithValue("@City_Town", poco.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    //command.Parameters.AddWithValue("@Time_Stamp", poco.TimeStamp);
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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[500];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Applicant_Profiles]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApplicantProfilePoco applicantProfilePoco = new ApplicantProfilePoco();

                    applicantProfilePoco.Id = reader.GetGuid(0);
                    applicantProfilePoco.Login = reader.GetGuid(1);
                    applicantProfilePoco.CurrentSalary = reader.GetDecimal(2);
                    applicantProfilePoco.CurrentRate = reader.GetDecimal(3);
                    applicantProfilePoco.Currency = reader.GetString(4);
                    applicantProfilePoco.Country = reader.GetString(5);
                    applicantProfilePoco.Province = reader.GetString(6);
                    applicantProfilePoco.Street = reader.GetString(7);
                    applicantProfilePoco.City = reader.GetString(8);
                    applicantProfilePoco.PostalCode = reader.GetString(9);
                    //applicantProfilePoco.TimeStamp = (byte[])reader[10];
                    

                    pocos[position] = applicantProfilePoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles] WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                         SET Login = @Login, Current_Salary = @Current_Salary, Current_Rate = @Current_Rate, Currency=@Currency, 
                                        Country_Code=@Country_Code, State_Province_Code=@State_Province_Code, Street_Address=@Street_Address, 
                                        City_Town=@City_Town, Zip_Postal_Code=@Zip_Postal_Code WHERE Id=@Id";


                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", poco.Currency);
                    command.Parameters.AddWithValue("@Country_Code", poco.Country);
                    command.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    command.Parameters.AddWithValue("@Street_Address", poco.Street);
                    command.Parameters.AddWithValue("@City_Town", poco.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    command.Parameters.AddWithValue("@Id", poco.Id);

                    
                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
