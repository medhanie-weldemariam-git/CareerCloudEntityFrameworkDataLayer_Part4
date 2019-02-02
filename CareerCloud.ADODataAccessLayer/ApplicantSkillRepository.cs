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
    public class ApplicantSkillRepository : BaseADO, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantSkillPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                        ([Id], [Applicant], [Skill], [Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year])
                        VALUES(@Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[500];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Applicant_Skills]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApplicantSkillPoco applicantResumePoco = new ApplicantSkillPoco();

                    applicantResumePoco.Id = reader.GetGuid(0);
                    applicantResumePoco.Applicant = reader.GetGuid(1);
                    applicantResumePoco.Skill = reader.GetString(2);
                    applicantResumePoco.SkillLevel = reader.GetString(3);
                    applicantResumePoco.StartMonth = reader.GetByte(4);
                    applicantResumePoco.StartYear = reader.GetInt32(5);
                    applicantResumePoco.EndMonth = reader.GetByte(6);
                    applicantResumePoco.EndYear = reader.GetInt32(7);
                    applicantResumePoco.TimeStamp = (byte[])reader[8];

                    pocos[position] = applicantResumePoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (ApplicantSkillPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Applicant_Skills] WHERE [Id]=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantSkillPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                         SET Applicant= @Applicant, Skill= @Skill, Skill_Level= @Skill_Level, Start_Month= @Start_Month, 
                                            Start_Year= @Start_Year, End_Month= @End_Month, End_Year= @End_Year WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
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
