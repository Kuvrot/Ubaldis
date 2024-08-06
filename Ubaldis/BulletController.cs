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
        public bool direction = false; //false for negative, true for positive
        public float speed = 50; 
        
        public override void Update()
        {
            if (direction)
            {
                Entity.Transform.Position.Y += speed * (float)Game.UpdateTime.Elapsed.TotalSeconds;
            }
            else
            {
                Entity.Transform.Position.Y -= speed * (float)Game.UpdateTime.Elapsed.TotalSeconds;
            }
        }
    }
}
