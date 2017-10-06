using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cinemachine.Editor
{
    [CustomEditor(typeof(CinemachineBasicMultiChannelPerlin))]
    internal sealed class CinemachineBasicMultiChannelPerlinEditor : UnityEditor.Editor
    {
        private CinemachineBasicMultiChannelPerlin Target { get { return target as CinemachineBasicMultiChannelPerlin; } }
        private static string[] m_excludeFields;

        List<NoiseSettings> mNoisePresets;
        string[] mNoisePresetNames;

        private void OnEnable()
        {
            m_excludeFields = new string[]
            {
                "m_Script",
                SerializedPropertyHelper.PropertyName(() => Target.m_NoiseProfile)
            };

            mNoisePresets = FindAssetsByType<NoiseSettings>();
            mNoisePresets.Insert(0, null);
            List<string> presetNameList = new List<string>();
            foreach (var n in mNoisePresets)
                presetNameList.Add((n == null) ? "(none)" : n.name);
            mNoisePresetNames = presetNameList.ToArray();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (Target.m_NoiseProfile == null)
                EditorGUILayout.HelpBox(
                    "A Noise Profile is required.  You may choose from among the NoiseSettings assets defined in the project.",
                    MessageType.Error);

            GUIContent editLabel = new GUIContent("Edit");
            Vector2 labelDimension = GUI.skin.button.CalcSize(editLabel);
            Rect rect = EditorGUILayout.GetControlRect(true);
            rect.width -= labelDimension.x;
            int preset = mNoisePresets.IndexOf(Target.m_NoiseProfile);
            preset = EditorGUI.Popup(rect, "Noise Profile", preset, mNoisePresetNames);
            NoiseSettings newProfile = preset < 0 ? null : mNoisePresets[preset];
            if (Target.m_NoiseProfile != newProfile)
            {
                Undo.RecordObject(Target, "Change Noise Profile");
                Target.m_NoiseProfile = newProfile;
            }
            if (Target.m_NoiseProfile != null)
            {
                rect.x += rect.width; rect.width = labelDimension.x;
                if (GUI.Button(rect, editLabel))
                    Selection.activeObject = Target.m_NoiseProfile;
            }

            DrawPropertiesExcluding(serializedObject, m_excludeFields);
            serializedObject.ApplyModifiedProperties();
        }

        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
    }
}
