using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass.Quest
{
    //시작 가능한 퀘스트
    public partial class QuestList
    {
        //시작 가능한 퀘스트가 있는지 확인
        public void CheckStQuest()
        {
            //플레이어 직업
            Job playerJob = Player.player.job;

            #region 메인퀘스트 (자동 진행)

            //아이템 장착 퀘스트 시작 조건
            if (questList[2] != null && !questList[2].isOngoing && !questList[2].isComplete)
            {
                questList[2].isPossible = true;
                questList[2].isOngoing = true;
            }

            //1차 전직 퀘스트 시작 조건 (직업: 모험가, 아이템 장착 퀘스트 클리어)
            if (questList[2].isClear)
            {
                questList[0].isPossible = true;
                questList[0].isOngoing = true;
            }


            //2차 전직 퀘스트 시작 조건 (1차 전직 퀘스트 완료)
            if (questList[0].isClear)
            {
                questList[1].isPossible = true;
                questList[1].isOngoing = true;
            }
            #endregion


            #region 서브 퀘스트 (수동 진행)

            //던전 입장하기 퀘스트 시작 조건
            if (questList[3] != null && !questList[3].isOngoing && !questList[3].isComplete)
                questList[3].isPossible = true;

            //휴식하기 퀘스트 시작 조건
            if (questList[4] != null && !questList[4].isOngoing && !questList[4].isComplete)
                questList[4].isPossible = true;

            //포션아이템 사용 퀘스트 시작 조건
            if (questList[6] != null && !questList[6].isOngoing && !questList[6].isComplete)
                questList[6].isPossible = true;

            //물고기 10마리 잡기
            if (questList[7] != null && !questList[7].isOngoing && !questList[7].isComplete)
                questList[7].isPossible = true;

            //실버 물고기 잡기
            if (questList[8] != null && !questList[8].isOngoing && !questList[8].isComplete)
                questList[8].isPossible = true;

            //골드 물고기 잡기
            if (questList[9] != null && !questList[9].isOngoing && !questList[9].isComplete)
                questList[9].isPossible = true;

            //불고기 잡기
            if (questList[10] != null && !questList[10].isOngoing && !questList[10].isComplete)
                questList[10].isPossible = true;

            #endregion
        }

        //시작 가능한 퀘스트 목록 보여주기
        public void ShowStQuestList()
        {
            int questNum = 0;
            stQuestNum = 0;

            int row, col;

            //시작 가능한 퀘스트가 있는지 확인
            CheckStQuest();

            //시작 가능한 퀘스트가 없을 경우
            if (questList.Find(x => x.isPossible && !x.isOngoing && !x.isComplete && x.type == QuestType.Sub) == null)
                Color.ChangeTextColor(Colors.RED, "", "[시작 가능한 퀘스트가 없습니다]\n");

            //시작 가능한 퀘스트가 있을 경우
            else
            {
                ////메인 퀘스트
                //Color.ChangeTextColor(Colors.RED, "", "[메인 퀘스트]\n");
                //foreach (Quest _quest in questList)
                //{
                //    if (_quest.isPossible && !_quest.isOngoing && !_quest.isComplete && _quest.type == QuestType.Main)
                //    {
                //        ++stQuestNum;

                //        (row, col) = Console.GetCursorPosition();
                //        Color.ChangeTextColor(Colors.YELLOW, "", $"{++questNum}.");
                //        //Console.Write($"{qeustNum++}.");
                //        Console.SetCursorPosition(3, col);

                //        //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                //        Console.Write($"{_quest.name} ");
                //        Console.SetCursorPosition(20, col);
                //        Console.Write('|');

                //        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                //        Console.Write($" {_quest.info}");
                //        Console.SetCursorPosition(60, col);
                //        Console.Write('|');


                //        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                //        Console.Write($" {_quest.gold}".PadLeft(6));
                //        Console.SetCursorPosition(68, col);
                //        //Color.ChangeTextColor(Colors.YELLOW, "", "G");
                //        Console.Write("G");
                //        Console.SetCursorPosition(70, col);
                //        Console.Write('|');

                //        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(5));
                //        Console.Write($" {_quest.exp}".PadLeft(5));
                //        Console.SetCursorPosition(77, col);
                //        //Color.ChangeTextColor(Colors.YELLOW, "", "EXP ");
                //        Console.Write("EXP ");
                //        Console.WriteLine("\n");
                //    }
                //}

                ////시작가능한 메인 퀘스트가 없다면
                //if (questNum == 0)
                //    Color.ChangeTextColor(Colors.RED, "", "[시작 가능한 메인 퀘스트가 없습니다]\n\n");
                ////Console.WriteLine();

                Color.ChangeTextColor(Colors.MAGENTA, "", "[서브 퀘스트]\n");
                //서브 퀘스트
                foreach (Quest _quest in questList)
                {
                    if (_quest.isPossible && !_quest.isOngoing && !_quest.isComplete && _quest.type == QuestType.Sub)
                    {
                        ++stQuestNum;

                        (row, col) = Console.GetCursorPosition();
                        Color.ChangeTextColor(Colors.YELLOW, "", $"{++questNum}.");
                        //Console.Write($"{qeustNum++}.");
                        Console.SetCursorPosition(3, col);

                        //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                        Console.Write($"{_quest.name} ");
                        Console.SetCursorPosition(22, col);
                        Console.Write('|');

                        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                        Console.Write($" {_quest.info}");
                        Console.SetCursorPosition(66, col);
                        Console.Write('|');


                        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                        Console.Write($" {_quest.gold}".PadLeft(6));
                        Console.SetCursorPosition(74, col);
                        //Color.ChangeTextColor(Colors.YELLOW, "", "G");
                        Console.Write("G");
                        Console.SetCursorPosition(76, col);
                        Console.Write('|');

                        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(5));
                        Console.Write($" {_quest.exp}".PadLeft(6));
                        Console.SetCursorPosition(84, col);
                        //Color.ChangeTextColor(Colors.YELLOW, "", "EXP ");
                        Console.Write("EXP ");
                        Console.WriteLine("");
                    }
                }

                ////시작 가능한 서브 퀘스트가 없다면
                //if (questNum == 0)
                //    Color.ChangeTextColor(Colors.MAGENTA, "", "[시작 가능한 서브 퀘스트가 없습니다]\n\n");
                //    //Console.WriteLine();
            }
        }


    }
}
