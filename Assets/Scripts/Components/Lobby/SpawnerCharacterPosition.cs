using System;
using UnityEngine;

namespace Components.Lobby
{
    [Serializable]
    public class SpawnerCharacterPosition
    {
        public Transform Position;
        public bool IsFree = true;
    }

}