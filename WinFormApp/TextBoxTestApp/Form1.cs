using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextBoxTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // textBox3.Text = "ID : " + textBox1.Text + Password : " + textBox2.Text; // 옛날방식

            textBox3.Text = $"ID : {textBox1.Text} \r\nPassword : {textBox2.Text}"; // 최신방식

            if ((textBox1.Text == "admin") 
                && (textBox2.Text == "p@ssw@rd!!")) 
            {
                MessageBox.Show("관리자로그인!!");

                /*
                if ((textBox1.Text.ToUpper() == "ADMIN")
                 && (textBox2.Text.ToUpper() == "P@SSW@RD!!")) // C#은 대소문자 구분하기 때문에 ToUpper() 사용
                {
                    MessageBox.Show("관리자로그인!!");
                }
                */
            }
        }
    }
}
