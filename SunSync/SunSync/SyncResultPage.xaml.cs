﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SunSync
{
    /// <summary>
    /// Interaction logic for SyncResultPage.xaml
    /// </summary>
    public partial class SyncResultPage : Page
    {
        private bool fileOverwrite;
        private int fileExistsCount;
        private int fileOverwriteCount;
        private int fileNotOverwriteCount;
        private int fileUploadErrorCount;
        private int fileUploadSuccessCount;
        private Dictionary<string, string> syncResultInfo;
        private MainWindow mainWindow;
        public SyncResultPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.fileOverwrite = false;
            this.syncResultInfo = new Dictionary<string, string>();
            this.syncResultInfo.Add("UPLOAD_SUCCESS", "本次同步成功同步到七牛云空间中的文件数量。");
            this.syncResultInfo.Add("UPLOAD_FAILURE", "本地同步因为各种原因没有成功同步到七牛云空间中的文件数量。");
            this.syncResultInfo.Add("UPLOAD_EXISTS_MATCH", "本次同步过程中发现的已存在于云空间且本地未改动的文件数量，这些文件本地和空间内容一致，所以同步过程中自动跳过。");
            this.syncResultInfo.Add("UPLOAD_EXISTS_NO_OVERWRITE", "本次同步过程中发现的已存在于云空间且本地已有改动的文件数量，这些文件没有进行覆盖上传。如果需要覆盖上传，请在同步设置里面勾选覆盖选项。");
            this.syncResultInfo.Add("UPLOAD_EXISTS_OVERWRITE", "本次同步过程中发现的已存在于云空间且本地已有改动的文件数量，这些文件进行了覆盖上传。");
        }

        public void LoadSyncResult(bool fileOverwrite, int fileExistsCount, int fileOverwriteCount, int fileNotOverwriteCount,
            int fileUploadErrorCount, int fileUploadSuccessCount)
        {
            this.fileOverwrite = fileOverwrite;
            this.fileExistsCount = fileExistsCount;
            this.fileOverwriteCount = fileOverwriteCount;
            this.fileNotOverwriteCount = fileNotOverwriteCount;
            this.fileUploadErrorCount = fileUploadErrorCount;
            this.fileUploadSuccessCount = fileUploadSuccessCount;
        }

        private void BackToHome_EventHandler(object sender, MouseButtonEventArgs e)
        {
            this.mainWindow.GotoHomePage();
        }

        private void SyncResultLoaded_EventHandler(object sender, RoutedEventArgs e)
        {
            int uploadSuccess = 0;
            int uploadFailure = 0;
            int uploadExistsMatch = 0;
            int uploadExistsNoOverwrite = 0;
            int uploadExistsOverwrite = 0;

            uploadSuccess = this.fileUploadSuccessCount;
            uploadFailure = this.fileUploadErrorCount;
            uploadExistsMatch = this.fileExistsCount;
            uploadExistsOverwrite = this.fileOverwriteCount;
            uploadExistsNoOverwrite = this.fileNotOverwriteCount;

            this.UploadSuccessTextBlock1.Text = string.Format("同步成功: {0}", uploadSuccess);
            this.UploadSuccessTextBlock2.Text = syncResultInfo["UPLOAD_SUCCESS"];

            this.UploadFailureTextBlock1.Text = string.Format("同步失败: {0}", uploadFailure);
            this.UploadFailureTextBlock2.Text = syncResultInfo["UPLOAD_FAILURE"];

            this.UploadExistsTextBlock1.Text = string.Format("智能跳过: {0}", uploadExistsMatch);
            this.UploadExistsTextBlock2.Text = syncResultInfo["UPLOAD_EXISTS_MATCH"];

            if (this.fileOverwrite)
            {
                this.UploadOverwriteTextBlock1.Text = string.Format("强制覆盖: {0}", uploadExistsOverwrite);
                this.UploadOverwriteTextBlock2.Text = syncResultInfo["UPLOAD_EXISTS_OVERWRITE"];
            }
            else
            {
                this.UploadOverwriteTextBlock1.Text = string.Format("未覆盖: {0}", uploadExistsNoOverwrite);
                this.UploadOverwriteTextBlock2.Text = syncResultInfo["UPLOAD_EXISTS_NO_OVERWRITE"];
            }
        }
    }
}