using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OBS.Internal;
using OBS.Model;
using OBS;
using System.Collections;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Threading;
using log4net;
using System.Net.Configuration;
using log4net.Core;
using log4net.Util;

namespace obs_client
{
    public class UploadFileHandler
    {
        public string FilePath;
        public string FiletName;
        public DelegateShowResult delegateShowUploadResult;
        public UploadFileHandler(string filePath, string fileName, DelegateShowResult showResultDeletage)
        {
            FilePath = filePath;
            FiletName = fileName;
            delegateShowUploadResult = showResultDeletage;
        }
        public void UploadFile()
        {
            BucketOperations bucektOpt = new BucketOperations();
            bucektOpt.UploadFile(FilePath, FiletName, delegateShowUploadResult);
        }
    }

    public class DownloadFileHandler
    {
        public string FilePath;
        public string FullFilePath;
        public DelegateShowResult delegateDownloadFile;
        public DownloadFileHandler(string filePath, string fullFilePath, DelegateShowResult DownloadFileHandler)
        {
            FilePath = filePath;
            FullFilePath = fullFilePath;
            delegateDownloadFile = DownloadFileHandler;
        }
        public void DownloadFile()
        {
            BucketOperations bucektOpt = new BucketOperations();
            bucektOpt.DownloadFile(FilePath, FullFilePath, delegateDownloadFile);
        }
    }

    class BucketOperations
    {
        ILog logger = LogManager.GetLogger(typeof(BucketOperations));
        private ObsClient client;
        private string bucketName;
        private string bucketPath;
        public BucketOperations() {
            var configDict = new Dictionary<string, string>();
            configDict = ObsXml.ReadXml();
            client = new ObsClient(configDict["Ak"], configDict["Sk"], configDict["Url"]);
            bucketName = configDict["BucketName"];
            bucketPath = configDict["Path"];
        }

        #region ListObjects
        public List<Dictionary<string, string>> ListObjects()
        {
            var fileName = new List<Dictionary<string,string>>();
            try
            {
                ListObjectsRequest request = new ListObjectsRequest()
                {
                    BucketName = bucketName
                };
                ListObjectsResponse response = client.ListObjects(request);
                logger.Info("Listing Objects response: {0}" + response.StatusCode);
                foreach (ObsObject entry in response.ObsObjects)
                {
                    var dictObj = new Dictionary<string, string>();
                    string name = entry.ObjectKey.ToString();
                    if (!name.StartsWith(bucketPath)) {
                        continue;
                    }
                    dictObj.Add("name", name);
                    int.TryParse(entry.Size.ToString(), out int size);
                    size = (size >> 20) + 1;
                    dictObj.Add("size", size.ToString());
                    dictObj.Add("lastModified", entry.LastModified.ToString());
                    fileName.Add(dictObj);
                }
                return fileName;
            }
            catch (ObsException ex)
            {
                string msg = "Exception errorcode:" + ex.ErrorCode + ", when list objects.Exception errormessage:" + ex.ErrorMessage;
                logger.Info(msg);
                return fileName;
            }
        }
        #endregion

        #region UploadFile 
        public void UploadFile(string filePath, string fileName, DelegateShowResult showResultDeletage) 
        {
            try
            {
                string fullObjectKey = bucketPath + fileName;
                UploadFileRequest request = new UploadFileRequest
                {
                    BucketName = bucketName,
                    ObjectKey = fullObjectKey,
                    UploadFile = filePath,
                    UploadPartSize = 10 * 1024 * 1024,
                    EnableCheckpoint = true,
                };
                request.ProgressType = ProgressTypeEnum.ByBytes;
                request.ProgressInterval = 1024 * 1024;
                // callback to show progress and speed
                request.UploadProgress += delegate (object sender, TransferStatus status) {
                    string msg;
                    string progressUpload= status.TransferPercentage.ToString();
                    if (progressUpload == "100")
                    {
                        msg = @"obs已经上传完成，请刷新";
                    }
                    else 
                    {
                        UInt64 speedUpload = Convert.ToUInt64(status.AverageSpeed / 1024);
                        msg = @"obs上传" + fullObjectKey + "速度:" + speedUpload.ToString() + @"KB/S,上传进度:" + progressUpload + @"%";
                    }
                    showResultDeletage(msg);

                };
                request.UploadEventHandler += delegate (object sender, ResumableUploadEvent e) {
                    logger.Info("upload file EventType: " +  e.EventType);
                };
                CompleteMultipartUploadResponse response = client.UploadFile(request);
                logger.Info("Upload File response:" + fullObjectKey + ",result:"+  response.StatusCode);
            }
            catch (ObsException ex)
            {
                string msg = "Exception errorcode:" + ex.ErrorCode + ", when upload file.Exception errormessage:" + ex.ErrorMessage;
                logger.Info(msg);
            }

        }
        #endregion

