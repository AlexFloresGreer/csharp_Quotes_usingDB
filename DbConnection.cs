using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DbConnection
{
    public class DbConnector
    {
        public static List<Dictionary<string, object>> ExecuteQuery(string queryString)
        {
            string Server = "localhost";
            string Port = "3307";
            string Database = "csharp_quotes";
            string UserId = "root";
            string Password = "root";
            string Connection = $"Server={Server};Port={Port};Database={Database};UserID={UserId};Password={Password};";
            using(MySqlConnection connection = new MySqlConnection(Connection))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand(queryString, connection))
                {
                    // Try to run the query
                    try
                    {
                        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                        Console.WriteLine("0");
                        using(DbDataReader rdr = command.ExecuteReader())
                        {
                            Console.WriteLine("1");
                            // While there are still entries coming back from the database
                            while(rdr.Read())
                            {
                                Dictionary<string, object> dict = new Dictionary<string, object>();
                                Console.WriteLine("2");
                                // Parse the database entries into simple Dictionaries
                                for( int i = 0; i < rdr.FieldCount; i++ ) {
                                    Console.WriteLine(i);
                                    dict.Add(rdr.GetName(i), rdr.GetValue(i));
                                }
                                Console.WriteLine("3");

                                result.Add(dict);
                            }
                            // Return all entries we received
                            return result;
                        }
                    }
                    // If the query could not be executed for any reason
                    catch
                    {
                        // Inform the user that something went wrong
                        Console.WriteLine("something went wrong"); 
                        return new List<Dictionary<string, object>>();
                    }
                }
            }
        }
    }
}