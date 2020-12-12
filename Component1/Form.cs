using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Component1
{
    public partial class Form : System.Windows.Forms.Form
    {
        Graphics g;
        public Form()
        {
            InitializeComponent();
        }
        Creator factory = new Factory();
        Pen myPen = new Pen(Color.Black);
        public Color newcolor;
        int x = 0, y = 0;
        public int width = 0;
        public int height = 0;
        public int radius = 0;

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Title = "Browse file from specified folder";
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
                openFileDialog1.Filter = "DOCX files (.docx)|*.docx|All files (.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                //Browse .txt file from computer             
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                // Insert code to read the stream here.                        
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error: Unable to read file from disk. Original error: " + ex.Message);
                    }
                    //displays the text inside the file on TextBox named as txtInput                
                    txt_input_command.Text = File.ReadAllText(openFileDialog1.FileName);
                }
            }

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("file:///E:/Help.pdf");
        }

        private void gitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Shreyabaral/Component1");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writes = new StreamWriter(File.Create(save.FileName));
                writes.WriteLine(txt_input_command.Text);
                writes.Close();
                MessageBox.Show("File saved successfully!");
            }
        }

        private void txt_execution_command_TextChanged(object sender, EventArgs e)
        {

            if (txt_execution_command.Text.ToLower().Trim() == "run")
            {
                loadShapes();
            }
            else
            {
                if (txt_execution_command.Text.ToLower().Trim() == "clear")
                {
                    picDisplay.Invalidate();

                }
                else if (txt_execution_command.Text.ToLower().Trim() == "reset")
                {
                    txt_input_command.Clear();
                }
            }
        }

        private void loadShapes()
        {
            Graphics g = picDisplay.CreateGraphics();
            string command = txt_input_command.Text.ToLower();
            string[] commandline = command.Split(new String[] { "\n" },

            StringSplitOptions.RemoveEmptyEntries);
            int numberOfLines = txt_input_command.Lines.Length;
            for (int k = 0; k < commandline.Length; k++)
            {
                string[] cmd = commandline[k].Split(' ');
                if (cmd[0].Equals("moveto") == true)
                {
                    picDisplay.Refresh();
                    string[] param = cmd[1].Split(',');
                    if (param.Length != 2)
                    {
                        MessageBox.Show("Incorrect Parameter");
                    }
                    else
                    {
                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        moveTo(x, y);
                    }

                }
            }
        }

        private void generateRectangle(int width, int height)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
        }


        private void generateCircle(int radius)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
        }

        private void generateTriangle(int rBase, int adj, int hyp)
        {
            Pen po = new Pen(Color.Black, 2);
            Point[] pnt = new Point[3];

            pnt[0].X = x;
            pnt[0].Y = y;

            pnt[1].X = x - rBase;
            pnt[1].Y = y;

            pnt[2].X = x;
            pnt[2].Y = y - adj;
            g.DrawPolygon(po, pnt);
        }
        public void moveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }

        public void drawTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }
    }


}
