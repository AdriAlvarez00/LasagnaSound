using System;
using System.Collections;
using UnityEngine;

namespace LasagnaSound
{
	class MultiSoundPlayer : MonoBehaviour
	{
		private MultiSound multiSound { get; set; }
		private AudioSource audioSource { get; set; }

		public float intensity { get; set; }
		private int[] timesPlaying;
		private int totalPlaying = 0;
		private bool active = false;


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
				if (totalPlaying < multiSound.maxSimultaneousPlaying.getValue(intensity))
				{
					int nextClip;
					int i = 0;
					do
					{
						nextClip = UnityEngine.Random.Range(0, multiSound.bundle.clips.Length);
						i++;
					}
					while (timesPlaying[nextClip] >= multiSound.maxSimultaneousPlaying.getValue(intensity) && i < 1000);
					if (i >= 1000)
						Debug.LogError("Infinite loop choosing clip");

					Debug.Log(multiSound.bundle.clips[nextClip].name);
					timesPlaying[nextClip]++;
					totalPlaying++;
					audioSource.PlayOneShot(multiSound.bundle.clips[nextClip], multiSound.volume.getValue(intensity));
					StartCoroutine(playOnShotEnd(nextClip, multiSound.bundle.clips[nextClip].length));

				}
				yield return new WaitForSeconds(UnityEngine.Random.Range(multiSound.minPlayInterval.getValue(intensity),
					multiSound.maxPlayInterval.getValue(intensity)));
			}
		}

		public void Init(AudioSource src, MultiSound multi)
		{
			audioSource = src;
			multiSound = multi;

			timesPlaying = new int[multiSound.bundle.clips.Length];
			for (int i = 0; i < timesPlaying.Length; i++)
				timesPlaying[i] = 0;

			Debug.Log("Loaded " + multiSound.bundle.clips.Length + " clips");
			//StartCoroutine(PlaySound());
		}

		public void SetActive(bool activate)
		{
			//If it isn't alive and you want to launch it
			if (activate && !active){
				active = activate;
				StartCoroutine(PlaySound());
			}

			active = activate;
		}
	}
}
