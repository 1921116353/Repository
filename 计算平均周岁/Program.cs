using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 计算平均周岁
{
    class Program
    {
        public static void Main(string[] arg)
        {
      
            PersonListRepository list = new PersonListRepository();
            var persons = PersonListRepository.InitialPersonList();

            var a = PersonListRepository.AverageAge(persons);
            Console.WriteLine("平均周岁————————————");
            Console.WriteLine(a);
            Console.WriteLine("-------------------------------------"); var w = PersonListRepository.InitialPersonList();
            pgdoj(w);
            Console.ReadKey();


           
        }
        public static void pgdoj(PersonList per)
        {
            var b = from a in per
                    where !a.Name.Contains("欧阳")
                    group a by a.Name.Substring(0, 1)
                  into box
                    select new { box.Key, sum = box.Count() };

            foreach (var i in b)
            {
                Console.WriteLine("姓：{0}，人数：{1}", i.Key, i.sum);
            }



            var name = from a in per
                       where a.Name.Contains("欧阳")
                       group a by a.Name.Substring(0, 2)
                  into box
                       select new { box.Key, sum = box.Count() };

            foreach (var i in name)
            {
                Console.WriteLine("姓：{0}，人数：{1}", i.Key, i.sum);
            }
            Console.Read();
        }

    }
}
