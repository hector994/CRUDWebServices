using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace Wservices
{
    /// <summary>
    /// Descripción breve de ServicioEstudiantil
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioEstudiantil : System.Web.Services.WebService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public XmlDocument createUser(int id,string nombre, string apellido, int telefono)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "insert into estudiantes (id, nombre, apellido, telefono) Values (@id, @nombre, @apellido, @telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);
                command.Parameters.AddWithValue("@telefono", telefono);
                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlresponse = new XmlDocument();
           
            XmlElement element = xmlresponse.CreateElement("response");
            xmlresponse.AppendChild(element);

            XmlElement responseElement = xmlresponse.CreateElement("message");
            responseElement.InnerText = "datos registrados exitosamente...";
            element.AppendChild(responseElement);

            return xmlresponse;
        }

        [WebMethod]
        public DataSet getUser() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select * from estudiantes";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                connection.Open();
                adapter.Fill(dataSet,"estudiantes");
                return dataSet; 
            }
               
        }

        [WebMethod]
        public XmlDocument updateUser(int id,int nuevoid, string nombre, string apellido, int telefono)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "update estudiantes set id=@nuevoid, nombre=@nombre, apellido=@apellido, telefono=@telefono where id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nuevoid", nuevoid);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);
                command.Parameters.AddWithValue("@telefono", telefono);

                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlresponse = new XmlDocument();

            XmlElement element = xmlresponse.CreateElement("response");
            xmlresponse.AppendChild(element);

            XmlElement responseElement = xmlresponse.CreateElement("message");
            responseElement.InnerText = "datos modificados exitosamente...";
            element.AppendChild(responseElement);

            return xmlresponse;
        }

        [WebMethod]
        public XmlDocument deleteteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "delete from estudiantes where id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlresponse = new XmlDocument();

            XmlElement element = xmlresponse.CreateElement("response");
            xmlresponse.AppendChild(element);

            XmlElement responseElement = xmlresponse.CreateElement("message");
            responseElement.InnerText = "datos modificados exitosamente...";
            element.AppendChild(responseElement);

            return xmlresponse;
        }
    }
}
