using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;

    private Vector3 _originalPosition;

    private void Awake()
    {
        Instance = this;
        _originalPosition = transform.position;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position += transform.right * x + transform.up * y;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = _originalPosition;
    }
}