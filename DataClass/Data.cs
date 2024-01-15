using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpartaTextRPG.DataClass.Quest;

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

        //bool값
        private bool isWrong, isSave, isLoad, loadfail;

        public void Save()
        {
            //데이터 초기화 (중복 저장 방지)
            gameData = new List<string>();

            //저장할 데이터를 gamData에 넣기
            //ItemData
            gameData.Add(new string(JsonConvert.SerializeObject(Item.Instance.equipItems)));
            gameData.Add(new string(JsonConvert.SerializeObject(Item.Instance.enforceInit)));
            gameData.Add(new string(JsonConvert.SerializeObject(Item.Instance.consumItems)));
            gameData.Add(new string(JsonConvert.SerializeObject(Item.Instance.fishList)));
            //Inventory Data
            gameData.Add(new string(JsonConvert.SerializeObject(Inventory.Instance.ownEquipCount)));
            gameData.Add(new string(JsonConvert.SerializeObject(Inventory.Instance.ownConsumCount)));
            gameData.Add(new string(JsonConvert.SerializeObject(Inventory.Instance.ownFishCount)));
            //Player
            gameData.Add(new string(JsonConvert.SerializeObject(Player.player)));
            //Quest
            gameData.Add(new string(JsonConvert.SerializeObject(QuestList.questList)));
            gameData.Add(new string(JsonConvert.SerializeObject(QuestBool.instance)));



            //파일 저장
            for (int i = 0; i < gameData.Count; i++)
            {
                File.WriteAllText(filePath + "/gameData" + i.ToString() + ".json", gameData[i].ToString());
            }
        }

        public void Load()
        {
            //데이터 파일의 존재 여부
            haveData = true;

            //디렉토리 내의 존재하는 json 파일의 갯수
            string[] files = Directory.GetFiles(filePath, "*.json", SearchOption.AllDirectories);

            //데이터 파일
            List<string> dataFiles = new List<string>();

            //데이터 파일만 가져오기
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains("gameData"))
                {
                    dataFiles.Add(files[i]);
                }
            }

            //데이터 파일이 존재하는지 체크
            if (File.Exists(filePath + "/gameData" + 0 + ".json"))
            {
                for (int i = 0; i < dataFiles.Count; i++)
                {
                    if (!File.Exists(filePath + "/gameData" + i.ToString() + ".json"))
                    {
                        haveData = false;
                    }
                }
            }
            else haveData = false;

            //데이터 파일들이 존재한다면
            if (haveData)
            {
                isLoad = true;
                //데이터 파일 읽기
                for (int i = 0; i < dataFiles.Count; i++)
                {
                    gameData.Add(new string(File.ReadAllText(filePath + "/gameData" + i.ToString() + ".json")));
                }

                //읽어온 내용 업데이트
                //Item Data
                Item.Instance.equipItems = JsonConvert.DeserializeObject<List<EquipItem>>(gameData[0]);
                Item.Instance.enforceInit = JsonConvert.DeserializeObject<List<EquipItem>>(gameData[1]);
                Item.Instance.consumItems = JsonConvert.DeserializeObject<List<ConsumItem>>(gameData[2]);
                Item.Instance.fishList = JsonConvert.DeserializeObject<List<Fish>>(gameData[3]);
                //Inventory Data
                Inventory.Instance.ownEquipCount = JsonConvert.DeserializeObject<List<int>>(gameData[4]);
                Inventory.Instance.ownConsumCount = JsonConvert.DeserializeObject<List<int>>(gameData[5]);
                Inventory.Instance.ownFishCount = JsonConvert.DeserializeObject<List<int>>(gameData[6]);
                //Player
                Player.player = JsonConvert.DeserializeObject<Player>(gameData[7]);
                //Quest
                QuestList.questList = JsonConvert.DeserializeObject<List<Quest.Quest>>(gameData[8]);
                QuestBool.instance = JsonConvert.DeserializeObject<QuestBool>(gameData[9]);
            }
            else loadfail = true;
        }

        //저장 및 불러오기 페이지
        public void ShowSaveLoadPage()
        {
            Console.Clear();
            if (isWrong)
                Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.\n");
            if (isSave)
                Color.ChangeTextColor(Colors.BLUE, "", "데이터를 저장했습니다.\n");
            if (isLoad)
                Color.ChangeTextColor(Colors.BLUE, "", "데이터를 불러왔습니다.\n");
            if (loadfail)
                Color.ChangeTextColor(Colors.RED, "", "데이터가 존재하지 않습니다.\n");

            Color.ChangeTextColor(Colors.YELLOW, "", "저장 및 불러오기\n");
            Console.WriteLine("게임의 진행 상황을 저장하거나 불러올 수 있습니다.");
            Console.WriteLine();
            Color.ChangeTextColor(Colors.YELLOW, "", "1. ", "저장하기\n");
            Color.ChangeTextColor(Colors.YELLOW, "", "2. ", "불러오기\n");
            Color.ChangeTextColor(Colors.RED, "", "0. ", "나가기\n");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Color.ChangeTextColor(Colors.YELLOW, "", ">>");
            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                default:
                    isWrong = true;
                    isSave = false;
                    isLoad = false;
                    loadfail = false;
                    //Program.scene = Scene.saveLoad;
                    break;

                //나가기
                case "0":
                    isWrong = false;
                    isSave = false;
                    isLoad = false;
                    loadfail = false;
                    Program.scene = Scene.mainScene;
                    break;

                //세이브
                case "1":
                    isWrong = false;
                    isSave = true;
                    isLoad = false;
                    loadfail = false;
                    Save();
                    //Program.scene = Scene.saveLoad;
                    break;

                //로드
                case "2":
                    isWrong = false;
                    isSave = false;
                    Load();
                    //Program.scene = Scene.saveLoad;
                    break;
            }
        }
    }
}
