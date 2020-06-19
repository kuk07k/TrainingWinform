using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BookRentalShop20
{
    public partial class DivForm : MetroForm
    {
        string strConnString = "Data Source=192.168.0.126;Initial Catalog = BookRentalshopDB; Persist Security Info=True;User ID = sa; Password=p@ssw0rd!!";
        string mode = "";
        public DivForm()
        {
            InitializeComponent();
        }

        private void DivForm_Load(object sender, EventArgs e)
        {
            UpdateData(); // 데이터그리드 DB 데이터 로딩하기
        }

        private void UpdateData()
        {
            using (SqlConnection conn = new SqlConnection(strConnString)) // using이 있어야 conn.close 안씀
            {
                conn.Open(); // DB 열기
                String strQuery = "SELECT Division, Names FROM divtbl";
                // SqlCommand cmd = new SqlCommand(strQuery, conn); // 쿼리문 사용시 반드시 필요 : sqlcommand
                SqlDataAdapter dataAdapter = new SqlDataAdapter(strQuery, conn); // 명령 수행(sqlcommand 역할까지 수행) - 꼽으면 끝나도록(플러그)
                DataSet ds = new DataSet(); // 데이터를 담는통 (테이블)
                dataAdapter.Fill(ds, "divtbl"); // ds 통에다가 divtbl이란 이름으로 채운다

                GridDivTbl.DataSource = ds; // gridDivtbl에 붓는다
                GridDivTbl.DataMember = "divtbl";
            }
            DataGridViewColumn
            column = GridDivTbl.Columns[0];
            column.Width = 200;
            column.HeaderText = "구분코드";

            column = GridDivTbl.Columns[1];
            column.Width = 200;
            column.HeaderText = "이름";
        }

        private void GridDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                DataGridViewRow data = GridDivTbl.Rows[e.RowIndex];
                TxtDivision.Text = data.Cells[0].Value.ToString();
                TxtNames.Text = data.Cells[1].Value.ToString();
                TxtDivision.ReadOnly = true; // PK값은 수정해서는 안되기때문에
                TxtDivision.BackColor = Color.Red; // 잘안보여서 색깔을 넣어줌 // metro는 색깔이 안바뀜

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
            if(string.IsNullOrEmpty(TxtDivision.Text) || string.IsNullOrEmpty(TxtNames.Text))
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
            TxtDivision.Text = TxtNames.Text = "";
            TxtDivision.ReadOnly = false;
            TxtDivision.Focus();
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

                if (mode == "UPDATE") // UPDATE 할때 쿼리
                {
                    strQurey = "UPDATE dbo.divtbl " +
                               "   SET Names = @Names " +
                               "  WHERE Division = @Division ";
                }
                else if (mode == "INSERT") // INSERT 할때 쿼리
                {
                    strQurey = "INSERT INTO dbo.divtbl(Division,Names) " +
                               " VALUES (@Division, @Names) ";
                }
                cmd.CommandText = strQurey;

                SqlParameter parmNames = new SqlParameter("@Names", SqlDbType.NVarChar, 45);
                parmNames.Value = TxtNames.Text;
                cmd.Parameters.Add(parmNames);
                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char, 4);
                parmDivision.Value = TxtDivision.Text;
                cmd.Parameters.Add(parmDivision);

                cmd.ExecuteNonQuery();
                // executenonqurey : 쿼리를 돌려받지않음 
                // executeread : 쿼리를 돌려받아 select 등등 사용가능
                // 쿼리문을 실행
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
            if (string.IsNullOrEmpty(TxtDivision.Text) || string.IsNullOrEmpty(TxtNames.Text))
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
            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM dbo.divtbl WHERE Division = @Division";
                SqlParameter parmDivision = new SqlParameter("@Division", SqlDbType.Char, 4);
                parmDivision.Value = TxtDivision.Text;
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
