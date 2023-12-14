using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api
{
    public class ConnectionString
    {
        public string cs {get; set;}

        public ConnectionString() {
            string server = "cis9cbtgerlk68wl.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "il9c1pkx2hw6914c";
            string port = "3306";
            string username = "zzze8s1cqu0wqly9";
            string password = "o34rik2udcbgjeds";

            cs = $@"server={server};database={database};port={port};username={username};password={password};";
        }
    }
}