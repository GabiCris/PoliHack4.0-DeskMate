using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DeskMate
{
    public partial class MainForm : Form
    {

        public static DateTime time = DateTime.Now;
        public static TimeSpan ts = new TimeSpan();
        public int seconds = 0;

        private KeyHandler ghk;
        private static int BackSpaceCount = 0;
        private ContextMenu sysTrayMenu;

        
        public MainForm()
        {
            this.Visible = false;
            this.Hide();

            this.ShowInTaskbar = false;
            this.KeyPreview = true;
            //// BACKSPACE HANDLER
            ghk = new KeyHandler(Keys.Back, this);
            ghk.Register();

            InitializeComponent();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /// GENERATE TRAY MENU
            sysTrayMenu = new ContextMenu();
            sysTrayMenu.MenuItems.Add("Recipe of the Day", GenerateRecipe);
            sysTrayMenu.MenuItems.Add("Smokes Aday", GenerateSmokes);
            sysTrayMenu.MenuItems.Add("Exit DeskMate", OnExit);

            // Adding menu and showing it
            notifyIcon1.Text = "DeskMate";
            notifyIcon1.ContextMenu = sysTrayMenu;
            notifyIcon1.Visible = true;

        }

        /// SYS TRAY BUTTONS FUNCTIONS
        private void OnExit (object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void GenerateRecipe (object sender, EventArgs e)
        {
            //
        }
        private void GenerateSmokes (object sender, EventArgs e)
        {
            Smoking sm = new Smoking();
            sm.Show();
        }


        private void reminder_Function()
        {
            ts = DateTime.Now - time;

            ReminderForm reminder_form = new ReminderForm();
            reminder_form.StartPosition = FormStartPosition.Manual;
            //reminder_form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - reminder_form.Width,
            //                      Screen.PrimaryScreen.WorkingArea.Height - reminder_form.Height);
            reminder_form.Location = new Point(Screen.FromPoint(reminder_form.Location).WorkingArea.Right - reminder_form.Width - 10, 10);
            // reminder_form.Location
            reminder_form.TopMost = true;
            reminder_form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds = seconds + 7;
            reminder_Function();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Back)
            {
                BackSpaceCount++;
                MessageBox.Show("You pressed the key");
                return true;    // indicate that you handled this keystroke
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        ////METHODS FOR BACKSPACE KEY
        ////
        private void HandleHotkey()
        {
            BackSpaceCount++;
            MessageBox.Show(Convert.ToString(BackSpaceCount));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }

    }
}
