using System;
using System.Collections.Generic;
using UnityEngine;

namespace LasagnaSound
{
    [CreateAssetMenu(fileName = "newClipBundle", menuName ="LasagnaSound/ClipBundle")]
    [System.Serializable]
    class ClipBundle : ScriptableObject
    {
        [SerializeField] public AudioClip[] clips;
    }
}
