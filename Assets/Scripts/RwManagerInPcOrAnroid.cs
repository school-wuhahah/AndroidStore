using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.Threading;

namespace RwManager
{
    public static class RwManagerInPcOrAnroid
    {
        /// <summary>
        /// pc或android端向指定文件路径存储数据
        /// </summary>
        /// <param name="path">Assets下的文件路径(xxx/xxx)</param>
        /// <param name="fileName">文件的名字(xxx.txt)</param>
        public static void WriteMessageInPcOrAndroid(string path, string fileName, string messageText)
        {

            //string fullPath = Path.Combine(Path.Combine(GetFrontPath(), path), fileName);
            string listPath = Path.Combine(GetFrontPath(), path);
            Debug.Log(listPath);
            if (!Directory.Exists(listPath))
            {
                //Debug.Log(fullPath);
                Directory.CreateDirectory(listPath);
                string fullPath = Path.Combine(listPath, fileName);
                FileStream fs = new FileStream(fullPath, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(messageText);
                sw.Flush();
                sw.Close();
                Debug.Log("写入文件完成");

            }


        }
        /// <summary>
        /// pc或android端读取指定文件夹中的数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName">文件的名字(xxx.txt)</param>
        /// <returns></returns>
        public static string ReadMessageInPcOrAndroid(string path, string fileName)
        {
            string fullPath = Path.Combine(Path.Combine(GetFrontPath(), path), fileName);
            string result = "";

            if (File.Exists(fullPath))
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                result = sr.ReadToEnd();
                sr.Close();
                return result;

            }
            Debug.Log("读取的文件不存在");
            return null;
        }
        /// <summary>
        /// pc或Android端从StreamingAssets中读取数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadMessageInStreamingAssets(string path, string fileName)
        {
            string fPath = Application.streamingAssetsPath;
            //Debug.Log(fPath);
            string fullPath = Path.Combine(Path.Combine(fPath, path), fileName);
            Debug.Log(fullPath);
            Debug.Log("Bool:"+File.Exists(fullPath));
            //android端判断File.Exists(fullPath)一直位false？？？，但可以通过这个路径利用www来读取(路径是对的)
            //有疑问
            //if (File.Exists(fullPath))
            //{
            Debug.Log("Path:" + fullPath);
                if (fullPath.Contains("://"))
                {

                    //new Thread(o =>
                    //{
                    //    //WWW www = new WWW(fullPath);

                    //    //if (www.isDone)



                    //});
                    WWW www = new WWW(fullPath);
                    while (true)
                    {
                        if (www.isDone)
                            break;
                    }
                    return www.text;
                }
                else
                {
                    return File.ReadAllText(fullPath);
                }
            //}

            //return null;
        }


        /// <summary>
        /// pc或android从Resources文件夹中读取数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadMessageInResources(string path, string fileName)
        {
            string name = null;
            char[] fc = fileName.ToCharArray();
            for (int i = 0; i < fc.Length; i++)
            {
                if (fc[i].ToString().Equals("."))
                {
                    name = fileName.Substring(0, i);
                    //Debug.Log(name);
                    break;
                }
                //Debug.Log(fc[i]);
            }
            if (string.IsNullOrEmpty(name))
            {
                name = fileName;
            }
            //Debug.Log(name);
            string fullpath = Path.Combine(path, name);
            //Debug.Log(fullpath);
            TextAsset ta = Resources.Load(fullpath) as TextAsset;
            if (ta)
            {
                return ta.text;
            }
            return null;
        }


        private static string GetFrontPath()
        {
#if UNITY_EDITOR
            return Application.dataPath;
#elif UNITY_STANDALONE_WIN
        return Application.dataPath ;
#elif UNITY_ANDROID
        return  Application.persistentDataPath ;
#endif
        }
    }
}
