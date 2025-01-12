using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UIGameController : MonoBehaviour
    {
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private float animationDuration = 1f;
        
        public static UIGameController Instance { get; private set; }
        
        private Graphic[] _deathPanelGraphics;
        private List<float> _starterAlpha = new();
        
        private void Awake()
        {
            Instance = this;
            
            _deathPanelGraphics = deathPanel.GetComponentsInChildren<Graphic>();
            deathPanel.SetActive(false);
            foreach (var graphic in _deathPanelGraphics)
            {
                Color color = graphic.color;
                _starterAlpha.Add(color.a);
                color.a = 0f;
                graphic.color = color;
            }            
        }

        public void ActivateDeathPanel()
        {
            deathPanel.SetActive(true);
            for (int i = 0; i< _deathPanelGraphics.Length; i++)
            {
                Color color = _deathPanelGraphics[i].color;
                color.a = _starterAlpha[i];
                _deathPanelGraphics[i].DOColor(color, 1);
            }
        }

        public void MainMenu() => SceneManager.LoadScene("MainMenu");

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
