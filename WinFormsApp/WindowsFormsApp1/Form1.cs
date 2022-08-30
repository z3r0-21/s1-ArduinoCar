using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmCar : Form
    {
        public frmCar()
        {
            InitializeComponent();
            Arduino.Open();
            timer1.Start();
            
            // *** *** *** IMPORTANT *** *** ***
            //
            //The code is working, however, if the timer frequency is set to a value lower tahn 10000ms (10 sec), the program start lagging (you can try it as well).
            //I believe that it should be set to not less than 100ms in order to work as a real car, however, if that is the case, the buttons do not work because the program is partially frozen.
            //By partially forzen I mean the following - the two program buttons can not be pressed, however, the blackbox, headlights status and temperature are updated just fine.
            //If it is set to update every 10 seconds or slower, the button work, but then because of the slow updates not all temperature, black box and headlights messages are shown.
            //
            //I am not sure whether the issue is caused by my PC or Visual Studio, but both the Arduino and Visual Studio codes should be perfectly working.
            //You can check that the car system is working perefectly fine by using the Arudino Serial Monitor. Type "hazard" and "normal" to simulate the two Windows form button clicks.

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Arduino.WriteLine("hazard"); //If the button is pressed it sends the message "hazard" to the Arduino.
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {   
            Arduino.WriteLine("normal"); //If the button is pressed it sends the message "normal" to the Arduino.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string message = Arduino.ReadLine(); //Reads the messages comming from the Arduino.
            

            if(message.Contains("C")) //Checks if the message contain the letter 'C' (only the temperature sensor message contains it).
            {
                lblInsideTemp.Text = $"The temperatrure inside the car is {message}."; //Display the temperature.
            }
            else if (message.Contains("Headlights")) //Checks if the message contain the word "Headlights".
            {
                if(message.Contains("OFF")) //Checks if the headlights message contain the word "OFF".
                {
                    lblHeadlights.Text = $"Headlights status: OFF";
                    lbxBlackBox.Items.Add(message); //Adds the message to the black box.
                }
                if (message.Contains("ON")) //Checks if the headlights message contain the word "ON".
                {
                    lblHeadlights.Text = $"Headlights status: ON"; 
                    lbxBlackBox.Items.Add(message); //Adds the message to the black box.
                }
            }
            else
            {
                lbxBlackBox.Items.Add(message); //All other events messages (left/right turns, hazard/alarm mode changes) are captures and stored in the black box.
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
