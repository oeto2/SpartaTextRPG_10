using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaTextRPG
{
    public enum ItemType
    {
        WEAPON,
        ARMOR,
        POTION,

    }

    internal class Item
    {
        public static Item _instance;

        public static Item Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Item();
                }
                return _instance;
            }
        }

        public int id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public ItemType type { get; set; }
        public int cost { get; set; }
        public bool isOwn { get; set; }

        public List<EquipItem> equipItems = new List<EquipItem>();
        public List<EquipItem> enforceInit = new List<EquipItem>();
        public List<ConsumItem> consumItems = new List<ConsumItem>();


        public void ItemInit()
        {
            equipItems.Add(new EquipItem(1, "돌 칼", "돌로 된 칼입니다.", ItemType.WEAPON, 1000, 5, 2));
            equipItems.Add(new EquipItem(2, "금 칼", "금으로 된 칼입니다.", ItemType.WEAPON, 2000, 10, 0));
            equipItems.Add(new EquipItem(3, "다이아몬드 칼", "다이아몬드로 된 칼입니다.", ItemType.WEAPON, 3000, 15, 0));

            equipItems.Add(new EquipItem(11, "가죽 갑옷", "가죽으로 된 갑옷입니다.", ItemType.ARMOR, 1000, 0, 5));
            equipItems.Add(new EquipItem(12, "강철 갑옷", "강철로 된 갑옷입니다.", ItemType.ARMOR, 2000, 0, 10));
            equipItems.Add(new EquipItem(13, "미스릴 갑옷", "미스릴로 된 갑옷입니다.", ItemType.ARMOR, 3000, 0, 10));

            enforceInit = equipItems.ConvertAll(p => new EquipItem(p.id, p.name, p.info, p.type, p.cost, p.atk, p.def));

            consumItems.Add(new ConsumItem(21, "하급 포션", "하급 포션입니다.", ItemType.POTION, 500, 30, 0));
            consumItems.Add(new ConsumItem(22, "중급 포션", "중급 포션입니다.", ItemType.POTION, 1500, 60, 30));
        }
    }

    // 장비 아이템 클래스
    internal class EquipItem : Item
    {

        public int atk { get; set; }
        public int def { get; set; }
        public int enforce { get; set; }

        public EquipItem(int _id, string _name, string _info, ItemType _type, int _cost, int _atk, int _def, int _enforce = 0)
        {
            id = _id;
            name = _name;
            info = _info;
            type = _type;
            cost = _cost;
            atk = _atk;
            def = _def;
            enforce = _enforce;
        }

        public void AddItemStat()
        {
            Player.player.addAtk += atk;
            Player.player.addDef += def;
        }
        public void SubItemStat()
        {
            Player.player.addAtk -= atk;
            Player.player.addDef -= def;
        }
    }

    // 소비 아이템 클래스
    internal class ConsumItem : Item
    {
        public int recoveryHp { get; set; }
        public int recoveryMp { get; set; }
        public int count { get; set; }

        public ConsumItem(int _id, string _name, string _info, ItemType _type, int _cost, int _recoveryHp, int _recoveryMp, int _count = 0)
        {
            id = _id;
            name = _name;
            info = _info;
            type = _type;
            cost = _cost;
            recoveryHp = _recoveryHp;
            recoveryMp = _recoveryMp;
            count = _count;
        }
    }




}
