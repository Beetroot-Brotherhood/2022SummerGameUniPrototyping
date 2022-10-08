using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Latch.Combat {
    public class ProjectileInfo : MonoBehaviour
    {
        public ProjectileStatistics projectileStatistics;

        [HideInInspector]
        public float currentLifeTimer;
    }
}