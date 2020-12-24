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
        SyntaxValidation syntaxCheck;
        public Form()
        {
            InitializeComponent();
            g = picDisplay.CreateGraphics();
        }
        Creator factory = new Factory();
        Pen myPen = new Pen(Color.Black);
        public Color newcolor;
        int counterLoop;
        int x = 0, y = 0;
        /// <summary>
        /// initialization of differnt variables
        /// </summary>
        public int counter = 0;
        public int dgSize = 0;
        public int width = 0;
        public int height = 0;
        public int radius = 0;
        /// <summary>
        /// used to browse the command that were saved 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// exits the program 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// has the document that entails about the program 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("file:///E:/Help.pdf");
        }
        /// <summary>
        /// redirects to github
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Shreyabaral/Component1");
        }
        /// <summary>
        /// used to save the command in specific file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Runs the program based on the command line provided in execution box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_execution_command_TextChanged(object sender, EventArgs e)
        {

            if (txt_execution_command.Text.ToLower().Trim() == "run")
            {
                if (txt_input_command.Text != null && txt_input_command.Text != "")
                {
                    syntaxCheck = new SyntaxValidation(txt_input_command);

                    if (!syntaxCheck.isSomethingInvalid)
                    {

                        loadShapes();
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
        /// <summary>
        /// loads the command in display panel
        /// </summary>
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
        /// <summary>
        /// runs the program after run command is provided
        /// </summary>
        /// <param name="oneLineCommand"></param>
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
                        MessageBox.Show("Please, provide correct if statement ");
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
        /// <summary>
        ///  used for returning the size of the shapes as per the commands provided
        /// </summary>
        /// <param name="lineOfCommand"></param>
        private void generateDrawCommand(string lineOfCommand)
        {
            string[] shapes = { "circle", "rectangle", "triangle" };
            String[] variable = { "radius", "width", "height", "counter", "size" };

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] cmd = lineOfCommand.Split(' ');
            //removing white spaces in between cmd
            for (int i = 0; i < cmd.Length; i++)
            {
                cmd[i] = cmd[i].Trim();
            }
            String firstWord = cmd[0].ToLower();
            Boolean firstcmdhape = shapes.Contains(firstWord);
            if (firstcmdhape)
            {

                if (firstWord.Equals("circle"))
                {
                    Boolean secondWordIsVariable = variable.Contains(cmd[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (cmd[1].ToLower().Equals("radius"))
                        {
                            generateCircle(radius);
                        }
                    }
                    else
                    {
                        generateCircle(Int32.Parse(cmd[1]));
                    }

                }
                else if (firstWord.Equals("rectangle"))
                {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            generateRectangle(width, height);
                        }
                        else
                        {
                            generateRectangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            generateRectangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            generateRectangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }
                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    generateTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]));
                }

            }
            else
            {
                if (firstWord.Equals("loop"))
                {
                    counter = int.Parse(cmd[1]);
                    int loopStartLine = (getLoopStartLineNumber());
                    int loopEndLine = (getLoopEndLineNumber() - 1);
                    counterLoop = loopEndLine;
                    for (int i = 0; i < counter; i++)
                    {
                        for (int j = loopStartLine; j <= loopEndLine; j++)
                        {
                            String oneLineCommand = txt_input_command.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                commandRun(oneLineCommand);
                            }
                        }
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(cmd[1]))
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
                            String oneLineCommand = txt_input_command.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                commandRun(oneLineCommand);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// sets  the size of the shapes based on user input
        /// </summary>
        /// <param name="lineCommand"></param>
        /// <returns></returns>
        private int getSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }
        /// <summary>
        /// Initiate whether if statement is present in the commands given in the command box
        /// </summary>
        /// <returns></returns>
        private int getIfStartLineNumber()
        {
            int numberOfLines = txt_input_command.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_input_command.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
        /// <summary>
        /// Checks if if statement ended with endif statement
        /// </summary>
        /// <returns></returns>
        private int getEndifEndLineNumber()
        {
            int numberOfLines = txt_input_command.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_input_command.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
        /// <summary>
        /// Initiates loop as per command given in the command box 
        /// </summary>
        /// <returns></returns>
        private int getLoopStartLineNumber()
        {
            int numberOfLines = txt_input_command.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_input_command.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("loop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;

        }
        /// <summary>
        /// Checks if the loop has ended with end loop
        /// </summary>
        /// <returns></returns>

        private int getLoopEndLineNumber()
        {
            try
            {
                int numberOfLines = txt_input_command.Lines.Length;
                int lineNum = 0;

                for (int i = 0; i < numberOfLines; i++)
                {
                    String oneLineCommand = txt_input_command.Lines[i];
                    oneLineCommand = oneLineCommand.Trim();
                    if (oneLineCommand.ToLower().Equals("endloop"))
                    {
                        lineNum = i + 1;

                    }
                }
                return lineNum;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// Draws rectangle based on user input
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>

        private void generateRectangle(int width, int height)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
        }
        /// <summary>
        /// Draws circle based on user input
        /// </summary>
        /// <param name="radius"></param>

        private void generateCircle(int radius)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
        }
        /// <summary>
        /// Draws Triangle based on user input
        /// </summary>
        /// <param name="rBase"></param>
        /// <param name="adj"></param>
        /// <param name="hyp"></param>
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
        /// <summary>
        /// sets the value of x-axis and y-axis
        /// </summary>
        /// <param name="toX">x-coordinate</param>
        /// <param name="toY"> y- coordinate</param>
        public void moveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }
        /// <summary>
        /// sets the position of the x and y axis
        /// </summary>
        /// <param name="toX"> x-axis</param>
        /// <param name="toY">y-axis </param>
        public void drawTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }
    }


}
