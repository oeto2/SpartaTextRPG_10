using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaTextRPG
{

    internal class Title
    {
        public static Title instance = new Title();

        //잘못된 입력 감지
        private bool isWrong = false;

        //이어하기
        private bool isContinue;
            
        //플레이어 이름
        string playerName;

        //타이틀 화면 시작
        public void StartTitle()
        {
            //메인 타이틀 화면
            while (ShowMainTitle() != 1) { };

            if(!isContinue)
            {
                //이름 짓기
                while (NamePlayer() != 1) { };

                //프롤로그 시작
                StartProlog();

                //플레이어 이름 변경
                Player.player.name = playerName;
            }    
        }

        //스토리 스킵
        public void StartTitle(bool _skip)
        {
            //메인 타이틀 화면
            ShowMainTitle();

            //이름 짓기
            while (NamePlayer() != 1) { };

            //플레이어 이름 변경
            Player.player.name = playerName;
        }

        //프롤로그
        private void StartProlog()
        {
            Console.Clear();
            Console.CursorVisible = false;

            //Prolog1-1: 평화로운 마을
            ShowImage(3);
            Console.WriteLine("================================================================================");
            WriteChar("인간들의 대륙 \"엘리시움\"은 신비로운 힘과 환상적인 존재로 가득 차 있었다.", 50);
            WriteChar("그곳에서는 인간 뿐만아니라 여러 종족이 어우러져 살아가고 있었다.", 50);
            WriteChar("하지만 평화는 오래가지 않았다.", 50);
            Console.ReadKey();

            //Prolog1-2: 드래곤의 습격
            Console.Clear();
            ShowImage(2);
            Console.WriteLine();
            Console.WriteLine("================================================================================");
            WriteChar("어느날, 드래곤과 마물들이 인간들을 습격했다.", 50);
            WriteChar("마물들의 습격으로 인해 마을은 황폐화되었고, 많은 인간들이 죽어나갔다.", 50);
            WriteChar("인간들은 마물들을 피해 숨었고, 원래 살던 마을은 던전이 되었다.", 50);
            Console.ReadKey();


            //Prolog1-3: 모험의 시작
            Console.Clear();
            ShowImage(5);
            Console.WriteLine("================================================================================");
            WriteChar("그로부터 20년 후..", 50);
            WriteChar("{0}(은)는 부모님의 복수를 하기 위해 던전으로 향했다.", 50, playerName);
            Console.ReadKey();
            Console.CursorVisible = true;
        }


        //유저 이름 짓기
        private int NamePlayer()
        {
            //이름을 지었는지
            bool isName = false;

            //캐릭터 이름
            playerName = "";

            while (playerName.Length < 1 || playerName.Contains(" "))
            {
                Console.Clear();
                Color.ChangeTextColor(Colors.YELLOW, "", "스파르타 RPG에 오신 여러분 환영합니다.\n\n");
                Color.ChangeTextColor(Colors.MAGENTA, "모험을 시작하기 앞서 ", "캐릭터의 이름", "을 입력 해주세요.\n");
                if (CheckEmpty(playerName))
                    Color.ChangeTextColor(Colors.RED, "", "******캐릭터 이름에는 공백이 포함될 수 없습니다.******\n");
                Console.Write(">>");
                playerName = Console.ReadLine();
            }

            while (!isName)
            {
                isName = false;

                Console.Clear();
                Color.ChangeTextColor(Colors.YELLOW, "", "캐릭터생성 - 이름\n");
                Console.WriteLine();

                Color.ChangeTextColor(Colors.MAGENTA, "", "캐릭터의 이름", "을 ");
                Color.ChangeTextColor(Colors.RED, "", playerName, "(으)로 하시겠습니까?\n");
                Console.WriteLine("=============================================");
                Console.WriteLine("1.예");
                Console.WriteLine("2.아니오");
                Console.WriteLine();
                if (isWrong)
                    Color.ChangeTextColor(Colors.RED, "", "******잘못된 입력입니다.******\n");
                Console.Write(">>");

                string input = Console.ReadLine();

                switch (input)
                {
                    //예
                    case "1":
                        isWrong = false;
                        isName = true;
                        return 1;

                    //아니오
                    case "2":
                        isWrong = false;
                        return 2;

                    default:
                        isWrong = true;
                        break;
                }
            }
            return 1;
        }

        //한 글자씩 출력
        public void WriteChar(string _Sentence, int _delayTime)
        {
            for (int i = 0; i < _Sentence.Length; i++)
            {
                Console.Write(_Sentence[i]);
                Thread.Sleep(_delayTime);
            }
            Console.WriteLine();
        }

        public void WriteChar(string _Sentence, int _delayTime, string _word)
        {
            if (_Sentence.Contains("{0}"))
            {
                string[] str = _Sentence.Split("{0}");
                string[] str2 = str[0].Split("{0}");

                _Sentence = str2[0] + _word + str[1];
            }

            for (int i = 0; i < _Sentence.Length; i++)
            {
                Console.Write(_Sentence[i]);
                Thread.Sleep(_delayTime);
            }
            Console.WriteLine();
        }

        //입력에 공백이 있으면 true
        public static bool CheckEmpty(string _input)
        {
            if (_input.Contains(" "))
                return true;
            else
                return false;
        }

        //입력 체크
        public static int CheckIntInput(int _min, int _max)
        {
            //입력 값, Nul이면 99반환
            int input = 99;

            //입력값이 int인지
            bool isInt;

            do
            {
                isInt = int.TryParse(Console.ReadLine(), out input);

                //null값 감지
                if (input == 0)
                    return input;
            } while (!isInt || (input < _min && input > _max));

            //이상이 없으면 입력 값 반환
            return input;
        }

        //현재 줄만 지우기
        public static void clearCurrentLine()
        {
            string s = "\r";
            s += new string(' ', Console.CursorLeft);
            s += "\r";
            Console.Write(s);
        }

        //메인 타이틀
        private int ShowMainTitle()
        {
            Console.Clear();
            //타이틀 이미지
            PrintTitleImage();

            Console.Write(">>");
            string input = Console.ReadLine();

            switch(input)
            {
                default:
                    break;
                case "1":
                    return 1;

                case "2":
                    return LoadingScene();

                case "4":
                    Guild.instance.ShowGuildPage();
                    break;
            }

            return 3;
        }

        //로딩화면
        private int LoadingScene()
        {
            Console.Clear();
            Console.WriteLine("게임 데이터를 불러오는 중입니다...");
            Thread.Sleep(100);
            Console.WriteLine("상점주인이 아이템을 준비하고 있습니다...");
            Thread.Sleep(100);
            Console.WriteLine("던전에 몬스터들이 배치되었습니다.");
            Thread.Sleep(100);
            Console.WriteLine("주인공이 모험을 떠날 준비를 하고있습니다...");
            Thread.Sleep(100);

            //메인화면
            isContinue = true;
            return 1;
        }

        //타이틀 이미지
        private void PrintTitleImage()
        {
            ShowImage(1);
            Color.ChangeTextColor(Colors.YELLOW, "", "  ____                   _          ____  ____   ____ \n");
            Color.ChangeTextColor(Colors.YELLOW, "", " / ___| _ __   __ _ _ __| |_ __ _  |  _ \\|  _ \\ / ___|\n");
            Color.ChangeTextColor(Colors.YELLOW, "", " \\___ \\| '_ \\ / _` | '__| __/ _` | | |_) | |_) | |  _ \n");
            Color.ChangeTextColor(Colors.YELLOW, "", "  ___) | |_) | (_| | |  | || (_| | |  _ <|  __/| |_| |\n");
            Color.ChangeTextColor(Colors.YELLOW, "", " |____/| .__/ \\__,_|_|   \\__\\__,_| |_| \\_\\_|    \\____|\n");
            Color.ChangeTextColor(Colors.YELLOW, "", "       |_|                                            \n");

            //Console.WriteLine("  ____                   _          ____  ____   ____ ");
            //Console.WriteLine(" / ___| _ __   __ _ _ __| |_ __ _  |  _ \\|  _ \\ / ___|");
            //Console.WriteLine(" \\___ \\| '_ \\ / _` | '__| __/ _` | | |_) | |_) | |  _ ");
            //Console.WriteLine("  ___) | |_) | (_| | |  | || (_| | |  _ <|  __/| |_| |");
            //Console.WriteLine(" |____/| .__/ \\__,_|_|   \\__\\__,_| |_| \\_\\_|    \\____|");
            //Console.WriteLine("       |_|                                            ");

            Console.WriteLine("======================================================");
            Color.ChangeTextColor(Colors.MAGENTA, "", "                      모험의 시작                      \n");
            Console.WriteLine("======================================================");
            Color.ChangeTextColor(Colors.YELLOW, "","                      1. 새로하기                      \n");
            Color.ChangeTextColor(Colors.RED, "", "                      2. 이어하기                      \n");
        }

        //이미지 보여주기
        private void ShowImage(int _num)
        {
            switch (_num)
            {
                //드래곤 이미지1
                case 1:
                    Console.Clear();
                    //Color.ChangeTextColor(Colors.RED, "", "@@@@@@@@@@@@@@@@@@@@@**^^\"\"~~~\"^@@^*@*@@**@@@@@@@@@@@@\r\n@@@@@@@@@@@@@*^^'\"~   , - ' '; ,@@b. '  -e@@@@@@@@@@@@\r\n@@@@@@@@*^\"~      . '     . ' ,@@@@(  e@*@@@@@@@@@@@@@\r\n@@@@@^~         .       .   ' @@@@@@, ~^@@@@@@@@@@@@@@\r\n@@@~ ,e**@@*e,  ,e**e, .    ' '@@@@@@e,  \"*@@@@@'^@@@@\r\n@',e@@@@@@@@@@ e@@@@@@       ' '*@@@@@@    @@@'   0@@@\r\n@@@@@@@@@@@@@@@@@@@@@',e,     ;  ~^*^'    ;^~   ' 0@@@\r\n@@@@@@@@@@@@@@@^\"\"^@@e@@@   .'           ,'   .'  @@@@\r\n@@@@@@@@@@@@@@'    '@@@@@ '         ,  ,e'  .    ;@@@@\r\n@@@@@@@@@@@@@' ,&&,  ^@*'     ,  .  i^\"@e, ,e@e  @@@@@\r\n@@@@@@@@@@@@' ,@@@@,          ;  ,& !,,@@@e@@@@ e@@@@@\r\n@@@@@,~*@@*' ,@@@@@@e,   ',   e^~^@,   ~'@@@@@@,@@@@@@\r\n@@@@@@, ~\" ,e@@@@@@@@@*e*@*  ,@e  @@\"\"@e,,@@@@@@@@@@@@\r\n@@@@@@@@ee@@@@@@@@@@@@@@@\" ,e@' ,e@' e@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@\" ,@\" ,e@@e,,@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@~ ,@@@,,0@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@,,@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"");
                    Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@**^^\"\"~~~\"^@@^*@*@@**@@@@@@@@@@@@\r\n@@@@@@@@@@@@@*^^'\"~   , - ' '; ,@@b. '  -e@@@@@@@@@@@@\r\n@@@@@@@@*^\"~      . '     . ' ,@@@@(  e@*@@@@@@@@@@@@@\r\n@@@@@^~         .       .   ' @@@@@@, ~^@@@@@@@@@@@@@@\r\n@@@~ ,e**@@*e,  ,e**e, .    ' '@@@@@@e,  \"*@@@@@'^@@@@\r\n@',e@@@@@@@@@@ e@@@@@@       ' '*@@@@@@    @@@'   0@@@\r\n@@@@@@@@@@@@@@@@@@@@@',e,     ;  ~^*^'    ;^~   ' 0@@@\r\n@@@@@@@@@@@@@@@^\"\"^@@e@@@   .'           ,'   .'  @@@@\r\n@@@@@@@@@@@@@@'    '@@@@@ '         ,  ,e'  .    ;@@@@\r\n@@@@@@@@@@@@@' ,&&,  ^@*'     ,  .  i^\"@e, ,e@e  @@@@@\r\n@@@@@@@@@@@@' ,@@@@,          ;  ,& !,,@@@e@@@@ e@@@@@\r\n@@@@@,~*@@*' ,@@@@@@e,   ',   e^~^@,   ~'@@@@@@,@@@@@@\r\n@@@@@@, ~\" ,e@@@@@@@@@*e*@*  ,@e  @@\"\"@e,,@@@@@@@@@@@@\r\n@@@@@@@@ee@@@@@@@@@@@@@@@\" ,e@' ,e@' e@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@\" ,@\" ,e@@e,,@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@~ ,@@@,,0@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@,,@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"");
                    break;

                //드래곤 이미지2
                case 2:
                    Console.WriteLine("< ~>\r\n \\ \\, _____\r\n       ___`\\\r\n       \\('>\\`-__\r\n         ~      ~~~--__            **              ***\r\n               ______  (@\\   *******  ****    *******    ******\r\n              /******~~~~\\|**********************************\r\n      \\       `--____******************************************\r\n     / ~~~--_____    ~~~/ ***************************************\r\n                 `~~~~~         ******************************\r\n                                      ****    **************\r\n                                        ***       ***********\r\n                                                        ********");
                    break;

                //마을 이미지1
                case 3:
                    Console.WriteLine(" (   )\r\n                          (    )\r\n                           (    )\r\n                          (    )\r\n                            )  )\r\n                           (  (                  /\\\r\n                            (_)                 /  \\  /\\\r\n                    ________[_]________      /\\/    \\/  \\\r\n           /\\      /\\        ______    \\    /   /\\/\\  /\\/\\\r\n          /  \\    //_\\       \\    /\\    \\  /\\/\\/    \\/    \\\r\n   /\\    / /\\/\\  //___\\       \\__/  \\    \\/\r\n  /  \\  /\\/    \\//_____\\       \\ |[]|     \\\r\n /\\/\\/\\/       //_______\\       \\|__|      \\\r\n/      \\      /XXXXXXXXXX\\                  \\\r\n        \\    /_I_II  I__I_\\__________________\\\r\n               I_I|  I__I_____[]_|_[]_____I\r\n               I_II  I__I_____[]_|_[]_____I\r\n               I II__I  I     XXXXXXX     I\r\n            ~~~~~\"   \"~~~~~~~~~~~~~~~~~~~~~~~~");
                    break;

                //칼 이미지1
                case 4:
                    Console.WriteLine(" ,\r\n          / \\\r\n         {   }\r\n         p   !\r\n         ; : ;\r\n         | : |\r\n         | : |\r\n         l ; l\r\n         l ; l\r\n         I ; I\r\n         I ; I\r\n         I ; I\r\n         I ; I\r\n         d | b\r\n         H | H\r\n         H | H\r\n         H I H\r\n ,;,     H I H     ,;,\r\n;H@H;    ;_H_;,   ;H@H;\r\n`\\Y/d_,;|4H@HK|;,_b\\Y/'\r\n '\\;MMMMM$@@@$MMMMM;/'\r\n   \"~~~*;!8@8!;*~~~\"\r\n         ;888;\r\n         ;888;\r\n         ;888;\r\n         ;888;\r\n         d8@8b\r\n         O8@8O\r\n         T808T\r\n          `~`");
                    break;

                //나무 이미지1
                case 5:
                    Console.WriteLine("   .              .              ;%     ;;\r\n        ,           ,                :;%  %;\r\n         :         ;                   :;%;'     .,\r\n,.        %;     %;            ;        %;'    ,;\r\n  ;       ;%;  %%;        ,     %;    ;%;    ,%'\r\n   %;       %;%;      ,  ;       %;  ;%;   ,%;'\r\n    ;%;      %;        ;%;        % ;%;  ,%;'\r\n     `%;.     ;%;     %;'         `;%%;.%;'\r\n      `:;%.    ;%%. %@;        %; ;@%;%'\r\n         `:%;.  :;bd%;          %;@%;'\r\n           `@%:.  :;%.         ;@@%;'\r\n             `@%.  `;@%.      ;@@%;\r\n               `@%%. `@%%    ;@@%;\r\n                 ;@%. :@%%  %@@%;\r\n                   %@bd%%%bd%%:;\r\n                     #@%%%%%:;;\r\n                     %@@%%%::;\r\n                     %@@@%(o);  . '\r\n                     %@@@o%;:(.,'\r\n                 `.. %@@@o%::;\r\n                    `)@@@o%::;\r\n                     %@@(o)::;\r\n                    .%@@@@%::;\r\n                    ;%@@@@%::;.\r\n                   ;%@@@@%%:;;;.\r\n               ...;%@@@@@%%:;;;;,.. ");
                    break;

            }

        }

        public static void PrintInputCursor()
        {
            Color.ChangeTextColor(Colors.YELLOW, "", ">>");
        }
    }
}

