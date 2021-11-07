using UnityEngine;

namespace StageObject.Debugger
{
    public static class TransformConverter
    {
        public static Vector3[] ToArray(Transform transform)
        {
            Vector3[] arr = new Vector3[transform.childCount];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = transform.GetChild(i).position;
            }

            return arr;
        }
    }
}