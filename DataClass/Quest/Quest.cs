using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace SpartaTextRPG.DataClass.Quest
{
     public class Quest
    {
        public int id { get; set; } //퀘스트 번호
        public string name { get; set; } //퀘스트 이름
        public string info { get; set; } //퀘스트 정보
        public int gold { get; set; } //퀘스트 보상 골드
        public int exp { get; set; } // 퀘스트 보상 경험치
        public bool isPossible = false; //퀘스트 시작 조건 충족 여부
        public bool isOngoing = false; //퀘스트 진행 여부
        public bool isComplete = false; //퀘스트 완료여부
        public char type; //퀘스트 타입 'M','S'


        public Quest(int _id, string _name, string _info, int _gold, int _exp, bool _isPossible, char _type)
        {
            id = _id;
            name = _name;
            info = _info;
            gold = _gold;
            exp = _exp;
            isPossible = _isPossible;
            type = _type;
        }
    }

    public partial class QuestList
    {
        public static QuestList instance = new QuestList();

        //시작 가능한 퀘스트 리스트
        public static List<Quest> stQuestList = new List<Quest>();
        //진행중인 퀘스트 리스트
        public static List<Quest> curQuestList = new List<Quest>();
        //완료한 퀘스트 리스트
        public static List<Quest> endQuestList = new List<Quest>();

        //퀘스트 목록
        public static List<Quest> questList = new List<Quest>()
        {
            new Quest(0,"1차 전직","캐릭터의 레벨을 올려 1차 전직을 해보자.",2000, 200,true,'M'),
            new Quest(1,"2차 전직","캐릭터의 레벨을 올려 2차 전직을 해보자.",3000, 500,false,'M'),
            new Quest(2,"아이템 장착","인벤토리에서 아이템을 장착해보자",500,50,true ,'S'),
            new Quest(3,"하급 던전 입장","하급 던전에 입장해보자",600,100, true,'S'),
            new Quest(4,"휴식","휴식 기능을 사용해보자",1000,150, true,'S'),
            new Quest(5,"하급 던전 클리어","하급 던전을 클리어해보자.",500,50, false,'S'),
            new Quest(6,"포션 아이템 사용","상점에서 포션을 구매해서 사용해보자",500,50, false,'S'),
        };

        //퀘스트 받기
        public void RequestQuest(Quest _quest)
        {
            curQuestList.Add(_quest);
        }

        //퀘스트 완료
        public void QuestClear(List<Quest> _questList, int _id)
        {
            _questList[_id].isComplete = true;
        }

        //퀘스트 보상받기
        public void GetQuestReward(List<Quest> _questList, int _id)
        {
            //골드 획득
            Player.player.gold += _questList[_id].gold;

            //경험치 획득 (이후에 함수 받아서 사용해야할수도)
            Player.player.exp += _questList[_id].exp;
        }

        public int GetPadLenght(string _str)
        {
            int padLen = 30 - Encoding.Default.GetBytes(_str).Length;
            return padLen;
        }

        //원하는 길이로 문자열 반환
        public static string SortString(string _str, int _Lenght, int _maxLength)
        {
            string str = _str;

            int addSpace = _maxLength - str.Length;

            for (int i = _str.Length; i < _Lenght + addSpace; i++)
            {
                str += " ";
            }

            return str;
        }
    }
}
