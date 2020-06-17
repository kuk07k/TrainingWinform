using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int index = 0;
        private void timer1_Tick(object sender, EventArgs e) //for문 X //자동으로 타이머가 돌아가기때문
        {
            index %= imageList1.Images.Count;
            label1.Image = imageList1.Images[index++];
        }
    }
}
