using System;
using System.Collections;
using HealthSystem;
using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private UHealth health;
        [SerializeField] private int respawnTimer;
        private ITargetService _targetService;
        private MeshRenderer _meshRenderer;
        private IEnumerator _respawn;
        public Action OnDestoy;
    
        private void Start()
        {
            health.OnDead += Destroyed;
            _meshRenderer = GetComponent<MeshRenderer>();
            _targetService = ServiceLocator.Instance.GetService<ITargetService>();
            _targetService.AddTarget(this.gameObject);
            _targetService.NoMoreBuildings += StopRespawn;
        }
        void Destroyed()
        {
            OnDestoy?.Invoke();
            _meshRenderer.enabled = false;
            _targetService.RemoveTarget(this.gameObject);
            _respawn = Respawn(respawnTimer);
            StartCoroutine(_respawn);
        }
        IEnumerator Respawn(int timer)
        {
            yield return new WaitForSeconds(timer);
            gameObject.SetActive(true);
            health.FullHeal();
            _meshRenderer.enabled = true;
            _targetService.AddTarget(this.gameObject);
        }
        void StopRespawn() => StopCoroutine(_respawn);
    
    }
}
