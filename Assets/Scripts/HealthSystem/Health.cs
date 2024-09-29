using System;

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
        
        public Health(int maxHp, int hp)
        {
            MaxHp = maxHp;
            Hp = hp;
        }

        public void Heal(int heal)
        {
            if (heal > 0)
            {
                int oldValue = Hp;
                Hp += heal;
                if (Hp > MaxHp)
                    Hp = MaxHp;
                OnHeal.Invoke(oldValue, Hp);
            }
            else
            {
                Console.WriteLine("Heal do not accept negative numbers");
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
                Console.WriteLine("Heal do not accept negative numbers");
            }
        }

    };
}
