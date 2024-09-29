using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Pool
{
    public class GoblinPool : MonoBehaviour,IPool<GameObject>
    {
        [SerializeField] private GameObject goblinPrefab;
        private readonly Queue<GameObject> _goblinQueue =new Queue<GameObject>();
        private IPrototype _prototype;
        public static GoblinPool Instance { get; private set; }
        private void Awake() 
        { 
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }
            _prototype=goblinPrefab.GetComponent<IPrototype>();
        }
        

        public GameObject GetElement(Vector3 pos,Quaternion rot)
        {
            if (_goblinQueue.Count > 0)
            {
                Debug.Log("New Goblin Respawn "+pos);
                GameObject reuseGoblin= _goblinQueue.Dequeue();
                reuseGoblin.SetActive(true);
                reuseGoblin.GetComponent<Enemy>().agent.Warp(pos);
                reuseGoblin.transform.rotation = rot;
                return reuseGoblin;
            }
            return CreateNewElement(pos,rot);
        }

        private GameObject CreateNewElement(Vector3 pos,Quaternion rot)
        {
            Debug.Log("New Goblin Born");
            GameObject newGoblin = _prototype.Clone(pos,rot);
            newGoblin.GetComponent<Enemy>().ReturnToPool += ReceiveElement;
            newGoblin.transform.parent = transform;
            return newGoblin;
        }

        public void ReceiveElement(GameObject element)
        {
            _goblinQueue.Enqueue(element);
        }
    }
}
