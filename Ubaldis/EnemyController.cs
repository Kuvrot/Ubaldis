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
        public int damage = 30;

        public TransformComponent target;
        public Prefab projectile;
        public ParticleSystemComponent muzzleFlash;
        public double stoppingDistance = 6;


        public Prefab deathVFX;

        private float _clock = 2f;
        private float _distance = 0f;

        public bool dead = false;

        public override void Start()
        {
            GameManager.EnemiesList.Add(Entity);
            target = GameManager.enemyTarget;
            muzzleFlash.ParticleSystem.Stop();

            stoppingDistance = (new Random().NextDouble() * 4) + 1; //Stopping distance will be randomized
        }

        public override void Update()
        {
            if (target == null)
            {
                target = GameManager.enemyTarget;
            }

            //_distance = Vector3.Distance(target.Position , Entity.Transform.Position);
            if (Entity.Transform.Position.Y <= stoppingDistance)
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

            if (health <= 0)
            {
                Death();
            }

        }

        private void Shoot ()
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

            GameManager.wall.Entity.Get<WallController>().GetHit(damage);

            muzzleFlash.ParticleSystem.ResetSimulation(); //Muzzle flash resets
        }

        private void Death ()
        {
            var vfx = deathVFX.Instantiate();

            foreach (var entity in vfx)
            {
                if (entity.Transform != null)
                {
                    entity.Transform.Position = Entity.Transform.Position;
                }

                Entity.Scene.Entities.Add(entity);
            }

            Entity.Scene.Entities.Remove(Entity);
            Entity.Dispose();
            dead = true;
        }
        public void GetDamage (float amount)
        {
            health -= amount;
        }
    }
}
