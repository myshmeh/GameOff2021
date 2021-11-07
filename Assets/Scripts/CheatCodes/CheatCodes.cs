using System;
using MonoBehaviourWatcher;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CheatCodes
{
    public class CheatCodes : MonoBehaviour
    {
        [Watchable] [SerializeField] private KeyCode reloadLevel = KeyCode.R;

        private void Update()
        {
            if (Input.GetKeyDown(reloadLevel))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}