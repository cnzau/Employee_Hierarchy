namespace Employee_Hierarchy
{
    public class Employees
    {
        public string IdOfEmployee { get; set; }
        public string IdOfManager { get; set; }
        public int EmployeeSalary { get; set; }

        public List<Employees> _employees = new List<Employees>();
        //constructor that takes a csv file and creates a list of employees
        public Employees(string csvFile)
        {

            using (var reader = new StreamReader(csvFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var employee = new Employees();
                    employee.IdOfEmployee = values[0];
                    employee.IdOfManager = values[1];
                    //Try to parse the salary to an integer
                    if (int.TryParse(values[2], out int salary))
                    {
                        employee.EmployeeSalary = salary;
                    }
                    else
                    {
                        employee.EmployeeSalary = 0;
                    }
                    _employees.Add(employee);
                }
            }
        }

        public Employees()
        {
            //parameter less employee constructor
        }

        public long ManagerSalaryBudget(string managerId)
        {
            //check if the manager id is valid from the list of employees
            if (_employees.Any(x => x.IdOfEmployee == managerId))
            {
                //get the manager's salary
                var managerSalary = (long)_employees.Where(x => x.IdOfEmployee == managerId).Select(x => x.EmployeeSalary).FirstOrDefault();
                //get the list of employees that report to the manager
                var employees = _employees.Where(x => x.IdOfManager == managerId).ToList();
                //check if the manager has any employees
                if (employees.Count > 0)
                {
                    //loop through the employees and add their salaries to the manager's salary
                    foreach (var employee in employees)
                    {
                        managerSalary += employee.EmployeeSalary;
                        //check if the employee is a manager of other employees
                        if (_employees.Any(x => x.IdOfManager == employee.IdOfEmployee))
                        {
                            //if the employee has employees, call the method again to get the salary of the employees
                            managerSalary -= employee.EmployeeSalary;
                            managerSalary += ManagerSalaryBudget(employee.IdOfEmployee);
                        }
                    }
                }
                return managerSalary;
            }
            else
            {
                return 0;
            }
        }

    }
}