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
        //Stats
        public float health = 100;
        public float speed = 0.5f;
        public float fireRate= 2f;
        public float damage = 30;

        public TransformComponent target;
        public Prefab projectile;
        public ParticleSystemComponent muzzleFlash;
        public float stoppingDistance = 6;

        
        private float _clock = 2f;
        private float _distance = 0f;
        public override void Start()
        {
             muzzleFlash.ParticleSystem.Stop();
        }

        public override void Update()
        {
            _distance = Vector3.Distance(target.Position , Entity.Transform.Position);
                
            if (_distance <= stoppingDistance)
            {
                if (_clock <= 0)
                {
                    Shoot();
                    _clock = fireRate;
                }
                else
                {
                    _clock -= 1 * GameManager.deltaTime;
                }
            }
            else
            {
                Entity.Transform.Position.Y -= speed * GameManager.deltaTime;
            }
        }

        public void Shoot ()
        {
            muzzleFlash.ParticleSystem.Play(); //Muzzle flash plays

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

            muzzleFlash.ParticleSystem.ResetSimulation(); //Muzzle flash resets
        }
    }
}
