using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI.Controls;
using System.Runtime.CompilerServices;

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
        public float spawnRate = 1;

        public static int health = 3000;


        private float _clock = 0;
        public override void Start()
        {
            wall = _wall;
            UI = _UI.Page;
            _clock = spawnRate;
        }

        public override void Update()
        {
            deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;

            var h = UI.RootElement.FindName("Health") as Slider;
            h.Value = health;

            var ht = UI.RootElement.FindName("healthText") as TextBlock;
            ht.Text = "3000/" + health.ToString();

            SpawningEnemies();
        }

        private void SpawningEnemies()
        {
            if (_clock <= 0)
            {
                //Spawn enemy
                Random random = new Random();
                var e = enemies[random.Next(0 , enemies.Length)].Instantiate();

                foreach (var entity in e)
                {
                    if (entity.Transform != null)
                    {
                        entity.Transform.Position = spawnPositions[random.Next(0 , spawnPositions.Length)].Entity.Transform.Position;
                        entity.Transform.Position.Z -= 10;
                    }

                    Entity.Scene.Entities.Add(entity);
                }
                _clock = spawnRate;
            }

            _clock -= 1 * deltaTime;
        }
    }
}
