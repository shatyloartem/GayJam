using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Utilities
{
    public class VictorySceneController : MonoBehaviour
    {
        [SerializeField] private string levelToLoad = "MainMenu";
        [SerializeField] private GameObject[] frames;
        [SerializeField] private float[] durations;
        
        private Animator[] _animators;

        private void Awake()
        {
            DisableAllFrames();
            StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            for (int i = 0; i < frames.Length; i++)
            {
                DisableAllFrames();
                frames[i].SetActive(true);
                
                yield return new WaitForSeconds(durations[i]);
            }
            
            SceneManager.LoadScene(levelToLoad);
            Checkpoint.ResetCheckpoint();
        }

        private void DisableAllFrames()
        {
            foreach (var f in frames)
                f.SetActive(false);
        }
    }
}
