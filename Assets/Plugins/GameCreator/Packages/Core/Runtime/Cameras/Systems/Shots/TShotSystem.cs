using System;
using UnityEngine;

namespace GameCreator.Runtime.Cameras
{
    [Serializable]
    public abstract class TShotSystem : IShotSystem
    {
        protected static readonly Color GIZMOS_COLOR_ACTIVE = new Color(1f, 0f, 0f, 0.9f);
        protected static readonly Color GIZMOS_COLOR_GHOST = new Color(1f, 0f, 0f, 0.2f);
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private bool m_IsActive = true;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool Active
        {
            get => this.m_IsActive;
            set => this.m_IsActive = value;
        }

        // INTERFACE METHODS: ---------------------------------------------------------------------
        
        public virtual void OnAwake(TShotType shotType)
        { }

        public virtual void OnStart(TShotType shotType)
        { }
        
        public virtual void OnDestroy(TShotType shotType)
        { }

        public virtual void OnUpdate(TShotType shotType)
        { }

        public virtual void OnEnable(TShotType shotType, TCamera camera)
        { }

        public virtual void OnDisable(TShotType shotType, TCamera camera)
        { }

        public virtual void OnDrawGizmos(TShotType shotType)
        { }

        public virtual void OnDrawGizmosSelected(TShotType shotType)
        { }
    }
}