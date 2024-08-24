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
using Stride.Core;
using SharpDX.MediaFoundation;

namespace Ubaldis
{
    public class GameManager : SyncScript
    {
        public static int money = 0;
        public static float deltaTime;
        public static EntityComponent wall;
        public EntityComponent _wall;

        public static TransformComponent enemyTarget; //The target that the enemies will move towards.
        public TransformComponent _enemyTarget;
        public static UIPage UI;
        public UIComponent _UI;
        public float spawnRate = 1;
        [DataMember]
        public List<Prefab> Enemies {get; set;} = new List<Prefab>() ;
        
        [DataMember]
        public List<Entity> SpawnPositions {get; set;} = new List<Entity>();

        public static int health = 3000;

        public static List<Entity> EnemiesList { get; set; } = new List<Entity>();
  
        private float _clock = 0;
        public override void Start()
        {
            wall = _wall;
            UI = _UI.Page;
            _clock = spawnRate;
            enemyTarget = _enemyTarget;
        }

        public override void Update()
        {
            deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;

            var h = UI.RootElement.FindName("Health") as Slider;
            h.Value = health;

            var ht = UI.RootElement.FindName("healthText") as TextBlock;
            ht.Text = "3000/" + health.ToString();

            var m = UI.RootElement.FindName("scoreText") as TextBlock;
            m.Text = "$" + money.ToString();
            
            SpawningEnemies();
        }

        private void SpawningEnemies()
        {
            if (_clock <= 0)
            {
                //Spawn enemy
                Random random = new Random();
                var e = Enemies[random.Next(0 , Enemies.Count)].Instantiate();

                foreach (var entity in e)
                {
                    if (entity.Transform != null)
                    {
                        entity.Transform.Position = SpawnPositions[random.Next(0 , SpawnPositions.Count)].Transform.Position;
                        switch (random.Next(0 , 3)) {
                            case 0:
                                entity.Transform.Position.X -= 2; break;
                            case 1:
                                entity.Transform.Position.X += 2; break;
                        }
                    }

                    Entity.Scene.Entities.Add(entity);
                }
                _clock = spawnRate;
            }

            _clock -= 1 * deltaTime;
        }
    }
}
