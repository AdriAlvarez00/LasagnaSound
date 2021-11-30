using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LasagnaSound
{
    class MultiSound : IntensityDrivenController
    {
        [SerializeField] ClipBundle bundle;
        private bool alive = true; //Set false to kill Coroutine
        [SerializeField] public FunctionValue volume = new FunctionValue("Volume");
        [SerializeField] public FunctionValue maxSimultaneousPlaying = new FunctionValue("Max simultaneous sounds",0.0f,100.0f,false);
        [SerializeField] public FunctionValue maxSimultaneousRepeat = new FunctionValue("Max sound repeats", 0.0f, 100.0f, false);
        [SerializeField] public FunctionValue minPlayInterval = new FunctionValue("Min play interval", 0.1f, 100.0f, true);
        [SerializeField] public FunctionValue maxPlayInterval = new FunctionValue("Max play interval", 0.1f, 100.0f, true);


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

            timesPlaying = new int[bundle.clips.Length];
            for (int i = 0; i < timesPlaying.Length; i++)
                timesPlaying[i] = 0;

            Debug.Log("Aguoken");

        }

        IEnumerator playOnShotEnd(int index, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            totalPlaying--;
            timesPlaying[index]--;
        }

        IEnumerator PlaySound()
        {
            while (alive)
            {
                if (totalPlaying < maxSimultaneousPlaying.getValue(m_intensity))
                {
                    int nextClip;
                    int i = 0;
                    do
                    {
                        nextClip = UnityEngine.Random.Range(0, bundle.clips.Length);
                        i++;
                    }
                    while (timesPlaying[nextClip] >= maxSimultaneousPlaying.getValue(GetIntensity()) && i < 1000);
                    if (i >= 1000)
                        Debug.LogError("Infinite loop choosing clip");

                    Debug.Log(bundle.clips[nextClip].name);
                    timesPlaying[nextClip]++;
                    totalPlaying++;
                    audioSource.PlayOneShot(bundle.clips[nextClip], volume.getValue(GetIntensity()));
                    StartCoroutine(playOnShotEnd(nextClip, bundle.clips[nextClip].length));

                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(minPlayInterval.getValue(m_intensity), maxPlayInterval.getValue(m_intensity)));
            }
        }

        void Start()
        {
            Debug.Log("Loaded " + bundle.clips.Length + " clips");
            StartCoroutine(PlaySound());
        }
    }
}
