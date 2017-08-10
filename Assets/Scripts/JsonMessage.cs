using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Hero;
using System.IO;
using RwManager;

public class JsonMessage : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Hero hero = new Hero();
        hero.heroName = "张三";
        hero.heroInt = 15;
        hero.heroMessage = "张三与李四、王五并列";

        string stringJson = JsonUtility.ToJson(hero);
        //Debug.Log(stringJson);
        //ReadMessageInResources();
        //ReadMessageStreamingAssets();
        //RwManagerInPcOrAnroid.WriteMessageInPcOrAndroid("test", "hahha.txt", stringJson);
        //string result = RwManagerInPcOrAnroid.ReadMessageInPcOrAndroid("test", "hahha.txt");
        //Debug.Log(result);
        //string result=RwManagerInPcOrAnroid.ReadMessageInResources("", "hahha");
        //Debug.Log(result);
    
        string result = RwManagerInPcOrAnroid.ReadMessageInStreamingAssets("","Message.txt");
        Debug.Log(result);
    }

    // Update is called once per frame
    void Update()
    {


    }
    /// <summary>
    /// 在Resources中读取文件
    /// </summary>
    private void ReadMessageInResources()
    {
        //在Resources文件夹中读取文件比较简单直接Resources.Load();
        //不用加文件格式
        TextAsset textAsset = Resources.Load("Message") as TextAsset;
        Debug.Log("Resource:" + textAsset.text);
    }

    /// <summary>
    /// 在StreamingAssets文件夹中读取数据
    /// 需要用携程+www将文件读取，
    /// 并且android端的路径比较特殊为
    /// </summary>
    private void ReadMessageStreamingAssets()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Message.txt");
        //Debug.Log("Path:" + path);
        //string result = "";
        if (path.Contains("://"))
        {
            Debug.Log("Android Path" + path);
            StartCoroutine(ReadMessage(path));
        }
        else
        {
            string result = File.ReadAllText(path);
            Debug.Log("PC Path：" + path);
            Debug.Log("PC StreamingAssets" + result);
        }

    }
    IEnumerator ReadMessage(string path)
    {

        WWW www = new WWW(path);
        yield return www;
        Debug.Log("Android " + "StreamAssets:" + www.text);
        //result = www.text;

    }

    /// <summary>
    /// pc端通过Application.datapath读写，android端通过Application.persistenPath读写
    /// 注意Application.persistenPath在android端指定的一个目录，不能在Editor访问，可以在通过将StreamingAssets或者Resources文件夹中的
    /// 文件转存在Application.persistenPath路径中，在读取；
    /// </summary>
    private void StoreMessage(string stringJson)
    {
        ReadAndWriteMessage.WriteMessage(stringJson);

        if (ReadAndWriteMessage.ReadMessage() != null)
        {
            Hero heroNew = JsonUtility.FromJson<Hero>(ReadAndWriteMessage.ReadMessage());
            Debug.Log("Android中通过Application.persistenPath读取的");
            heroNew.PrintHero();
        }
    }
}
