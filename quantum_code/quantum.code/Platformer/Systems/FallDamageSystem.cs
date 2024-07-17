using Photon.Deterministic;
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
            public FallDamageComponent* FallDamageComponent;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            //Log.Info(filter.HealthComponent->currentHealth);

            var verticalVelocity = filter.CharacterController->Velocity.Y;
            //Log.Info(verticalVelocity);

            if (filter.FallDamageComponent->IsFalling)
            {
                if (filter.CharacterController->Grounded)
                {
                    var fallDistance = filter.FallDamageComponent->StartingHeight - filter.Transform3D->Position.Y;
                    ApplyFallDamage(ref filter, fallDistance);
                    filter.FallDamageComponent->IsFalling = false;
                    Log.Info(filter.HealthComponent->currentHealth);
                }
            }
            else if (!filter.CharacterController->Grounded && verticalVelocity < FP._0)
            {
                Log.Info(verticalVelocity);

                filter.FallDamageComponent->StartingHeight = filter.Transform3D->Position.Y;
                filter.FallDamageComponent->IsFalling= true;
            }

            if (filter.HealthComponent->currentHealth == 0)
            {
                ResetPlayer(ref filter);
            }

        }

        private void ApplyFallDamage(ref Filter filter, FP fallDistance)
        {
            var finalDamage = FPMath.Abs(fallDistance - 1);
            filter.HealthComponent->currentHealth = FPMath.Max(filter.HealthComponent->currentHealth - finalDamage, FP._0);
        }

        private void ResetPlayer(ref Filter filter)
        {
            filter.Transform3D->Position = new FPVector3(FP._0, 10, FP._0);
            filter.HealthComponent->currentHealth = filter.HealthComponent->maxHealth;
        }

    }
}
