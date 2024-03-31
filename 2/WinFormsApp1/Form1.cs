using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class PexesoForm : Form
    {
        Label[] labelList;
        int otoceno = 0;

        public PexesoForm()
        {
            InitializeComponent();

            labelList = new Label[16];

            for (int i = 0; i < labelList.Length; i++)
            {
                labelList[i] = new Label();
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
            if (otoceno == 2)
            {
                foreach (var t in labelList)
                {
                    t.Text = "";
                }
                otoceno = 0;
            }
            else
            {
                Label l = (Label)sender;
                l.Text = l.Tag.ToString();
                otoceno++;
            }
        }


    }
}