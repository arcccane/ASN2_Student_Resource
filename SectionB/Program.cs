// Calculate monthly payout using the following formula for the list of employee records in the masterlist.txt.

// Full-time = salary (assuming full time works 40 hours per week)
// Part-time = 0.5 * salary (assuming part-timer works 20 hours per week)
// Hourly = 0.25 * salary (assuming the employee works 10 hours per week)

// Print out the list of employees with the computed monthly payout.
// At the end of the list, display total payout and number of total employees by the company.
// Generate a new HR masterlist.

using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using SectionA;

namespace SectionB
{
    class SectionB_Methods
    {
        public enum HireTypes
        {
            FullTime,
            PartTime,
            Hourly
        }
        public static double HireTypesPayout(HireTypes types,double salary){
            switch (types)
            {
                case HireTypes.FullTime:
                    return salary;
                case HireTypes.PartTime:
                    return salary * 0.5;
                case HireTypes.Hourly:
                    return salary * 0.25;
            }
            return 0;
        }
        public static List<Employee> processPayroll(){
            double TotalPay = 0;
            int no_of_employees = 0;
            var employees = SectionA_Methods.readHRMasterList();
            foreach(Employee employee in employees){
                Console.WriteLine(employee.Fullname + " (" + employee.NRIC + ")");
                Console.WriteLine(employee.Designation);
                no_of_employees += 1;
                if(employee.HireType == "FullTime"){
                    double FullTimePay = HireTypesPayout(HireTypes.FullTime,employee.Salary);
                    Console.WriteLine(HireTypes.FullTime + " Payout" + ": $" + FullTimePay);
                    Console.WriteLine("---------------------------------------------------");
                    TotalPay += FullTimePay;
                }
                else if (employee.HireType == "PartTime"){
                    double PartTimePay = HireTypesPayout(HireTypes.PartTime,employee.Salary);
                    Console.WriteLine(HireTypes.PartTime + " Payout" + ": $" + PartTimePay);
                    Console.WriteLine("---------------------------------------------------");
                    TotalPay += PartTimePay;
                }
                else{
                    double HourlyPay = HireTypesPayout(HireTypes.Hourly,employee.Salary);
                    Console.WriteLine(HireTypes.Hourly + " Payout" + ": $" + HourlyPay);
                    Console.WriteLine("---------------------------------------------------");
                    TotalPay += HourlyPay;
                }
            }
            Console.WriteLine("Total Payroll Amount: $" + TotalPay + " to be paid to " + no_of_employees + " employees.");
            return employees;
        }

        public static void updateMonthlyPayoutToMasterlist(){
            dynamic employees = SectionA_Methods.readHRMasterList();
            using (StreamWriter writer = new StreamWriter("../HRMasterlistB.txt")){
                foreach (Employee employee in employees){
                    if(employee.HireType == "FullTime"){
                        employee.MonthlyPayout = HireTypesPayout(HireTypes.FullTime,employee.Salary);
                    }
                    else if (employee.HireType == "PartTime"){
                        employee.MonthlyPayout = HireTypesPayout(HireTypes.PartTime,employee.Salary);
                    }
                    else{
                        employee.MonthlyPayout = HireTypesPayout(HireTypes.Hourly,employee.Salary);
                    }
                    string line = employee.MasterList();
                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }

        public static async Task Main(string[] args)
        {
            var AsyncProcessPayroll = await Task.Run(() => processPayroll());
            updateMonthlyPayoutToMasterlist();
        }
    }
}
