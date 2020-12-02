using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                    else if (cmd[0].Equals("triangle") == true)
                    {
                        string[] param = cmd[1].Split(',');
                        if (param.Length != 2)
                        {
                            MessageBox.Show("Incorrect Parameter");

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
