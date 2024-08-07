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
    public class AllyController : SyncScript
    {
        public int allyType = 0;
        public float health = 100;
        public float fireRate = 2;
        public EntityComponent target;

        private float clock = 0;

        public override void Start()
        {
            clock = fireRate;
        }

        public override void Update()
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
            Quaternion rotation = Quaternion.RotationZ(angle + 180);

            // Set the entity's rotation to the calculated rotation
            Entity.Transform.Rotation = rotation;
        }
    }
}
