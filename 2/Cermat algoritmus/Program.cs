using System;
using System.Collections.Generic;


// vstup:
// #StudentNum #SchoolNum
// #StudentNum times: id schoolId1 schoolId2 schoolId3
// #SchoolNum times: schoolId capacity _Results from the test, first is first place id, last is last place id_

// vystup:
// #SchoolNum times: (0 first)schoolId _id of accepted students, in order by results_

// Chceme max top priorit, potom max 2. priorit

namespace Cermat_algoritmus
{
    public class Student
    {
        public int Id;
        public List<int> ChosenSchools; // index 0 is top priority, 2 is lowest
        //public int currentPick; // 0 - not accepted, 1 is lowest priority, 3 is top

        public Student(int id, int s1, int s2, int s3)
        {
            this.Id = id;
            this.ChosenSchools = new List<int> { s1, s2, s3 }; 
            //this.currentPick = 0;
        }
    }

    public class School
    {
        public int Id;
        public List<int> Results;
        public int Capacity;
        public List<int> AcceptedStudents;
        public int NextPick; // Next index to pick from Results
        public Queue<int> HappyStudents;

        public School(int id, int cap, List<int> res)
        {
            this.Id = id;
            this.Capacity = cap;
            this.Results = res;
            this.HappyStudents = new Queue<int>();
            this.AcceptedStudents = new List<int>();
            this.NextPick = 0;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            int StudentNum = int.Parse(input[0]);
            int SchoolNum = int.Parse(input[1]);
            Console.WriteLine(StudentNum.ToString() + " " + SchoolNum.ToString());

            Dictionary<int, Student> Students = new Dictionary<int, Student>();
            Dictionary<int, School> Schools = new Dictionary<int, School>();
            for (int i = 0; i < StudentNum; i++)
            {
                // id  s1 s2 s3
                input = Console.ReadLine().Split();
                Students.Add(int.Parse(input[0]), new Student(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]), int.Parse(input[3])));
            }
            for (int i = 0; i < SchoolNum; i++)
            {
                // id  cap #StudentNum times id
                input = Console.ReadLine().Split();
                int id = int.Parse(input[0]);
                int cap = int.Parse(input[1]);
                List<int> res = new List<int>();
                for (int j = 2; j < StudentNum+2; j++)
                {
                    res.Add(int.Parse(input[j]));
                }
                Schools.Add(id, new School(id, cap, res));

                School curSchool = Schools[id];
                for (int j = 0; j < Math.Min(cap, res.Count); j++)
                {
                    curSchool.AcceptedStudents.Add(res[j]);
                    if (Students[res[j]].ChosenSchools[0] == id)
                    {
                        curSchool.HappyStudents.Enqueue(res[j]);
                    }
                }
                Schools[id].NextPick = Math.Min(cap, res.Count);
                //Schools[id].AcceptedStudents = res.Take(Schools[id].NextPick).ToList(); // Take isn't inclusive, so this will not choose NextPick
            }


            // Al-Gore Rhythm

            // Getting rid of Students accepted by their top schools in other schools
            // In other words, finalize the position of students accepted by their top schools

            Queue<int> Q = new Queue<int>(Schools.Values.Select(x => x.Id));
            
            while (Q.Count > 0)
            {
                School curS = Schools[Q.First()];
                Q.Dequeue();

                while (curS.HappyStudents.Count > 0)
                {
                    int HS = curS.HappyStudents.First();
                    curS.HappyStudents.Dequeue();

                    foreach (School otherS in Schools.Values)
                    {
                        // if it isn't the same school and the school still has other students to pick from
                        if (curS.Id != otherS.Id && otherS.NextPick < otherS.Results.Count)
                        {
                            int ind = otherS.Results.FindIndex(x => x == HS);
                            if (ind == -1) continue;
                            if (ind >= otherS.NextPick) // the student hasn't been accepted, but is in the results
                            {
                                otherS.Results.Remove(HS);
                            }
                            else // the student was accepted
                            {
                                otherS.Results.Remove(HS);
                                otherS.AcceptedStudents.Remove(HS);
                                otherS.NextPick--;
                                if (otherS.NextPick < otherS.Capacity) // if the school still has unaccepted students in the results
                                {
                                    int NextStudent = otherS.Results[otherS.NextPick];
                                    otherS.AcceptedStudents.Add(NextStudent);
                                    if (Students[NextStudent].ChosenSchools[0] == otherS.Id) // if the new accepted student became happy
                                    {
                                        otherS.HappyStudents.Enqueue(HS);
                                        Q.Enqueue(otherS.Id);
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }
    }
}