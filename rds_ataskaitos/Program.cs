using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace rds_ataskaitos
{
    class Program
    { 
   

        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer bTimer;
        private static bool isCompleted = false;
        static void Main(string[] args)
        {
            try
            {
                SetTimer();

                Console.WriteLine("\nPress the Enter key to exit the application...\n");
                Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
                Console.ReadLine();
                aTimer.Stop();
                aTimer.Dispose();
            }
            catch(Exception e)
            {
                Console.WriteLine(
                    "\nStackTrace ---\n{0}", e.StackTrace);
                Console.ReadLine();
            }
            
        }
        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(60000);

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            radio rds = new radio();
            rds.RDS_Load();


        }
        
        

    }

       
    
}
