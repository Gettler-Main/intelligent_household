﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Control
{
    public partial class BedroomLight : UserControl
    {
        public BedroomLight()
        {
            InitializeComponent();
        }

        private void BedroomLight_Load(object sender, EventArgs e)
        {
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            Form1 f = new Form1();
            int t = -1;
            if (value) t = 1;
            else t = 0;
            f.send(Form1.scoketClient, "BedroomLight:OP" + t.ToString());
        }

        private void uiLight1_Click(object sender, EventArgs e)
        {

        }

        private void uiLabel2_Click(object sender, EventArgs e)
        {

        }

        private void uiLabel1_Click(object sender, EventArgs e)
        {

        }

        private void uiLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
