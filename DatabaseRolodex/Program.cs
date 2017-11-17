using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRolodex
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=c:\users\andrew\source\repos\DatabaseRolodex\DatabaseRolodex\Database1.mdf;Integrated Security=True");
            connection.Open();
            while (true)
            {
                Console.WriteLine("Would you like to 1) add a contact to the rolodex or 2) view the rolodex?  Type '1' or '2.'  You can type 'quit' at any time to exit the program.");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "quit")
                {
                    break;
                }
                else if (userInput == "1")
                {
                    Console.WriteLine("What is the name?");
                    string userName = Console.ReadLine();
                    Console.WriteLine("What is his/her phone number.  Type exactly 10 digits.");
                    string userPhoneNumber = Console.ReadLine();
                    SqlCommand addToRolodex = new SqlCommand($"INSERT INTO PhoneRolodex (Name, PhoneNumber) VALUES ('{userName}', '{userPhoneNumber}')", connection);
                    addToRolodex.ExecuteNonQuery();
                }
                else if (userInput == "2")
                {
                    SqlCommand retrieveRolodex = new SqlCommand("SELECT * from PhoneRolodex", connection);
                    SqlDataReader currentRolodex = retrieveRolodex.ExecuteReader();

                    while (currentRolodex.Read())
                    {
                        Console.WriteLine(currentRolodex["Name"] + "--------------" + currentRolodex["PhoneNumber"]);
                    }
                    currentRolodex.Close();
                }
                else
                {
                    Console.WriteLine("You have entered an invalid response. \n\n\n");
                }
            }
            connection.Close();
        }
    }
}
