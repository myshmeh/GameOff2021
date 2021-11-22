namespace Game.GameScene
{
    public class GameSceneTransitions
    {
        private string[] scenePaths;
        private int current;

        public GameSceneTransitions(string[] scenePaths)
        {
            this.scenePaths = scenePaths;
            current = 0;
        }

        public string NextScenePath()
        {
            int next = current + 1;
            
            return next < scenePaths.Length ?
                scenePaths[next] : null;
        }

        public string CurrentScenePath()
        {
            return scenePaths[current];
        }

        public void TransitionToNext()
        {
            int next = current + 1;

            if (next == scenePaths.Length) return;
            
            current++;
        }
    }
}