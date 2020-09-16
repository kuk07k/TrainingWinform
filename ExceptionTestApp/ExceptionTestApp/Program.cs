using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionTestApp

    //try, catch 문
{
    class Program
    {
        static void Main(string[] args) // try, catch, finally 
        {
            int x = 100 , y = 5 , value = 0;
            
            try // 에러가 날 구문은 이곳에 적음 (정상실행되면 finally로) 
            {
                value = x / y;
                Console.WriteLine($"{x} / {y} = {value}");
                throw new Exception("1. 사용자 에러"); // throw : 에러를 내버린다
            }
            catch (DivideByZeroException ex) // 에러잡기 단계별
            {
                Console.WriteLine("2. 작은 에러를 잡아주세요.");
            }
            catch (Exception ex) // 에러가 나면 catch에 떨어짐 //Exception 은 모든 에러의 부모(위치가 항상 아래 - finally 아래)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("3." + "y의 값을 0보다 크게 입력하세요. ");
            }

            finally //에러가 났던 안났던 마지막 실행구문
            {
                Console.WriteLine("4. 프로그램이 종료되었습니다.");
            }
        }
    }
}
