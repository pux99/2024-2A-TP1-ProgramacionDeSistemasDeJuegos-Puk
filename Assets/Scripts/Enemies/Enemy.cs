using System;
using System.Collections;
using HealthSystem;
using Pool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour,IPrototype
    {
        [SerializeField] public NavMeshAgent agent;
        [SerializeField] private UHealth health;
        private GameObject _townCenter;
        private ITargetGiverService _targetGiverService;
        private Building _building;
        public event Action OnSpawn = delegate { };
        public event Action OnDeath = delegate { };
        public event Action<GameObject> OnDeath1 = delegate { };
    
        private void Reset() => FetchComponents();

        private void Awake()
        {
            FetchComponents();
            _targetGiverService = ServiceLocator.Instance.GetService<ITargetGiverService>();
            health.OnDead += Die;
            _targetGiverService.NoMoreBuildings += Die;
        }

        private void FetchComponents()
        {
            agent ??= GetComponent<NavMeshAgent>();
            health ??= GetComponent<UHealth>();
        }


        private void OnEnable()
        {
            //Is this necessary?? We're like, searching for it from every enemy D:
            if (!_townCenter)
                //_townCenter = GameObject.FindGameObjectWithTag("TownCenter");//solo Busca Cuando se Crea
            {
                if (_targetGiverService.tryToGet(out _townCenter))
                {
                    _building= _townCenter.GetComponent<Building>();
                    _building.OnDestoy += GetNewTarget;
                }
            }
            
            if (_townCenter == null)
            {
                //Debug.LogError($"{name}: Found no {nameof(_townCenter)}!! :(");
                return;
            }

            var destination = _townCenter.transform.position;
            destination.y = transform.position.y;
            StartCoroutine(Wait2FramesAndSetDestination(destination));
            StartCoroutine(AlertSpawn());
        }

        private IEnumerator Wait2FramesAndSetDestination(Vector3 destination)
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
                if(_townCenter.TryGetComponent(out UHealth uHealth))
                    uHealth.TakeDamage(50);
                health.TakeDamage(100);
            }
        }

        private void Die()
        {
            OnDeath();
            OnDeath1(this.gameObject);
            gameObject.SetActive(false);

        }

        private void GetNewTarget()
        {
            _building.OnDestoy -= GetNewTarget;
            if (_targetGiverService.tryToGet(out _townCenter)&& gameObject.activeSelf==true )// nose porque se activava La corutina sin el segundo chequeo
            {
                _building = _townCenter.GetComponent<Building>();
                _building.OnDestoy += GetNewTarget;
                var destination = _townCenter.transform.position;
                destination.y = transform.position.y;
                StartCoroutine(Wait2FramesAndSetDestination(destination));
            }
            else
            {
                Die();
            }
        }

        public GameObject Clone(Vector3 Pos,Quaternion rot)
        {
            return Instantiate(this.gameObject,Pos,rot);
        }
    } 
}

