using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

[System.Serializable]
public class Statistics {
    public WeaponStatistics weapon;
    public ArmourStatistics[] armour;
}

[System.Serializable]
public class WeaponStatistics{
    public int damage;
    public DamageType damageTypes;
}

[System.Serializable]
public class ArmourStatistics{
    public int health;
    public ShieldType[] shieldTypes;
}

public class CombatManager : MonoBehaviour
{
    public Statistics defaultStats;
    public Statistics currentStats;
    public Statistics maxStats;

    // Start is called before the first frame update
    void Start()
    {
        ResetStatistics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetStatistics() {
        currentStats = defaultStats.DeepClone();
    }
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
