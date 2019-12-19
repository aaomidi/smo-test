using System;
using SqlClient = Microsoft.Data.SqlClient;
//using SqlClient = System.Data.SqlClient;
using Smo = Microsoft.SqlServer.Management.Smo;
using Common = Microsoft.SqlServer.Management.Common;

namespace SmoTestPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlClient.SqlConnection(args[0]);

            connection.Open();
            Console.WriteLine("connected");
            var serverConnection = new Common.ServerConnection(connection);
            var server = new Smo.Server(serverConnection);
            var db = new Smo.Database(server, "master");
            Console.WriteLine(db.ToString());
            var results = db.ExecuteWithResults("SELECT * FROM sys.tables");
            DoQuery(db);
            while (true)
            {
                Console.WriteLine("Want to try again?");
                var key = Console.ReadKey(true);
                if (key.KeyChar.Equals('n'))
                {
                    break;
                }
                try
                {
                    DoQuery(db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        static void DoQuery(Smo.Database db)
        {
            Console.WriteLine("Executing...");
            var results = db.ExecuteWithResults("SELECT * FROM sys.tables");
            Console.WriteLine(results.Tables[0].Columns[0].ToString());
            Console.WriteLine(results.Tables[0].Columns[1].ToString());
            Console.WriteLine(results.Tables[0].Columns[2].ToString());
            Console.WriteLine(results.Tables[0].Columns[3].ToString());
        }
    }
}
