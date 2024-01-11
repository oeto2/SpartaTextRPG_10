using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace SpartaTextRPG.DataClass.Quest
{
    public partial class QuestList
    {
        public static QuestList instance = new QuestList();

        public static int stQuestNum = 0; //시작 가능한 퀘스트 갯수
        public static int curQuestNum = 0; //진행중인 퀘스트 갯수
        public static int compQuestNum = 0; //완료가능한 퀘스트 갯수

        //퀘스트 목록
        public static List<Quest> questList = new List<Quest>()
        {
            new Quest(0,"1차 전직","캐릭터의 레벨을 올려 1차 전직을 해보자.",2000, 200,'M'),
            new Quest(1,"2차 전직","캐릭터의 레벨을 올려 2차 전직을 해보자.",3000, 500,'M'),
            new Quest(2,"아이템 장착","인벤토리에서 아이템을 장착해보자",500,50 ,'M'),
            new Quest(3,"던전 입장하기","던전에 입장해보자",600,100,'S'),
            new Quest(4,"휴식하기","휴식 기능을 사용해보자",1000,150,'S'),
            new Quest(5,"Stage.2 던전 클리어","Stage.2 던전을 클리어해보자.",500,50,'M'),
            new Quest(6,"포션 아이템 사용","상점에서 포션을 구매해서 사용해보자",500,50,'S'),
        };

        //퀘스트 시작
        public void StartQuest(int _questNum)
        {
            int count = 0;

            foreach (Quest _quest in questList)
            {
                if (_quest.isPossible && !_quest.isOngoing && _quest.type == 'M')
                {
                    ++count;
                    if (_questNum == count)
                        _quest.isOngoing = true;
                }
            }

            foreach (Quest _quest in questList)
            {
                if (_quest.isPossible && !_quest.isOngoing && _quest.type == 'S')
                {
                    ++count;
                    if (_questNum == count)
                        _quest.isOngoing = true;
                }
            }
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


        public Quest(int _id, string _name, string _info, int _gold, int _exp, char _type)
        {
            id = _id;
            name = _name;
            info = _info;
            gold = _gold;
            exp = _exp;
            type = _type;
        }
    }

}
