using System;
using _Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Utilities
{
    public class VictoryController : MonoBehaviour
    {
        private bool _isGameOver;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !_isGameOver)
            {
                UIGameController.Instance.SetIconActive(true);
                _isGameOver = true;
                Victory();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                UIGameController.Instance.SetIconActive(false);
        }

        private void Victory()
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
