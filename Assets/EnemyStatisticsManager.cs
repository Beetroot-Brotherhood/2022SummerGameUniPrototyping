using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

[System.Serializable]
public class EnemyStatistics {
    public float stagger;
}

public class EnemyStatisticsManager : MonoBehaviour
{

    [Header("Statistics")]
    public EnemyStatistics defaultStatistics;
    public EnemyStatistics currentStatistics;
    public EnemyStatistics maxStatistics;

    [Header("General")]
    public YbotTestController2 thisYbotTestController2;

    // Start is called before the first frame update
    void Start()
    {
        ResetCurrentStatistics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseStagger(float staggerDamage) {
        currentStatistics.stagger += staggerDamage;
        if (currentStatistics.stagger >= maxStatistics.stagger) {
            thisYbotTestController2.YbotStagger();
        }
    }

    public void ResetCurrentStatistics() {
        currentStatistics = defaultStatistics.DeepClone();
    }

    public void ResetStagger() {
        thisYbotTestController2.staggered = false;
        currentStatistics.stagger = defaultStatistics.stagger;
    }
}
