using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Particles.Components;
using SharpFont.PostScript;

namespace Ubaldis
{
    public class EnemyController : SyncScript
    {
       public float speed = 0.5f;
        public TransformComponent target;
        public Prefab projectile;
        public ParticleSystemComponent particleSystem;
        public float stoppingDistance;

        float timer = 2f;
        float clock = 2f;
        public override void Start()
        {
             particleSystem.ParticleSystem.Stop();
        }
    
        public override void Update()
        {
            stoppingDistance = Vector3.Distance(target.Position , Entity.Transform.Position);
                
            if (stoppingDistance <= 4)
            {
                if (clock <= 0)
                {
                    particleSystem.ParticleSystem.Play(); //Muzzle flash plays

                    //Projectile instance
                    var p = projectile.Instantiate();

                    foreach (var entity in p)
                    {
                        if (entity.Transform != null)
                        {
                            entity.Transform.Position = Entity.Transform.Position;
                            entity.Transform.Position.Z -= 10;
                        }

                        Entity.Scene.Entities.Add(entity);
                    }

                    particleSystem.ParticleSystem.ResetSimulation(); //Muzzle flash plays

                    clock = timer;
                }
                else
                {
                    clock -= 1 * (float)Game.UpdateTime.Elapsed.TotalSeconds;
                }
            }
            else
            {
                Entity.Transform.Position.Y -= speed * (float)Game.UpdateTime.Elapsed.TotalSeconds;
            }
        }
    }
}
