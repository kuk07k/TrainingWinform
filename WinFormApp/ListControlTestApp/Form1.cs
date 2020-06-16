using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListControlTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                listBox1.Items.Add(textBox1.Text);
                comboBox1.Items.Add(textBox1.Text);
            }
            textBox1.Text = "";
            textBox1.Focus(); // 텍스트박스 1번에 커서가 다시 가도록 설정
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1) // 아무것도 선택안했을때 == -1 // 따라서 값을 넣으면 0 , 1, 2 순차적으로 증가
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
                
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = "결과 : " + comboBox1.SelectedItem.ToString();
        }
    }
}
