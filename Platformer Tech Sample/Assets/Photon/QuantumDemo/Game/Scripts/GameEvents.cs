using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class GameEvents
{
    public static System.Action<EntityView> OnEntityViewCreated;
    public static System.Action OnEntityViewDestroyed;
}
