using System;
using GenericSingleton;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : GenericSingleton<GameManager>
    {
        void MoveToNextScene()
        {
            int _currentStageSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int _nextStageSceneIndex = (_currentStageSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(_nextStageSceneIndex);
        }

        void MoveToTitle()
        {
            var _titleScene = SceneManager.GetSceneByName("Title");
            if (!_titleScene.IsValid()) throw new Exception("Title Scene could not be found");
            int _titleSceneIndex = _titleScene.buildIndex;
            SceneManager.LoadScene(_titleSceneIndex);
        }

        void RevisitSameScene()
        {
            int _currentStageSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(_currentStageSceneIndex);
        }

        public void OnTitleCompleted()
        {
            MoveToNextScene();
        }

        public void OnStageCompleted()
        {
            MoveToNextScene();
        }

        public void OnStageFailed()
        {
            RevisitSameScene();
        }

        public void OnEndingCompleted()
        {
            MoveToTitle();
        }
    }
}