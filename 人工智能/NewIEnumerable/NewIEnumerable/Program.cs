﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewIEnumerable
{
    /// <summary>
    /// 对一个普通的文本文件，检索指定的关键词在文件中的数量
    /// 练习使用迭代子 Iterator 以及其中的处理性能
    /// </summary>
    class Program
    {
        static void Main()
        {
            var keyString = "人工智能";
            //TestReadingFile(keyString);
            //Console.WriteLine("---");
            TestStreamReaderEnumerable(keyString);

            Console.ReadKey();
        }

        /// <summary>
        /// 不使用自定义的迭代子检索指定的文本文件中，包含指定字符串的个数的方法
        /// </summary>
        /// <param name="keyString"></param>
        public static void TestReadingFile(string keyString)
        {
            var memoryBefore = GC.GetTotalMemory(true);
            System.IO.StreamReader sr;
            try
            {
                sr = File.OpenText("temp\\tempFile.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"这个例子需要一个名为 C:\temp\tempFile1.txt 的文件。");
                return;
            }
            var fileContents = new List<string>(); // 将文本内容添加到一个 List<string> 变量
            while (!sr.EndOfStream)
            {
                fileContents.Add(sr.ReadLine());
            }

            // 检索目标文本（字符串）
            var stringsFound =
                from line in fileContents
                where line.Contains(keyString)
                select line;

            sr.Close();
            Console.WriteLine("数量：" + stringsFound.Count());

            var memoryAfter = GC.GetTotalMemory(false); // 检查不使用迭代子并将结果输出到控制台之后的内存用量.
            Console.WriteLine("不使用 Iterator 的内存用量 = \t" + string.Format(((memoryAfter - memoryBefore) / 1000).ToString(), "n") + "kb");
        }

        /// <summary>
        /// 使用迭代子方式检索指定的文本文件中，包含指定字符串的个数的方法
        /// </summary>
        /// <param name="keyString"></param>
        public static void TestStreamReaderEnumerable(string keyString)
        {
            var memoryBefore = GC.GetTotalMemory(true); // 检查使用迭代子之前的内存用量
            IEnumerable<String> stringsFound;
            // 使用 StreamReaderEnumerable 打开一个示例文件，检索对应的字符串
            try
            {
                stringsFound =
                      from line in new StreamReaderEnumerable(@"temp\tempFile.txt")
                      //where line.Contains(keyString)
                      select line;


                Console.WriteLine("文件名：tempFile1.txt");
                Console.WriteLine("关键字：{0}",keyString );
                int i = 1;
                foreach (var w in stringsFound )
                {
                    if (w.Contains(keyString))
                    {
                        int x = w.IndexOf(keyString);
                        int j = w.Length - x;
                        if (j > 13)
                            Console.WriteLine("第{0}行，第{1}个字母开始：{2}...", i, x + 1, w.Substring(x, 13) );
                        if (j == 13)
                            Console.WriteLine("第{0}行，第{1}个字母开始：{2}", i,x+1 , w.Substring(x, 13));

                        if (j < 13)
                        {
                            Console.WriteLine("第{0}行，第{1}个字母开始：{2}", i, x + 1, w.Substring(x));
                        }
                    }
                  
                    i++;
                }

                //Console.WriteLine("数量：" + stringsFound.Count());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"这个例子需要一个名为 temp\tempFile.txt 的文件。");
                return;
            }

            //var memoryAfter = GC.GetTotalMemory(false); // 检查使用迭代子并将结果输出到控制台之后的内存用量。
            //Console.WriteLine("使用 Iterator 的内存用量 = \t" + string.Format(((memoryAfter - memoryBefore) / 1000).ToString(), "n") + "kb");
        }


    }

    /// <summary>
    /// 一个定制的实现IEnumerable<T> 的类，为此，还需要实现相应的
    ///  IEnumerable 和 IEnumerator<T>
    /// </summary>
    public class StreamReaderEnumerable : IEnumerable<string>
    {
        private string _filePath;
        public StreamReaderEnumerable(string filePath)
        {
            _filePath = filePath;
        }

        // 必须实现 GetEnumerator，用于返回一个新的 StreamReaderEnumerator.
        public IEnumerator<string> GetEnumerator() => new StreamReaderEnumerator(_filePath);

        // 同时必须实现 IEnumerable.GetEnumerator，但当成一个私有方法实现
        private IEnumerator GetEnumerator1() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator1();

        /// <summary>
        /// 在实现 IEnumerable<T> 时，必须实现IEnumerator<T>，范例代码中，遍历文件内容时，每一行一次
        /// 实现 IEnumerable<T> 还需要实现 IEnumerator 和析构函数 IDisposable
        /// </summary>
        public class StreamReaderEnumerator : IEnumerator<string>
        {
            private StreamReader _sr;
            public StreamReaderEnumerator(string filePath)
            {
                _sr = new StreamReader(filePath);
            }
            private string _current;
            // 实现 IEnumerator<T>().Current 公开属性，但实现所必须的 IEnumerator.Current 则为私有属性.
            public string Current
            {
                get
                {
                    if (_sr == null || _current == null)
                        throw new InvalidOperationException();
                    return _current;
                }
            }
            private object Current1 => this.Current;
            object IEnumerator.Current => Current1;
            // 实现 IEnumerator 所必须的 MoveNext 和 Reset。
            public bool MoveNext()
            {
                _current = _sr.ReadLine();
                if (_current == null)
                    return false;
                return true;
            }
            public void Reset()
            {
                _sr.DiscardBufferedData();
                _sr.BaseStream.Seek(0, SeekOrigin.Begin);
                _current = null;
            }
            // 实现析构函数，必须的。
            private bool disposedValue = false;
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposedValue)
                {
                    if (disposing) { } // 析构所需要的资源
                    _current = null;
                    if (_sr != null)
                    {
                        _sr.Close();
                        _sr.Dispose();
                    }
                }
                this.disposedValue = true;
            }
            ~StreamReaderEnumerator() { Dispose(false); }
        }
    }
}
