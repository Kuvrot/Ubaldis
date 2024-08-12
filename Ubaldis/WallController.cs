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

    /*
     This script makes the wall get a hit after being shot, and increase the intensity for a few milliseconds.
     Most part of the code is just to make the wall shine after beign hit to give feedback to the player.

     The script must be in a parent entity with sprite children.
     */
    public class WallController : SyncScript
    {
        public List<Entity> sprites;
        public bool hit = false;

        private int damage = 15;
        private float _clock = 0.1f;


        public override void Start()
        {
            sprites = Entity.GetChildren().ToList();
        }
        
        public override void Update()
        {
            GetDamage();
        }

        //This method will be called to make the hit boolean true,
        //so the process of increasing the intensity of the sprites begin.
        public void GetHit (int damage)
        {
            this.damage = damage;
            hit = true;
        }

        //After GetHit is called, hit boolean will be true.
        private void GetDamage ()
        {
            if (hit)
            {

                //Here a simple clock is beign used.
                //A foreach is used to take and manipulate all the children sprites.
                if (_clock > 0)
                {
                    foreach (var entity in sprites)
                    {
                        entity.Get<SpriteComponent>().Intensity = 2;
                    }
                }
                else
                {
                    foreach (var entity in sprites)
                    {
                        entity.Get<SpriteComponent>().Intensity = 1;
                    }

                    _clock = 0.1f;
                    GameManager.health -= damage;
                    hit = false;
                }
                _clock -= 1 * GameManager.deltaTime;
            }
        }
    }
}
