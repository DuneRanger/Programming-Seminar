using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Form1 : Form
    {
        Label[] labelList;
        bool[] foundList;
        int otoceno = 0;
        int prvniTag = -1;

        public Form1()
        {
            InitializeComponent();

            labelList = new Label[16];
            foundList= new bool[16];

            for (int i = 0; i < labelList.Length; i++)
            {
                labelList[i] = new Label();
                foundList[i] = false;
                this.Controls.Add(labelList[i]);
                Label l = labelList[i];


                l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                l.Location = new System.Drawing.Point(50 * (i / 4) + 50, 50 * (i % 4) + 50);
                l.Name = "label" + i;
                l.Size = new System.Drawing.Size(40, 40);
                l.TabIndex = 1;
                l.Tag = i / 2;
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                l.Text = "";

                l.Click += new System.EventHandler(pexesoClick);
            }
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
        }

        private void pexesoClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            if (otoceno == 2)
            {
                if ((int)l.Tag == prvniTag)
                {
                    foundList[(int)l.Tag] = true;
                    foundList[(int)l.Tag+1] = true;
                }
                for (int i = 0; i < labelList.Length; i++)
                {
                    if (!foundList[i]) labelList[i].Text = "";
                }
                otoceno = 0;
            }
            else
            {
                l.Text = l.Tag.ToString();
                otoceno++;
                prvniTag = (int)l.Tag;
            }
        }


    }
}