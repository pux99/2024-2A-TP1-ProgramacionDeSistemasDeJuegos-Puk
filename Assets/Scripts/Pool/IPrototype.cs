using UnityEditor;
using UnityEngine;
namespace Pool
{
    public interface IPrototype
    {
        public GameObject Clone(Vector3 Position,Quaternion rot);
    }
}