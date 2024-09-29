using UnityEngine;
namespace Pool
{
    public interface IPrototype
    {
        public GameObject Clone(Vector3 position,Quaternion rot);
    }
}