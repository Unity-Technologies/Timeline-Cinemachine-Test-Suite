using UnityEngine;

namespace Cinemachine
{
    /// <summary>
    /// A class to get around the limitation in timeline that array members can't be animated.
    /// A fixed number of slots are made available, rather than a dynamic array.  
    /// If you want to add more slots, just modify this code.
    /// </summary>
    [RequireComponent(typeof(CinemachineTargetGroup))]
#if UNITY_2018_3_OR_NEWER
    [ExecuteAlways]
#else
    [ExecuteInEditMode]
#endif
    public class TargetGroup_Weights : MonoBehaviour
    {
        /// <summary>The weight of the group member at index 0</summary>
        [Tooltip("The weight of the group member at index 0")]
        public float m_Weight0 = 1;
        /// <summary>The weight of the group member at index 1</summary>
        [Tooltip("The weight of the group member at index 1")]
        public float m_Weight1 = 1;
        /// <summary>The weight of the group member at index 2</summary>
        [Tooltip("The weight of the group member at index 2")]
        public float m_Weight2 = 1;
        /// <summary>The weight of the group member at index 3</summary>

        CinemachineTargetGroup m_group;
        void Start()
        {
            m_group = GetComponent<CinemachineTargetGroup>();
        }
        
        void OnValidate()
        {
            m_Weight0 = Mathf.Max(0, m_Weight0);
            m_Weight1 = Mathf.Max(0, m_Weight1);
            m_Weight2 = Mathf.Max(0, m_Weight2);
        }
        
        void Update()
        {
            if (m_group != null)
                UpdateWeights();
        }
        
        void UpdateWeights()
        {
            var targets = m_group.m_Targets;
            int last = targets.Length - 1;
            if (last < 0) return; targets[0].weight = m_Weight0;
            if (last < 1) return; targets[1].weight = m_Weight1;
            if (last < 2) return; targets[2].weight = m_Weight2;
        }
    }
}