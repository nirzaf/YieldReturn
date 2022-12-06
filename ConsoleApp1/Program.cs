// See https://aka.ms/new-console-template for more information

using Bogus;
using static System.Console;
using static System.Diagnostics.Stopwatch;

WriteLine("Printing from C#");

// Generate 100 Employees using Bogus
var listEmployee = new Faker<Employee>()
    .RuleFor(o => o.Id, f => f.IndexFaker)
    .RuleFor(o => o.Name, f => f.Name.FullName())
    .RuleFor(o => o.Age, f => f.Random.Int(18, 60))
    .GenerateBetween(500000, 500000);

// Log the duration to filter all employees with age > 30
var watch = StartNew();
var filteredList = listEmployee.Where(x => x.Age > 30).ToList();
watch.Stop();

WriteLine($"Filtering took {filteredList.Count} using LINQ {watch.ElapsedTicks} ticks");

// Log the duration to filter all employees with age > 30
watch = StartNew();
var filtered = FilterAllEmployeesAbove30(listEmployee);
watch.Stop();

WriteLine($"Filtering took {filtered.Count()} using yield {watch.ElapsedTicks} ticks");

ReadLine();
IEnumerable<Employee> FilterAllEmployeesAbove30(List<Employee> listEmployee)
{
    foreach (Employee emp in listEmployee.Where(emp => emp.Age > 30))
    {
        yield return emp;
    }
}


public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

