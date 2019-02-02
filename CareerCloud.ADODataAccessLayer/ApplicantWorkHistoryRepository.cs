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
    public class ApplicantWorkHistoryRepository : BaseADO, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                        ([Id], [Applicant], [Company_Name], [Country_Code], [Location], [Job_Title], 
                        [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_Year])
                        VALUES(@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month,
                        @Start_Year, @End_Month, @End_Year)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    command.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    command.Parameters.AddWithValue("@Location", poco.Location);
                    command.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    command.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    command.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    command.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", poco.EndYear);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[500];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Applicant_Work_History]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco applicantResumePoco = new ApplicantWorkHistoryPoco();

                    applicantResumePoco.Id = reader.GetGuid(0);
                    applicantResumePoco.Applicant = reader.GetGuid(1);
                    applicantResumePoco.CompanyName = reader.GetString(2);
                    applicantResumePoco.CountryCode = reader.GetString(3);
                    applicantResumePoco.Location = reader.GetString(4);
                    applicantResumePoco.JobTitle = reader.GetString(5);
                    applicantResumePoco.JobDescription = reader.GetString(6);
                    applicantResumePoco.StartMonth = reader.GetInt16(7);
                    applicantResumePoco.StartYear = reader.GetInt32(8);
                    applicantResumePoco.EndMonth = reader.GetInt16(9);
                    applicantResumePoco.EndYear = reader.GetInt32(10);
                    applicantResumePoco.TimeStamp = (byte[])reader[11];

                    pocos[position] = applicantResumePoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History] WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                         SET Applicant= @Applicant, Company_Name= @Company_Name, Country_Code= @Country_Code, 
                                            Location= @Location, Job_Title= @Job_Title, Job_Description= @Job_Description, 
                                            Start_Month= @Start_Month, Start_Year= @Start_Year, End_Month= @End_Month, 
                                            End_Year= @End_Year WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    command.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    command.Parameters.AddWithValue("@Location", poco.Location);
                    command.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    command.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    command.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    command.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    command.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
