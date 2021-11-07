using System;
using System.Collections.Generic;
using System.Linq;
using StageObject.DoorMechanic.Button;
using StageObject.DoorMechanic.Door;
using UnityEngine;

namespace StageObject.DoorMechanic
{
    public class ButtonDoorBrandingHandler : MonoBehaviour
    {
        [SerializeField] private Color brandColor = Color.gray;
        [SerializeField] private Transform buttonHolderTransform;
        [SerializeField] private Transform doorHolderTransform;
        private List<ButtonController> buttons = new List<ButtonController>();
        private List<DoorController> doors = new List<DoorController>();

        void SetupBrandColor()
        {
            buttons.ForEach(button => button.SetupBrandColor(brandColor));
            doors.ForEach(door => door.SetupBrandColor(brandColor));
        }

        void SetupButtonsNDoors()
        {
            foreach (Transform _child in buttonHolderTransform)
                buttons.Add(_child.GetComponent<ButtonController>());
            foreach (Transform _child in doorHolderTransform)
                doors.Add(_child.GetComponent<DoorController>());
        }
        
        private void Start()
        {
            SetupButtonsNDoors();
            SetupBrandColor();
        }

        ButtonController DetectPressedButton()
        {
            return buttons.FirstOrDefault(button => button.IsPressed());
        }

        void NotifyDoorsButtonWasPressed()
        {
            var _pressedButton = DetectPressedButton();
            
            if (_pressedButton == null) return;
            
            _pressedButton.MoveToAwaitingState();
            doors.ForEach(door => door.SwitchState());
        }
        
        private void Update()
        {
            NotifyDoorsButtonWasPressed();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = brandColor;
            foreach (Transform _child in buttonHolderTransform)
                Gizmos.DrawCube(_child.position + Vector3.up * .5f, Vector3.one * .2f);
            foreach (Transform _child in doorHolderTransform)
                Gizmos.DrawCube(_child.position + Vector3.up * 1f, Vector3.one * .2f);
        }
    }
}