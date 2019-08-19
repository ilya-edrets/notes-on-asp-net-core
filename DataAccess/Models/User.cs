namespace DataAccess.Models
{
    using System;
    using System.Data.SqlClient;
    using Settings;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public static User Find(string login)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Login='{login}'", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        user = new User();
                        user.Id = new Guid(Convert.ToString(dataReader["Id"]));
                        user.Login = Convert.ToString(dataReader["Login"]);
                        user.Password = Convert.ToString(dataReader["Password"]);
                    }
                }

                connection.Close();
            }

            return user;
        }

        public void Insert()
        {
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                string sql = $"Insert Into Users (Id, Login, Password) Values ('{this.Id}', '{this.Login}','{this.Password}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update()
        {
            string connectionString = string.Empty;
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                string sql = $"Update User SET Login='{this.Login}', Password='{this.Password}', Where Id='{this.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
