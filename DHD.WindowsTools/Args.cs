using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHD.WindowsTools
{
    internal class Args
    {
        /// <summary>
        /// 解析参数为字典
        /// </summary>
        /// <param name="args"></param>
        internal static void  ParseArgs(String[] args) {
            Arguements = new Dictionary<string, string>();
            if (args == null || args.Length == 0) return;

            for (Int32 i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith('-') || args[i].StartsWith('/'))
                {
                    if (i + 1 < args.Length && !args[i + 1].StartsWith('-') && !args[i + 1].StartsWith('/'))
                    {
                        AddArgs(args[i].TrimStart('-').TrimStart('/'), args[i + 1]);
                    }
                    else
                    {
                        AddArgs(args[i].TrimStart('-').TrimStart('/'), String.Empty);
                    }
                }
            }
        }

        private static void AddArgs(String key, String value)
        {
            if (Arguements.ContainsKey(key))
            { 
                Arguements[key] = value;
            }
            else
            {
                Arguements.Add(key, value)
;            }
        }

        /// <summary>
        /// 命令行参数说明
        /// </summary>
        internal static String HelperString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("参数帮助");
                sb.AppendLine("=".PadLeft(20, '='));

                // 无界面操作参数
                sb.AppendLine("/o:无界面操作，配合子命令，如：/o ad:\"D:\\test.txt\"。子命令如下：");
                sb.AppendLine("\tad:\t给当前文件或文件夹的名添加日期前缀，");
                sb.AppendLine("\tmt:\t移动文件或文件夹的到指定目录；");
                sb.AppendLine("\tamt:\t将当前文件夹添加到移动至列表中；");
                sb.AppendLine("\tcp:\t复制文件或文件夹的完整路径；");
                sb.AppendLine("\tcan:\t复制目录下的所有文件名，只针对文件夹有效。");


                sb.AppendLine("=".PadLeft(20, '='));

                return sb.ToString();
            }

        }


        /// <summary>
        /// 参数字典形式
        /// </summary>
        internal static Dictionary<string, string> Arguements = new Dictionary<string,string>();

    }
}
