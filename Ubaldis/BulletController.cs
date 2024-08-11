/*
 This script only makes the bullet travel in positive or negative directions in the Y axis.
 How bullets work: 
                    The bullet only works as visual feedback, if you disable all the bullets the game should be working as usual.
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace Ubaldis
{
    public class BulletController : SyncScript
    {
        public float speed = 50;
        public float lifeTime = 0.5f;
        public TransformComponent target;


        private float _clock;

        public override void Start()
        {
            _clock = lifeTime;
        }

        public override void Update()
        {
            if (target != null)
            {
                Vector3 direction = (target.Position - Entity.Transform.Position);
                direction.Normalize();
                Entity.Transform.Position += direction;
            }
            else
            {
                Entity.Transform.Position.Y -= speed * GameManager.deltaTime;
            }

            if (_clock <= 0)
            {
                Entity.Scene.Entities.Remove(Entity);
                Entity.Dispose();
            }

            _clock -= 1 * GameManager.deltaTime;
        }
    }
}
