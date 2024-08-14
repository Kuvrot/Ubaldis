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
    public class EnemyDeath : SyncScript
    {
        public float lifeTime = 0.5f;

        private float _clock = 0;

        public override void Update()
        {
            if (_clock == lifeTime)
            {
                Entity.Scene.Entities.Remove(Entity);
                Entity.Dispose();
            }

            _clock += 1 * GameManager.deltaTime;
        }
    }
}
