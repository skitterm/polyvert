using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

//how do I get listbox in here?                                                                  





namespace Cartography
{
    public class ProcessTimer
    {
         private Stopwatch myWatch;

            public Stopwatch yWatch
        {
            get
            {
                return myWatch;
            }

            set
            {
                myWatch = value;
            }
        }
       

            public List<LogEntry> LogEntries { get; private set; }

        /////////////need to make elapsed a timespan object

            //constructor
            public ProcessTimer()
            {
                //instantiates stopwatch class.
                yWatch = new Stopwatch();
                //instantiates list. 
                LogEntries = new List<LogEntry>();
                
               
            }

        
   
            //deconstructor
            public void DeconstructProcessTimer()
            {
                yWatch = null;
                LogEntries = null;
            }
        //clear log, stop watch as well. 


            public void Start()
            {
                yWatch.Start();
               
               
            }

            //stop

            public void Stop()
            {
                yWatch.Stop();
            }



            //addlog
            public void addlog(string info)     //so, what this is doing is...logs the current elapsed time and adds it to the list of log entries.
                                    //therefore, this uses the current elapsed time (elapsed) and adds to list. so it should be the 
            {//create new log 
                LogEntry log = new LogEntry();
                log.timeElapsed = elapsed();
                log.timeComment = info;
                LogEntries.Add(log);
            }
        //what if you got timecomment as input parameter? 
        //if(write to console) item.write to console

            //write1
            public void writetoConsole()
            {

                
                
                foreach(LogEntry value in LogEntries)
                {
                    

                    Console.WriteLine(value.timeElapsed.ToString() + "  " + value.timeComment);
                }

                
                

            }



            //write2
            public void writetoListBox(ref ListBox listbox)
            {
                foreach(LogEntry value in LogEntries)
                {
                    listbox.Items.Add(value.timeElapsed.ToString() + "  " + value.timeComment);
                }

                

            }




            //write3
            public void writetoTextBox(ref TextBox textbox)
            {
               foreach(LogEntry value in LogEntries)
               {
                   textbox.Text = textbox.Text + "         "   + (value.timeElapsed.ToString() + "  " + value.timeComment);
               }
                
                
            }


            

            //clearlog
            public void clearlog()
            {
                LogEntries.Clear();
               
            }



            //reset
            public void reset(bool wanttoclearlist = false)
            {
                yWatch.Restart();

               

                if (wanttoclearlist == true)
                {
                    LogEntries.Clear();
                }

                
            }




            //elapsed
            public TimeSpan elapsed()
            {
                TimeSpan spam = new TimeSpan();
                
                spam = yWatch.Elapsed;
                return spam;
            }





            public class LogEntry
            {
                public TimeSpan timeElapsed;

                public string timeComment;





                //constructors
                public LogEntry()
                {
                    timeElapsed = new TimeSpan();
                    timeElapsed = TimeSpan.MinValue;
                    timeComment = null;
                }




                //destructors
                public void deconstructLogEntry()
                {
                    timeElapsed = TimeSpan.MinValue;
                    timeComment = null;
                }



            }

       
        





    }
}
