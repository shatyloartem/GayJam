using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Utilities
{
    public class VictorySceneController : MonoBehaviour
    {
        [SerializeField] private GameObject[] frames;
        [SerializeField] private float[] durations;
        
        private Animator[] _animators;

        private void Awake()
        {
            // for(int i = 0; i < frames.Length; i++)
            //     _animators[i] = frames[i].GetComponent<Animator>();
         
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
            
            SceneManager.LoadScene("MainMenu");
            Checkpoint.ResetCheckpoint();
        }

        private void DisableAllFrames()
        {
            foreach (var f in frames)
                f.SetActive(false);
        }
    }
}
