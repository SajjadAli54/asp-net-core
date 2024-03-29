﻿using MyStore.Pages.Clients;
using System.Data.SqlClient;

namespace MyStore.Model
{
    public class MyDatabase
    {
        private string connectionString =
            "Data Source=(LocalDB)\\V12.0;Initial Catalog=mystore;Integrated Security=True";
        private string tableName = "clients";


        public Client Get(int id)
        {
            var client =  new Client();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM {tableName} where id = {id}";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        setClient(client, reader);
                    }
                }
            }

            return client;
        }

        public List<Client> GetAll()
        {
            var list = new List<Client>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM {tableName}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var client = new Client();
                            setClient(client, reader);
                            list.Add(client);
                        } 
                    }
                }
            }
            var reversed = list.Reverse<Client>().ToList<Client>();
            return reversed;
        }
        public void Insert(Client client)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"INSERT INTO {tableName}(name, email, phone, address) " +
                    $"VALUES ('{client.Name}', '{client.Email}', '{client.Phone}', '{client.Address}')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }


        }

        public void Update(int id, Client client) {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"UPDATE {tableName} SET name='{client.Name}', email='{client.Email}', phone='{client.Phone}', address='{client.Address}' WHERE id={id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = $"DELETE FROM {tableName} WHERE id={id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void setClient(Client client, SqlDataReader reader) {
            client.Id = reader.GetInt32(0);
            client.Name = reader.GetString(1);
            client.Email = reader.GetString(2);
            client.Phone = reader.GetString(3);
            client.Address = reader.GetString(4);
            client.Create_At = reader.GetDateTime(5);
        }
    }
}
