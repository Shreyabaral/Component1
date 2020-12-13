using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        int counterLoop;
        int x = 0, y = 0;
        public int counter = 0;
        public int dgSize = 0;
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



                for (counterLoop = 0; counterLoop < numberOfLines; counterLoop++)
                {
                    String oneLineCommand = txt_input_command.Lines[counterLoop];
                    oneLineCommand = oneLineCommand.Trim();
                    if (!oneLineCommand.Equals(""))
                    {
                        commandRun(oneLineCommand);
                    }

                }
            }
        }

        private void commandRun(String oneLineCommand)
        {


            Boolean hasPlus = oneLineCommand.Contains("+");
            Boolean hasEquals = oneLineCommand.Contains("=");
            if (hasEquals)
            {
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                for (int i = 0; i < cmd.Length; i++)
                {
                    cmd[i] = cmd[i].Trim();
                }
                String firstWord = cmd[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (getIfStartLineNumber());
                    int ifEndLine = (getEndifEndLineNumber() - 1);
                    counterLoop = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand1 = txt_input_command.Lines[j];
                            oneLineCommand1 = oneLineCommand1.Trim();
                            if (!oneLineCommand1.Equals(""))
                            {
                                commandRun(oneLineCommand1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Provided if statement is false");
                    }
                }
                else
                {
                    string[] cmd2 = oneLineCommand.Split('=');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("counter"))
                    {
                        counter = int.Parse(cmd2[1]);
                    }
                }
            }
            else if (hasPlus)
            {
                oneLineCommand = System.Text.RegularExpressions.Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                if (cmd[0].ToLower().Equals("repeat"))
                {
                    counter = int.Parse(cmd[1]);
                    if (cmd[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = getSize(oneLineCommand);
                        radius = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            generateCircle(radius);
                            radius += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = getSize(oneLineCommand);
                        dgSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            generateRectangle(dgSize, dgSize);
                            dgSize += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = getSize(oneLineCommand);
                        dgSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            generateTriangle(dgSize, dgSize, dgSize);
                            dgSize += increaseValue;
                        }
                    }
                }
                else
                {
                    string[] cmd2 = oneLineCommand.Split('+');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(cmd2[1]);
                    }
                }
            }
            else
            {
                generateDrawCommand(oneLineCommand);
            }


        }

        private void generateDrawCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle" };
            String[] variable = { "radius", "width", "height", "counter", "size" };
        }


        private int getSize(string lineCommand)
        {
            int value = 0;
            return value;
        }

        private int getIfStartLineNumber()
        {
            int numberOfLines = txt_input_command.Lines.Length;
            int lineNum = 0;
            return lineNum;
        }

        private int getEndifEndLineNumber()
        {
            int numberOfLines = txt_input_command.Lines.Length;
            int lineNum = 0;

            
            return lineNum;
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
