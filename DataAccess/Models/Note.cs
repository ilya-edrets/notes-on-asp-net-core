namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Infrastructure;

    public class Note
    {
        public Note(Guid userId)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string Text { get; set; }

        public static List<Note> GetAllByUserId(Guid userId)
        {
            var result = new List<Note>();
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Notes WHERE UserId='{userId}'", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var note = new Note(userId);
                        note.Id = new Guid(Convert.ToString(dataReader["Id"]));
                        note.Text = Convert.ToString(dataReader["Text"]);

                        result.Add(note);
                    }
                }

                connection.Close();
            }

            return result;
        }

        public static Note Find(Guid id)
        {
            Note note = null;
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Notes WHERE Id='{id}'", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        var userId = new Guid(Convert.ToString(dataReader["UserId"]));
                        note = new Note(userId);
                        note.Id = new Guid(Convert.ToString(dataReader["Id"]));
                        note.Text = Convert.ToString(dataReader["Text"]);
                    }
                }

                connection.Close();
            }

            return note;
        }

        public void Insert()
        {
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                string sql = $"INSERT INTO Notes (Id, UserId, Text) VALUES ('{this.Id}', '{this.UserId}','{this.Text}')";
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
                string sql = $"UPDATE Notes SET Text='{this.Text}' WHERE Id='{this.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Delete()
        {
            string connectionString = string.Empty;
            using (SqlConnection connection = new SqlConnection(Settings.Current.ConnectionString))
            {
                string sql = $"DELETE FROM Notes WHERE Id='{this.Id}'";
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
