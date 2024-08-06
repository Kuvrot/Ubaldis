using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Particles.Components;

namespace Ubaldis
{
    public class EnemyController : SyncScript
    {
       public float speed = 0.5f;
        public TransformComponent target;
        public EntityComponent projectile;
        public ParticleSystemComponent particleSystem;
        public float stoppingDistance;

        public override void Start()
        {
             particleSystem.ParticleSystem.Stop();
        }

        public override void Update()
        {
            stoppingDistance = Vector3.Distance(target.Position , Entity.Transform.Position);
                
                if (stoppingDistance <= 4)
                {
                    particleSystem.ParticleSystem.Play();
                }
                else
                {
                    Entity.Transform.Position.Y -= speed * (float)Game.UpdateTime.Elapsed.TotalSeconds;
                }
        }
    }
}
