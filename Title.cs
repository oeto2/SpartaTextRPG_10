﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Title
    {
        public static Title instance = new Title();



        //타이틀 화면 시작
        public void StartTitle()
        {
            ShowTitle();


        }

        
        //타이틀 보여주기
        public void ShowTitle()
        {

            Console.Clear();
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@**^^\"\"~~~\"^@@^*@*@@**@@@@@@@@@@@@\r\n@@@@@@@@@@@@@*^^'\"~   , - ' '; ,@@b. '  -e@@@@@@@@@@@@\r\n@@@@@@@@*^\"~      . '     . ' ,@@@@(  e@*@@@@@@@@@@@@@\r\n@@@@@^~         .       .   ' @@@@@@, ~^@@@@@@@@@@@@@@\r\n@@@~ ,e**@@*e,  ,e**e, .    ' '@@@@@@e,  \"*@@@@@'^@@@@\r\n@',e@@@@@@@@@@ e@@@@@@       ' '*@@@@@@    @@@'   0@@@\r\n@@@@@@@@@@@@@@@@@@@@@',e,     ;  ~^*^'    ;^~   ' 0@@@\r\n@@@@@@@@@@@@@@@^\"\"^@@e@@@   .'           ,'   .'  @@@@\r\n@@@@@@@@@@@@@@'    '@@@@@ '         ,  ,e'  .    ;@@@@\r\n@@@@@@@@@@@@@' ,&&,  ^@*'     ,  .  i^\"@e, ,e@e  @@@@@\r\n@@@@@@@@@@@@' ,@@@@,          ;  ,& !,,@@@e@@@@ e@@@@@\r\n@@@@@,~*@@*' ,@@@@@@e,   ',   e^~^@,   ~'@@@@@@,@@@@@@\r\n@@@@@@, ~\" ,e@@@@@@@@@*e*@*  ,@e  @@\"\"@e,,@@@@@@@@@@@@\r\n@@@@@@@@ee@@@@@@@@@@@@@@@\" ,e@' ,e@' e@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@\" ,@\" ,e@@e,,@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@~ ,@@@,,0@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@,,@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"");
            Console.WriteLine("  ____                   _          ____  ____   ____ ");
            Console.WriteLine(" / ___| _ __   __ _ _ __| |_ __ _  |  _ \\|  _ \\ / ___|");
            Console.WriteLine(" \\___ \\| '_ \\ / _` | '__| __/ _` | | |_) | |_) | |  _ ");
            Console.WriteLine("  ___) | |_) | (_| | |  | || (_| | |  _ <|  __/| |_| |");
            Console.WriteLine(" |____/| .__/ \\__,_|_|   \\__\\__,_| |_| \\_\\_|    \\____|");
            Console.WriteLine("       |_|                                            ");
            Console.WriteLine("============================================================");
            Console.WriteLine("                      모험의 시작                            ");
            Console.WriteLine("============================================================");
            Console.WriteLine("                 PRESS ANYKEY TO START                      ");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
        }
    }
}
