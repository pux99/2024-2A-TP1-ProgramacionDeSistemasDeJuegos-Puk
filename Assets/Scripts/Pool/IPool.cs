using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public interface IPool<T>
    {
        public T GetElement(Vector3 position, Quaternion rotation);
        public void ReceiveElement(T element);

    }
}
