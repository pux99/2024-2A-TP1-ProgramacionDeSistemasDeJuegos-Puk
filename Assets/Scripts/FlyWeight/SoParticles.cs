using UnityEngine;
using Enemies;
namespace FlyWeight
{
    [CreateAssetMenu(fileName = "Particles", menuName = "ScriptableObjects/Particles")]
    public class SoParticles : ScriptableObject
    {
        public RandomContainer<ParticleSystem> particles;
    }
}
