using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            label1.Text = now.ToString("HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Form 로드시 발생");
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            MessageBox.Show("Form 활성화시 발생");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Form 클로즈시 발생");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("진짜 닫을래?", "경고",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // (질문 , 질문창, 질문에 대한 대답, 아이콘종류)
            {
                e.Cancel = false; //취소를 안시켜서 종료
            }
            else
            {
                e.Cancel = true; //취소시켜서 종료 안됨
            }
        }

    }
}
