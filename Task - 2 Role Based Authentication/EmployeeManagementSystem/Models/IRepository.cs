using System.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystem.Models{

    public interface  IRepository{

        public List<User> ViewUserAdmin(string role);
        public List<User> ViewManager();
        public User ViewUser(string username);
        public string ValidateUser(User user);
        public string GetRole(string username);
        public bool AuthenticateUser(User user);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public string ChangePassword(User user);
        public void DeleteUser(string userName);
        public List<User> ViewTeamMember(string userName);
        public List<User> ViewTeam (string manager);
        public List<User> ViewEmployee();
        public List<Request> ViewRequestUser(string userName);
        public List<Request> ViewRequestReceiver(string receiver);
        public List<Request> ViewRequest(string receiver);
        public void AddRequest(Request request);
        public void UpdateRequest(int requestID,string status);
        public List<Project> ViewProject();
        public Project ViewProject (string userName);
        public void AddProject(Project project);
        public void DeleteProject(string name);
        public byte[] ViewPDF(string projectname);
        public List<Team> ViewTaskManager(string manager);
        public List<Team> ViewTaskUser(string username);
        public List<Team> ViewTasksManager(string manager);
        public List<Team> ViewTasksUser(string username);
        public void AddTeam(string username,string manager);
        public void RemoveTeam(string username);
        public string ValidateTask(string manager);
        public void UpdateTask(Team team);
        public string AddTask(Team task);
        public string? GetConnection();
    }
}
