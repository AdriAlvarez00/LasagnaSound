﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace LasagnaSound
{
    [CreateAssetMenu(fileName = "newClipBundle", menuName ="LasagnaSound/ClipBundle")]
    class ClipBundle : ScriptableObject
    {
        public AudioClip[] clips;
    }
}
