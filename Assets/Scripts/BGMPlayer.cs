using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource audioSource;
    public float Volume { get { return audioSource.volume; } }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBGM(int i)
    {
        audioSource.clip= audioClips[i];
        audioSource.loop = true;
        audioSource.Play();
    }
    public void ChangeVolume(float f)
    {
        audioSource.volume = f;
    }
}
