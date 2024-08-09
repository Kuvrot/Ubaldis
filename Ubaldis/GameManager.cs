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
    public class GameManager : SyncScript
    {

        public static float deltaTime;

        public override void Start()
        {
            // Initialization of the script.
        }

        public override void Update()
        {
            deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
        }
    }
}
