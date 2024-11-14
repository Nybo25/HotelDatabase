using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HotelDBConnection
{
    public class Hotel
    {
        public int Hotel_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Hotel_No: {Hotel_No}, Name: {Name}, Address: {Address}";
        }
    }

    public class Facility
    {
        public int Facility_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Facility_Id: {Facility_Id}, Name: {Name}, Description: {Description}";
        }
    }

    public class DBClient
    {
        //Erstat min connection string med din egen
        private string connectionString = "Data Source=DESKTOP-KGRT8UK\\SQLEXPRESS;Initial Catalog=HotelDatabase;Integrated Security=True;Encrypt=False";

        public void Start()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection established.\n");

                    bool running = true;
                    while (running)
                    {
                        Console.WriteLine("HOTEL MANAGEMENT SYSTEM");
                        Console.WriteLine("1. List All Hotels");
                        Console.WriteLine("2. Create Hotel");
                        Console.WriteLine("3. Update Hotel");
                        Console.WriteLine("4. Delete Hotel");
                        Console.WriteLine("5. List All Facilities");
                        Console.WriteLine("6. Create Facility");
                        Console.WriteLine("7. Update Facility");
                        Console.WriteLine("8. Delete Facility");
                        Console.WriteLine("9. Show Hotel with Facilities");
                        Console.WriteLine("10. Exit");
                        Console.Write("Choose an option: ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                ListAllHotels(connection);
                                break;
                            case "2":
                                InsertHotel(connection);
                                break;
                            case "3":
                                UpdateHotel(connection);
                                break;
                            case "4":
                                DeleteHotel(connection);
                                break;
                            case "5":
                                ListAllFacilities(connection);
                                break;
                            case "6":
                                InsertFacility(connection);
                                break;
                            case "7":
                                UpdateFacility(connection);
                                break;
                            case "8":
                                DeleteFacility(connection);
                                break;
                            case "9":
                                ShowHotelsWithFacilities(connection);
                                break;
                            case "10":
                                running = false;
                                break;
                            default:
                                Console.WriteLine("Invalid option. Try again.");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private void ListAllHotels(SqlConnection connection)
        {
            string query = "SELECT * FROM Hotel";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("\nHotels:");
                while (reader.Read())
                {
                    Console.WriteLine($"Hotel_No: {reader["Hotel_No"]}, Name: {reader["Name"]}, Address: {reader["Address"]}");
                }
                Console.WriteLine();
            }
        }

        private void InsertHotel(SqlConnection connection)
        {
            Console.Write("Enter Hotel Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Hotel Address: ");
            string address = Console.ReadLine();
            int hotelNo = GetMaxHotelNo(connection) + 1;

            string query = "INSERT INTO Hotel (Hotel_No, Name, Address) VALUES (@Hotel_No, @Name, @Address)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Hotel_No", hotelNo);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Address", address);

            command.ExecuteNonQuery();
            Console.WriteLine("Hotel created successfully.\n");
        }

        private void UpdateHotel(SqlConnection connection)
        {
            Console.Write("Enter Hotel_No to update: ");
            int hotelNo = int.Parse(Console.ReadLine());
            Console.Write("Enter New Hotel Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New Hotel Address: ");
            string address = Console.ReadLine();

            string query = "UPDATE Hotel SET Name = @Name, Address = @Address WHERE Hotel_No = @Hotel_No";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Hotel_No", hotelNo);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Address", address);

            command.ExecuteNonQuery();
            Console.WriteLine("Hotel updated successfully.\n");
        }

        private void DeleteHotel(SqlConnection connection)
        {
            Console.Write("Enter Hotel_No to delete: ");
            int hotelNo = int.Parse(Console.ReadLine());

            string query = "DELETE FROM Hotel WHERE Hotel_No = @Hotel_No";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Hotel_No", hotelNo);

            command.ExecuteNonQuery();
            Console.WriteLine("Hotel deleted successfully.\n");
        }

        private void ListAllFacilities(SqlConnection connection)
        {
            string query = "SELECT * FROM Facility";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("\nFacilities:");
                while (reader.Read())
                {
                    Console.WriteLine($"Facility_Id: {reader["Facility_Id"]}, Name: {reader["Name"]}, Description: {reader["Description"]}");
                }
                Console.WriteLine();
            }
        }

        private void InsertFacility(SqlConnection connection)
        {
            Console.Write("Enter Facility Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Facility Description: ");
            string description = Console.ReadLine();
            int facilityId = GetMaxFacilityId(connection) + 1;

            string query = "INSERT INTO Facility (Facility_Id, Name, Description) VALUES (@Facility_Id, @Name, @Description)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Facility_Id", facilityId);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);

            command.ExecuteNonQuery();
            Console.WriteLine("Facility created successfully.\n");
        }

        private void UpdateFacility(SqlConnection connection)
        {
            Console.Write("Enter Facility_Id to update: ");
            int facilityId = int.Parse(Console.ReadLine());
            Console.Write("Enter New Facility Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New Facility Description: ");
            string description = Console.ReadLine();

            string query = "UPDATE Facility SET Name = @Name, Description = @Description WHERE Facility_Id = @Facility_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Facility_Id", facilityId);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);

            command.ExecuteNonQuery();
            Console.WriteLine("Facility updated successfully.\n");
        }

        private void DeleteFacility(SqlConnection connection)
        {
            Console.Write("Enter Facility_Id to delete: ");
            int facilityId = int.Parse(Console.ReadLine());

            string query = "DELETE FROM Facility WHERE Facility_Id = @Facility_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Facility_Id", facilityId);

            command.ExecuteNonQuery();
            Console.WriteLine("Facility deleted successfully.\n");
        }

        private void ShowHotelsWithFacilities(SqlConnection connection)
        {
            string query = @"
                SELECT h.Name AS HotelName, f.Name AS FacilityName
                FROM Hotel_Facility hf
                JOIN Hotel h ON hf.Hotel_No = h.Hotel_No
                JOIN Facility f ON hf.Facility_Id = f.Facility_Id
                ORDER BY h.Name, f.Name";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("\nHotels with Facilities:");
                while (reader.Read())
                {
                    Console.WriteLine($"Hotel: {reader["HotelName"]}, Facility: {reader["FacilityName"]}");
                }
                Console.WriteLine();
            }
        }

        private int GetMaxHotelNo(SqlConnection connection)
        {
            string query = "SELECT MAX(Hotel_No) FROM Hotel";
            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        private int GetMaxFacilityId(SqlConnection connection)
        {
            string query = "SELECT MAX(Facility_Id) FROM Facility";
            SqlCommand command = new SqlCommand(query, connection);
            object result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DBClient client = new DBClient();
            client.Start();
        }
    }
}

