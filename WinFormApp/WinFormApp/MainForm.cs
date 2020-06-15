using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp
{
    public partial class MainForm : Form // Form 이라는 클래스에 상속받음
    {
        public MainForm()
        {
            InitializeComponent(); // 필수 : 디자인을 보여주기 위한 메소드 (건들지 마세요 !!!)
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnMessage_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            // MessageBox.Show($"Hello World // {now}");
            TxtCurrentDate.Text = now.ToString();
        }
    }
}
