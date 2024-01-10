using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass
{
    internal class Quest
    {
        //퀘스트 번호
        public int id { get; set; }
        //퀘스트 이름
        public string name { get; set; }
        //퀘스트 내용
        public string info { get; set; }
        //퀘스트 보상 골드
        public int gold { get; set; }
        //퀘스트 보상 경험치
        public int exp { get; set; }
        
    }
}
