
using Game;
using UnityEngine;

namespace Title
{
    public class TitleSceneController : MonoBehaviour
    {
        [SerializeField] private KeyCode startKey = KeyCode.Space;
        void Update()
        {
            if (Input.GetKeyDown(startKey))
            {
                GameManager.Instance.OnTitleCompleted();
            }
        }
    }
}