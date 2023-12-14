using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;


namespace EmployeeManagementSystem.Models{

    public class Repository{


        public List<User> Display(string Role){

            List<User> userlist = new List<User>();


            string? connectionString= getConntection();


            SqlConnection connection = new SqlConnection(connectionString);
        
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from Login where Role='{Role}'",connection);

            dataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                User user = new User();
                user.Username  = Convert.ToString(dr["Username"]);
                user.Name = Convert.ToString(dr["Name"]);
                user.EmailID = Convert.ToString(dr["EmailID"]);
                user.Phoneno = Convert.ToDouble(dr["Phoneno"]);
                user.Designation = Convert.ToString(dr["Designation"]);
                user.age = Convert.ToInt32(dr["Age"]);
                userlist.Add(user);
            }
            connection.Close();
            return userlist;

        }

        public List<Request> RequestDetail(string Username){

            List<Request> requestlist = new List<Request>();


            string? connectionString= getConntection();


            SqlConnection connection = new SqlConnection(connectionString);
        
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from Requests where Username='{Username}'",connection);

            dataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                Request request = new Request();
                request.RequestID = Convert.ToInt32(dr["Sno"]);
                request.Receiver  = Convert.ToString(dr["Receiver"]);
                request.Description = Convert.ToString(dr["Description"]);
                request.Status = Convert.ToString(dr["Status"]);
                requestlist.Add(request);
            }
            connection.Close();
            return requestlist;

        }

        public List<Request> Requestdetail(string Receiver){

            List<Request> requestlist = new List<Request>();


            string? connectionString= getConntection();


            SqlConnection connection = new SqlConnection(connectionString);
        
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from Requests where Receiver='{Receiver}' AND Status='In Progress'",connection);

            dataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                Request request = new Request();
                request.RequestID = Convert.ToInt32(dr["Sno"]);
                request.Username  = Convert.ToString(dr["Username"]);
                request.Description = Convert.ToString(dr["Description"]);
                request.Status = Convert.ToString(dr["Status"]);
                requestlist.Add(request);
            }
            connection.Close();
            return requestlist;

        }
        public List<Request> Requestdetail(string Receiver,string Status){

            List<Request> requestlist = new List<Request>();


            string? connectionString= getConntection();


            SqlConnection connection = new SqlConnection(connectionString);
        
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from Requests where Receiver='{Receiver}' AND Status!='{Status}' ",connection);

            dataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                Request request = new Request();
                request.RequestID = Convert.ToInt32(dr["Sno"]);
                request.Username  = Convert.ToString(dr["Username"]);
                request.Description = Convert.ToString(dr["Description"]);
                request.Status = Convert.ToString(dr["Status"]);
                requestlist.Add(request);
            }
            connection.Close();
            return requestlist;

        }

        public void update(User user){
            SqlConnection connection = new SqlConnection(getConntection());
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand("update Login set Name=@name,Age=@age,Designation=@designation,EmailID=@email,Phoneno=@phoneno where Username=@username",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
            sqlcommand.Parameters.Add("@name",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Name;
            sqlcommand.Parameters.Add("@age",sqlDbType:System.Data.SqlDbType.Int).Value = user.age;
            sqlcommand.Parameters.Add("@designation",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Designation;
            sqlcommand.Parameters.Add("@email",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.EmailID;
            sqlcommand.Parameters.Add("@phoneno",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Phoneno;
            sqlcommand.ExecuteNonQuery();
            return;
        }
        public void UpdateStatus(int RequestID,string Status){
            SqlConnection connection = new SqlConnection(getConntection());
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand("update Requests set Status=@status where Sno=@requestid",connection);
            sqlcommand.Parameters.Add("@requestid",sqlDbType:System.Data.SqlDbType.Int).Value = RequestID;
            sqlcommand.Parameters.Add("@status",sqlDbType:System.Data.SqlDbType.VarChar).Value = Status;
            sqlcommand.ExecuteNonQuery();
            return;
        }

        public User display(string username){

            string? connectionString= getConntection();

            SqlConnection connection = new SqlConnection(connectionString);
        
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from Login where Username='{username}'",connection);

            dataAdapter.Fill(dataTable);
            User user = new User();

            foreach (DataRow dr in dataTable.Rows)
            {
                user.Username  = Convert.ToString(dr["Username"]);
                user.Name = Convert.ToString(dr["Name"]);
                user.EmailID = Convert.ToString(dr["EmailID"]);
                user.Phoneno = Convert.ToDouble(dr["Phoneno"]);
                user.Designation = Convert.ToString(dr["Designation"]);
                user.age = Convert.ToInt32(dr["Age"]);
            }
            return user;

        }

        public List<Project> Viewproject (){

            string? connectionString= getConntection();
            List<Project> projects= new List<Project>();

            SqlConnection connection = new SqlConnection(connectionString);
        
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            DataTable dataTable = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from Project",connection);

            dataAdapter.Fill(dataTable);
            

            foreach (DataRow dr in dataTable.Rows)
            {
                Project project = new Project();
                project.Name  = Convert.ToString(dr["Name"]);
                project.Deadline = Convert.ToString(dr["Deadline"]);
                project.Manager = Convert.ToString(dr["Manager"]);
                project.Document = Convert.ToString(dr["Document"]);
                projects.Add(project);
            }
            return projects;

        }

        public string ValidUser(User user){
            using(SqlConnection connection = new SqlConnection(getConntection())){
                connection.Open();
                SqlCommand sqlcommand = new SqlCommand($"select count(*) from Login where Username='{user.Username}'",connection);
                SqlCommand sqlcommand1 = new SqlCommand($"select count(*) from Login where Username='{user.EmailID}'",connection);
                if((Convert.ToInt32(sqlcommand.ExecuteScalar()))==1){
                    return "Exist username";
                }
                else if((Convert.ToInt32(sqlcommand1.ExecuteScalar()))==1){
                    return "Exist Emailid";
                }
                else{
                    return "Valid";
                }
            }
        }

        public string GetRole(User user){
            using(SqlConnection connection = new SqlConnection(getConntection())){
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"select Role from Login where Username=@username",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
            string? role = Convert.ToString(sqlcommand.ExecuteScalar());
            return role;  
        }
        }


        public bool login(User user){

            SqlConnection connection = new SqlConnection(getConntection());
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"select count(*) from Login where Username=@username and Password=@password",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
            sqlcommand.Parameters.Add("@password",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Password;
            if(Convert.ToInt32(sqlcommand.ExecuteScalar())==1){
                    return true;
                }
            return false;
        }

        // public bool signup(NewUser newUser){
        //     bool flag = true;
        //     List<User> userlist = Display();
        //     foreach(User data in userlist){
        //         if(data.Username.Equals(newUser.Username)){
        //             flag=false;
        //         }
        //     }
        //     if(newUser.Password.Equals(newUser.confirmpassword)==false){
        //         flag=false;
        //     }
        //     return flag;
        // }

        public void insert(User user){

            string? connectionString= getConntection();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlcommand = new SqlCommand("insert into Login values(@username,@password,@role,@name,@age,@designation,@emailid,@phoneno)",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
            sqlcommand.Parameters.Add("@password",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Password;
            sqlcommand.Parameters.Add("@role",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Role;
            sqlcommand.Parameters.Add("@name",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Name;
            sqlcommand.Parameters.Add("@age",sqlDbType:System.Data.SqlDbType.Int).Value = user.age;
            sqlcommand.Parameters.Add("@designation",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Designation;
            sqlcommand.Parameters.Add("@emailid",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.EmailID;
            sqlcommand.Parameters.Add("@phoneno",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Phoneno;
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
        } 

         public void Addrequest(Request request){

            string? connectionString= getConntection();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlcommand = new SqlCommand("insert into Requests values(@username,@receiver,@description,@status)",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = request.Username;
            sqlcommand.Parameters.Add("@receiver",sqlDbType:System.Data.SqlDbType.VarChar).Value = request.Receiver;
            sqlcommand.Parameters.Add("@description",sqlDbType:System.Data.SqlDbType.VarChar).Value =request.Description;
            sqlcommand.Parameters.Add("@status",sqlDbType:System.Data.SqlDbType.VarChar).Value = request.Status;
            sqlcommand.ExecuteNonQuery();

            connection.Close();
            return;
        } 

        public void AddProject(Project project){

            string? connectionString= getConntection();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            Byte[] Document = System.IO.File.ReadAllBytes("C:/Viyan/28-03-2023/EmployeeManagementSystem/wwwroot/Pdf/"+project.Document);

            SqlCommand sqlcommand = new SqlCommand($"insert into Project values(@name,'{Convert.ToString(project.Deadline)}',@manager,@document)",connection);
            sqlcommand.Parameters.Add("@name",sqlDbType:System.Data.SqlDbType.VarChar).Value = project.Name;
            //sqlcommand.Parameters.Add("@deadline",sqlDbType:System.Data.SqlDbType.VarChar).Value = project.Deadline;
            sqlcommand.Parameters.Add("@manager",sqlDbType:System.Data.SqlDbType.VarChar).Value = project.Manager;
            sqlcommand.Parameters.Add("@document",sqlDbType:System.Data.SqlDbType.VarBinary).Value = Document;
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            return;
        } 

        

        public string change(User user){
            SqlConnection connection = new SqlConnection(getConntection());
            connection.Open();
            SqlCommand sqlcommand = new SqlCommand($"select count(*) from Login where Username=@username",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
            if(Convert.ToInt32(sqlcommand.ExecuteScalar())==0){
                return "exist";
                connection.Close();
            }
            else if(user.Password.Equals(user.Confirmpassword)==false){
                return "dismatch";
                connection.Close();
            }

            SqlCommand sqlcommand1 = new SqlCommand("update Login set Password=@password where Username=@username",connection);
            sqlcommand1.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
            sqlcommand1.Parameters.Add("@password",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Password;
            sqlcommand1.ExecuteNonQuery();
            connection.Close();
            return "success";

        }

        public void Delete(string username){
            string? connectionString= getConntection();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlcommand = new SqlCommand("delete from Login where Username=@username",connection);
            sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = username;
            sqlcommand.ExecuteNonQuery();

            connection.Close();
        }


        public string? getConntection()
        {

        var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);

        IConfiguration configuration = build.Build();

        string? connectionString = Convert.ToString(configuration.GetConnectionString("DB1"));

        return connectionString;
        }
    }
}
