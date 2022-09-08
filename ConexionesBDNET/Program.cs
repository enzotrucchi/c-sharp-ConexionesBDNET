using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Para conexión con provider*/
using System.Data;
using System.Data.SqlClient;

using System.Linq.Expressions;

namespace ConexionesBDNET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * ADO.NET con DATA Provider
             * ADO .NET con DATA SET
             * EF (ORM)
             * 
             */
            /*Para conexión con ADO.NET Data Provider*/
            string cadenaConexion = "Data Source=ENZO\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=BDTiendaDigital";

            string query = "SELECT * FROM Producto";
            
            using(SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        Console.WriteLine("{0},{1}", reader[0], reader[1]);
                    }
                    connection.Close();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            /*Para conexión con ADO.NET DataSet*/
            Console.WriteLine("------------------------------");
            var tableAdapter = new MiDataSetTableAdapters.ProductoTableAdapter(); //ta

            var dt = tableAdapter.GetData();

            foreach (MiDataSet.ProductoRow item in dt.Rows)
            {
                Console.WriteLine("{0},{1}", item.id, item.descripcion);
            }

            //Libero los objetos de memoria
            dt.Dispose();
            tableAdapter.Dispose();

            /*Para conexión con EF*/
            Console.WriteLine("------------------------------");

            using (var context = new Model.BDTiendaDigitalEntities())
            {
                var lst = context.Productoes;

                foreach (var item in lst)
                {
                    Console.WriteLine(item.id + " " + item.descripcion);
                }
            }


            


            
            Console.Read();
        }
    }
}
