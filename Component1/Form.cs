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
        int x = 0, y = 0, width, height, radius;

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
                Graphics g = picDisplay.CreateGraphics();
                string command = txt_input_command.Text.ToLower();
                string[] commandline = command.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < commandline.Length; i++)
                {
                    string[] cmd = commandline[i].Split(' ');
                    if (cmd[0].Equals("moveto") == true)
                    {
                        picDisplay.Refresh();
                        string[] param = cmd[1].Split(',');
                        if (param.Length != 2)
                        {
                            MessageBox.Show("Invalid Input");
                        }
                        else
                        {
                            Int32.TryParse(param[0], out x);
                            Int32.TryParse(param[1], out y);
                            moveTo(x, y);
                        }
                    }
                    else if (cmd[0].Equals("drawto") == true)
                    {
                        string[] param = cmd[1].Split(',');
                        int x = 0, y = 0;
                        if (param.Length != 2)
                        {
                            MessageBox.Show("Invalid Input");
                        }
                        else
                        {
                            Int32.TryParse(param[0], out x);
                            Int32.TryParse(param[1], out y);
                            drawTo(x, y);
                        }
                    }

                    else if (cmd[0].Equals("rectangle") == true)
                    {
                        if (cmd.Length < 2)
                        {
                            MessageBox.Show("Invalid Input");
                        }
                        else
                        {
                            string[] param = cmd[1].Split(',');
                            if (param.Length < 2)
                            {
                                MessageBox.Show("Invalid Input ");

                            }
                            else
                            {
                                Int32.TryParse(param[0], out width);
                                Int32.TryParse(param[1], out height);
                                IBasicShapes circle = factory.getShape("rectangle");
                                Rectangle r = new Rectangle();
                                r.set(Color.Black, x, y, width, height);
                                r.draw(g);
                            }
                        }
                    }
                    else if (cmd[0].Equals("circle") == true)
                    {
                        if (cmd.Length != 2)
                        {
                            MessageBox.Show("Incorrect Parameter");
                        }
                        else
                        {
                            if (cmd[1].Equals("radius") == true)
                            {
                                IBasicShapes circle = factory.getShape("circle");
                                Circle c = new Circle();
                                c.set(Color.AliceBlue, x, y, radius);
                                c.draw(g);
                            }
                            else
                            {
                                Int32.TryParse(cmd[1], out radius);
                                IBasicShapes circle = factory.getShape("circle");
                                Circle c = new Circle();
                                c.set(Color.AliceBlue, x, y, radius);
                                c.draw(g);
                            }
                        }
                    }
                    else if (cmd[0].Equals("triangle") == true)
                    {
                        string[] param = cmd[1].Split(',');
                        if (param.Length != 2)
                        {
                            MessageBox.Show("Invalid Input");

                        }
                        else
                        {
                            Int32.TryParse(param[0], out width);
                            Int32.TryParse(param[1], out height);
                            IBasicShapes circle = factory.getShape("triangle");
                            Triangle r = new Triangle();
                            r.set(Color.Black, x, y, width, height);
                            r.draw(g);
                        }
                    }
                    else if (!cmd[0].Equals(null))
                    {
                        int errorLine = i + 1;
                        MessageBox.Show("Invalid command recognised on line " + errorLine, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
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
