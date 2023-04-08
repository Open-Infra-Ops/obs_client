using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace obs_client
{
    public delegate void DelegateShowResult(string msg);

    public partial class obs_client : Form
    {
        ILog logger = LogManager.GetLogger(typeof(obs_client));
        public obs_client()
        {
            InitializeComponent();
            refreshBucketObjectsHandler();
        }

        private void refreshBucketObjectsHandler() {
            this.labelResult.Text = "obs桶开始刷新";
            this.refreshBucketOjects();
            this.labelResult.Text = "obs桶刷新成功";
        }

        private void refreshBucketOjects() {
            var fileName = new List<Dictionary<string, string>>();
            BucketOperations bucektOpt = new BucketOperations();
            fileName = bucektOpt.ListObjects();
            this.listViewObs.Items.Clear();
            foreach (var fileDict in fileName)
            {
                ListViewItem item = new ListViewItem(fileDict["name"]);
                item.SubItems.Add(fileDict["size"]);
                item.SubItems.Add(fileDict["lastModified"]);
                this.listViewObs.Items.Add(item);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshBucketObjectsHandler();
        }

        private void ShowMessage(string msg)
        {
            if (this.labelResult.InvokeRequired)
            {
                this.Invoke(new Action(() => { labelResult.Text = msg; }));
            } else {
                this.labelResult.Text = msg;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Title = "请选择要打开的文件";
            ofd.Multiselect = true;
            ofd.Filter = "文本文件|*.txt|音频文件|*.wav|图片文件|*.jpg|所有文件|*.*";
            ofd.FilterIndex = 4;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                string fileName = ofd.SafeFileName;
                string msg = "用户选择的文件目录为:" + filePath + ";用户选择的文件名称为:" + fileName;
                logger.Info(msg);
                this.labelResult.Text = "obs开始上传：" + fileName;
                DelegateShowResult showResultDeletage = new DelegateShowResult(ShowMessage);
                UploadFileHandler uploadFileHandler = new UploadFileHandler(filePath, fileName, showResultDeletage);
                var threadUpload = new Thread(new ThreadStart(uploadFileHandler.UploadFile));
                threadUpload.Start();
                //threadUpload.Join();
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (this.listViewObs.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一个文件下载，请勿选择多个或未选中");
                return;
            }
            string fullPath = this.listViewObs.SelectedItems[0].SubItems[0].Text;
            string[] arrayFullPath = fullPath.Trim().Split('/');
            string currentFileName = arrayFullPath[arrayFullPath.Length - 1];
            if (arrayFullPath.Length == 0 || currentFileName == "")
            {
                MessageBox.Show("请选择一个文件下载，请勿选择文件夹");
                return;
            }
            string msg = "用户下载目录是:" + fullPath + ";用户保存的文件名称：" + currentFileName;
            logger.Info(msg);
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Title = "请选择要保存的文件";
            ofd.FileName = currentFileName;
            ofd.Filter = "All files(*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {   
                string filePath = ofd.FileName;
                logger.Info("用户选择的文件目录为:" + filePath);
                this.labelResult.Text = "obs开始下载：" + fullPath;
                DelegateShowResult showResult = new DelegateShowResult(ShowMessage);
                DownloadFileHandler downloadFileHandler = new DownloadFileHandler(filePath, fullPath, showResult);
                var threadUpload = new Thread(new ThreadStart(downloadFileHandler.DownloadFile));
                threadUpload.Start();
                //threadUpload.Join();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listViewObs.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一个文件删除，请勿选择多个或未选中");
                return;
            }
            string fullPath = this.listViewObs.SelectedItems[0].SubItems[0].Text;
            string[] arrayFullPath = fullPath.Trim().Split('/');
            string currentFileName = arrayFullPath[arrayFullPath.Length - 1];
            if (arrayFullPath.Length == 0 || currentFileName == "")
            {
                MessageBox.Show("请选择一个文件删除，请勿选择文件夹");
                return;
            }
            logger.Info("删除文件路径："+ fullPath);
            BucketOperations bucektOpt = new BucketOperations();
            bucektOpt.DeleteFile(fullPath);
            this.refreshBucketOjects();
            this.labelResult.Text = "obs删除文件" + fullPath + "成功";
        }

        private void listViewObs_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                e.NewWidth = listViewObs.Columns[e.ColumnIndex].Width;
            }
            catch(Exception Ex) {
                logger.Error("listViewObs_ColumnWidthChanging Ex:" + Ex.Message.ToString());
            }
            
        }

    }
}
