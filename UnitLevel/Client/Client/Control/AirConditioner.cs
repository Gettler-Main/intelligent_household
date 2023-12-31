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
    public partial class AirConditioner : UserControl
    {
        public AirConditioner()
        {
            InitializeComponent();
        }


        private void uiIntegerUpDown1_ValueChanged(object sender, int value)
        {
            Form1 f = new Form1();
            if (uiSwitch1.Active)
            {
                f.send(Form1.scoketClient, "AirCondition:OP2" + value.ToString());
            }
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            Form1 f = new Form1();
            int t = -1;
            if (value)
            {
                uiIntegerUpDown1.ReadOnly = false;
                t = 1;
            }
            else
            {
                uiIntegerUpDown1.ReadOnly = true;
                t = 0;
            }
            f.send(Form1.scoketClient, "AirCondition:OP" + t.ToString());
        }
    }
}
