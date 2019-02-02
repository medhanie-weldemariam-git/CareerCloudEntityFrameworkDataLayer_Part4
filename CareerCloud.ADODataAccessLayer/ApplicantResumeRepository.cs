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
    public class ApplicantResumeRepository : BaseADO, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantResumePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                        ([Id], [Applicant], [Resume], [Last_Updated])
                        VALUES(@Id, @Applicant, @Resume, @Last_Updated)";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Resume", poco.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            ApplicantResumePoco[] pocos = new ApplicantResumePoco[500];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Applicant_Resumes]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApplicantResumePoco applicantResumePoco = new ApplicantResumePoco();

                    applicantResumePoco.Id = reader.GetGuid(0);
                    applicantResumePoco.Applicant = reader.GetGuid(1);
                    applicantResumePoco.Resume = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                    {
                        applicantResumePoco.LastUpdated = reader.GetDateTime(3);
                    }
                    else
                    {
                        applicantResumePoco.LastUpdated = null;
                    }
                    

                    pocos[position] = applicantResumePoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (ApplicantResumePoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes] WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantResumePoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                         SET Applicant = @Applicant, Resume = @Resume, Last_Updated= @Last_Updated WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Resume", poco.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
                    command.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
