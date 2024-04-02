using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


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
        public int currentPick; // 0 is top priority, 2 is lowest, 3 is none picked
        // Also indicates the lenght of ChosenSchools

        public Student(int id, int s1, int s2, int s3)
        {
            this.Id = id;
            this.ChosenSchools = new List<int> { s1, s2, s3 };
            this.currentPick = 3;
        }
    }

    public class School
    {
        public int Id;
        public List<int> Results;
        public int Capacity;
        public List<int> AcceptedStudents;
        public int NextPick; // Next index to pick from Results

        public School(int id, int cap, List<int> res)
        {
            this.Id = id;
            this.Capacity = cap;
            this.Results = res;
            this.AcceptedStudents = new List<int>();
            this.NextPick = 0;
        }

        public void RemoveStudent(int id)
        {
            int ind = this.Results.FindIndex(x => x == id); // index of the removed student
            this.Results.Remove(id);
            if (ind < this.NextPick && ind > -1) // if the removed student was in the accepted list
            {
                this.AcceptedStudents.Remove(id);
                this.NextPick--;
            }
        }

        public void AcceptNewStudent()
        {
            if (this.NextPick < this.Results.Count && this.AcceptedStudents.Count < this.Capacity)
            {
                int NextStudent = this.Results[this.NextPick];
                this.AcceptedStudents.Add(NextStudent);
                this.NextPick++;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            int StudentNum = int.Parse(input[0]);
            int SchoolNum = int.Parse(input[1]);
            //Console.WriteLine(StudentNum.ToString() + " " + SchoolNum.ToString());

            Dictionary<int, Student> Students = new Dictionary<int, Student>();
            Dictionary<int, School> Schools = new Dictionary<int, School>();
            Queue<int> HappyStudents = new Queue<int>();

            // Student initialization, where key=id, value=Student object
            for (int i = 0; i < StudentNum; i++)
            {
                // id  school1 school2 school3
                input = Console.ReadLine().Split();
                Students.Add(int.Parse(input[0]), new Student(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]), int.Parse(input[3])));
            }

            // School initialization, where key=id, value=School object
            // Also accepts the first set of students
            for (int i = 0; i < SchoolNum; i++)
            {
                // (id)  (capacity) (#StudentNum times id)
                input = Console.ReadLine().Split();
                int id = int.Parse(input[0]);
                int cap = int.Parse(input[1]);

                // Initializing results list
                List<int> res = new List<int>();
                for (int j = 2; j < input.Count(); j++)
                {
                    if (input[j] == " ") continue;
                    res.Add(int.Parse(input[j]));
                }

                Schools.Add(id, new School(id, cap, res));

                // Accepting students according to the school's capacity
                // If the school isn't the students worst school (there is some degree of happiness), the student is added to HappyStudents
                School curSchool = Schools[id];
                //Console.WriteLine(cap + " " + res.Count);
                for (int j = 0; j < Math.Min(cap, res.Count); j++)
                {
                    curSchool.AcceptedStudents.Add(res[j]);
                    if (Students[res[j]].ChosenSchools[0] == id) // if this is the student's top school
                    {
                        HappyStudents.Enqueue(res[j]);
                        Students[res[j]].currentPick = 0;
                    }
                    // else if the student hasn't been accepted (yet) by their top school and this is their second pick
                    else if (Students[res[j]].ChosenSchools[1] == id && Students[res[j]].currentPick > 1)
                    {
                        HappyStudents.Enqueue(res[j]);
                        Students[res[j]].currentPick = 1;
                    }
                    else if (Students[res[j]].currentPick > 2) Students[res[j]].currentPick = 2;
                    // If this school is their third pick, we won't add them to the happy list, because there isn't anything to remove
                }
                Schools[id].NextPick = Math.Min(cap, res.Count);
            }


            // Al-Gore Rhythm

            // Remove students who were accepted by their preffered schools from the list of schools they prefer less

            while (HappyStudents.Count > 0)
            {
                int HS = HappyStudents.First();
                HappyStudents.Dequeue();

                // For every school *worse* than what the student got in
                // Start from the back, so that we can remove the schools from the students choice
                for (int i = Students[HS].ChosenSchools.Count - 1; i > Students[HS].currentPick; i--)
                {
                    School CurSchool = Schools[Students[HS].ChosenSchools[i]];
                    CurSchool.RemoveStudent(HS);
                    // If the school still has unaccepted students in the results, then accept a new student
                    if (CurSchool.NextPick < CurSchool.Results.Count && CurSchool.AcceptedStudents.Count < CurSchool.Capacity)
                    {
                        CurSchool.AcceptNewStudent();
                        int NS = CurSchool.AcceptedStudents.Last();

                        // If this school is a better pick for this student, add him back into HappyStudents
                        if (Students[NS].currentPick > Students[NS].ChosenSchools.FindIndex(x => x == CurSchool.Id));
                        {
                            HappyStudents.Enqueue(HS);
                        }
                    }

                    // Removes this school from the possible schools the student may choose (since he is already accepted by a better one)
                    Students[HS].ChosenSchools.Remove(CurSchool.Id);
                }
            }

            // At this point the main algorithm is done and now comes the optimization
            List<School> SchoolsAsList = Schools.Values.ToList();

            // Searches for a [1, 1] priority pair
            for (int i = 0; i < Schools.Count; i++)
            {
                School CurSchool = SchoolsAsList[i];
                if (CurSchool.NextPick >= CurSchool.Results.Count) continue; // Checks if there are still more students to pick from

                Student StudentA = Students[CurSchool.Results[CurSchool.NextPick]];
                if (StudentA.currentPick == 0 || StudentA.currentPick == 3) continue; // Checks if the student may be swappable (an edge case to save time searching)
                if (StudentA.ChosenSchools[0] != CurSchool.Id) continue; // Student A prefers CurSchool

                School OtherSchool = Schools[StudentA.ChosenSchools[StudentA.currentPick]];
                if (OtherSchool.NextPick >= OtherSchool.Results.Count) continue; // Checks if there are still more students to pick from

                Student StudentB = Students[OtherSchool.Results[OtherSchool.NextPick]]; // The school where Student A was accepted
                if (StudentB.currentPick == 0 || StudentB.currentPick == 3) continue; // Checks if the student may be swappable (an edge case to save time searching)
                if (StudentB.ChosenSchools[0] != OtherSchool.Id) continue; // Student B prefers OtherSchool

                OtherSchool.RemoveStudent(StudentA.Id);
                CurSchool.RemoveStudent(StudentB.Id);

                CurSchool.AcceptNewStudent();
                OtherSchool.AcceptNewStudent();

                i--; // To check the same school again for a new match
            }

            // Searches for a [1, 2] priority pair
            for (int i = 0; i < Schools.Count; i++)
            {
                School CurSchool = SchoolsAsList[i];
                if (CurSchool.NextPick >= CurSchool.Results.Count) continue; // Checks if there are still more students to pick from

                Student StudentA = Students[CurSchool.Results[CurSchool.NextPick]];
                if (StudentA.currentPick == 0 || StudentA.currentPick == 3) continue; // Checks if the student may be swappable (an edge case to save time searching)
                if (StudentA.ChosenSchools[0] != CurSchool.Id) continue; // Student A prefers CurSchool

                School OtherSchool = Schools[StudentA.ChosenSchools[0]];
                if (OtherSchool.NextPick >= OtherSchool.Results.Count) continue; // Checks if there are still more students to pick from

                Student StudentB = Students[OtherSchool.Results[OtherSchool.NextPick]];
                if (StudentB.currentPick != 2) continue; // Checks if the student may be swappable (an edge case to save time searching)
                if (StudentB.ChosenSchools[1] != OtherSchool.Id) continue; // Student B prefers OtherSchool

                OtherSchool.RemoveStudent(StudentA.Id);
                CurSchool.RemoveStudent(StudentB.Id);

                CurSchool.AcceptNewStudent();
                OtherSchool.AcceptNewStudent();

                i--; // To check the same school again for a new match
            }

            // Searches for a [2, 2] priority pair
            for (int i = 0; i < Schools.Count; i++)
            {
                School CurSchool = SchoolsAsList[i];
                if (CurSchool.NextPick >= CurSchool.Results.Count) continue; // Checks if there are still more students to pick from

                Student StudentA = Students[CurSchool.Results[CurSchool.NextPick]];
                if (StudentA.currentPick != 2) continue; // Checks if the student may be swappable (an edge case to save time searching)
                if (StudentA.ChosenSchools[1] != CurSchool.Id) continue; // Student A prefers OtherSchool

                School OtherSchool = Schools[StudentA.ChosenSchools[1]];
                if (OtherSchool.NextPick >= OtherSchool.Results.Count) continue; // Checks if there are still more students to pick from

                Student StudentB = Students[OtherSchool.Results[OtherSchool.NextPick]];
                if (StudentB.currentPick != 2) continue; // Checks if the student may be swappable (an edge case to save time searching)
                if (StudentB.ChosenSchools[1] != OtherSchool.Id) continue; // Student B prefers OtherSchool

                OtherSchool.RemoveStudent(StudentA.Id);
                CurSchool.RemoveStudent(StudentB.Id);

                CurSchool.AcceptNewStudent();
                OtherSchool.AcceptNewStudent();

                i--; // To check the same school again for a new match
            }

            // Output

            foreach (School school in Schools.Values)
            {
                school.AcceptedStudents.Sort(); // Since it was originally ordered by test results
                Console.Write(school.Id.ToString());
                foreach (int AS  in school.AcceptedStudents)
                {
                    Console.Write(" " +  AS.ToString());
                }
                Console.WriteLine();
            }
        }
    }
}