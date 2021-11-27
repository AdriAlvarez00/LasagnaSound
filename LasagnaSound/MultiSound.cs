using System;
using UnityEngine;

namespace LasagnaSound
{
    public class MultiSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;

        void Start()
        {
            Debug.Log("Loaded " + clips.Length + " clips");
            Debug.Log("oleole autocopy");
        }
    }
}
