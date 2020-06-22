using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BookRentalShop20
{
    public partial class MemberForm : MetroForm
    {
        string mode = "";
        public MemberForm()
        {
            InitializeComponent();
        }

        private void UpdateData()
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                String strQuery = "SELECT Idx, Names, Levels, Addr, Mobile, Email " +
                                  "  FROM membertbl ";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "membertbl");

                GrdMemberTbl.DataSource = ds;
                GrdMemberTbl.DataMember = "membertbl";
            }
            DataGridViewColumn
            column = GrdMemberTbl.Columns[0];
            column.Width = 80;
            column.HeaderText = "Number";

            column = GrdMemberTbl.Columns[1];
            column.Width = 60;
            column.HeaderText = "Name";

            column = GrdMemberTbl.Columns[5];
            column.Width = 300;
            column.HeaderText = "E-mail";
        }

        private void GridDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                DataGridViewRow data = GrdMemberTbl.Rows[e.RowIndex];
                TxtIdx.Text = data.Cells[0].Value.ToString();
                TxtNames.Text = data.Cells[1].Value.ToString();
                CboLevels.SelectedIndex = CboLevels.FindString(data.Cells[2].Value.ToString()); // 값을 넣고 가져올때 selectedindex 사용
                TxtAddr.Text = data.Cells[3].Value.ToString();
                TxtMobile.Text = data.Cells[4].Value.ToString();
                TxtEmail.Text = data.Cells[5].Value.ToString();
                TxtIdx.ReadOnly = true;
                TxtIdx.BackColor = Color.Red;

                mode = "UPDATE";
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearTextControl();

            mode = "INSERT"; // 신규는 INSERT
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(TxtAddr.Text) || string.IsNullOrEmpty(TxtNames.Text)
                || string.IsNullOrEmpty(TxtMobile.Text) || string.IsNullOrEmpty(TxtEmail.Text))
            {
                MetroMessageBox.Show(this, "빈값은 저장할 수 없습니다.", "경고", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveProcess();
            UpdateData();
            ClearTextControl();
        }

        private void ClearTextControl()
        {
            TxtMobile.Text = TxtEmail.Text = TxtAddr.Text = TxtIdx.Text = TxtNames.Text = "";
            CboLevels.SelectedIndex = -1;
            TxtIdx.ReadOnly = true;
            TxtNames.Focus();
        }

        private void SaveProcess()
        {
            if (string.IsNullOrEmpty(mode))
            {
                MetroMessageBox.Show(this, "신규버튼을 누르고 데이터를 저장하십시오", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //DB저장프로세스
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strQurey = ""; // 쿼리만들기

                if (mode == "UPDATE") // UPDATE 할때 쿼리
                {
                    strQurey = "UPDATE membertbl " +
                               " SET Names = @Names " +
                               "   , Levels = @Levels " +
                               "   , Addr = @Addr " +
                               "   , Mobile = @Mobile " +
                               "   , Email = @Email " +
                               " WHERE Idx = @Idx ";
                }
                else if (mode == "INSERT") // INSERT 할때 쿼리
                {
                    strQurey = "INSERT INTO membertbl(Names, Levels, Addr, Mobile, Email) " + 
                               "    VALUES (@Names, @Levels, @Addr, @Mobile, @Email) ";
                }
                cmd.CommandText = strQurey;

                SqlParameter parmNames = new SqlParameter("@Names", SqlDbType.NVarChar, 45);
                parmNames.Value = TxtNames.Text;
                cmd.Parameters.Add(parmNames);

                SqlParameter parmLevels = new SqlParameter("@Levels", SqlDbType.Char, 1);
                parmLevels.Value = CboLevels.SelectedItem;
                cmd.Parameters.Add(parmLevels);

                SqlParameter parmAddr = new SqlParameter("@Addr", SqlDbType.VarChar, 100);
                parmAddr.Value = TxtAddr.Text;
                cmd.Parameters.Add(parmAddr);

                SqlParameter parmMobile = new SqlParameter("@Mobile", SqlDbType.VarChar, 13);
                parmMobile.Value = TxtMobile.Text;
                cmd.Parameters.Add(parmMobile);

                SqlParameter parmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                parmEmail.Value = TxtEmail.Text;
                cmd.Parameters.Add(parmEmail);

                if(mode == "UPDATE")
                {
                    SqlParameter parmIdx = new SqlParameter("@Idx", SqlDbType.Int);
                    parmIdx.Value = TxtIdx.Text;
                    cmd.Parameters.Add(parmIdx);
                }

                cmd.ExecuteNonQuery();
            }
        }

        private void TxtNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                BtnSave_Click(sender, new EventArgs()); // 엔터치면 바로 저장되기
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtIdx.Text) || string.IsNullOrEmpty(TxtNames.Text))
            {
                MetroMessageBox.Show(this, "빈값은 삭제할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DeleteProcess();
            UpdateData();
            ClearTextControl(); // 쳤던 텍스트 삭제
        }

        private void DeleteProcess()
        {
            using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM dbo.divtbl WHERE Division = @Division";
                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char, 4);
                parmDivision.Value = TxtIdx.Text;
                cmd.Parameters.Add(parmDivision);

                cmd.ExecuteNonQuery();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearTextControl();
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
