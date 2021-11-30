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

        public override void SetIntensity(float intensity)
        {
            base.SetIntensity(intensity);
            foreach (SoundLayer layer in layers)
            {
                if (m_intensity >= layer.inPoint && m_intensity <= layer.outPoint)
                {
                    float normalizedValue = (m_intensity - layer.inPoint) / (layer.outPoint - layer.inPoint);
                    layer.sound.SetIntensity(layer.intensityCurve.Evaluate(normalizedValue));
                }
                else
                {
                    layer.sound.SetIntensity(0.0f);
                    //TODO desactivar
                }

            }
        }
    }
}
