using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HealthSystem
{
    public class UHealth : MonoBehaviour
    {
        [SerializeField] private int maxHp;
        [SerializeField] private int currentHp;
        private Health _health ;
        public Action OnDead;

        private void Awake()
        {
            _health = new Health(maxHp,currentHp);
            _health.OnHeal +=LifeChange ;
            _health.OnDamage += LifeChange;
            _health.OnDeath += Death;
        }

        public void Heal(int heal)
        {
            _health.Heal(heal);
        }
        public void FullHeal()
        {
            _health.Heal(maxHp);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
        private void LifeChange(int oldHp, int newHp)
        {
            currentHp = newHp;
        }

        void Death()
        {
            OnDead?.Invoke();
        }
    }
}
