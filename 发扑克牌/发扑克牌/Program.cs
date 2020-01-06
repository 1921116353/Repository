using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 发扑克牌
{
  
    enum Color { 红桃 = -1, 黑桃 = -2, 梅花 = -3, 方块 = -4 }//花色
        enum Point { A, _2, _3, _4, _5, _6, _7, _8, _9, _10, J, Q, K }//点数
        struct Poker
        {//扑克
            private string p1, p2;
            public Poker(string p1, string p2)
            {
                // TODO: Complete member initialization
                this.p1 = p1;
                this.p2 = p2;
            }
            public void Printp()
            {
                Console.Write("({0},{1}) ", this.p1, this.p2);
            }
        }
        class Program
        {

            static void Main(string[] args)
            {
          
                ArrayList myPoker = new ArrayList();//实例化一个ArrayList存放所有的扑克牌
                ArrayList Person1 = new ArrayList();
                ArrayList Person2 = new ArrayList();
                ArrayList Person3 = new ArrayList();
                ArrayList Person4 = new ArrayList();
                Random r = new Random();
                for (int i = -4; i <= -1; i++)
                {//外循环初始化扑克的花色
                    for (int j = 0; j <= 12; j++)
                    {//外层循环初始化扑克的点数
                        myPoker.Add(new Poker(
                            Enum.GetName(typeof(Color), i),
                            Enum.GetName(typeof(Point), j)
                            ));
                    }
                }//for循环结束，52张扑克牌已经生成
                Console.WriteLine("所有的扑克牌：");
                for (int i = 0; i < 52; i++)
                {//打印52张生成的扑克牌
                    Poker poAll = (Poker)myPoker[i];
                    poAll.Printp();
                }
                //开始发牌，一个人一个人的发，每发一张牌得从myPoker中RemoveAt掉扑克牌，Count数减小；第一个人Add到一张牌。
                for (int i = 0; i < 13; i++)
                {
                    int te = r.Next(0, myPoker.Count);
                    Person1.Add(myPoker[te]);
                    myPoker.RemoveAt(te);
                }
                for (int i = 0; i < 13; i++)
                {
                    int te = r.Next(0, myPoker.Count);
                    Person2.Add(myPoker[te]);
                    myPoker.RemoveAt(te);
                }
                for (int i = 0; i < 13; i++)
                {
                    int te = r.Next(0, myPoker.Count);
                    Person3.Add(myPoker[te]);
                    myPoker.RemoveAt(te);
                }
                for (int i = 0; i < 13; i++)
                {
                    int te = r.Next(0, myPoker.Count);
                    Person4.Add(myPoker[te]);
                    myPoker.RemoveAt(te);
                }

                Console.WriteLine();
                Console.WriteLine("\r\n--------------------------------------------");
                Console.WriteLine("第一个人的扑克牌：");
                for (int i = 0; i < 13; i++)
                {
                    Poker po1 = (Poker)Person1[i];
                    po1.Printp();
                }
                Console.WriteLine();
                Console.WriteLine("\r\n--------------------------------------------");
                Console.WriteLine("第二个人的扑克牌：");
                for (int i = 0; i < 13; i++)
                {
                    Poker po2 = (Poker)Person2[i];
                    po2.Printp();
                }
                Console.WriteLine();
                Console.WriteLine("\r\n--------------------------------------------");
                Console.WriteLine("第三个人的扑克牌：");
                for (int i = 0; i < 13; i++)
                {
                    Poker po3 = (Poker)Person3[i];
                    po3.Printp();
                }
                Console.WriteLine();
                Console.WriteLine("\r\n--------------------------------------------");
                Console.WriteLine("第四个人的扑克牌：");
            for (int i = 0; i < 13; i++)
            {
                Poker po4 = (Poker)Person4[i];
                po4.Printp();
            }
            Console.WriteLine();
            Console.ReadLine();
        }
        }
    }
