using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class DungeonEnter
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
        public int dunAtk { get; set; }
        public string name { get; set; }

        public int index = 0;

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
    }
}
