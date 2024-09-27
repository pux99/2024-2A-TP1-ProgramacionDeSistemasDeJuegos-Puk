using System;
using UnityEngine;

namespace HealthSystem
{
    [Serializable]
    public class Health
    {
        public int MaxHp;
        public int Hp;
        public Action OnDeath;
        public Action<int, int> OnDamage;
        public Action<int, int> OnHeal;

        public Health()
        {
            
        }
        public Health(int maxHp, int hp)
        {
            MaxHp = maxHp;
            Hp = hp;
        }

        public void Heal(int Heal)
        {
            if (Heal > 0)
            {
                int oldValue = Hp;
                Hp += Heal;
                if (Hp > MaxHp)
                    Hp = MaxHp;
                OnHeal.Invoke(oldValue, Hp);
            }
            else
            {
                Debug.LogWarning("Heal do not accept negative numbers");
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                int oldValue = Hp;
                Hp -= damage;
                OnDamage(oldValue,Hp);
                if (Hp <= 0)
                    OnDeath?.Invoke();

            }
            else
            {
                Debug.LogWarning("Heal do not accept negative numbers");
            }
        }

    };
}
