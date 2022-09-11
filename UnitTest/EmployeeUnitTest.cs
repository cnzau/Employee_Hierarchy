

namespace UnitTest
{
    public class EmployeeUnitTest
    {
        [Fact]
        public void TestConstructor()
        {
            //get path to the employee info.csv file in the test project bin
            var csvFile = Path.Combine(Directory.GetCurrentDirectory(), "Employee info.csv");
            //create a new employee object
            var employee = new Employees(csvFile);
            //get the first employee in the list
            var firstEmployee = employee._employees[0];
            //check that the first employee's id is Employee 01
            Assert.Equal("Employee 01", firstEmployee.IdOfEmployee);
            var secondEmployee = employee._employees[1];
            //check that the first employee's id is Employee 02
            Assert.Equal("Employee 02", secondEmployee.IdOfEmployee);

            //check salary for second employee
            Assert.Equal(3800, employee.ManagerSalaryBudget("Employee 01"));
            Assert.Equal(1800, employee.ManagerSalaryBudget("Employee 02"));
            Assert.Equal(500, employee.ManagerSalaryBudget("Employee 03"));
        }


    }
}