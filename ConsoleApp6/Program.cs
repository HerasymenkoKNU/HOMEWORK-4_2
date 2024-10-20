using System;
using System.Collections.Generic;
using System.Linq;

abstract class Worker
{

    public string Name { get; }
    public string Position { get; }
    public int WorkDay { get; }
    public Worker(string name, string position, int workDay)
    {
        Name = name;
        Position = position;
        WorkDay = workDay;
    }
    public void Call()
    {
        Console.WriteLine($"{Position} calling");
    }
    public void WriteCode()
    {
        Console.WriteLine($"{Position} coding");
    }
    public void Relax()
    {
        Console.WriteLine($"{Position} chill out");
    }
    abstract public void FillWorkDay();
}

class Developer : Worker
{
    public Developer(string name, int workDay) : base(name, "developer", workDay) { }
    public override void FillWorkDay()
    {
        WriteCode();
        Call();
        Relax();
        WriteCode();
    }
}

class Manager : Worker
{
    private Random random = new Random();
    public Manager(string name, int workDay) : base(name, "manager", workDay) { }
    public override void FillWorkDay()
    {
        Console.WriteLine($"{Position} day");
        int firstTime = random.Next(1, 11);
        for (int i = 0; i < firstTime; i++)
        {
            Call();
        }
        Relax();
        int secondTime = random.Next(1, 6);
        for (int i = 0; i < secondTime; i++)
        {
            Call();
        }
    }
}

class Team
{
    public string NameOfTeam { get; }
    private List<Worker> workers = new List<Worker>();

    public Team(string nameOfTeam)
    {
        NameOfTeam = nameOfTeam;
    }
    public bool ExistenceСheck(Worker worker)
    {
        return workers.Any(existingWorker =>
            existingWorker.Name == worker.Name && existingWorker.Position == worker.Position && existingWorker.WorkDay == worker.WorkDay);
    }
    public int NumberOfWorking()
    {
        return workers.Sum(worker => worker.WorkDay);
    }


    public void AddWorker(Worker worker)
    {

        if (!ExistenceСheck(worker) && NumberOfWorking() + worker.WorkDay <= 24)
        {
            workers.Add(worker);
        }
        else if (ExistenceСheck(worker))
        {
            Console.WriteLine("Information about the employee already exists.");
        }
        else
        {
            Console.WriteLine("An employee cannot work more than 24 hours a day.");
        }
    }

    public void PrintTeamInfo()
    {
        Console.WriteLine($"Team: {NameOfTeam}");
        Console.WriteLine("Employees:");
        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.Name}");
        }
    }

    public void PrintDetailedTeamInfo()
    {
        Console.WriteLine($"team: {NameOfTeam}");
        Console.WriteLine("details:");

        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.Name}  {worker.Position}, works {worker.WorkDay} hours");
        }
    }


}

public class Program
{
    private static void Main()
    {
        List<Team> teams = new List<Team>();

        bool add = true;

        while (add)
        {
            Console.Write("Enter a team name: ");
            string teamName = Console.ReadLine();
            Team team = new Team(teamName);
            teams.Add(team);

            bool addWorker = true;

            while (addWorker)
            {
                Console.Write("Do you want to add an employee? Write yes or no: ");
                string answer = Console.ReadLine();

                if (answer == "yes")
                {
                    Console.Write("Enter the name of your worker: ");
                    string name = Console.ReadLine();

                    Console.Write("What position does he hold? Write a manager or developer: ");
                    string position = Console.ReadLine();

                    Console.Write("Write the number of working hours: ");
                    int workDay = int.Parse(Console.ReadLine());

                    if (position.ToLower() == "developer")
                    {
                        Developer developer = new Developer(name, workDay);
                        team.AddWorker(developer);
                    }
                    else if (position.ToLower() == "manager")
                    {
                        Manager manager = new Manager(name, workDay);
                        team.AddWorker(manager);
                    }
                    else
                    {
                        Console.WriteLine("wrong info");
                    }
                }
                else if (answer == "no")
                {
                    addWorker = false;
                }

                Console.Write("Would you like to add another employee to this team? If yes, then enter enter yes or no: ");
                string answer3 = Console.ReadLine();
                if (answer3 != "yes")
                {
                    addWorker = false;
                }
            }

            Console.Write("Want to add another team? If yes, please enter yes or no  : ");
            string answer4 = Console.ReadLine();
            if (answer4 != "yes")
            {
                add = false;
            }
        }

        Console.Write("Would you like to receive detailed information? Write yes or no: ");
        string answer2 = Console.ReadLine();
        if (answer2 == "yes")
        {
            foreach (var team in teams)
            {
                team.PrintDetailedTeamInfo();
            }
        }
    }
}
