using System;
using UnityEngine;

namespace CastleEscape.Stats
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float level = 1;

        public event Action OnExperienceGained;
        
        public void IncrementLevel()
        {
            level += 1;
            OnExperienceGained?.Invoke();
        }
    
        public float GetLevel()
        {
            return level;
        }
    }
}
