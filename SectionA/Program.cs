// Read a list of new employee records from a text file, masterlist.txt from HR. 
// The HRMasterlist.txt file has full details of new employee records.
// II. Generate the txt files that store the required list of new employee records by different department.

// Corporate Admin Department: FullName, Designation, Department
// Procurement Department: Salutation, FullName, MobileNo, Designation, Department
// IT Department: Nric, FullName, StartDate, Department, MobileNo

using System;
using System.IO;
using System.Collections.Generic;

namespace SectionA
{
    public class Employee
    {
        public string NRIC { get; set; }
        public string Fullname { get; set; }
        public string Salutation { get; set; }
        public DateTime StartDate { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string MobileNo { get; set; }
        public string HireType { get; set; }
        public double Salary { get; set; }
        public double MonthlyPayout { get; set; }

        public Employee(string nric, string fullname, string salutation, DateTime startDate, string designation, string department, string mobileNo, string hireType, double salary)
        {
            this.NRIC = nric;
            this.Fullname = fullname;
            this.Salutation = salutation;
            this.StartDate = startDate;
            this.Designation = designation;
            this.Department = department;
            this.MobileNo = mobileNo;
            this.HireType = hireType;
            this.Salary = salary;
            this.MonthlyPayout = 0.0;
        }

        public string Corporate()
        {
            return this.Fullname + ',' + this.Designation + ',' + this.Department;
        }
        public string Procurement()
        {
            return this.Salutation + ',' + this.Fullname + ',' + this.MobileNo + ',' + this.Designation + ',' + this.Department;
        }
        public string IT()
        {
            return this.NRIC + ',' + this.Fullname + ',' + this.StartDate.ToString("dd/MM/yyyy") + ',' + this.Department + ',' + this.MobileNo;
        }
        public string MasterList(){
            return this.NRIC + '|' + this.Fullname + '|' + this.Salutation + '|' + this.StartDate.ToString("dd/MM/yyyy")
            + '|' + this.Designation + '|' + this.Department + '|' + this.MobileNo + '|' + this.HireType + '|' + this.Salary + '|' 
            + this.MonthlyPayout;
        }
    }

    public class SectionA_Methods
    {
        public static List<Employee> readHRMasterList()
        {
            string line;
            List<Employee> employees = new List<Employee>();
            System.IO.StreamReader file = new System.IO.StreamReader("../HRMasterlist.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split('|');
                employees.Add(new Employee(data[0], data[1], data[2], Convert.ToDateTime(data[3]), data[4], data[5], data[6], data[7], Convert.ToDouble(data[8])));
            }
            file.Close();
            return employees;
        }
        public static void generateInfoForCorpAdmin()
        {
            List<Employee> employees = readHRMasterList();
            using (StreamWriter writer = new StreamWriter("../CorporateAdmin.txt"))
            {
                foreach (Employee employee in employees)
                {
                    string line = employee.Corporate();
                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }
        public static void generateInfoForProcurement()
        {
            List<Employee> employees = readHRMasterList();
            using (StreamWriter writer = new StreamWriter("../Procurement.txt"))
            {
                foreach (Employee employee in employees)
                {
                    string line = employee.Procurement();
                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }
        public static void generateInfoForITDepartment()
        {
            List<Employee> employees = readHRMasterList();
            using (StreamWriter writer = new StreamWriter("../ITDepartment.txt"))
            {
                foreach (Employee employee in employees)
                {
                    string line = employee.IT();
                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }
        public delegate void GenerateInfo();
        
        static void Main(string[] args)
        {
            GenerateInfo d1 = new GenerateInfo(generateInfoForCorpAdmin);
            GenerateInfo d2 = new GenerateInfo(generateInfoForProcurement);
            GenerateInfo d3 = new GenerateInfo(generateInfoForITDepartment);

            GenerateInfo generateAllInfo = d1 + d2 + d3;
            generateAllInfo();
        }
    }
}
