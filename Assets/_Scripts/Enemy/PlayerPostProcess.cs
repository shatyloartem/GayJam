using UnityEngine;

namespace _Scripts.Enemy
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerPostProcess", fileName = "PlayerPostProcess")]
    public class PlayerPostProcess : ScriptableObject
    {
        public float defaultAberrationIntensity;
        public float maxAberrationIntensity;
        
        public float defaultVignetteIntensity;
        public float maxVignetteIntensity;
    }
}