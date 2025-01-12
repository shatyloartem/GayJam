using UnityEngine;

public class StepSoundController : MonoBehaviour
{

    private AudioSource _audioSource;

    private bool _isPlaying;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SoundOn(float pitch = 1)
    {
        if (_isPlaying)
            return;

        _audioSource.Play();
        _audioSource.pitch = pitch;

        _isPlaying = true;
    }

    public void SoundOff()
    {
        _audioSource.Stop();

        _isPlaying = false;
    }

    public void ChangePitch(float pitch)
    {
        _audioSource.pitch = pitch;
    }
}
