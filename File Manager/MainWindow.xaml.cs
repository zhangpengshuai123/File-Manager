using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Media;
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
            SizeLabel.Text = file.Size;
            FullPathLabel.Text = file.FullPath;
            CreateTimeLabel.Text = file.CreateTime;
            LastWriteTimeLabel.Text = file.WriteTime;
            LastAccessTimeLabel.Text = file.AccessTime;
        }

        private void MoveUpCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            object curItem = SortPriorityList.SelectedItem;
            int index = SortPriorityList.SelectedIndex;
            if (index == 0)
            {
                return;
            }
            SortPriorityList.Items.Remove(curItem);
            SortPriorityList.Items.Insert(index-1, curItem);
            SortPriorityList.SelectedIndex = index - 1;
        }

        private void MoveUpCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SortPriorityList.SelectedIndex > -1)
            {
                e.CanExecute = true;
                return;
            }
            e.CanExecute = false;
            return;
        }

        private void MoveDownCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            object curItem = SortPriorityList.SelectedItem;
            int index = SortPriorityList.SelectedIndex;
            if (index == SortPriorityList.Items.Count -1)
            {
                return;
            }
            SortPriorityList.Items.Remove(curItem);
            SortPriorityList.Items.Insert(index + 1, curItem);
            SortPriorityList.SelectedIndex = index + 1;
        }

        private void MoveDownCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SortPriorityList.SelectedIndex > -1)
            {
                e.CanExecute = true;
                return;
            }
            e.CanExecute = false;
            return;
        }

        private void SortFileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Sort("FileOnly");
        }

        private void SortFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Sort("FolderOnly");
        }

        private void SortAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Sort("All");
        }

        private void Sort(string sSortMode)
        {
            List<SysFileIf> targetList;
            List<SysSorter> condList;

            condList = BuildCondition();
            targetList = SelectFiles(sSortMode);
            FileComparer newComparer = new FileComparer(condList);
            targetList.Sort(newComparer);
            foreach (SysFileIf curItem in targetList)
            {
                CurrentFiles.Add(curItem);
            }
            FileListView.Items.Clear();
            FileListView.ItemsSource = CurrentFiles;

            this.RibbonTaskBar.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
        }

        private List<SysFileIf> SelectFiles(string sSortMode)
        {
            List<SysFileIf> targetList = new List<SysFileIf>();

            foreach (SysFileIf curItem in CurrentFiles)
            {
                switch (sSortMode)
                {
                    case "FolderOnly":
                        if (curItem.Type == "Folder")
                        {
                            targetList.Add(curItem);
                        }
                        break;
                    case "All":
                        targetList.Add(curItem);
                        break;
                    default:
                        if (curItem.Type != "Folder")
                        {
                            targetList.Add(curItem);
                        }
                        break;
                }
            }
            foreach (SysFileIf curItem in targetList)
            {
                CurrentFiles.Remove(curItem);
            }
            return targetList;
        }

        private List<SysSorter> BuildCondition()
        {
            List<SysSorter> resultList = new List<SysSorter>();

            foreach (StackPanel curItem in SortPriorityList.Items)
            {
                SysSorter newCond;

                if (((TextBlock)curItem.Children[0]).Text == "Name")
                {
                    if (((CheckBox)curItem.Children[1]).IsChecked == false)
                    {
                        newCond = new SysSorter(SortField.Name, SortDirectrion.Ascending);
                    }
                    else
                    {
                        newCond = new SysSorter(SortField.Name, SortDirectrion.Descending);
                    }
                    resultList.Add(newCond);
                    continue;
                }

                if (((TextBlock)curItem.Children[0]).Text == "Type")
                {
                    if (((CheckBox)curItem.Children[1]).IsChecked == false)
                    {
                        newCond = new SysSorter(SortField.Type, SortDirectrion.Ascending);
                    }
                    else
                    {
                        newCond = new SysSorter(SortField.Type, SortDirectrion.Descending);
                    }
                    resultList.Add(newCond);
                    continue;
                }

                if (((TextBlock)curItem.Children[0]).Text == "Size")
                {
                    if (((CheckBox)curItem.Children[1]).IsChecked == false)
                    {
                        newCond = new SysSorter(SortField.Size, SortDirectrion.Ascending);
                    }
                    else
                    {
                        newCond = new SysSorter(SortField.Size, SortDirectrion.Descending);
                    }
                    resultList.Add(newCond);
                    continue;
                }

                if (((TextBlock)curItem.Children[0]).Text == "Create Time")
                {
                    if (((CheckBox)curItem.Children[1]).IsChecked == false)
                    {
                        newCond = new SysSorter(SortField.CreateTime, SortDirectrion.Ascending);
                    }
                    else
                    {
                        newCond = new SysSorter(SortField.CreateTime, SortDirectrion.Descending);
                    }
                    resultList.Add(newCond);
                    continue;
                }
                
                if (((TextBlock)curItem.Children[0]).Text == "Write Time")
                {
                    if (((CheckBox)curItem.Children[1]).IsChecked == false)
                    {
                        newCond = new SysSorter(SortField.WriteTime, SortDirectrion.Ascending);
                    }
                    else
                    {
                        newCond = new SysSorter(SortField.WriteTime, SortDirectrion.Descending);
                    }
                    resultList.Add(newCond);
                    continue;
                }
            }

            return resultList;
        }

    }
}
