using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Battle
    {
        private static Battle _instance;
        public static Battle Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Battle();
                }
                return _instance;
            }
        }
        public void BattleScene()
        {
            Console.WriteLine();
        }
    }
}