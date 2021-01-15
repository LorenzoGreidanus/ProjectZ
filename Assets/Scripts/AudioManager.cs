using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    float masterVolume = 0.3f;
    float sfxVolume = 1;
    float musicVolume = 1;

    AudioSource[] musicSources;
    int activeMusicSourceIndex;

    public static AudioManager instance;

    Transform player;
    Transform audioListener;

    private void Awake()
    {
        instance = this;

        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music Source" + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

        audioListener = FindObjectOfType<AudioListener>().transform;
        player = FindObjectOfType<PlayerController>().transform;
    }

    public void Update()
    {
        if(player != null)
        {
            audioListener.position = player.position;
        }
    }

    public void PlayMusic(AudioClip clip, float fadeDur = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(CrossFade(fadeDur));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolume * masterVolume);
        }
    }

    IEnumerator CrossFade(float duration)
    {
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolume * masterVolume, percent);
            musicSources[1-activeMusicSourceIndex].volume = Mathf.Lerp( musicVolume * masterVolume, 0, percent);
            yield return null;
        }
    }
}
