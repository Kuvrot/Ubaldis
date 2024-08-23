using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI.Controls;

namespace Ubaldis
{
    public class GameManager : SyncScript
    {
        public static float deltaTime;
        public static EntityComponent wall;
        public EntityComponent _wall;

        public static UIPage UI;
        public UIComponent _UI;

        public Prefab[] enemies;
        public TransformComponent[] spawnPositions;

        public static int health = 3000;

        public override void Start()
        {
            wall = _wall;
            UI = _UI.Page;
        }

        public override void Update()
        {
            deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;

            var h = UI.RootElement.FindName("Health") as Slider;
            h.Value = health;

            var ht = UI.RootElement.FindName("healthText") as TextBlock;
            ht.Text = "3000/" + health.ToString();
        }
    }
}
