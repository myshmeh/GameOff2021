using TMPro;

namespace UI.Dialogue
{
    [System.Serializable]
    public class DialogueBoxUI
    {
        public TextMeshProUGUI nameAndCommand;
        public TextMeshProUGUI responses;

        public void AddToNameAndCommand(char c)
        {
            nameAndCommand.text += c;
        }
        
        public void AddToNameAndCommand(string str)
        {
            nameAndCommand.text += str;
        }

        public void AddToResponse(char c)
        {
            responses.text += c;
        }
        
        public void AddToResponse(string str)
        {
            responses.text += str;
        }
        
        public void ClearNameAndCommand()
        {
            nameAndCommand.text = "";
        }

        public void ClearResponse()
        {
            responses.text = "";
        }
    }
}