using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass.Quest
{
    public partial class QuestList
    {
        //퀘스트 완료 조건
        public void CheckCompQuest()
        {
            //퀘스트 완료 조건 부탁드립니다...
            //조건에 따라 questList.isComplite = true

            // 1. 1차 전직
            if (questList[0].isOngoing && (int)Player.player.job != 0)
            {
                questList[0].isComplete = true;
            }
            // 2. 2차 전직
            if (questList[1].isOngoing && 4 < (int)Player.player.job)
            {
                questList[1].isComplete = true;
            }
            // 3. 장착 해보기
            if (questList[2].isOngoing && (Player.player.weapon != 0 || Player.player.armor != 0))
            {
                questList[2].isComplete = true;
            }
            // 4. 던전 입장하기
            // bool 하나 추가해서 던전 쪽에서 값 변경해줘야 할 듯
            // 5. 휴식하기
            // 4와 동일
            // 6. 던전 스테이지 2 클리어하기
            // 4와 동일
            // 7. 포션 사용하기 
            // 4와 동일 (기능 없음)
        }
    }
}
