using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//C:\Users\haris\source\repos\Quest\Controllers\Pokemon Quest Movesheet.csv
namespace Quest.Controllers.ReadCSV
{

    public class pokemon 
    {
        public string number;
        public string name;
        public string type_one,type_two;
        public string hp, att,auto,total;
        public List<string> moves = new List<string>();

    }

    public class attack_attributes
    {
    public string type;
    public string tier;
    public string attack;
    public string wait;
    public string description;
        
    }

    
    public class attack 
    {
        public string name;
        public attack_attributes stats = new attack_attributes();
    }

    public class Excel
    {

        public string[] read(string Path, string search,int col_name) 
        {
            string[] send = {"Nothing found"};
            string[] fields = {""};
            try 
            {
                string[] lines = System.IO.File.ReadAllLines(@Path);
                for (int i = 0; i < lines.Length; i++) 
                {

                    fields = lines[i].Split(",");
                    if (fields[col_name].Equals(search)) {
                        Console.WriteLine("Record found");
                        return fields;

                    }

                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("there was an error");
                return send;
            }



            return send;
        }

        public string all_moves(string Path) {
            string[] feilds;
            List<attack> attacks = new List<attack>();
            int i = 0;
            try {
                string[] lines = System.IO.File.ReadAllLines(@Path);
                foreach (var t in lines) 
                {
                    if (i == 0)
                    {
                        i++;

                    }
                    else
                    {
                        attack temp = new attack();
                        feilds = t.Split(",");
                        temp.name = feilds[0];
                        temp.stats.type = feilds[1];
                        temp.stats.tier = feilds[2];
                        temp.stats.attack = feilds[3];
                        temp.stats.wait = feilds[4];
                        temp.stats.description = feilds[11];
                        attacks.Add(temp);
                    }
                }
                
            }
            catch (Exception ex) 
            {
                Console.WriteLine("error");
            }

            string js = JsonConvert.SerializeObject(attacks.ToArray(), Formatting.Indented);
            return js;
        }


        public string[] read_all(string path) 
        {
            List<string> send = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(@path);
            try 
            {
                
                
                foreach (var t in lines) 
                {
                    send.Add(t);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(" there was an error");
            }
            return send.ToArray();

        
        }
    
    
    }




}
