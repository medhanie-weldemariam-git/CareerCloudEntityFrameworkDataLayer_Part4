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
    public class CompanyJobSkillRepository : BaseADO, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                        ([Id], [Job], [Skill], [Skill_Level], [Importance])
                        VALUES(@Id, @Job, @Skill, @Skill_Level, @Importance)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Job", poco.Job);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", poco.Importance);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[50000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Company_Job_Skills]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CompanyJobSkillPoco companyJobSkillPoco = new CompanyJobSkillPoco();

                    companyJobSkillPoco.Id = reader.GetGuid(0);
                    companyJobSkillPoco.Job = reader.GetGuid(1);
                    companyJobSkillPoco.Skill = reader.GetString(2);
                    companyJobSkillPoco.SkillLevel = reader.GetString(3);
                    companyJobSkillPoco.Importance = reader.GetInt32(4);
                    companyJobSkillPoco.TimeStamp = (byte[])reader[5];

                    pocos[position] = companyJobSkillPoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyJobSkillPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills] WHERE [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                         SET Job= @Job, Skill= @Skill, Skill_Level= @Skill_Level, 
                                        Importance= @Importance WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Job", poco.Job);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", poco.Importance);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
