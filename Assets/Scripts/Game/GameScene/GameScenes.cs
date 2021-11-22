using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameScene
{
    public class GameScenes : GenericSingleton.GenericSingleton<GameScenes>
    {
        [SerializeField] private Transform tutorialSceneTransform;
        [SerializeField] private Transform stageIntroSceneTransform;
        [SerializeField] private Transform stageScenesTransform;
        [SerializeField] private Transform gameCompleteSceneTransform;
        
        private string tutorialScenePath;
        private string stageIntroScenePath;
        private List<string> stageScenePaths;
        private string gameCompleteScenePath;

        public string StageIntroScenePath => stageIntroScenePath;
        public List<string> StageScenePaths => stageScenePaths;
        public string GameCompleteScenePath => gameCompleteScenePath;
        public string TutorialScenePath => tutorialScenePath;

        public List<string> AllScenePaths()
        {
            tutorialScenePath = tutorialSceneTransform.GetComponent<ScenePicker>().scenePath;
            stageIntroScenePath = stageIntroSceneTransform.GetComponent<ScenePicker>().scenePath;
            stageScenePaths = stageScenesTransform.GetComponents<ScenePicker>()
                .Select(s => s.scenePath).ToList();
            gameCompleteScenePath = gameCompleteSceneTransform.GetComponent<ScenePicker>().scenePath;
            
            return new List<string>()
                .Append(tutorialScenePath)
                .Append(stageIntroScenePath)
                .Concat(stageScenePaths)
                .Append(gameCompleteScenePath)
                .ToList();
        }
    }
}