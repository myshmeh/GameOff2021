#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.GameScene
{
    [CustomEditor(typeof(GameScenes), true)]
    public class GameScenesEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(15f);
            if(GUILayout.Button("Apply To Build Settings")) {
                GameScenes gameScenes = target as GameScenes;
                ApplyToBuildSettings(gameScenes.AllScenePaths());
            }
        }

        void ApplyToBuildSettings(List<string> scenePaths)
        {
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (var scenePath in scenePaths)
            {
                if (string.IsNullOrEmpty(scenePath)) continue;
                editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }

            // Set the Build Settings window Scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }
    }
}
#endif