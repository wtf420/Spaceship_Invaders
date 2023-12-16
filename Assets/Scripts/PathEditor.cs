using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Assets.Scripts
{
#if UNITY_EDITOR
    [CustomEditor(typeof(Path))]
    public class PathEditor : Editor
    {
        int zValue;
        bool MouseInput = false;

        public override void OnInspectorGUI()
        {
            zValue = EditorGUILayout.IntField("Z value:", zValue);
            DrawDefaultInspector();
            Path path = (Path)target;
            if (GUILayout.Button("Add New"))
            {
                MouseInput = true;
            }
            if (GUILayout.Button("Clear"))
            {
                path.Clear();
            }
        }

        void OnSceneGUI()
        {
            Path path = (Path)target;
            if (MouseInput)
            {
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    Vector3 MousePostion = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
                    MousePostion.z = zValue;
                    path.AddNode(MousePostion);
                    MouseInput = false;
                }
            }
        }
    }

#endif
}