using System;

namespace MidtermProject{
    class Manager : Employee{

        private string department;
        private string region;


        public Manager(string firstName, string lastName, string id, string department, string region) : base(firstName, lastName, id, EmployeeType.Manager){
            this.department = department;
            this.region = region;

        }

        public string getDepartment(){
            return this.department;
        }

        public void setDepartment(string newDepartment){
            this.department = newDepartment;
        }

        public string getRegion(){
            return this.region;
        }

        public void setRegion(string newArea){
            this.region = newArea;
        }  
        
    }
}