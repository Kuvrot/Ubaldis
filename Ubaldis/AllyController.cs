using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Particles.Components;
using System.Windows.Media.Animation;

namespace Ubaldis
{
    public class AllyController : SyncScript
    {
        public int allyType = 0;
        public float health = 100;
        public float fireRate = 2;
        public float damage = 50;
        public EntityComponent target;
        public Prefab projectile;
        public ParticleSystemComponent muzzleFlash;

        private float _clock = 0;

        public override void Start()
        {
            _clock = fireRate;
        }

        public override void Update()
        {
            LookTarget();

            if (_clock <= 0)
            {
                Shoot();
                _clock = fireRate;
            }
            _clock -= 1 * GameManager.deltaTime;
        }

        public void LookTarget()
        {
            // Get the positions of the current entity and the target
            var currentPosition = Entity.Transform.Position;
            var targetPosition = target.Entity.Transform.Position;

            // Calculate the direction vector on the X-Y plane
            Vector2 direction = new Vector2(targetPosition.X - currentPosition.X, targetPosition.Y - currentPosition.Y);

            // Normalize the direction vector
            direction.Normalize();

            // Calculate the angle in radians using the arctangent function
            float angle = MathF.Atan2(direction.Y, direction.X);

            // Create a rotation quaternion for the Z-axis only
            Quaternion rotation = Quaternion.RotationZ(angle);

            // Set the entity's rotation to the calculated rotation
            Entity.Transform.Rotation = rotation;
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
