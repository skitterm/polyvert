using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
                         //I re-write all of the code? Or write once and then call the method?
                         //Need to write form program too? Error message for my text/list boxes. 
                            // Do I need to find a way for them to input the comments in the main method, not in the library? 
                         
namespace Cartography
{
    public class ProcessTimer_Inherit : Stopwatch
    {

        public class Logentry
        {
            public string comments;

            public TimeSpan timespan;


            public Logentry()
            {
                timespan = new TimeSpan();
                timespan = TimeSpan.MinValue;
                comments = "";

            }


            public void deconstructortimespan()
            {
                timespan = TimeSpan.MinValue;

                comments = null;
            }


        }

       

        public List<Logentry> logentries { get; private set; }


        //constructor. 
        public ProcessTimer_Inherit()
        {
           

            logentries = new List<Logentry>();
        }


        public void deconstruct()
        {
            
            logentries = null;
        }



       



        public void addlog(string information)
        {
            Logentry log = new Logentry();
            log.timespan = Elapsed;
            log.comments = information;
            logentries.Add(log);
            
        }


        public void writetoConsole()
        {

            foreach (Logentry value in logentries)
            {


                Console.WriteLine(value.timespan.ToString() + "  " + value.comments);
            }

        }

        public void clearlog()
        {
            logentries.Clear();
        }

        //to hide
        new public void Start()//hides the inherited start method
        {
            base.Start();
            addlog("Timer Started");
        }

        new public void Stop()
        {
            base.Stop();
            addlog("Timer Stopped");
        }














    }
}
