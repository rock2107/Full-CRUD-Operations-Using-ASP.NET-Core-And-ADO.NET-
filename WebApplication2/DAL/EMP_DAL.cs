
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public class EMP_DAL
    {
        SqlConnection _Connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("MVCConnectionString");

        }
        public List<Employee> GetAll()
        {
            List<Employee> employeelist = new List<Employee>();
            using (_Connection = new SqlConnection(GetConnectionString()))
            {
                _command = _Connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_EMPS]";
                _Connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.Name = dr["Name"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);

                    employeelist.Add(employee);


                }
                _Connection.Close();

            }
            return employeelist;

        }
        public bool Insert(Employee model)
        {
            int id = 0;
            using (_Connection = new SqlConnection(GetConnectionString()))
            {
                _command = _Connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[dbo].[usp_Insert_EMPS]";

                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@Name", model.Name);
                _command.Parameters.AddWithValue("@DOB", model.DOB);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _Connection.Open();
                id = _command.ExecuteNonQuery();
                _Connection.Close();
            }
            return id > 0 ? true : false;
        }
        public Employee GetbyId(int id)
        {
            Employee employee = new Employee();
            using (_Connection = new SqlConnection(GetConnectionString()))
            {
                _command = _Connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_EMPSById]";
                _command.Parameters.AddWithValue("@Id", id);
                _Connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {

                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.Name = dr["Name"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);



                }
                _Connection.Close();

            }
            return employee;

        }

        public bool Update(Employee model)
        {
            int id = 0;
            using (_Connection = new SqlConnection(GetConnectionString()))
            {
                _command = _Connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Update_EMPSById]";


                _command.Parameters.AddWithValue("@Name", model.Name);
                _command.Parameters.AddWithValue("@DOB", model.DOB);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _Connection.Open();
                id = _command.ExecuteNonQuery();
                _Connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int id = 0;
            using (_Connection = new SqlConnection(GetConnectionString()))
            {
                _command = _Connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Update_EMPSById]";


                _command.Parameters.AddWithValue("@Id", id);
            
           
                _Connection.Open();
                id = _command.ExecuteNonQuery();
                _Connection.Close();
            }
            return id > 0 ? true : false;
        }

    }
}