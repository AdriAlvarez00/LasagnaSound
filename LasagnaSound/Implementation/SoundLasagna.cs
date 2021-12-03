using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LasagnaSound.Implementation
{
	/// <summary>
	/// Controls other IntensityDrivenControllers using its own intensity through SoundLayers
	/// </summary>
	class SoundLasagna : IntensityDrivenController
	{
		[SerializeField] SoundLayer[] layers;
		private MultiSoundPlayer[] players;

		private AudioSource audioSource;

		void Awake()
		{
			players = new MultiSoundPlayer[layers.Length];
		}

		void Start()
		{
			//Get gameobject's audioSource so we can play our audios
			//if none exist, add new one
			audioSource = GetComponent<AudioSource>();

			if (audioSource == null)
			{
				audioSource = gameObject.AddComponent<AudioSource>();
			}

			//Create a multisound component for each layer
			for (int i = 0; i < players.Length; i++)
			{
				players[i] = gameObject.AddComponent<MultiSoundPlayer>();
				players[i].Init(audioSource, layers[i].sound);
			}

			//Run checks for layers
			SetIntensity(m_intensity);

		}

		public override void SetIntensity(float intensity)
		{
			base.SetIntensity(intensity);
			for (int i = 0; i < players.Length; i++)
			{
				if (m_intensity >= layers[i].inPoint && m_intensity <= layers[i].outPoint)
				{
					float normalizedValue = (m_intensity - layers[i].inPoint) / (layers[i].outPoint - layers[i].inPoint);
					players[i].intensity = layers[i].intensityCurve.Evaluate(normalizedValue);
					players[i].SetActive(true);
				}
				else
				{
					players[i].intensity = 0.0f;
					players[i].SetActive(false);
				}

			}
		}


	}
}
