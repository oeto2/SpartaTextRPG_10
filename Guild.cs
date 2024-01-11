using SpartaTextRPG.DataClass;
using SpartaTextRPG.DataClass.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Guild
    {
        //싱글톤
        public static Guild instance = new Guild();

        //잘못된 입력인지 체크
        private bool isWrong = false;

        //길드 페이지
        public void ShowGuildPage()
        {
            Console.Clear();
            if(isWrong)
                Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.\n");

            Color.ChangeTextColor(Colors.YELLOW, "", "길드\n");
            Console.WriteLine("퀘스트를 확인할 수 있습니다.");
            Console.WriteLine();
            Color.ChangeTextColor(Colors.YELLOW, "", "1. ", "새로운 퀘스트 받기\n");
            Color.ChangeTextColor(Colors.YELLOW, "", "2. ", "진행중인 퀘스트 확인\n");
            Color.ChangeTextColor(Colors.YELLOW, "", "3. ", "퀘스트 보상 받기\n");
            Color.ChangeTextColor(Colors.RED, "", "0. ", "나가기\n");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Color.ChangeTextColor(Colors.YELLOW, "", ">>");
            string input = Console.ReadLine();

            switch (input)
            {
                //잘못된 입력
                default:
                    Program.scene = Scene.guild;
                    isWrong = true;
                    break;

                //나가기
                case "0":
                    Program.scene = Scene.mainScene;
                    isWrong = false;
                    break;

                //퀘스트 시작 페이지
                case "1":
                    ShowRequestPage();
                    isWrong = false;
                    break;

                //퀘스트 확인 페이지
                case "2":
                    ShowCurQuestPage();
                    isWrong = false;
                    break;

                //퀘스트 완료 페이지
                case "3":
                    ShoCompQuestPage();
                    isWrong = false;
                    break;
            }
        }

        //퀘스트 받기
        public void ShowRequestPage()
        {
            Console.Clear();
            Color.ChangeTextColor(Colors.YELLOW, "", "길드 - 퀘스트 받기\n");
            Console.WriteLine("새로운 퀘스트를 받을 수 있습니다.\n");

            //시작 가능한 퀘스트 목록 보여주기
            QuestList.instance.ShowStQuestList();

            Color.ChangeTextColor(Colors.RED, "", "\n0. ", "나가기\n");

            Console.WriteLine("\n의뢰 받을 퀘스트의 번호를 입력해주세요.");
            Title.PrintInputCursor();
            string input = Console.ReadLine();
            
            switch(input)
            {
            }

            QuestList.instance.StartQuest(int.Parse(input));
        }

        //진행중인 퀘스트
        public void ShowCurQuestPage()
        {
            Console.Clear();
            Color.ChangeTextColor(Colors.YELLOW, "", "길드 - 퀘스트 진행\n");
            Console.WriteLine("진행중인 퀘스트를 확인할 수 있습니다.\n");

            //진행중인 퀘스트 목록 보여주기
            QuestList.instance.ShowCurQuestList();

            Color.ChangeTextColor(Colors.RED, "", "\n0. ", "나가기\n");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Title.PrintInputCursor();
            Console.ReadLine();
        }

        //완료한 퀘스트
        public void ShoCompQuestPage()
        {
            Console.Clear();
            Color.ChangeTextColor(Colors.YELLOW, "", "길드 - 퀘스트 완료\n");
            Console.WriteLine("완료한 퀘스트를 확인할 수 있습니다.\n");

            //완료한 퀘스트 목록 보여주기
            QuestList.instance.ShowCompQuestList();

            Color.ChangeTextColor(Colors.RED, "", "\n0. ", "나가기\n");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Title.PrintInputCursor();
            Console.ReadLine();
        }
    }
}
