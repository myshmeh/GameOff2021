using System;
using UnityEngine;

namespace StageObject
{
    public class BrandColor
    {
        public static void SetupBrandColor(MonoBehaviour monoBehaviour, Color brandColor)
        {
            MeshRenderer _meshRenderer = monoBehaviour.GetComponentInChildren<MeshRenderer>();
            _meshRenderer.material.color = brandColor;
        }
    }
}