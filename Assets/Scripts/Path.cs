using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Path : MonoBehaviour
    {
        [SerializeField]
        List<Vector3> PathNodes;

        public void InitPath()
        {
            PathNodes = new List<Vector3>();
        }

        public Vector3 GetNodePosition(int index)
        {
            if (index >= PathNodes.Count)
            {
                return Vector3.zero;
            }
            else
            {
                return PathNodes[index];
            }
        }

        public int NodeCount()
        {
            return PathNodes.Count;
        }

        public void Clear()
        {
            PathNodes.Clear();
        }

        public void AddNode(Vector3 node)
        {
            PathNodes.Add(node);
        }

        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            if (PathNodes.Count > 1)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < PathNodes.Count - 1; i++)
                {
                    Gizmos.DrawLine(PathNodes[i], PathNodes[i + 1]);
                }
            }
        }
    }
}