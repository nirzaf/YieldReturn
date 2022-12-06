// See https://aka.ms/new-console-template for more information

using Bogus;
using static System.Console;
using static System.Diagnostics.Stopwatch;

internal class Program
{
    public static void Main(string[] args)
    {
        WriteLine("Printing from C#");

        /*Circle c1 = new()
        {
            Radius = 7
        };
        
        WriteLine(c1.GetArea());
        WriteLine(c1.GetCircumference());

        ReadLine();*/

        // Generate 100 Employees using Bogus
        var listEmployee = new Faker<Employee>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => f.Name.FullName())
            .RuleFor(o => o.Age, f => f.Random.Int(18, 60))
            .GenerateBetween(500000, 500000).ToArray();

        // Log the duration to filter all employees with age > 30
        var watch = StartNew();
        var filteredList = listEmployee.Where(x => x.Age > 30);
        watch.Stop();

        WriteLine($"Filtering took {filteredList.Count()} using LINQ {watch.ElapsedTicks} ticks");

        /*
        foreach (var emp in filteredList.Take(10))
        {
            WriteLine($"{emp.Id} - {emp.Name} - {emp.Age}");
        }
        */

        // Log the duration to filter all employees with age > 30
        watch = StartNew();
        var filtered = FilterAllEmployeesAbove30(filteredList);
        watch.Stop();

        WriteLine($"Filtering took {filtered.Count()} using yield {watch.ElapsedTicks} ticks");

        ReadLine();
        IEnumerable<Employee> FilterAllEmployeesAbove30(IEnumerable<Employee> listEmployee)
        {
            foreach (Employee emp in listEmployee.Where(emp => emp.Age > 30))
            {
                yield return emp;
            }
        }
    }
}

//Implement a Queue using two Stacks

public class Queue<T> where T : class
{
    private readonly Stack<T> _stack1 = new();
    private readonly Stack<T> _stack2 = new();

    public void Enqueue(T item)
    {
        _stack1.Push(item);
    }

    public T Dequeue()
    {
        if (_stack2.Count == 0)
        {
            while (_stack1.Count > 0)
            {
                _stack2.Push(_stack1.Pop());
            }
        }

        return _stack2.Pop();
    }
}