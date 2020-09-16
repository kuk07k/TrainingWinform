using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    // GenericApp
    public class SimpleGerneric<T> // 함수를 하나가지고 여러 변수형을 사용하기 위해서
    {
        private T[] values;
        private int index;

        public SimpleGerneric(int length)
        {
            values = new T[length];
            index = 0;
        }

        public void Add(params T[] args)
        {
            foreach (T item in args)
            {
                values[index++] = item;
            }
        }

        public void Print()
        {
            foreach (T item in values)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(String[] args)
        {
            SimpleGerneric<Int32> gIntegers = new SimpleGerneric<int>(10);
            SimpleGerneric<double> gDoubles = new SimpleGerneric<double>(10);

            gIntegers.Add(1, 2);
            gIntegers.Add(1, 2, 3, 4, 5, 6, 7);
            gIntegers.Add(10);

            gDoubles.Add(10.0, 12.4, 37.5);
            gIntegers.Print();
            gDoubles.Print();
        }
    }
}
