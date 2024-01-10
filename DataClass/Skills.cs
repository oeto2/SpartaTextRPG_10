using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass
{

    public enum Jobs
    {
        //초보자, 전사, 도적, 마법사, 궁수
        Warrior,
        Thief,
        Berserker,
        Warlord,
        Reaper,
        Demonic
    }
    public class Skills
    {
        public int id { get; set; }
        public string name { get; set; }
        public float damage { get; set; }
        public float baseAtk { get; set; }
        public float baseDef { get; set; }
        public Jobs job { get; set; }
        public float hp { get; set; }
        public float mp { get; set; }
        public string text { get; set; }
        public bool learn = false;

        //캐릭터 생성자
        public Skills(int _id, string _name, float _damage, float _baseAtk, float _baseDef, Jobs _job, float _hp, float _mp, string _text)
        {
            id = _id;
            name = _name;
            damage = _damage;
            baseAtk = _baseAtk;
            baseDef = _baseDef;
            job = _job;
            hp = _hp;
            mp = _mp;
            text = _text;
        }
        public static Skills smash = new Skills(0, "스매쉬", 1.5f, 0, 0, Jobs.Warrior, 0, -10, "온 몸의 기운을 한 데 모아 적에게 일격을 가하는 기술");
        public static Skills luncky7 = new Skills(1, "럭키세븐", 2.0f, 0, 0, Jobs.Thief, 0, -20, "대상에게 행운을 담은 2개의 표창을 연속으로 던진다.");
        public static Skills frenzy = new Skills(2, "프렌지", 2.0f, 0, 0, Jobs.Berserker, -15, 0, "일정량의 HP를 소모하여 연속해서 베고 바닥에 처박는다.");
        public static Skills bash = new Skills(3, "배쉬", 1.2f, 0, 0, Jobs.Warlord, 0, 0, "MP소모 없이 적을 방패로 강하게 타격한다.");
        public static Skills requiem = new Skills(4, "레퀴엠", 2.3f, 0, 0, Jobs.Reaper, 0, -25, "정신을 집중하여 그림자의 기운을 받아 적을 기습한다.");
        public static Skills howling = new Skills(5, "하울링", 2.1f, 0, 0, Jobs.Demonic, 0, -25, "악마의 힘을 모으며 울부 짖어 주변 적에게 피해를 준다.");

        public static List<Skills> allskills = new List<Skills>();
        public static List<Skills> myskills = new List<Skills>();
    }
}