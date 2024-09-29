using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public struct RandomContainer<T>
    {
        [SerializeField] private T[] candidates;

        public bool TryGetRandom(out T result)
        {
            if (candidates.Length < 1)
            {
                result = default;
                return false;
            }
            result = candidates[UnityEngine.Random.Range(0, candidates.Length)];
            return result != null;
        }
    }
}