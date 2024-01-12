using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{


    internal class DungeonEnter
    {
        public static DungeonEnter _instance;

        public static DungeonEnter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DungeonEnter(0, "");
                }
                return _instance;
            }
        }
        // 던전 클래스 안에 넣기
        //List<DungeonEnter> dungeonList = new List<DungeonEnter>()
        //{
        //    new DungeonEnter(10, "모험의 시작"),
        //    new DungeonEnter(20, "용의 둥지"),
        //    new DungeonEnter(30, "불의 둥지"),
        //    new DungeonEnter(40, "문영오의 둥지"),
        //    new DungeonEnter(50, "한효승의 안식처")
        //};

        public int dunAtk { get; set; }
        public string name { get; set; }

        public DungeonEnter(int _dunAtk, string _name)
        {
            dunAtk = _dunAtk;
            name = _name;
        }

        public void DungeonAnnounce(int index)
        {
            Console.WriteLine($"Stage.{index + 1} - {name}");
            if (Player.player.baseAtk + Player.player.addAtk >= dunAtk)
            {
                Color.ChangeTextColor(Colors.GREEN, "", $"   적정 공격력 : {dunAtk}\n");
            }
            else
            {
                Color.ChangeTextColor(Colors.RED, "", $"   적정 공격력 : {dunAtk}\n");
            }
        }
        // 던전 출력 문구 대신
        //for (int i = 0; i<dungeonList.Count; i++)
        //    {
        //        dungeonList[i].DungeonAnnounce(i);
        //}
    }
}
