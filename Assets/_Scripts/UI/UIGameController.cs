using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UIGameController : MonoBehaviour
    {
        [SerializeField] private GameObject deathPanel, gameUI, eIcon;
        [SerializeField] private float animationDuration = 1f;
        
        public static UIGameController Instance { get; private set; }
        
        private Graphic[] _deathPanelGraphics;
        private List<float> _starterAlpha = new();
        
        private void Awake()
        {
            Instance = this;
            
            eIcon.SetActive(false);
            gameUI.SetActive(true);
            deathPanel.SetActive(false);

            _deathPanelGraphics = deathPanel.GetComponentsInChildren<Graphic>();
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
            gameUI.SetActive(false);
            deathPanel.SetActive(true);
            for (int i = 0; i< _deathPanelGraphics.Length; i++)
            {
                Color color = _deathPanelGraphics[i].color;
                color.a = _starterAlpha[i];
                _deathPanelGraphics[i].DOColor(color, animationDuration);
            }
        }

        public void MainMenu() => SceneManager.LoadScene("MainMenu");

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        public void SetIconActive(bool active) => eIcon.SetActive(active);
    }
}
