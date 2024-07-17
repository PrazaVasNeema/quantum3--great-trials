using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Platformer
{
    public unsafe class FallDamageSystem : SystemMainThreadFilter<FallDamageSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform3D;
            public CharacterController3D* CharacterController;
            public PlayerPlatformController* PlatformController;
            public HealthComponent* HealthComponent;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            Log.Info(filter.HealthComponent->currentHealth);
        }

       }
}
