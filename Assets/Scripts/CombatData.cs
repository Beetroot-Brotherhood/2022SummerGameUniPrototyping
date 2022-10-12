using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    [System.Serializable]
    public class Statistics {
        public ArmourStatistics[] armours;
    }

    [System.Serializable]
    public class ArmourStatistics{
        public int health;
        public ShieldType shieldTypes;
    }

    [System.Serializable]
    public class ProjectileStatistics {
        public int damage;
        public float fireRate;
        public DamageType damageType;
        [Range(0.001f, 80f)]
        public float angle;
        public float mass;
        public float lifeTime;
        public float distance;
        public float speed;
        public float maxSpeed;
        public float angleBetweenObjects;
    }

    [System.Serializable]
    public class AttackStatistics {
        public int damage;
        public DamageType damageType;
    }

    public static class CombatData
    {
        public static IDictionary<DamageType, ShieldType> superEffectiveDamageShieldMap { get; } = new Dictionary<DamageType, ShieldType>() {
            {DamageType.Electric, ShieldType.EnergyField},
            {DamageType.Plasma, ShieldType.ChestPlate},
            {DamageType.Metal, ShieldType.HazardHelmet}
        };

        public static IDictionary<DamageType, ShieldType> notVeryEffectiveDamageShieldMap { get; } = new Dictionary<DamageType, ShieldType>() {
            {DamageType.Electric, ShieldType.ChestPlate},
            {DamageType.Plasma, ShieldType.HazardHelmet},
            {DamageType.Metal, ShieldType.EnergyField}
        };

        public static IDictionary<DamageType, ShieldType> ineffectiveDamageShieldMap { get; } = new Dictionary<DamageType, ShieldType>() {
            {DamageType.Electric, ShieldType.HazardHelmet},
            {DamageType.Plasma, ShieldType.EnergyField},
            {DamageType.Metal, ShieldType.ChestPlate}
        };

        public static IDictionary<DamageType, ShieldType>[] damageShieldMaps { get; } = new IDictionary<DamageType, ShieldType>[3] {
            superEffectiveDamageShieldMap,
            notVeryEffectiveDamageShieldMap,
            ineffectiveDamageShieldMap
        };

        /// <summary>
        /// Used to divide damage by a factor depending on the armour effectiveness
        /// </summary>
        /// <typeparam name="int">For loop index key that will refer to a divider</typeparam>
        /// <typeparam name="int">the divider value used to divide the damage received</typeparam>
        public static IDictionary<int, int> damageEffectivenessDividerMap { get; } = new Dictionary<int, int>() {
            {0, 1},
            {1, 2},
            {2, 0}
        };
    }

    public enum ShieldType{
        None,
        EnergyField,
        ChestPlate,
        HazardHelmet
    }

    public enum DamageType{
        None,
        Electric,
        Plasma,
        Metal
    }
}