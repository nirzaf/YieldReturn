// See https://aka.ms/new-console-template for more information

using Bogus;
using Newtonsoft.Json;
using static System.Console;
using static System.Diagnostics.Stopwatch;
using static System.IO.Directory;

internal class Program
{
    public static void Main(string[] args)
    {
        List<string> firstName = new List<string>();
        for (int i = 0; i < 5000; i++)
        {
            firstName.Add(new Faker().Name.FirstName());
        }
        
        List<string> lastName = new List<string>();
        for (int i = 0; i < 5000; i++)
        {
            lastName.Add(new Faker().Name.LastName());
        }
        
        List<string> fullNames = new List<string>();
        for (int i = 0; i < 5000; i++)
        {
            fullNames.Add(new Faker().Name.FullName());
        }

        //remove all duplicates from the list by age
        var track = StartNew();
        HashSet<string> names = new HashSet<string>();
        foreach (var item in firstName)
        {
            names.Add(item);
        }

        foreach (var item in lastName)
        {
            names.Add(item);
        }
        
        foreach (var item in fullNames)
        {
            names.Add(item);
        }
        
        var hashSetEmployeeList = names.ToList();
        track.Stop();
        WriteLine($"HashSet: {track.ElapsedTicks} ticks");
        
        //remove all duplicates from the int list
        track = StartNew();
        var distinctFirstNames = firstName.Distinct().ToList();
        var distinctLastNames = lastName.Distinct().ToList();
        var distinctFullNames = fullNames.Distinct().ToList();
        var combinedList = distinctFirstNames.Concat(distinctLastNames).ToList();
        combinedList = combinedList.Concat(distinctFullNames).ToList();
        
        var distinctEmployeeList = combinedList.Distinct().ToList();
        
        track.Stop();
        WriteLine($"Distinct: {track.ElapsedTicks} ticks");

        foreach (var ages in names.Take(50))
        {
            WriteLine(ages);
        }
        

        WriteLine("Printing from C#");
        
        // Generate 100 Employees using Bogus
        var listEmployee = new Faker<Employee>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => f.Name.FullName())
            .RuleFor(o => o.Age, f => f.Random.Int(18, 60))
            .GenerateBetween(500000, 500000).ToArray();
        
        // read values from Global.json file from root directory
        string root = GetParent(GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName!;
        var globalJson = File.ReadAllText($"{root }\\global.json");
        var globalJsonObj = JsonConvert.DeserializeObject<GlobalJson>(globalJson);

        WriteLine($"version: {globalJsonObj.sdk.version}");
        WriteLine($"rollForward: {globalJsonObj.sdk.rollForward}");
        WriteLine($"allowPrerelease: {globalJsonObj.sdk.allowPrerelease}");


        ReadLine();


        /*Circle c1 = new()
        {
            Radius = 7
        };
        
        WriteLine(c1.GetArea());
        WriteLine(c1.GetCircumference());

        ReadLine();*/


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

public class Names
{
    public string Name { get; set; }
}

public class GlobalJson
{
    public Sdk sdk { get; set; }
}

public class Sdk
{
    public string version { get; set; }
    public string rollForward { get; set; }
    public bool allowPrerelease { get; set; }
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