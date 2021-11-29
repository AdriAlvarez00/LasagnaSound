using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LasagnaSound
{
    public class MultiSound : MonoBehaviour
    {
        [SerializeField] ClipBundle bundle;
        [SerializeField] float intensity = 1.0f;
        [SerializeField] AnimationCurve volume;
        [SerializeField] int minSimultaneousPlaying;
        [SerializeField] int maxSimultaneousPlaying;
        [SerializeField] int maxSimultaneousRepeat;
        [SerializeField] float minPlayInterval;
        [SerializeField] float maxPlayInterval;
        [SerializeField] bool active = true;


        private AudioSource audioSource;
        private int totalPlaying = 0;
        private int[] timesPlaying;
        void Awake()
        {
            //Get gameobject's audioSource so we can play our audios
            //if none exist, add new one
            audioSource = GetComponent<AudioSource>();

            if(audioSource == null)
			{
                audioSource = gameObject.AddComponent<AudioSource>();
			}

            //TODO que es timesPlaying?
            timesPlaying = new int[bundle.clips.Length];
            for (int i = 0; i < timesPlaying.Length; i++)
                timesPlaying[i] = 0;
        }

        IEnumerator playOnShotEnd(int index, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            totalPlaying--;
            timesPlaying[index]--;
        }

        IEnumerator PlaySound()
        {
            while (active)
            {
                if (totalPlaying < maxSimultaneousPlaying)
                {
                    int nextClip;
                    int i = 0;
                    do
                    {
                        nextClip = UnityEngine.Random.Range(0, bundle.clips.Length);
                        i++;
                    }
                    while (timesPlaying[nextClip] >= maxSimultaneousPlaying && i < 1000);
                    if (i >= 1000)
                        Debug.LogError("Infinite loop choosing clip");

                    Debug.Log(bundle.clips[nextClip].name);
                    timesPlaying[nextClip]++;
                    totalPlaying++;
                    //audioSource.PlayOneShot(bundle.clips[nextClip], volume.Evaluate(intensity));
                    audioSource.PlayOneShot(bundle.clips[nextClip]);
                    StartCoroutine(playOnShotEnd(nextClip, bundle.clips[nextClip].length));

                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(minPlayInterval, maxPlayInterval));
            }
        }

        void Start()
        {
            Debug.Log("Loaded " + bundle.clips.Length + " clips");
            StartCoroutine(PlaySound());
        }
    }
}
