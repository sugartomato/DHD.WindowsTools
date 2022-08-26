using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DHD.WindowsTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                Config.Load();

                Args.ParseArgs(e.Args);

                if (Args.Arguements.ContainsKey("help") || Args.Arguements.ContainsKey("?"))
                {
                    MessageBox.Show(Args.HelperString);
                    Environment.Exit(0);
                }

                if (Args.Arguements.ContainsKey("o"))
                {
                    // 添加日期前缀
                    if (Args.Arguements["o"].StartsWith("ad:"))
                    {
                        String objName = Args.Arguements["o"].Replace("ad:", "");
                        if (objName.StartsWith('"'))
                        {
                            objName = objName.Trim('"');
                        }
                        // 指定对象添加日期前缀
                        if (System.IO.Directory.Exists(objName))
                        {
                            if(System.IO.Directory.Exists(objName))
                            {
                                System.IO.Directory.Move(objName, $"{Path.GetDirectoryName(objName)}\\{DateTime.Now.ToString("yyyyMMdd")}_{Path.GetFileName(objName)}");

                            }
                        }
                        else
                        {
                            if (System.IO.File.Exists(objName))
                            {
                                System.IO.File.Move(objName, $"{Path.GetDirectoryName(objName)}\\{DateTime.Now.ToString("yyyyMMdd")}_{Path.GetFileName(objName)}");
                            }
                        }

                    }

                    // 复制路径
                    if (Args.Arguements["o"].StartsWith("cp:"))
                    {
                        String objName = Args.Arguements["o"].Replace("cp:", "").Trim('"');
                        System.Windows.Clipboard.SetText(objName);
                    }


                    // 移动到指定目录
                    if (Args.Arguements["o"].StartsWith("mt:"))
                    {
                        String objName = Args.Arguements["o"].Replace("mt:", "").Trim('"');

                        Controls.MoveToList mWindow = new Controls.MoveToList();
                        if (mWindow.ShowDialog() == true)
                        {
                            if (System.IO.Directory.Exists(objName))
                            {
                                System.IO.Directory.Move(objName, $"{mWindow.SelectedDirectory}\\{Path.GetFileName(objName)}");
                            }
                            else if (System.IO.File.Exists(objName))
                            {
                                System.IO.File.Move(objName, $"{mWindow.SelectedDirectory}\\{Path.GetFileName(objName)}");
                            }
                            else
                            {
                                MessageBox.Show($"该项目【{objName}】既不是目录也不是文件，如何处理呢？");
                            }
                        }
                    }

                    // 添加目录到移动目录列表
                    if (Args.Arguements["o"].StartsWith("amt:"))
                    {
                        String objName = Args.Arguements["o"].Replace("amt:", "").Trim('"');
                        if (System.IO.Directory.Exists(objName))
                        {
                            Config.AddToMoveToList(objName);
                            MessageBox.Show("添加完成！");
                        }
                    }

                    Environment.Exit(0);
                }


            }
            catch (Exception ex)
            {
                String argsString = String.Empty;
                if (e.Args != null && e.Args.Length > 0)
                { 
                    foreach(String a in e.Args)
                    {
                        argsString += a + " ";
                    }
                }

                MessageBox.Show($"程序启动发生异常(启动参数：{argsString})：{ex.Message + ex.StackTrace}") ;
            }
        }
    }
}
