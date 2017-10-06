using UnityEditor;

namespace Cinemachine.Editor
{
    [CustomEditor(typeof(CinemachinePOV))]
    public sealed class CinemachinePOVEditor : UnityEditor.Editor
    {
        private CinemachinePOV Target { get { return target as CinemachinePOV; } }
        private static readonly string[] m_excludeFields = new string[] { "m_Script" };

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            DrawPropertiesExcluding(serializedObject, m_excludeFields);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}
