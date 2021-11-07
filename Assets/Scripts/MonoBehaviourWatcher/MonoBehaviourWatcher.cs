using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MonoBehaviourWatcher
{
    public class MonoBehaviourWatcher : MonoBehaviour
    {
        private static int _count;

        [SerializeField] private MonoBehaviour monoBehaviour;
        [SerializeField] private Rect windowRect = new Rect(0, 0, 250f, Screen.height * .6f);

        private int _id;
        private Type _monoBehaviourType;

        private BindingFlags _bindingFlags = BindingFlags.Instance |
                                             BindingFlags.Static |
                                             BindingFlags.Public |
                                             BindingFlags.NonPublic;

        private Vector2 _scrollPosition = Vector2.zero;

        private void Awake()
        {
            _id = _count++;
            _monoBehaviourType = monoBehaviour.GetType();
        }

        private void OnGUI()
        {
            string windowName = monoBehaviour.gameObject.name;

            void Contents(int id)
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

                foreach (var keyValue in LabelInfos(_monoBehaviourType, _bindingFlags))
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(keyValue.Key);
                    GUILayout.Box(keyValue.Value);
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
            }

            GUILayout.Window(_id, windowRect, Contents, windowName);
        }

        private IEnumerable<KeyValuePair<string, string>> LabelInfos(Type type, BindingFlags bindingFlags) =>
            FieldInfoDictionary(type, bindingFlags)
                .Concat(PropertyInfoDictionary(type, bindingFlags))
                .Concat(MethodInfoDictionary(type, bindingFlags));

        private Dictionary<string, string> FieldInfoDictionary(Type type, BindingFlags bindingFlags) => type
            .GetFields(bindingFlags)
            .Where(fieldInfo => fieldInfo
                .GetCustomAttributes(true)
                .Any(attr => attr is Watchable))
            .ToDictionary(i => i.Name,
                i => i.GetValue(monoBehaviour).ToString());

        private Dictionary<string, string> PropertyInfoDictionary(Type type, BindingFlags bindingFlags) => type
            .GetProperties(bindingFlags)
            .Where(propertyInfo => propertyInfo
                .GetCustomAttributes(true)
                .Any(attr => attr is Watchable))
            .ToDictionary(i => i.Name,
                i => i.GetValue(monoBehaviour).ToString());

        private Dictionary<string, string> MethodInfoDictionary(Type type, BindingFlags bindingFlags) => type
            .GetMethods(bindingFlags)
            .Where(methodInfo => methodInfo
                .GetCustomAttributes(true)
                .Any(attr => attr is Watchable))
            .ToDictionary(i => i.Name,
                i => i.Invoke(monoBehaviour, null).ToString());
    }
}