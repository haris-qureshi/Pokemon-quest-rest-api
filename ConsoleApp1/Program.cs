using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {

         static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@"DEPARTMENT.csv");
            string[] emp_test = System.IO.File.ReadAllLines(@"EMPLOYEE.csv");
            string[] project_text = System.IO.File.ReadAllLines(@"PROJECT.csv");
            string[] works_on_text = System.IO.File.ReadAllLines(@"WORKS_ON.csv");

            List<Department> departments = new List<Department>();
            List<Employee> employees = new List<Employee>();
            List<Project> projects = new List<Project>();
            List<Works_On> works = new List<Works_On>();
            List<Project_toJson> project_ToJsons = new List<Project_toJson>();
            List<Employee_JSON> employee_JSONs = new List<Employee_JSON>();

            IDictionary<string, string> deparment_dict = new Dictionary<string, string>();
            IDictionary<string, string> project_dict = new Dictionary<string, string>();

            

            string[] temp;

            // this is for reading the lines in the department folder
            foreach (var t in lines)
            {
                Department thing = new Department();
                temp = t.Split(",");
                thing.department_name = temp[0];
                thing.department_number = temp[1];
                thing.manager_ssn = temp[2];
                thing.managet_start_date = temp[3];
                departments.Add(thing);
                deparment_dict.Add(temp[1],temp[0]);
                //Console.WriteLine(temp[1]+" "+temp[0]);

                //Console.WriteLine(thing.to_string());

            }

            foreach (var t in emp_test)
            {
                Employee thing = new Employee();
                temp = t.Split(",");
                thing.first_name = temp[0];
                thing.minit = temp[1];
                thing.last_name = temp[2];
                thing.ssn = temp[3];
                thing.birth_date = temp[4];
                thing.address = temp[5] +", "+ temp[6] + ", "+ temp[7];
                thing.sex = temp[8];
                thing.salary = temp[9];
                thing.super_ssn = temp[10];
                thing.deparment_number = temp[11];
                employees.Add(thing);
                //Console.WriteLine(thing.to_string());


            }

            foreach (var t in project_text)
            {
                Project thing = new Project();
                temp = t.Split(",");
                thing.project_name = temp[0];
                thing.project_number = temp[1];
                thing.project_location = temp[2];
                thing.deparment_number = temp[3];
                //Console.WriteLine(temp[3]);
                projects.Add(thing);
                project_dict.Add(temp[1], temp[0]);

                //Console.WriteLine(thing.to_string());
            }

            foreach (var t in works_on_text)
            {
                Works_On thing = new Works_On();
                temp = t.Split(",");
                thing.emp_ssn = temp[0];
                thing.project_number = temp[1];
                thing.hours = temp[2];
                works.Add(thing);
                //Console.WriteLine(thing.to_string());

            }
            foreach (var t in employees) 
            {
                Employee_JSON thing = new Employee_JSON();
                thing.first_name = t.first_name;
                thing.last_name = t.last_name;
                thing.department_name = deparment_dict[t.deparment_number];
               

                foreach (var p in works) 
                {
                    string t1 = t.ssn;
                    t1 = t1.Remove(0,1);

                    if (t1.Equals(p.emp_ssn)) 
                    {
                        Projects nnh = new Projects();
                        nnh.Pname = project_dict[p.project_number];
                        nnh.Pnumber = p.project_number;
                        nnh.hours = p.hours;
                        thing.projects.Add(nnh);
                    }
                }
                employee_JSONs.Add(thing);

            }//end of foreach loop for empJSON

            Console.WriteLine(JsonConvert.SerializeObject(employee_JSONs.ToArray(), Formatting.Indented));
            try
            {
                StreamWriter sw = new StreamWriter("EMPLOYEE.JSON");
                sw.Write(JsonConvert.SerializeObject(employee_JSONs.ToArray(), Formatting.Indented));
                sw.Flush();
                sw.Close();
            }
            catch (Exception e) 
            {
                Console.WriteLine("nope");
            }

            foreach (var t in projects) 
            {
                Project_toJson pj = new Project_toJson();
                pj.pname = t.project_name;
                pj.pnumber = t.project_number;
                pj.dname = deparment_dict[t.deparment_number];

                
                foreach (var p in employee_JSONs) 
                {
                    foreach (var n in p.projects) 
                    {
                        if (pj.pname.Equals(n.Pname)) 
                        {
                            Worker worker = new Worker();
                            worker.first_name = p.first_name;
                            worker.last_name = p.last_name;
                            worker.hours = n.hours;
                            pj.workers.Add(worker);
                        
                        }
                    }
                }
                project_ToJsons.Add(pj);
                

              
                
            }

            Console.WriteLine(JsonConvert.SerializeObject(project_ToJsons.ToArray(), Formatting.Indented));
            try
            {
                StreamWriter sw = new StreamWriter("PROJECTS.JSON");
                sw.Write(JsonConvert.SerializeObject(project_ToJsons.ToArray(), Formatting.Indented));
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("nope");
            }

        }//end of main
    }// end of class


    public class Department
    {
        public string department_name;
        public string department_number;
        public string manager_ssn;
        public string managet_start_date;

        public string to_string()
        {
            string send = department_name + " " + department_number + " " + manager_ssn + " " + managet_start_date;
            return send;
        }
    }

    public class Employee
    {
        public string first_name;
        public string minit;
        public string last_name;
        public string ssn;
        public string birth_date;
        public string address;
        public string sex;
        public string salary;
        public string super_ssn;
        public string deparment_number;

        public string to_string()
        {
            string send = "first name: "+first_name + " minit: " + minit + " last: " + last_name + " ssn: " + ssn + " bday: " + birth_date + " addy: " + address + " sex: " + sex + " sal: " + salary + " super ssn: " + super_ssn + " department:  " + deparment_number;
            return send;
        }

    }

    public class Project
    {
        public string project_name;
        public string project_number;
        public string project_location;
        public string deparment_number;
        public string to_string()
        {
            string send = project_name + " " + project_number + " " + project_location + " " + deparment_number;
            return send;
        }

    }
  
    public class Works_On 
    {
        public string emp_ssn;
        public string project_number;
        public string hours;

        public string to_string() 
        {
            string send = emp_ssn + " " + project_number + " " + hours;
            return send;
        }
    }



    public class Project_toJson
    {
        public string pname;
        public string pnumber;
        public string dname;
        public List<Worker> workers = new List<Worker>();

        public string to_string() 
        {
            string send = pname + " " + pnumber + " " + dname+" " ;
            foreach (var t in workers) 
            {
                send += t.last_name + " " + t.first_name + " " + t.hours;
            }
            return send;
        }
    }

    public class Worker 
    {
        public string last_name;
        public string first_name;
        public string hours;

    }

    public class Employee_JSON 
    {
        public string last_name;
        public string first_name;
        public string department_name;
        public List<Projects> projects = new List<Projects>();

        public string to_string() 
        {
            string send = last_name + " " + first_name + " " + department_name+ " ";
            foreach (var t in projects) 
            {
            send+=t.Pname+" "+t.Pnumber+" "+t.hours+ ""; 
            }
            return send;
        }
    }

    public class Projects 
    {
        public string Pname;
        public string Pnumber;
        public string hours;
    }

    


}

