using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenuApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            textBox1.Text += NewFile.Text + Environment.NewLine; // 새로운 라인 만들기 : environment.newline
            toolStripStatusLabel1.Text = NewFile.Text; // 현재 뭐하고 있는지 밑라벨에 띄워줌
        }

        private void 열기QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text += 열기QToolStripMenuItem.Text + Environment.NewLine;
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text += 저장SToolStripMenuItem.Text + Environment.NewLine;
            MessageBox.Show("저장했습니다");
        }

        private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // 프로그램 종료
        }

        private void 프로그램정보AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(); // 프로그램 정보보기 // 새항목으로 폼을 추가해줘야함
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e) // e는 자주 쓰임 // 마우스이벤트 
        {
            if (e.Button == MouseButtons.Right) //마우스 오른쪽 버튼을 눌렸을때,
            {
                contextMenuStrip1.Show(e.Location); //위치에서 보여줘라
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            LblMouseLocation.Text = $"(X , Y) = ({e.X} , {e.Y})"; // 마우스 좌표
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) // 툴바위에 마우스 올렸을때, 파일설명
        {
            NewFile_Click(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Add("C");
            toolStripComboBox1.Items.Add("C++");
            toolStripComboBox1.Items.Add("C#");
            toolStripComboBox1.Items.Add("Java");
        }
    }
}
