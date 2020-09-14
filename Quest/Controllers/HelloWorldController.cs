using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Quest.Controllers.ReadCSV;
using Microsoft.AspNetCore.CookiePolicy;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;

namespace Quest.Controllers
{
    public class HelloWorldController : Controller
    {
        //Get: /HelloWorld/
        // dotnet watch run this works like django hit control s server reloads 
        // to run this on the terminal go the folder Quest and run the command 
        //dotnet run -v q
        //it will give you the link use the first one 
        // run this ./ngrok http https://localhost:5001 -host-header="localhost:5001" 5001 is the port number run this in the ngrok file on the desktop 
        // use this http://localhost:4040/inspect/http to get the domain for the api calls

        public string Index() 
                {
                    return "this is the index action or page";
                }
        

        //Get /HelloWorld/all_moves
        public string all_moves() 
        {
            Excel e = new Excel();
            string send = "error";
            send = e.all_moves(@"/home/pi/Desktop/resume-projects/Quest/Controllers/Pokemon Quest Movesheet.csv");
            return send;
        }



        // Get:/HelloWorld/thing
        //HelloWorld/certain_move?name=enter name&ti=int
        public string certain_move(string name)
        {
            Excel ex = new Excel();
            string [] send = ex.read(@"/home/pi/Desktop/resume-projects/Quest/Controllers/Pokemon Quest Movesheet.csv", name, 0);
            //Type,Tier,Attack,Wait,WL,WW,BB,SC,SH,ST,In-game Description
            //make a dictionary to send the stuff like json format
            Dictionary<string, string> tosend = new Dictionary<string, string>();
            tosend.Add("Name",send[0]);
            tosend.Add("Type", send[1]);
            tosend.Add("Tier",send[2]);
            tosend.Add("Wait",send[3]);
            tosend.Add("Description",send[11]);

            string js = JsonConvert.SerializeObject(tosend, Formatting.Indented);
            return js;
            //return HtmlEncoder.Default.Encode($"Hello {name}, times is : {ti}");

        }

        // get: HelloWorld/pokemon
        public string pokemon() 
        {
            Excel ex = new Excel();


            string[] js = ex.read_all(@"/home/pi/Desktop/resume-projects/Quest/Controllers/Pokemon Quest.csv");
            string[] temp;
            List<pokemon> send = new List<pokemon>();
            int i = 0;
            
            foreach (var t in js) 
            {
                if (i == 0)
                    i++;
                else 
                {

                    pokemon pk = new pokemon();
                    temp = t.Split(",");

                    pk.number = temp[0];
                    pk.name = temp[1];
                    pk.type_one = temp[13];
                    pk.auto = temp[12];
                    pk.hp = temp[15];
                    pk.att = temp[16];
                    pk.total = temp[17];
                    if (temp[14].Equals(""))
                        pk.type_two = "no type 2";
                    else pk.type_two = temp[14];


                    // this function adds to the moves list if the spot is empty it skips it
                    for (int j = 2; j < 12; j++) 
                    {
                        if (temp[j].Equals("")) 
                        {
                            break; // on the first empty move we can just break out the loop
                        }

                        else
                        {
                            pk.moves.Add(temp[j]);
                        }
                    }


                    send.Add(pk);

                }

            }
            

            return JsonConvert.SerializeObject(send.ToArray(), Formatting.Indented);

        }
        //HelloWorld/certain_pokemon? pokemon = enter pokemon
        public string certain_pokemon(string pokemon) 
        {

            return "oof";
        }

        //so in the Startup.cs file if you look at {controller=Home}/{action=Index}/{id?}") the id portion is optional
        // any public function is a url or else they are not just make it private so onbly the calss can access them and no one else 
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
    }
}