using System.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystem.Models{

    public class Repository : IRepository{
        
        public List<User> ViewUserAdmin(string role){
            List<User> userList = new List<User>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Role",role);
                    sqlCommand.Parameters.AddWithValue("@Method","Admin");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
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
                        userList.Add(user);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return userList;
        }

        public List<User> ViewManager(){
            List<User> userList = new List<User>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Method","Manager");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
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
                        userList.Add(user);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return userList;
        }

        public User ViewUser(string userName){
            User user = new User();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Username",userName);
                    sqlCommand.Parameters.AddWithValue("@Method","User");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        user.Username  = Convert.ToString(dr["Username"]);
                        user.Name = Convert.ToString(dr["Name"]);
                        user.EmailID = Convert.ToString(dr["EmailID"]);
                        user.Phoneno = Convert.ToDouble(dr["Phoneno"]);
                        user.Designation = Convert.ToString(dr["Designation"]);
                        user.age = Convert.ToInt32(dr["Age"]);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return user;
        }

        public string ValidateUser(User user){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType= CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Username",user.Username);
                    sqlCommand.Parameters.AddWithValue("@Method","Username");
                    SqlCommand sqlcommand = new SqlCommand("userDetails",sqlConnection);
                    sqlcommand.CommandType= CommandType.StoredProcedure;
                    sqlcommand.Parameters.AddWithValue("@EmailID",user.EmailID);
                    sqlcommand.Parameters.AddWithValue("@Method","Email");
                    sqlConnection.Open();
                    if((Convert.ToInt32(sqlCommand.ExecuteScalar()))==1){
                        return "Exist Username";
                    }
                    else if((Convert.ToInt32(sqlcommand.ExecuteScalar()))==1){
                        return "Exist Emailid";
                    }
                    else{
                        return "Valid";
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return "Invalid";
            }
        }

        public string GetRole(string userName){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = userName;
                    sqlCommand.Parameters.AddWithValue("@Method","Role");
                    sqlConnection.Open();
                    string? role = Convert.ToString(sqlCommand.ExecuteScalar());
                    return role;  
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return null;
            }
        }

        public bool AuthenticateUser(User user){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
                    sqlCommand.Parameters.Add("@Password",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Password;
                    sqlCommand.Parameters.AddWithValue("@Method","Login");
                    sqlConnection.Open();
                    if(Convert.ToInt32(sqlCommand.ExecuteScalar())==1){
                            return true;
                        }
                    return false;
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return false;
            }
        }

        public void AddUser(User user){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
                    sqlCommand.Parameters.Add("@password",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Password;
                    sqlCommand.Parameters.Add("@role",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Role;
                    sqlCommand.Parameters.Add("@name",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Name;
                    sqlCommand.Parameters.Add("@age",sqlDbType:System.Data.SqlDbType.Int).Value = user.age;
                    sqlCommand.Parameters.Add("@designation",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Designation;
                    sqlCommand.Parameters.Add("@emailid",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.EmailID;
                    sqlCommand.Parameters.Add("@phoneno",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Phoneno;
                    sqlCommand.Parameters.AddWithValue("@Method","Insert");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        } 
        public void UpdateUser(User user){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
                    sqlCommand.Parameters.Add("@role",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Role;
                    sqlCommand.Parameters.Add("@name",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Name;
                    sqlCommand.Parameters.Add("@age",sqlDbType:System.Data.SqlDbType.Int).Value = user.age;
                    sqlCommand.Parameters.Add("@designation",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Designation;
                    sqlCommand.Parameters.Add("@emailid",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.EmailID;
                    sqlCommand.Parameters.Add("@phoneno",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Phoneno;
                    sqlCommand.Parameters.AddWithValue("@Method","Update");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }

        public string ChangePassword(User user){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlcommand = new SqlCommand("userDetails",sqlConnection);
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
                    sqlcommand.Parameters.AddWithValue("@Method","Username");
                    sqlConnection.Open();
                    if(Convert.ToInt32(sqlcommand.ExecuteScalar())==0){
                        return "exist";
                    }
                    else if(user.Password.Equals(user.Confirmpassword)==false){
                        return "dismatch";
                    }
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Username;
                    sqlCommand.Parameters.Add("@password",sqlDbType:System.Data.SqlDbType.VarChar).Value = user.Password;
                    sqlCommand.Parameters.AddWithValue("@Method","Password");
                    sqlCommand.ExecuteNonQuery();
                    return "success";
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return "exist";
            }
        }

        public void DeleteUser(string userName){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = userName;
                    sqlCommand.Parameters.AddWithValue("@Method","Delete");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }

        public List<User> ViewTeamMember(string userName){
            List<User> teamList = new List<User>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = userName;
                    sqlCommand.Parameters.AddWithValue("@Method","TeamUser");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
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
                        teamList.Add(user);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return teamList;
        }

        public List<User> ViewTeam(string manager){
            List<User> teamList = new List<User>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = manager;
                    sqlCommand.Parameters.AddWithValue("@Method","TeamManager");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
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
                        teamList.Add(user);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return teamList;
        }

        public List<User> ViewEmployee(){
            List<User> userList = new List<User>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("userDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Method","Employee");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
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
                        userList.Add(user);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return userList;
        }
         
        public List<Request> ViewRequestUser(string userName){
            List<Request> requestList = new List<Request>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    DataTable dataTable = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand("requestDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Username",userName);
                    sqlCommand.Parameters.AddWithValue("@Method","User");
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Request request = new Request();
                        request.RequestID = Convert.ToInt32(dr["Sno"]);
                        request.Receiver  = Convert.ToString(dr["Receiver"]);
                        request.Description = Convert.ToString(dr["Description"]);
                        request.Status = Convert.ToString(dr["Status"]);
                        requestList.Add(request);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return requestList;
        }

        public List<Request> ViewRequestReceiver(string receiver){
            List<Request> requestList = new List<Request>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    DataTable dataTable = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand("requestDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Receiver",receiver);
                    sqlCommand.Parameters.AddWithValue("@Method","Receiver");
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Request request = new Request();
                        request.RequestID = Convert.ToInt32(dr["Sno"]);
                        request.Username  = Convert.ToString(dr["Username"]);
                        request.Description = Convert.ToString(dr["Description"]);
                        request.Status = Convert.ToString(dr["Status"]);
                        requestList.Add(request);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return requestList;
        }

        public List<Request> ViewRequest(string receiver){
            List<Request> requestList = new List<Request>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    DataTable dataTable = new DataTable();
                    SqlCommand sqlCommand = new SqlCommand("requestDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Receiver",receiver);
                    sqlCommand.Parameters.AddWithValue("@Method","Over");
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Request request = new Request();
                        request.RequestID = Convert.ToInt32(dr["Sno"]);
                        request.Username  = Convert.ToString(dr["Username"]);
                        request.Description = Convert.ToString(dr["Description"]);
                        request.Status = Convert.ToString(dr["Status"]);
                        requestList.Add(request);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return requestList;

        }
        public void AddRequest(Request request){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("requestDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = request.Username;
                    sqlCommand.Parameters.Add("@Receiver",sqlDbType:System.Data.SqlDbType.VarChar).Value = request.Receiver;
                    sqlCommand.Parameters.Add("@Description",sqlDbType:System.Data.SqlDbType.VarChar).Value =request.Description;
                    sqlCommand.Parameters.AddWithValue("@Method","Insert");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        } 
        public void UpdateRequest(int requestID,string status){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("requestDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Sno",sqlDbType:System.Data.SqlDbType.Int).Value = requestID;
                    sqlCommand.Parameters.Add("@Status",sqlDbType:System.Data.SqlDbType.VarChar).Value = status;
                    sqlCommand.Parameters.Add("@Method",sqlDbType:System.Data.SqlDbType.VarChar).Value ="Update";
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }
        
        public List<Project> ViewProject(){
            string? connectionString= GetConnection();
            List<Project> projectList = new List<Project>();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("projectDetail",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Method","Admin");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Project project = new Project();
                        project.Name  = Convert.ToString(dr["Name"]);
                        project.Deadline = Convert.ToDateTime(dr["Deadline"]);
                        project.Manager = Convert.ToString(dr["Manager"]);
                        projectList.Add(project);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return projectList;
        }

        public Project ViewProject (string userName){
            string? connectionString = GetConnection();
            Project project = new Project();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("projectDetail",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Manager",userName);
                    sqlCommand.Parameters.AddWithValue("@Method","Manager");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        project.Name  = Convert.ToString(dr["Name"]);
                        project.Deadline = Convert.ToDateTime(dr["Deadline"]);
                        project.Manager = Convert.ToString(dr["Manager"]);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return project;
        }

        public void AddProject(Project project){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    Byte[] Document = System.IO.File.ReadAllBytes("E:/Vs Code/Viyan_EmployeeManagementSystem_CIT/EmployeeManagementSystem/wwwroot/Pdf/"+project.Document);
                    SqlCommand sqlCommand = new SqlCommand("projectDetail",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Name",sqlDbType:System.Data.SqlDbType.VarChar).Value = project.Name;
                    sqlCommand.Parameters.AddWithValue("@Deadline", project.Deadline);
                    sqlCommand.Parameters.Add("@Manager",sqlDbType:System.Data.SqlDbType.VarChar).Value = project.Manager;
                    sqlCommand.Parameters.Add("@Document",sqlDbType:System.Data.SqlDbType.VarBinary).Value = Document;
                    sqlCommand.Parameters.AddWithValue("@Method","Insert");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        } 

        public void DeleteProject(string name){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("projectDetail",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Name",sqlDbType:System.Data.SqlDbType.VarChar).Value = name;
                    sqlCommand.Parameters.AddWithValue("@Method","Delete");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }

        public byte[] ViewPDF(string name){
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("projectDetail",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Name",name);
                    sqlCommand.Parameters.AddWithValue("@Method","ViewPDF");
                    sqlConnection.Open();
                    var document = sqlCommand.ExecuteScalar();
                    return document as byte[];
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return null;
            }
        }

        public List<Team> ViewTaskManager(string manager){
            List<Team> taskList = new List<Team>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Manager",manager);
                    sqlCommand.Parameters.AddWithValue("@Method","ManagerTask");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Team task = new Team();
                        task.Username  = Convert.ToString(dr["Username"]);
                        task.Taskname = Convert.ToString(dr["Taskname"]);
                        task.Deadline = Convert.ToString(dr["Deadline"]);
                        task.Status = Convert.ToString(dr["Status"]);
                        taskList.Add(task);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return taskList;
        }

        public List<Team> ViewTaskUser(string username){
            List<Team> taskList = new List<Team>();
            string? connectionString= GetConnection();
            using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Username",username);
                sqlCommand.Parameters.AddWithValue("@Method","UserTask");
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    Team task = new Team();
                    task.Taskname = Convert.ToString(dr["Taskname"]);
                    task.Manager  = Convert.ToString(dr["Manager"]);
                    task.Deadline = Convert.ToString(dr["Deadline"]);
                    task.Status = Convert.ToString(dr["Status"]);
                    taskList.Add(task);
                }
            }
            return taskList;
        }

        public List<Team> ViewTasksManager(string manager){
            List<Team> taskList = new List<Team>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Manager",manager);
                    sqlCommand.Parameters.AddWithValue("@Method","ManagerTasks");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Team task = new Team();
                        task.Username  = Convert.ToString(dr["Username"]);
                        task.Taskname = Convert.ToString(dr["Taskname"]);
                        task.Deadline = Convert.ToString(dr["Deadline"]);
                        task.Dateofcompletion = Convert.ToString(dr["Dateofcompletion"]);
                        task.Status = Convert.ToString(dr["Status"]);
                        taskList.Add(task);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return taskList;
        }

        public List<Team> ViewTasksUser(string username){
            List<Team> taskList = new List<Team>();
            string? connectionString= GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Username",username);
                    sqlCommand.Parameters.AddWithValue("@Method","UserTasks");
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Team task = new Team();
                        task.Taskname = Convert.ToString(dr["Taskname"]);
                        task.Manager  = Convert.ToString(dr["Manager"]);
                        task.Deadline = Convert.ToString(dr["Deadline"]);
                        task.Dateofcompletion = Convert.ToString(dr["Dateofcompletion"]);
                        task.Status = Convert.ToString(dr["Status"]);
                        taskList.Add(task);
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return taskList;
        }
        public void AddTeam(string username,string manager){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Username",username);
                    sqlCommand.Parameters.AddWithValue("@Manager",manager);
                    sqlCommand.Parameters.AddWithValue("@Method","InsertTeam");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }

        public void RemoveTeam(string username){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Username",username);
                    sqlCommand.Parameters.AddWithValue("@Method","DeleteTeam");
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }

        public string ValidateTask(string manager){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Manager",manager);
                    sqlCommand.Parameters.AddWithValue("@Method","Validate");
                    sqlConnection.Open();
                    if((Convert.ToInt32(sqlCommand.ExecuteScalar()))==0){
                        return "Invalid";
                    }
                    else{
                        return "Valid";
                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return "Invalid";
            }
        }

        public void UpdateTask(Team team){
            try{
                using(SqlConnection sqlConnection = new SqlConnection(GetConnection())){
                    SqlCommand sqlcommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.Parameters.AddWithValue("@Username",team.Username);
                    sqlcommand.Parameters.AddWithValue("@Method","Update");
                    sqlConnection.Open();
                    sqlcommand.ExecuteNonQuery();
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = team.Username;
                    sqlCommand.Parameters.Add("@Manager",sqlDbType:System.Data.SqlDbType.VarChar).Value = team.Manager;
                    sqlCommand.Parameters.Add("@Taskname",sqlDbType:System.Data.SqlDbType.VarChar).Value = team.Taskname;
                    sqlCommand.Parameters.Add("@Deadline",sqlDbType:System.Data.SqlDbType.VarChar).Value = team.Deadline;
                    sqlCommand.Parameters.Add("@Date",sqlDbType:System.Data.SqlDbType.VarChar).Value = team.Dateofcompletion;
                    sqlcommand.Parameters.AddWithValue("@Method","InsertTask");
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
            }
            return;
        }

        public string AddTask(Team task){
            List<string> list = new List<string>();
            string? connectionString = GetConnection();
            try{
                using(SqlConnection sqlConnection = new SqlConnection(connectionString)){
                    SqlCommand sqlCommand = new SqlCommand("taskDetails",sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Manager",task.Manager);
                    sqlCommand.Parameters.AddWithValue("@Domain",task.Domain);
                    sqlCommand.Parameters.AddWithValue("@Method","CheckUser");
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while(sqlDataReader.Read())
                    {
                        string? username = Convert.ToString(sqlDataReader["Username"]);
                        list.Add(username);
                    }
                    if(!sqlDataReader.HasRows){
                        sqlDataReader.Close();
                        return "Invalid";}
                    else if(list.Count()==1){
                        sqlDataReader.Close();
                        SqlCommand sqlcommand = new SqlCommand("taskDetails",sqlConnection);
                        sqlcommand.CommandType = CommandType.StoredProcedure;
                        sqlcommand.Parameters.Add("@username",sqlDbType:System.Data.SqlDbType.VarChar).Value = list[0];
                        sqlcommand.Parameters.Add("@taskname",sqlDbType:System.Data.SqlDbType.VarChar).Value = task.Taskname;
                        sqlcommand.Parameters.Add("@deadline",sqlDbType:System.Data.SqlDbType.VarChar).Value = task.Deadline;
                        sqlcommand.Parameters.AddWithValue("@Method","AddTask");
                        sqlcommand.ExecuteNonQuery();
                        return "Valid";
                    }
                    else{
                        string Username="";
                        sqlDataReader.Close();
                        DateTime mindate = DateTime.Now;
                        foreach (string username in list){
                            SqlCommand sqlcommand = new SqlCommand("taskDetails",sqlConnection);
                            sqlcommand.CommandType = CommandType.StoredProcedure;
                            sqlcommand.Parameters.AddWithValue("@Username",username);
                            sqlcommand.Parameters.AddWithValue("@Method","FindUser");
                            DateTime Mindate = Convert.ToDateTime(sqlcommand.ExecuteScalar());
                            if(Mindate<mindate){
                                mindate = Mindate;
                                Username = username;
                            }
                        }
                        SqlCommand command = new SqlCommand("taskDetails",sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Username",sqlDbType:System.Data.SqlDbType.VarChar).Value = Username;
                        command.Parameters.Add("@Taskname",sqlDbType:System.Data.SqlDbType.VarChar).Value = task.Taskname;
                        command.Parameters.Add("@Deadline",sqlDbType:System.Data.SqlDbType.VarChar).Value = task.Deadline;
                        command.Parameters.AddWithValue("@Method","AddTask");
                        command.ExecuteNonQuery();
                        return "Valid";

                    }
                }
            }
            catch(SqlException sqlException){
                Console.WriteLine(sqlException);
                return "Invalid";
            }
        }

        public string? GetConnection()
        {
            try{
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
            IConfiguration configuration = build.Build();
            string? connectionString = Convert.ToString(configuration.GetConnectionString("DB1"));
            return connectionString;
            }
            catch(Exception exception){
                Console.WriteLine(exception);
                return null;
            }
        }
    }
}
