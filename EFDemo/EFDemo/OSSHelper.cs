using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.OSS;
using System.IO;


namespace EFDemo
{
    public class OSSHelper
    {
        public const string bucketName = "yixun-picture";
        public static OssClient createClient()  //创建OSS连接OssClient
        {
            const string accessKeyId = "LTAI5t8NSKrYjEyYSzMfGv7A";
            const string accessKeySecret = "pfvwYE5eHNsTcDSNykVU3mcoWVuuQz";
            const string endpoint = "http://oss-cn-shanghai.aliyuncs.com";
            return new OssClient(endpoint, accessKeyId, accessKeySecret);
        }
        
        public static string uploadImage(Stream text, string path)
        {
            const string accessKeyId = "LTAI5t8NSKrYjEyYSzMfGv7A";
            const string accessKeySecret = "pfvwYE5eHNsTcDSNykVU3mcoWVuuQz";
            const string endpoint = "http://oss-cn-shanghai.aliyuncs.com";
            const string bucketName = "yixun-picture";
            var filebyte = StreamHelper.StreamToBytes(text);        //流转为byte[]
            var client = new OssClient(endpoint, accessKeyId, accessKeySecret); //新建连接
            MemoryStream stream = new MemoryStream(filebyte, 0, filebyte.Length);
            client.PutObject(bucketName, path, stream);
            string imgurl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/" + path;
            return imgurl;
        }
    }
}
