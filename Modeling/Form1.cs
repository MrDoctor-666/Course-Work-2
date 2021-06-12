using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modeling
{
    public partial class Form1 : Form
    {
        Factory factory;
        int ticks = 0, time = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            int[] C = new int[3];
            int[] D = new int[3];
            C[0] = (int)numericUpDown2.Value;
            C[1] = (int)numericUpDown3.Value;
            C[2] = (int)numericUpDown4.Value;
            D[0] = (int)numericUpDown5.Value;
            D[1] = (int)numericUpDown6.Value;
            D[2] = (int)numericUpDown7.Value;
            factory = new Factory(C, D);
            factory.SetWorkingParamets((int)numericUpDown1.Value, (int)numericUpDown9.Value, (int)numericUpDown8.Value);
            timer2.Start();
            label10.Text = "Process is not finished";
            ticks = 0;
        }

        //for each new iteration
        private void timer2_Tick(object sender, EventArgs e)
        {
            time = factory.Work();
            //check if processing is finished
            if (time == -1) { 
                timer2.Stop(); ShowStats();
                label10.Text = "Process is finished";
            }
            else timer2.Interval = time * 50;

            //get statictic and put it into listbox
            ShowStats();

            ticks++;
        }

        private void ShowStats()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listView1.Clear();
            listBox3.Items.Clear();
            Statistic curSats = new Statistic(factory);
            if (ticks == 0) curSats.PrintBeginning();
            //current time and ready details
            listBox1.Items.Add("Time: " + factory.minutesWorked.ToString());
            listBox1.Items.Add("Ready details: " + curSats.readyDetails.Count);
            for (int i = 0; i < 3; i++)
               if (curSats.procentage[i] != 0) listBox1.Items.Add(curSats.procentage[i].ToString() + "% of type " + curSats.prTypes[i].ToString());
            //queues in machines current
            for (int i = 0; i < 3; i++)
            {
                listBox2.Items.Add("Machine " + (i+1));
                listBox2.Items.Add("    Queue: " + curSats.Queues[i]);
            }
            //all details info
            listBox3.Items.Add("All details: " + curSats.allD.Length/2);
            for (int i = 0; i < 3; i++)
                listBox3.Items.Add("Details of type " + Enum.GetValues(typeof(Type)).GetValue(i).ToString() + " : " + curSats.allDetails[i].Count);
            //list of all detais
            ListViewItem lvi = new ListViewItem();
            lvi.Text = curSats.allD;
            listView1.Items.Add(lvi);
            curSats.PrintToFile();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curItem = listBox2.SelectedIndex;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ticks != 0 && time != -1)
            {
                if (button2.Text == "Pause")
                {
                    timer2.Stop();
                    button2.Text = "Continue";
                }
                else
                {
                    timer2.Start();
                    button2.Text = "Pause";
                }
            }
        }
    }
}
