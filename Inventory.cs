using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Inventory
    {
        List<EquipItem> equipInven = new List<EquipItem>();
        List<ConsumItem> consumInven = new List<ConsumItem>();

        public void ShowInvenPage()
        {
            Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리", "\n");
        }
    }
}
