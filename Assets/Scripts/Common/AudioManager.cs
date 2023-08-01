using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBGM;
    private int bgmIndex;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if(!playBGM)
            StopAllBGM();
        else
            if (!bgm[bgmIndex].isPlaying)
        {
            PlayBGM(bgmIndex);
        }
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < sfx.Length)
        {
            sfx[sfxIndex].pitch = Random.Range(0.85f, 1.1f);
            sfx[sfxIndex].Play();
        }
    }

    public void StopSFX(int index) => sfx[index].Stop();
    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();
        bgm[bgmIndex].Play();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
            bgm[i].Stop();
    }
}
