using System;
using MonoBehaviourWatcher;
using StageObject.Debugger.State;
using UnityEngine;

namespace StageObject.Decoy
{
    public class DecoyController : MonoBehaviour, IAttackable
    {
        [Watchable] private bool alive = true;

        public bool Alive => alive;

        public void OnAttack()
        {
            alive = false;
        }
    }
}