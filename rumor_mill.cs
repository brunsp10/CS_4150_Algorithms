using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public class Program
{
    public static void Main()
    {
        Dictionary<String, Student> students = new Dictionary<String, Student>();
        Dictionary<string, LinkedList<Student>> friendMap = new Dictionary<string, LinkedList<Student>>();

        int numStudents = int.Parse(Console.ReadLine());
        for (int i = 0; i < numStudents; i++)
        {
            string stuName = Console.ReadLine();
            students.Add(stuName, new Student(stuName,-1));
        }

        int friendLinks = int.Parse(Console.ReadLine());
        for (int i = 0; i < friendLinks; i++)
        {
            string input = Console.ReadLine();
            string origin = input.Split(" ")[0];
            string destination = input.Split(" ")[1];
            students[origin].addFriend(destination);
            students[destination].addFriend(origin);
        }

        int numReports = int.Parse(Console.ReadLine());

        for (int i = 0; i < numReports; i++)
        {
            string origin = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            generateReport(origin, sb, students, friendMap, new HashSet<string>(students.Keys)); 
            Console.WriteLine(sb.ToString());
        }

      
    }

    private static void generateReport(string origin, StringBuilder sb, Dictionary<string, Student> students, Dictionary<string, LinkedList<Student>> friendMap, HashSet<string> notSeen)
    {
        int day = 0;
        Queue<Student> q = new Queue<Student>();
        Student originStudent = students[origin];
        originStudent.dayTold = 0;
        q.Enqueue(originStudent);
        sb.Append(originStudent.name);
        HashSet<string> seen = new HashSet<string>();
        SortedSet<string> toldStudents = new SortedSet<string>();
        seen.Add(originStudent.name);

        while (q.Count > 0)
        {
            Student currentStudent = q.Dequeue();

            if (currentStudent.dayTold > day)
            {
                day++;
                foreach (string s in toldStudents)
                {
                    sb.Append($" {s}");
                }
                toldStudents.Clear();
            }

            foreach (string friend in currentStudent.friends)
            {
                if (seen.Contains(friend)) {
                    continue;
                }
                seen.Add(friend);
                Student friendStudent = students[friend];
                friendStudent.dayTold = day+1;
                toldStudents.Add(friend);
                q.Enqueue(friendStudent);
            }
           notSeen.Remove(currentStudent.name);

        }
        SortedSet<string> notTold = new SortedSet<string>(notSeen);
        foreach (string s in notTold)
        {
            sb.Append($" {s}");
        }
    }
}

public class Student
{
    public string name;
    public int dayTold;
    public List<String> friends;

    public Student(string name, int dayTold)
    {
        this.name = name;
        this.dayTold = dayTold;
        friends = new List<String>();
    }


    public void addFriend(string name)
    {
        this.friends.Add(name);
    }

    public override int GetHashCode()
    {
        return this.name.GetHashCode();
    }

    public override bool Equals(Object other)
    {
        if (!other.GetType().Equals(typeof(Student)))
        {
            return false;
        }
        Student otherStudent = (Student)other;
        return this.name == otherStudent.name;
    }
}