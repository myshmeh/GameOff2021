using UnityEngine;

namespace StageObject.Server
{
    public interface IClient
    {
        void OnServerSetup(Color serverColor);
        void OnServerDown();
    }
}