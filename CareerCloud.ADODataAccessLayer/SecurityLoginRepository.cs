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
    public class SecurityLoginRepository : BaseADO, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                        ([Id], [Login], [Password], [Created_Date], [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked], [Is_Inactive], [Email_Address], [Phone_Number],
                            [Full_Name], [Force_Change_Password], [Prefferred_Language])
                        VALUES(@Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked,
                                @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Password", poco.Password);
                    command.Parameters.AddWithValue("@Created_Date", poco.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);
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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[50000];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Security_Logins]", conn);
                int position = 0;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SecurityLoginPoco securityLoginPoco = new SecurityLoginPoco();

                    securityLoginPoco.Id = reader.GetGuid(0);
                    securityLoginPoco.Login = reader.GetString(1);
                    securityLoginPoco.Password = reader.GetString(2);
                    securityLoginPoco.Created = reader.GetDateTime(3);

                    if (!reader.IsDBNull(4))
                    {
                        securityLoginPoco.PasswordUpdate = reader.GetDateTime(4);
                    }
                    else
                    {
                        securityLoginPoco.PasswordUpdate = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        securityLoginPoco.AgreementAccepted = reader.GetDateTime(5);
                    }
                    else
                    {
                        securityLoginPoco.AgreementAccepted = null;
                    }
                    securityLoginPoco.IsLocked = reader.GetBoolean(6);
                    securityLoginPoco.IsInactive = reader.GetBoolean(7);
                    securityLoginPoco.EmailAddress = reader.GetString(8);

                    if (!reader.IsDBNull(9))
                    {
                        securityLoginPoco.PhoneNumber = reader.GetString(9);
                    }
                    else
                    {
                        securityLoginPoco.PhoneNumber = null;
                    }

                    
                    securityLoginPoco.FullName = reader.GetString(10);
                    securityLoginPoco.ForceChangePassword = reader.GetBoolean(11);


                    if (!reader.IsDBNull(12))
                    {
                        securityLoginPoco.PrefferredLanguage = reader.GetString(12);
                    }
                    else
                    {
                        securityLoginPoco.PrefferredLanguage = null;
                    }
                    
                    securityLoginPoco.TimeStamp = (byte[])reader[13];

                    pocos[position] = securityLoginPoco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).ToList();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (SecurityLoginPoco poco in items)
                {
                    command.CommandText = @"DELETE FROM [dbo].[Security_Logins] WHERE [Id]= @Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int numRows = command.ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco poco in items)
                {
                    command.CommandText = @"UPDATE [dbo].[Security_Logins]
                                         SET Login= @Login, Password= @Password, Created_Date= @Created_Date, Password_Update_Date=@Password_Update_Date,
                                        Agreement_Accepted_Date= @Agreement_Accepted_Date, Is_Locked=@Is_Locked, Is_Inactive=@Is_Inactive,
                                        Email_Address=@Email_Address, Phone_Number=@Phone_Number, Full_Name=@Full_Name, Force_Change_Password=@Force_Change_Password,
                                        Prefferred_Language=@Prefferred_Language WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Password", poco.Password);
                    command.Parameters.AddWithValue("@Created_Date", poco.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

                    conn.Open();
                    int rowAffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
