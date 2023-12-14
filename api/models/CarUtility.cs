using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;

namespace api.models
{
    public class CarUtility
    {
        public List<Car> GetAllCars() {
            List<Car> myCars = new List<Car>();
            ConnectionString myConnection = new();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();
            string stm = "SELECT * FROM cars";

            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read()) {
                myCars.Add(new Car() {CarID = rdr.GetInt32(0), CarMakeModel = rdr.GetString(1), Mileage = rdr.GetInt32(2), Date = rdr.GetString(3), Hold = rdr.GetBoolean(4), Sold = rdr.GetBoolean(5)});
            }
            con.Close();
            return myCars;
        }

        public void AddCar(Car myCar){
            ConnectionString myConnection = new();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO cars(CarID, CarMakeModel, Mileage, DateEntered, Hold, Sold) VALUES(@CarID, @CarMakeModel, @Mileage, @Date, @Hold, @Sold)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@CarID", myCar.CarID);
            cmd.Parameters.AddWithValue("@CarMakeModel", myCar.CarMakeModel);
            cmd.Parameters.AddWithValue("@Mileage", myCar.Mileage);
            cmd.Parameters.AddWithValue("@Date", myCar.Date);
            cmd.Parameters.AddWithValue("@Hold", myCar.Hold);
            cmd.Parameters.AddWithValue("@Sold", myCar.Sold);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void UpdateHold(Car myCar){
            ConnectionString myConnection = new();
            int boolVal = 0;
            if(myCar.Hold == true) {
                boolVal = 1;
            }
            else if(myCar.Hold == false) {
                boolVal = 0;
            }
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = $"UPDATE cars SET Hold = '{boolVal}' WHERE CarID = '{myCar.CarID}'";

            using var cmd = new MySqlCommand(stm, con);  

            cmd.ExecuteNonQuery();

            int rowsAffected = cmd.ExecuteNonQuery();
            System.Console.WriteLine($"Rows Affected: {rowsAffected}");

            con.Close();
        }

        public void UpdateSold(Car myCar){
            ConnectionString myConnection = new();
            int boolVal = 0;
            if(myCar.Sold == true) {
                boolVal = 1;
            }
            else if(myCar.Sold == false) {
                boolVal = 0;
            }
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = $"UPDATE cars SET Sold = '{boolVal}' WHERE CarID = '{myCar.CarID}'";

            using var cmd = new MySqlCommand(stm, con);  

            cmd.ExecuteNonQuery();

            int rowsAffected = cmd.ExecuteNonQuery();
            System.Console.WriteLine($"Rows Affected: {rowsAffected}");

            con.Close();
        }
    }
}