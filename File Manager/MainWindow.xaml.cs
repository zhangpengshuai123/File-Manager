using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;
using File_Manager.Model;

namespace File_Manager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        DispatcherTimer StatusUpadteTimer;
        List<SysFileIf> CurrentFiles;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FrmMain_Loaded(object sender, RoutedEventArgs e)
        {
            InitCustomerData();
            StatusUpdateTimer_Init();
        }

        public void InitCustomerData()
        {
            string DesktopFolder;
            //根据当前用户PC具体环境，选择桌面作为初始目录
            DesktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            MainLayout.Resources["CurrentPath"] = DesktopFolder;
            CurrentFiles = new List<SysFileIf>();
            UpdateFileList(DesktopFolder);
        }

        #region StatusTimer
        private void StatusUpdateTimer_Init()
        {
            StatusUpadteTimer = new DispatcherTimer();
            StatusUpadteTimer.Interval = new TimeSpan(0, 0, 3);
            StatusUpadteTimer.Tick += new EventHandler(StatusUpdateTimer_Tick);
            StatusUpadteTimer.IsEnabled = false;
        }

        private void StatusUpdateTimer_Tick(object sender, EventArgs e)
        {
            StatusLabel.Content = "Ready";
            StatusUpadteTimer.IsEnabled = false;
        }

        public void ShowStatus(string sStatus, bool isLongTime = false)
        {
            StatusLabel.Content = sStatus;
            if (isLongTime == true)
            {
                return;
            }
            StatusUpadteTimer.Start();
        }
        #endregion

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
                
                FileListView.Items.Clear();
                CurrentFiles.Clear();
                //列出文件夹
                foreach (DirectoryInfo dir in dirs)
                {
                    SysDirectory newDirItem = new SysDirectory(dir);
                    CurrentFiles.Add(newDirItem);
                    FileListView.Items.Add(newDirItem);
                }
                //列出文件
                foreach (FileInfo file in files)
                {
                    SysFile newFileItem = new SysFile(file);
                    CurrentFiles.Add(newFileItem);
                    FileListView.Items.Add(newFileItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void FileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileListView.SelectedItems.Count < 1)
            {
                ShowStatus("Ready");
                return;
            }
            ShowStatus(FileListView.SelectedItems.Count.ToString() + " Items been Selected", true);
            if (FileListView.SelectedItems.Count > 1)
            {
                return;
            }
            UpdateFileInfo((SysFileIf)FileListView.SelectedItem);
            return;
        }

        private void FileListView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            FileListView.SelectedItems.Clear();
        }

        private void UpdateFileInfo(SysFileIf file)
        {
            NameLabel.Text = file.Name;
            TypeLabel.Text = file.Type;
            FullPathLabel.Text = file.FullPath;
            CreateTimeLabel.Text = file.CreateDateTime;
            LastWriteTimeLabel.Text = file.WriteDateTime;
            LastAccessTimeLabel.Text = file.AccessDateTime;
        }

    }
}
