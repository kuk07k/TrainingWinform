using MetroFramework;
using MetroFramework.Forms;
using System.Windows.Forms;

namespace BookRentalShop20
{
    public partial class MainForm : MetroForm // namespace
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e) //메인폼(엄마폼)
        {
            LoginForm loginForm = new LoginForm(); // LoadForm 을 띄움
            loginForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MetroMessageBox.Show(this, "정말 종료하시겠습니까?", "종료", 
                                    MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                foreach (Form item in this.MdiChildren) // 닫을때 에러 없애기
                {
                    item.Close(); // metro item 창을 전부 내리고 닫음
                }
                e.Cancel = false; // 닫히는거
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void InitChildForm(Form form, string strFormTitle)
        {
            form.Text = strFormTitle;
            form.Dock = DockStyle.Fill;
            form.MdiParent = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        private void MnuItem_DivMng_Click(object sender, System.EventArgs e) // 자식폼
        {
            DivForm form = new DivForm();
            InitChildForm(form, "구분코드 관리");
        }

        private void 사용자관리UToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            UserForm form = new UserForm();
            InitChildForm(form, "사용자코드 관리");
        }
    }
}
