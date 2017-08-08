using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Player.Hero
{
    [Serializable]
    public class Hero
    {
        public string heroName;
        public int heroInt;
        public string heroMessage;

        public void PrintHero()
        {
            Debug.Log("姓名："+heroName+" 年龄："+heroInt+" 描述："+heroMessage);
        }
    }
}
