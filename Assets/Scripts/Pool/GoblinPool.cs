using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Pool
{
    public class GoblinPool : MonoBehaviour,IPool<GameObject>
    {
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
        }
        private Queue<GameObject> _goblinQueue =new Queue<GameObject>();
        [SerializeField]private GameObject goblinPrefab;

        public GameObject GetElement(Vector3 pos,Quaternion rot)
        {
            if (_goblinQueue.Count > 0)
            {
                //Debug.Log("New Goblin Respawn "+pos);
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
            //Debug.Log("New Goblin Born");
            GameObject newGoblin= Instantiate(goblinPrefab,pos,rot);
            newGoblin.GetComponent<Enemy>().OnDeath1 += ReceiveElement;
            return newGoblin;
        }

        public void ReceiveElement(GameObject element)
        {
            _goblinQueue.Enqueue(element);
        }
    }
}
