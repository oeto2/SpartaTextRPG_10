using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpartaTextRPG.DataClass
{
    internal class Data
    {
        public static Data instnace = new Data();

        //Json 저장 파일
        List<string> gameData = new List<string>();

        //저장된 데이터 파일 존재하는지
        private bool haveData = false;

        //파일 저장 경로(현재 directory)
        string filePath = Directory.GetCurrentDirectory();

        public void Save()
        {
            //데이터 초기화 (중복 저장 방지)
            gameData = new List<string>();

            //저장할 데이터를 gamData에 넣기
            gameData.Add(new string(JsonConvert.SerializeObject(Item.Instance.equipItems)));


        //    //아이템 데이터들
        //    public List<EquipItem> equipItems = new List<EquipItem>();
        //public List<EquipItem> enforceInit = new List<EquipItem>();
        //public List<ConsumItem> consumItems = new List<ConsumItem>();
        //public List<Fish> fishList = new List<Fish>(); 

            //파일 저장
            for (int i = 0; i < gameData.Count; i++)
            {
                File.WriteAllText(filePath + "gameData" + i.ToString() + ".json", gameData[i].ToString());
            }
        }

        public void Load()
        {
            //데이터 파일의 존재 여부
            haveData = true;

            //디렉토리 내의 존재하는 json 파일의 갯수
            string[] files = Directory.GetFiles(filePath, "*.json", SearchOption.TopDirectoryOnly);

            //데이터 파일이 존재하는지 체크
            for (int i = 0; i < files.Length; i++)
            {
                if (!File.Exists(filePath + "gameData" + i.ToString() + ".json"))
                {
                    haveData = false;
                }
            }

            //데이터 파일들이 존재한다면
            if (haveData)
            {
                //데이터 파일 읽기
                for (int i = 0; i < files.Length; i++)
                {
                    gameData.Add(new string(File.ReadAllText(filePath + "gameData" + i.ToString() + ".json")));
                }

                //읽어온 내용 업데이트
                //

            }
        }
    }
}
