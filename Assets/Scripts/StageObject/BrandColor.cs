using System;
using UnityEngine;

namespace StageObject
{
    public class BrandColor
    {
        public static void SetupBrandColor(MonoBehaviour monoBehaviour, Color brandColor)
        {
            MeshRenderer _meshRenderer = monoBehaviour.GetComponentInChildren<MeshRenderer>();
            if (_meshRenderer != null)
            {
                _meshRenderer.material.color = brandColor;
                return;
            }
            
            SkinnedMeshRenderer _skinnedMeshRenderer = monoBehaviour.GetComponentInChildren<SkinnedMeshRenderer>();
            if (_skinnedMeshRenderer != null)
            {
                _skinnedMeshRenderer.material.color = brandColor;
                return;
            }

            throw new Exception("no material was found");
        }

        public static Color GetBrandColor(MonoBehaviour monoBehaviour)
        {
            MeshRenderer _meshRenderer = monoBehaviour.GetComponentInChildren<MeshRenderer>();
            if (_meshRenderer != null)
            {
                return _meshRenderer.material.color;
            }
            
            SkinnedMeshRenderer _skinnedMeshRenderer = monoBehaviour.GetComponentInChildren<SkinnedMeshRenderer>();
            if (_skinnedMeshRenderer != null)
            {
                return _skinnedMeshRenderer.material.color;
            }

            throw new Exception("no material was found");
        }
    }
}