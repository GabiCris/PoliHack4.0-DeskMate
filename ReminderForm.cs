using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Plasmoid.Extensions;
using System.Drawing.Text;
using DeskMate.Properties;

namespace DeskMate
{
    public partial class ReminderForm : Form
    {
        public string[] stretch_seconds = new string[2] { "Do 20 crunches!" , "Try your luck with "};

        //IMport for rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );


        public ReminderForm()
        {
            this.ShowInTaskbar = false;
            InitializeComponent();
            // show seconds 
            
            label2.Text = "You have been sitting down for " + Convert.ToString(MainForm.ts.Seconds) + " seconds." ;
           
            label1.Text = Convert.ToString(3);
            /*
            SolidBrush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush);
            System.Drawing.Graphics g = this.CreateGraphics();
            g.DrawRoundedRectangle(pen, 85, 20, 400, 150, 35);
            */

            //rounded edges
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
            

        }

        private void timer_counter_Tick(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(Convert.ToInt32(label1.Text) - 1);
            if (Convert.ToInt32(label1.Text) == 0)
                this.Close();
        }

        private void ReminderForm_MouseHover(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.time = DateTime.Now;
            this.Close();

        }
    
    }
}

