using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using WinFrom = System.Windows.form


namespace DHD.WindowsTools.Controls
{
    /// <summary>
    /// MoveToList.xaml 的交互逻辑
    /// </summary>
    public partial class MoveToList : Window
    {
        private List<String>? MoveToFolders { get; set; }

        public MoveToList()
        {
            InitializeComponent();

            MoveToFolders = new List<string>();
        }

        private void OnClick_OK(object sender, RoutedEventArgs e)
        {
            if (CTRL_LIST_Main.Items.Count > 0 && CTRL_LIST_Main.SelectedItem != null && MoveToFolders != null)
            {
                _propSelectedDir = MoveToFolders[CTRL_LIST_Main.SelectedIndex];

                // 检查是否存在
                if (!System.IO.Directory.Exists(_propSelectedDir))
                {
                    MessageBox.Show("此目录已不存在，请选择其它目录！");
                    return;
                }
            }
            this.DialogResult = true;
            this.Close();
        }

        private void OnClick_SelectOther(object sender, RoutedEventArgs e)
        {
            

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "占位文件名不用管";
            sfd.DefaultExt = "*.txt";
            sfd.Filter = "文本文件(.txt)|*.txt";
            if (sfd.ShowDialog() == true)
            {
                String fName = sfd.FileName;
                String? path = System.IO.Path.GetDirectoryName(fName);
                if (System.IO.Directory.Exists(path))
                {
                    _propSelectedDir = path;
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"选择的目录[{path}]不正确");
                }

            }
        }

        private void OnClick_Remove(object sender, RoutedEventArgs e)
        {
            if (CTRL_LIST_Main.Items.Count > 0 && CTRL_LIST_Main.SelectedItem != null && MoveToFolders != null)
            {
                String path = MoveToFolders[CTRL_LIST_Main.SelectedIndex];
                if (MessageBox.Show("确实要删除吗？","", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Config.RemoveFromMoveToList(path);
                    MessageBox.Show("已删除！");
                    LoadList();
                }
            }
        }



        private void OClick_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private String _propSelectedDir = String.Empty;
        /// <summary>
        /// 获取该对话窗口选择的内容
        /// </summary>
        public String SelectedDirectory
        {
            get {
                return _propSelectedDir;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadList();
        }


        private void LoadList() { 
            MoveToFolders = Config.MoveToList;
            CTRL_LIST_Main.ItemsSource = MoveToFolders;
        }
}
}
