using Photon.Deterministic;

namespace Quantum.Platformer
{
    unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerDataSet
    {
        public void OnPlayerDataSet(Frame frame, PlayerRef player)
        {
            var data = frame.GetPlayerData(player);

            var prototype = frame.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);

            // Create a new entity for the player based on the prototype.
            var entity = frame.Create(prototype);

            // Create a PlayerLink component. Initialize it with the player. Add the component to the player entity.
            var playerLink = new PlayerLink()
            {
                Player = player,
            };

            var healthComponent = new HealthComponent()
            {
                currentHealth = 20,
                maxHealth = 20,
            };

            var fallDamageComponent = new FallDamageComponent()
            {
                StartingHeight = 10 + player,
                IsFalling = false,
                PreviousVerticalVelocity = FP._0,
            };

            frame.Add(entity, playerLink);
            frame.Add(entity, healthComponent);
            frame.Add(entity, fallDamageComponent);

            // Offset the instantiated object in the world, based in its ID.
            if (frame.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
            {
                transform->Position.X = 0 + player;
                transform->Position.Y = 10 + player;

            }
        }
    }
}
