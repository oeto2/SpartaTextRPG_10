﻿using System.ComponentModel;

namespace SpartaTextRPG.DataClass
{
    public class Quest
    {
        public int id { get; set; } //퀘스트 번호
        public string name { get; set; } //퀘스트 이름
        public string info { get; set; } //퀘스트 정보
        public int gold { get; set; } //퀘스트 보상 골드
        public int exp { get; set; } // 퀘스트 보상 경험치
        public bool isComplete = false; //퀘스트 완료여부

        public Quest(int _id, string _name, string _info, int _gold, int _exp)
        {
            id = _id;
            name = _name; 
            info = _info;
            gold = _gold; 
            exp = _exp;
        }
    }

    public class QuestList
    {
        public static QuestList instance = new QuestList();

        //메인 퀘스트 목록
        public static List<Quest> mainQuestList = new List<Quest>()
        {
            new Quest(0,"1차 전직 하기","캐릭터의 레벨을 올려 1차 전직을 해보자.",2000, 200),
            new Quest(2,"2차 전직 하기","캐릭터의 레벨을 올려 2차 전직을 해보자.",3000, 500)
        };

        //서브 퀘스트 목록
        public static List<Quest> subQuestList = new List<Quest>()
        {
            new Quest(0,"아이템 장착하기","인벤토리에서 아이템을 장착해보자",500,50),
            new Quest(1,"하급 던전 입장하기","하급 던전에 입장해보자",600,100),
            new Quest(2,"휴식하기","상점에서 아이템을 구매해보자",500,50),
            new Quest(3,"하급 던전 클리어","하급 던전을 클리어해보자.",500,50),
            new Quest(4,"포션 아이템 사용하기","상점에서 포션을 구매해서 사용해보자",500,50),
        };

        //진행중인 퀘스트
        public static List<Quest> curQuestList = new List<Quest>();

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

        //메인 퀘스트 목록 보여주기
        public void ShowMainQuest()
        {
            foreach(Quest _quest in mainQuestList)
            {
                Console.WriteLine("{0}    |{1}    |보상 : {2} Gold, {3} Exp", _quest.name, _quest.info, _quest.gold, _quest.exp);
            }
        }
    }
}