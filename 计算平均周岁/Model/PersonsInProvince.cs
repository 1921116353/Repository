using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 计算平均周岁.Model
{
    
        public class PersonsInProvince
        {
            public string ProvinceName { get; set; }
            public int Amount { get; set; }

            public override string ToString()
            {
                return $"{this.ProvinceName} \t{this.Amount}";
            }

            public string ToBarChartStyle()
            {
                var space = "";

                var builder = new StringBuilder();
                builder.Append(space);
                for (int i = 0; i < Amount; i++)
                {
                    builder.Append("█");
                }
                space = builder.ToString();

                return $"{this.ProvinceName} \t{this.Amount} \t{space}";
            }
        }
    }

