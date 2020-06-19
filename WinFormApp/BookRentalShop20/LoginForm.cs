using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace BookRentalShop20
{
    public partial class LoginForm : MetroForm
    {
        string strConnString = "Data Source=192.168.0.126;Initial Catalog = BookRentalshopDB; Persist Security Info=True;User ID = sa; Password=p@ssw0rd!!";
        public LoginForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Cancel 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //Application.Exit(); // 화면 해제
            Environment.Exit(1); // 성공적으로 종료 == 1 (에러가 있다) , 그외는 0
        }
        /// <summary>
        /// Login 처리 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            Loginprocess();
        }

        private void TxtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Enter
            {
                TxtPassword.Focus();
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Enter
            {
                Loginprocess();
            }
        }

        private void Loginprocess()
        {
            //throw new NotImplementedException(); 
            // 사용자가 던지는 예외(함수를 만들었지만 구현되지 않아 실행되지 않도록 C#에서 만들어놓은 구문)
            if (string.IsNullOrEmpty(TxtUserID.Text) || string.IsNullOrEmpty(TxtPassword.Text)) // isnullempty : 널이거나 스트링이거나
            {
                MetroMessageBox.Show(this, "아이디/패스워드를 입력하세요!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string struserid = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT userID FROM usertbl " + //  처음과 마지막에 띄어쓰기 해줘야함
                                      " WHERE userID = @userID " +    //  해킹방지
                                      "   AND password = @password ";

                    SqlParameter parmUserId = new SqlParameter("@userID", SqlDbType.VarChar, 12);
                    parmUserId.Value = TxtUserID.Text;
                    cmd.Parameters.Add(parmUserId);
                    SqlParameter parmPassworrd = new SqlParameter("@password", SqlDbType.VarChar, 20);
                    parmPassworrd.Value = TxtPassword.Text;
                    cmd.Parameters.Add(parmPassworrd);

                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    struserid = reader["userID"] != null ? reader["userID"].ToString() : "";

                    if (struserid != "")
                    {
                        MetroMessageBox.Show(this, "접속성공", "로그인성공");
                        this.Close(); // 로그인 폼이 닫힘
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "접속성공", "로그인실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    //Debug.WriteLine("On the Debug");
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"Error : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // StackTrace : 에러난 부분을 알려줌 // Message : 오류가 났다고 알려줌
                return;
            }

           
        }
    }
}
