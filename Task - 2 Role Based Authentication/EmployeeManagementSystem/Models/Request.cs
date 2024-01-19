using System;

namespace EmployeeManagementSystem.Models{
    public class Request{
        public int RequestID{get;set;}
        public string? Username{get;set;}
        public string? Receiver{get;set;}
        public string? Description{get;set;}
        public string? Status{get;set;}
        
    }
}