using System;
using _Scripts.Enemy;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float starterHealth = 100;
        [SerializeField] private float regenerationCooldown = 4;
        [SerializeField] private float regenerationAmount = 15;
        [SerializeField] private PlayerPostProcess playerPostProcess;
        
        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;

        private float _lastTimeTookDamage;
        private float _health;
        private bool _isDead;
        private bool _isPostProcessingSetUp;
        
        private void Awake()
        {
            _health = starterHealth;
            
            Volume volume = FindObjectOfType<Volume>();
            if (!volume)
                return;
            
            volume.profile.TryGet(out _vignette);
            volume.profile.TryGet(out _chromaticAberration);
            _isPostProcessingSetUp = true;
            
            UpdatePostProcess();
        }

        private void Update()
        {
            if(_lastTimeTookDamage + regenerationCooldown < Time.time)
                Heal(regenerationAmount * Time.deltaTime);
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, maxHealth);
            _lastTimeTookDamage = Time.time;
            UpdatePostProcess();

            if (_health <= 0 && !_isDead)
                Die();
        }
        
        private void Heal(float healAmount)
        {
            _health = Mathf.Clamp(_health + healAmount, 0, maxHealth);
            UpdatePostProcess();
        }

        private void Die()
        {
            _isDead = true;
            
            Debug.Log("Player dead");
        }

        private void UpdatePostProcess()
        {
            if(!_isPostProcessingSetUp)
                return;
            
            float kof = 1 - _health / maxHealth;
            
            _vignette.intensity.value = Mathf.Lerp(playerPostProcess.defaultVignetteIntensity, 
                    playerPostProcess.maxVignetteIntensity, kof);
            
            _chromaticAberration.intensity.value = Mathf.Lerp(playerPostProcess.defaultAberrationIntensity, 
                playerPostProcess.maxAberrationIntensity, kof);
        }
    }
}
