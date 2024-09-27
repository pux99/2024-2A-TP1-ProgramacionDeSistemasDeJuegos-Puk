using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public NavMeshAgent agent;
        private GameObject _townCenter;
        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };
        public event Action<GameObject> OnDeath1 = delegate { };
    
        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();
    
        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            //Is this necessary?? We're like, searching for it from every enemy D:
            if(!_townCenter)
                _townCenter = GameObject.FindGameObjectWithTag("TownCenter");//solo Busca Cuando se Crea
            if (_townCenter == null)
            {
                Debug.LogError($"{name}: Found no {nameof(_townCenter)}!! :(");
                return;
            }

            var destination = _townCenter.transform.position;
            destination.y = transform.position.y;
            StartCoroutine(Waiting(destination));
            StartCoroutine(AlertSpawn());
        }

        private IEnumerator Waiting(Vector3 destination)
        {
            yield return 2;
            agent.SetDestination(destination);
        }

        private IEnumerator AlertSpawn()
        {
            //Waiting one frame because event subscribers could run their onEnable after us.
            yield return null;
            OnSpawn();
        }

        private void Update()
        {
            if (agent.hasPath
                && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                Debug.Log($"{name}: I'll die for my people!");
                Die();
            }
        }

        private void Die()
        {
            OnDeath();
            OnDeath1(this.gameObject);
            //agent.ResetPath();
            //agent.isStopped = true;
            //agent.updatePosition = false;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
