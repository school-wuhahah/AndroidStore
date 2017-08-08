using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Hero;

public class JsonMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Hero hero = new Hero();
        hero.heroName = "张三";
        hero.heroInt = 15;
        hero.heroMessage = "张三与李四、王五并列";

        string  stringJson=JsonUtility.ToJson(hero);
        Debug.Log(stringJson);
        ReadAndWriteMessage.WriteMessage(stringJson);

        if(ReadAndWriteMessage.ReadMessage()!=null)
        {
            Hero heroNew = JsonUtility.FromJson<Hero>(ReadAndWriteMessage.ReadMessage());
            heroNew.PrintHero();
        }
	}
	
	// Update is called once per frame
	void Update () {
       

    }
}
