using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetGiverService: MonoBehaviour,ITargetGiverService
 {
     private void Awake()
     {
         ServiceLocator serviceLocator = ServiceLocator.Instance;
         serviceLocator.RegisterService<ITargetGiverService>(this);
     }

     [SerializeField] private List<GameObject> targets;
     public GameObject GetTarget()
     {
         return targets[Random.Range(0, targets.Count)];
     }

     public bool TryToGet(out GameObject target)
     {
         if (targets.Count > 0)
         {
             target=targets[Random.Range(0, targets.Count)];
             return true;
         }

         target = default;
         NoMoreBuildings?.Invoke();
         return false;
     }

     public void RemoveTarget(GameObject target)
     {
         targets.Remove(target);
     }

     public void AddTarget(GameObject target)
     {
         targets.Add(target);
     }

     public event Action NoMoreBuildings;
 }
