using System.Diagnostics;

namespace SpartaTextRPG
{
    internal class Program
    {
        public static Scene scene;
        static void Main(string[] args)
        {
            Title.instance.StartTitle();
            while (true)
            {
                switch (scene)
                {
                    case Scene.mainScene:
                        GameManager.instance.MainGameScene();
                        break;
                    case Scene.playerState:
                        State.instance.Status();
                        break;

                    case Scene.inventory:

                        break;
                    case Scene.shop:
                        Shop.Instance.ShowShopPage();
                        break;
                    case Scene.dungeon:

                        break;
                    case Scene.rest:
                        GameManager.instance.Rest();
                        break;
                    case Scene.fishing:
                        Fishing.instance.StartFishing();
                        break;
                }
            }
        }
    }
}
