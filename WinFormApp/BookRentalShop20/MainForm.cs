using MetroFramework.Forms;

namespace BookRentalShop20
{
    public partial class MainForm : MetroForm // namespace
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            LoginForm loginForm = new LoginForm(); // LoadForm 을 띄움
            loginForm.ShowDialog();
        }
    }
}