        # region DownloadFile
        public void DownloadFile(string filePath, string fullFilePath, DelegateShowResult delegateDownloadFile)
        {
            try
            {
                DownloadFileRequest request = new DownloadFileRequest
                {
                    BucketName = bucketName,
                    ObjectKey = fullFilePath,
                    DownloadFile = filePath,
                    DownloadPartSize = 1024 * 1024 * 10,
                    EnableCheckpoint = true,
                };
                request.ProgressType = ProgressTypeEnum.ByBytes;
                request.ProgressInterval = 1024 * 1024;
                request.DownloadProgress += delegate (object sender, TransferStatus status) {
                    string msg;
                    UInt64 speedUpload = Convert.ToUInt64(status.AverageSpeed / 1024);
                    string progressUpload = status.TransferPercentage.ToString();
                    if (progressUpload == "100")
                    {
                        msg = @"obs已经下载完成";
                    }
                    else
                    {
                        msg = @"obs下载" + fullFilePath + "速度:" + speedUpload.ToString() + @"KB/S,下载进度:" + progressUpload + @"%";
                    }
                    delegateDownloadFile(msg);
                };
                request.DownloadEventHandler += delegate (object sender, ResumableDownloadEvent e) {
                    logger.Info("download file EventType: " + e.EventType);
                };
                GetObjectMetadataResponse response = client.DownloadFile(request);
                logger.Info("Download File response: " + fullFilePath + ",result:" + response.StatusCode);
            }
            catch (ObsException ex)
            {
                string msg = "Exception errorcode:" + ex.ErrorCode + ", when download file.Exception errormessage:" + ex.ErrorMessage;
                logger.Info(msg);
            }

        }
        #endregion

        #region DeleteFile
        public void DeleteFile(string fullPath) 
        {
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = fullPath,
                };
                DeleteObjectResponse response = client.DeleteObject(request);
                logger.Info("Delete object response: " + fullPath + ",result:" +response.StatusCode);
            }
            catch (ObsException ex)
            {
                string msg = "Exception errorcode:" + ex.ErrorCode + ", when delete file.Exception errormessage:" + ex.ErrorMessage;
                logger.Info(msg);
            }
        }
        #endregion

        #region SetBucketPolicy
        public void SetBucketPolicy()
        {
            try
            {
                SetBucketPolicyRequest request = new SetBucketPolicyRequest()
                {
                    BucketName = bucketName,
                    ContentMD5 = "md5",
                    Policy = "policy"
                };
                SetBucketPolicyResponse response = client.SetBucketPolicy(request);
                logger.Info("Set bucket policy response: " + response.StatusCode);
            }
            catch (ObsException ex)
            {
                string msg = "Exception errorcode:" + ex.ErrorCode + ", when set bucket policy.Exception errormessage:" + ex.ErrorMessage;
                logger.Info(msg);
            }
        }
        #endregion

        #region GetBucketPolicy
        public void GetBucketPolicy()
        {
            try
            {
                GetBucketPolicyRequest request = new GetBucketPolicyRequest()
                {
                    BucketName = bucketName,
                };
                GetBucketPolicyResponse response = client.GetBucketPolicy(request);
                logger.Info("Get bucket policy response: " + response.StatusCode);
                logger.Info("Bucket policy: " + response.Policy);
            }
            catch (ObsException ex)
            {
                string msg = "Exception errorcode:" + ex.ErrorCode + ", when get bucket policy.Exception errormessage:" + ex.ErrorMessage;
                logger.Info(msg);
            }
        }
        #endregion




    }
}
