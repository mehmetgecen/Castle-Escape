using System;
using UnityEngine;

namespace CastleEscape.Stats
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private int level = 1;

        public event Action OnExperienceGained;
        
        public void IncrementLevel()
        {
            level += 1;
            OnExperienceGained?.Invoke();
        }
    
        public int GetLevel()
        {
            return level;
        }
    }
}
