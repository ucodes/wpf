using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                var PSScript = "Get-Service";

                // use "AddScript" to add the contents of a script file to the end of the execution pipeline.
                // use "AddCommand" to add individual commands/cmdlets to the end of the execution pipeline.
                PowerShellInstance.AddScript(PSScript);

                Collection <PSObject> PSO = PowerShellInstance.Invoke();

                if (PowerShellInstance.Streams.Error.Count > 0)
                {
                       // var error = PowerShellInstance.Streams.Error as Collection<ErrorRecord>;
                    var error = PowerShellInstance.Streams.Error.ReadAll() as Collection<ErrorRecord>;
                    if (error != null)
                        {
                            foreach (ErrorRecord er in error)
                            {
                            TextBox1.AppendText("[PowerShell]: Error in cmdlet: " + er.Exception.Message + "\n");
                            }
                        }
                    return;
                }

                if (PSO.Count() == 0)

                {
                    TextBox1.AppendText("Null PS object was returned during the script" + "\n");
                }
                

                // loop through each output object item
                foreach (dynamic outputItem in PSO.ToList())
                {
                    // if null object was dumped to the pipeline during the script then a null
                    // object may be present here. check for null to prevent potential NRE.

                    if (outputItem == null)

                    {
                        TextBox1.AppendText("Null null object was dumped to the pipeline during the PS script" + "\n");
                    }

                    if (outputItem != null)

                    {
                        //TODO: do something with the output item  
                        TextBox1.AppendText(outputItem.DisplayName+"\n");
                    }
                }

            }
        } 
    }
}
