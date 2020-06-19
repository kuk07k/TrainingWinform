using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BookRentalShop20
{
    public partial class UserForm : MetroForm
    {
        string strConnString = "Data Source=192.168.0.126;Initial Catalog = BookRentalshopDB; Persist Security Info=True;User ID = sa; Password=p@ssw0rd!!";
        string mode = "";
        public UserForm()
        {
            InitializeComponent();
        }

        private void DivForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            using (SqlConnection conn = new SqlConnection(strConnString)) 
            {
                conn.Open(); 
                String strQuery = "SELECT id, userID, password, lastLoginDt, loginIpAddr " +
                                  "  FROM dbo.userTbl ";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strQuery, conn); 
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "userTbl");

                GridUserTbl.DataSource = ds;
                GridUserTbl.DataMember = "userTbl";
            }

            DataGridViewColumn 
            column = GridUserTbl.Columns[0]; // id컬럼
            column.Width = 40;
            column.HeaderText = "순번";

            column = GridUserTbl.Columns[1]; // userid컬럼
            column.Width = 80;
            column.HeaderText = "아이디";

            column = GridUserTbl.Columns[2]; // password컬럼
            column.Width = 100;
            column.HeaderText = "패스워드";

            column = GridUserTbl.Columns[3]; // 최종접속시간
            column.Width = 120;
            column.HeaderText = "최종접속시간";

            column = GridUserTbl.Columns[4]; // 접속IP주소
            column.Width = 150;
            column.HeaderText = "접속IP주소";
        }
        /// <summary>
        /// 그리드 셀클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow data = GridUserTbl.Rows[e.RowIndex];
                TxtID.Text = data.Cells[0].Value.ToString();
                TxtuserID.Text = data.Cells[1].Value.ToString();
                TxtPassword.Text = data.Cells[2].Value.ToString();

                mode = "UPDATE"; // 수정은 UPDATE
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearTextControl();

            mode = "INSERT"; // 신규는 INSERT
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtuserID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MetroMessageBox.Show(this, "빈값은 저장할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveProcess();
            UpdateData(); // 이미 수정
            ClearTextControl();
        }

        private void ClearTextControl()
        {
            TxtID.Text = TxtuserID.Text = TxtPassword.Text = ""; // 값을 비우기
            //TxtuserID.ReadOnly = false;
            TxtuserID.Focus();
        }

        private void SaveProcess()
        {
            if (string.IsNullOrEmpty(mode))
            {
                MetroMessageBox.Show(this, "신규버튼을 누르고 데이터를 저장하십시오", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //DB저장프로세스
            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strQurey = ""; // 쿼리만들기

                //*************** 1. 필요한 쿼리문을 정해주기 ******************//
                if (mode == "UPDATE") // UPDATE 할때 쿼리
                {
                    strQurey = "UPDATE dbo.userTbl " +
                               "    SET userID = @userID " +
                               "    , password = @password " +
                               "    WHERE Id = @Id ";
                }
                else if (mode == "INSERT") // INSERT 할때 쿼리
                {
                    strQurey = "INSERT INTO dbo.userTbl ( userID, password ) " +
                               "    VALUES (@userID, @password) ";
                } 
                cmd.CommandText = strQurey; 


                //*************** 2. 필요한 파라미타 정해주기 ******************//
                SqlParameter parmuserID = new SqlParameter("@userID", SqlDbType.VarChar, 12);
                parmuserID.Value = TxtuserID.Text;
                cmd.Parameters.Add(parmuserID);

                SqlParameter parmPassword = new SqlParameter("@password", SqlDbType.VarChar, 20);
                parmPassword.Value = TxtPassword.Text;
                cmd.Parameters.Add(parmPassword);

                if (mode == "UPDATE") //@ID를 UPDATE 할때만 사용하기 때문에 IF문을 활용
                {
                    SqlParameter parmID = new SqlParameter("@Id", SqlDbType.Int);
                    parmID.Value = TxtID.Text;
                    cmd.Parameters.Add(parmID); 
                }

                cmd.ExecuteNonQuery();
                // executenonqurey : 쿼리를 돌려받지않음 
                // executeread : 쿼리를 돌려받아 select 등등 사용가능
                // 쿼리문을 실행
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtuserID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MetroMessageBox.Show(this, "빈값은 삭제할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DeleteProcess();
            UpdateData();
            ClearTextControl();
        }

        private void DeleteProcess()
        {
            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM dbo.divtbl WHERE Division = @Division";
                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char, 4);
                parmDivision.Value = TxtuserID.Text;
                cmd.Parameters.Add(parmDivision);

                cmd.ExecuteNonQuery();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearTextControl();
        }
    }
}
