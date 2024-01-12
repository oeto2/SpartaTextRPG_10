using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
        Archer,
        Berserker,
        Warlord,
        Reaper,
        Demonic
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
        public int needExp = 100; //레벨업에 필요한 경험치 총량
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
        //레벨업 전용 생성자
        public Player(int _level,float _baseAtk, float _baseDef, float _maxHp, float _maxMp)
        {
            level = _level;
            baseAtk = _baseAtk;
            baseDef = _baseDef;
            maxHp = _maxHp;
            maxMp = _maxMp;
        }
        

        public static Player player = new Player(0, "미정", 3, 0, 0, 50, 50, 20, 20);

        //플레이어 경험치 획득
        public void GetExp(int _getExp)
        {
            //이전 능력치
            //Player pastState = player;
            Player pastState = new Player(player.level, player.baseAtk, player.baseDef, player.maxHp, player.maxMp);

            //획득 경험치 적용
            player.exp += _getExp;

            //레벨업 조건
            while (player.exp >= needExp)
            {
                LevelUP(needExp);
                needExp = player.level * 50;
            }

            //레벨업 페이지
            if (pastState.level != player.level)
                ShowLevelUpPage(pastState);
        }

        public void LevelUP(int _needExp)
        {
            player.exp -= _needExp;
            player.level++;

            //다음 레벨업에 필요한 경험치
            needExp += player.level * 50;
        }

        public void ShowLevelUpPage(Player _pastState)
        {
            Console.Clear();
            Console.WriteLine("플레이어 - 레벨업\n");
            Console.WriteLine("축하합니다! 플레이어가 레벨업 했습니다.\n");
            Console.WriteLine("[능력치 변화]");
            Console.WriteLine($"Lv : {_pastState.level} Lv -> {player.level} Lv");
            Console.WriteLine($"공격력 : {_pastState.baseAtk} -> {player.baseAtk}");
            Console.WriteLine($"방어력 : {_pastState.baseDef} -> {player.baseDef}");
            Console.WriteLine($"최대 체력 : {_pastState.maxHp} -> {player.maxHp}");
            Console.WriteLine($"최대 마력 : {_pastState.maxMp} -> {player.maxMp}");

            Console.ReadLine();
            Console.Clear();
        }
    }
}