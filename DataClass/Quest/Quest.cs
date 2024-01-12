using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace SpartaTextRPG.DataClass.Quest
{
    //퀘스트 타입
    public enum QuestType
    {
        Main, //메인 퀘스트
        Sub //서브 퀘스트
    }

    public partial class QuestList
    {
        public static QuestList instance = new QuestList();

        public static int stQuestNum = 0; //시작 가능한 퀘스트 갯수
        public static int curQuestNum = 0; //진행중인 퀘스트 갯수
        public static int compQuestNum = 0; //완료가능한 퀘스트 갯수

        //퀘스트 목록
        public static List<Quest> questList = new List<Quest>()
        {
            new Quest(0,"1차 전직","캐릭터의 레벨을 올려 1차 전직을 해보자.",500, 30,QuestType.Main),
            new Quest(1,"2차 전직","캐릭터의 레벨을 올려 2차 전직을 해보자.",3000, 500,QuestType.Main),
            new Quest(2,"아이템 장착","인벤토리에서 아이템을 장착해보자",200,500 ,QuestType.Main),
            new Quest(3,"던전 입장하기","던전에 입장해보자",600,100,QuestType.Sub),
            new Quest(4,"휴식하기","휴식 기능을 사용해보자",1000,150,QuestType.Sub),
            new Quest(5,"던전 클리어","난이도 상관없이 던전을 클리어해보자.",500,50,QuestType.Main),
            new Quest(6,"포션 아이템 사용","상점에서 포션을 구매해서 사용해보자",500,50,QuestType.Sub),
        };

        //퀘스트 시작
        public void StartQuest(int _questNum)
        {
            int count = 0;

            ////메인퀘스트
            //foreach (Quest _quest in questList)
            //{
            //    if (_quest.isPossible && !_quest.isOngoing && _quest.type == QuestType.Main)
            //    {
            //        ++count;
            //        if (_questNum == count)
            //            _quest.isOngoing = true;
            //    }
            //}

            //서브 퀘스트
            foreach (Quest _quest in questList)
            {
                if (_quest.isPossible && !_quest.isOngoing && _quest.type == QuestType.Sub)
                {
                    ++count;
                    if (_questNum == count)
                        _quest.isOngoing = true;
                }
            }
        }

        //퀘스트 완료
        public void QuestClear(int _questNum)
        {
            int count = 0;

            //클리어한 퀘스트
            Quest clearQuest = null;

            foreach (Quest _quest in questList)
            {
                if (_quest.isComplete && !_quest.isClear && _quest.type == QuestType.Main)
                {
                    ++count;
                    if (_questNum == count)
                    {
                        clearQuest = _quest;
                        _quest.isClear = true;
                    }
                }
            }

            foreach (Quest _quest in questList)
            {
                if (_quest.isComplete && !_quest.isClear && _quest.type == QuestType.Sub)
                {
                    ++count;
                    if (_questNum == count)
                    {
                        clearQuest = _quest;
                        _quest.isClear = true;
                    }
                }
            }

            //퀘스트 보상 받기
            GetQuestReward(clearQuest);
        }

        //퀘스트 보상받기
        public void GetQuestReward(Quest _quest)
        {
            if (_quest != null)
            {
                //골드 획득
                Player.player.gold += _quest.gold;

                //경험치 획득
                Player.player.GetExp(_quest.exp);
                Console.Clear();
            }
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
        public bool isComplete = false; //퀘스트 완료가능 여부 (퀘스트 완료조건 달성시 true)
        public bool isClear = false; //퀘스트 완료 (보상 받으면 true)
        public QuestType type;

        public Quest(int _id, string _name, string _info, int _gold, int _exp, QuestType _type)
        {
            id = _id;
            name = _name;
            info = _info;
            gold = _gold;
            exp = _exp;
            type = _type;
        }
    }

    //퀘스트 조건 (해당 퀘스트가 진행 중이여야만 동작)
    public class QuestBool
    {
        //휴식 기능을 사용 했는지
        public static bool IsRest = false;

        //물약을 사용했는지
        public static bool UsePotion = false;

        // 던전 클리어 했을 경우 (아무 던전이나 가능)
        public static bool DungeonClear = false;

        //던전에 입장했는지
        public static bool EnterDungeon = false;


        //프로퍼티
        public static bool usePotion
        {
            get { return UsePotion; }
            set { if (QuestList.questList[6].isOngoing) UsePotion = value; }
        } 

        public static bool dungeonClear
        {
            get { return DungeonClear; }
            set { if (QuestList.questList[5].isOngoing) DungeonClear = value; }
        }

        public static bool enterDungeon
        {
            get { return EnterDungeon; }
            set { if (QuestList.questList[3].isOngoing) EnterDungeon = value; }
        }

        public static bool isRest
        {
            get { return IsRest; }
            set { if (QuestList.questList[4].isOngoing) IsRest = value; }
        }
    }
}
