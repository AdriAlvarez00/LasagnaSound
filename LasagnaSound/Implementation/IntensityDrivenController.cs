using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace LasagnaSound
{
	class IntensityDrivenController : MonoBehaviour
	{
		[SerializeField, Range(0, 1)] protected float m_intensity = 1.0f;

		public virtual void SetIntensity(float intensity) { m_intensity = intensity; }
		public float GetIntensity() { return m_intensity; }

		void OnValidate()
		{
			if (EditorApplication.isPlaying)
				SetIntensity(m_intensity);
		}
	}
}
