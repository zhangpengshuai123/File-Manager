using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace File_Manager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            InitCustomerData();
            
        }

        public void InitCustomerData()
        {
            string DesktopFolder;
            //根据当前用户PC具体环境，选择桌面作为初始目录
            DesktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            MainLayout.Resources["CurrentPath"] = DesktopFolder;
            UpdateFileList(DesktopFolder);
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog opendialog;
            string strPath;

            //初始化对话框，文件类型，过滤器，初始路径等设置
            opendialog = new System.Windows.Forms.FolderBrowserDialog();
            //成功选取文件后，根据文件类型执行读取函数
            if (opendialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            strPath = opendialog.SelectedPath;
            //更新系统资源
            MainLayout.Resources["CurrentPath"] = strPath;
            //更新当前目录下文件列表
            UpdateFileList(strPath);
        }

        private void UpdateFileList(string strPath)
        {
            if (strPath == "")
            {
                return;
            }
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(strPath);
                DirectoryInfo[] dirs = currentDir.GetDirectories(); //获取目录
                FileInfo[] files = currentDir.GetFiles();   //获取文件
                //删除ImageList中的程序图标
                //foreach (ListViewItem item in listView1.Items)
                //{
                //    if (item.Text.EndsWith(".exe"))  //是程序
                //    {
                //        imageList2.Images.RemoveByKey(item.Text);
                //        imageList3.Images.RemoveByKey(item.Text);
                //    }
                //}
                //listView1.Items.Clear();
                //列出文件夹
                foreach (DirectoryInfo dir in dirs)
                {
                    //ListViewItem newDirItem = new ListViewItem();
                    //newDirItem.Content = new File();
                    //FileListView.Items.Add((ListViewItem));
                    //ListBoxItem item = pictureBox.ItemContainerGenerator.ContainerFromItem(pictureBox.SelectedItem) as ListBoxItem;
                }
                ////列出文件
                //foreach (FileInfo file in files)
                //{
                //    ListViewItem fileItem = listView1.Items.Add(file.Name);
                //    if (file.Extension == ".exe" || file.Extension == "")   //程序文件或无扩展名
                //    {
                //        Icon fileIcon = GetSystemIcon.GetIconByFileName(file.FullName);
                //        imageList2.Images.Add(file.Name, fileIcon);
                //        imageList3.Images.Add(file.Name, fileIcon);
                //        fileItem.ImageKey = file.Name;
                //    }
                //    else    //其它文件
                //    {
                //        if (!imageList2.Images.ContainsKey(file.Extension))  //ImageList中不存在此类图标
                //        {
                //            Icon fileIcon = GetSystemIcon.GetIconByFileName(file.FullName);
                //            imageList2.Images.Add(file.Extension, fileIcon);
                //            imageList3.Images.Add(file.Extension, fileIcon);
                //        }
                //        fileItem.ImageKey = file.Extension;
                //    }
                //    fileItem.Name = file.FullName;
                //    fileItem.SubItems.Add(file.Length.ToString() + "字节");
                //    fileItem.SubItems.Add(file.Extension);
                //    fileItem.SubItems.Add(file.LastWriteTimeUtc.ToString());
                //}
                //currentPath = newPath;
                //toolStripComboBox1.Text = currentPath;   //更新地址栏
                //toolStripStatusLabel1.Text = listView1.Items.Count + "个对象";     //更新状态栏
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
