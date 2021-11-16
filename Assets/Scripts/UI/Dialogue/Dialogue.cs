using UnityEngine;

namespace UI.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        public string name = "bug-1.32";
        public string command;
        public string bossName = "boss";
        
        [TextArea(3, 10)]
        public string[] responses;
    }
}