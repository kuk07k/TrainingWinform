using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalShop20
{
    public static class Commons //commons라는 static 아이디
    {   
        //공용 연결문자열
        public static string CONNSTRING =
            "Data Source=192.168.0.126;Initial Catalog = BookRentalshopDB; Persist Security Info=True;User ID = sa; Password=p@ssw0rd!!";
        public static string LOGINUSERID = "";  
    }
}
