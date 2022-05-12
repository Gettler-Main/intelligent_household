using System;
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
    public partial class LivingLight : UserControl
    {
        public LivingLight()
        {
            InitializeComponent();
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            Form1 f = new Form1();
            int t = -1;
            if (value) t = 1;
            else t = 0;
            f.send(Form1.scoketClient, "LivingLight:OP" + t.ToString());
        }
    }
}
