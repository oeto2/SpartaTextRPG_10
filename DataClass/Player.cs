using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    //플레이어 직업
    public enum Job
    {
        //초보자, 전사, 도적, 마법사, 궁수
        Beginner,
        Warrior,
        Thief,
        Wizard,
        Archer
    }

    internal class Player
    {
        public int id { get; set; }
        public string name { get; set; }
        public float baseAtk { get; set; }
        public float baseDef { get; set; }
        public float addAtk { get; set; }
        public float addDef { get; set; }
        public Job job { get; set; }
        public int level = 1;
        public float maxHp { get; set; }
        public float hp { get; set; }
        public float maxMp { get; set; }
        public float mp { get; set; }
        public int gold = 1000;
        public int exp = 0;
        public int weapon { get; set; }
        public int armor { get; set; }

        //캐릭터 생성자
        public Player(int _id, string _name, float _baseAtk, float _baseDef, Job _job, float _maxHp, float _hp,
                      float _maxMp, float _mp)
        {
            id = _id;
            name = _name;
            baseAtk = _baseAtk;
            baseDef = _baseDef;
            job = _job;
            maxHp = _maxHp;
            hp = _hp;
            maxMp = _maxMp;
            mp = _mp;
        }
        public static Player player = new Player(0, "미정", 5, 0, 0, 50, 50, 20, 20);
    }
}