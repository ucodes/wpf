using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            //var script = "Get-Process | select -Property @{N='Name';E={$_.Name}},@{N='CPU';E={$_.CPU}}";

            var script = "get-service";
         

            var powerShell = PowerShell.Create().AddScript(script);

            foreach (dynamic item in powerShell.Invoke().ToList())
            {
                //check if the CPU usage is greater than 10

                Console.WriteLine("The process greater than 10 CPU counts is : " + item.DisplayName);
            }
            Console.ReadLine();
        }
        
   }

  
}
