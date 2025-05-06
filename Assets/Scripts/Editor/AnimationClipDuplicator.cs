using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEditor;
using System.IO;

public class AnimationClipDuplicator : EditorWindow
{
    private string saveFolder = "Assets";

    private string newID = "0"; 


    [MenuItem("Tools/Animation Clip Duplicator")]
    static void Init()
    {
        AnimationClipDuplicator window = (AnimationClipDuplicator)EditorWindow.GetWindow(typeof(AnimationClipDuplicator));
        window.titleContent = new GUIContent("Clip Duplicator");
        window.Show();
    }

    void OnGUI()
    {
        UpdateSaveFolderFromSelection();

        GUILayout.Label("Animation Clip Duplication Tool", EditorStyles.boldLabel);
        saveFolder = EditorGUILayout.TextField("Save Folder", saveFolder);
        newID = EditorGUILayout.TextField("New ID", newID);


        if (GUILayout.Button("Duplicate Selected Clips"))
        {
            DuplicateSelectedClips();
        }
    }

    void UpdateSaveFolderFromSelection()
    {
        Object selected = Selection.activeObject;
        if (selected != null)
        {
            string path = AssetDatabase.GetAssetPath(selected);
            if (AssetDatabase.IsValidFolder(path))
            {
                saveFolder = path;
            }
            else
            {
                saveFolder = Path.GetDirectoryName(path);
            }
        }
    }

    void DuplicateSelectedClips()
    {
        var selectedObjects = Selection.objects;

        // Ensure a valid save folder is selected
        if (!AssetDatabase.IsValidFolder(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
            AssetDatabase.Refresh();
        }

        foreach (var obj in selectedObjects)
        {
            if (obj is AnimationClip originalClip)
            {
                string[] parts = originalClip.name.Split('_');

                if (parts.Length == 4)
                {
                    // Rebuild name with new ID
                    string newName = $"{parts[0]}_{newID}_{parts[2]}_{parts[3]}";

                    // Duplicate the clip
                    AnimationClip newClip = Object.Instantiate(originalClip);
                    newClip.name = newName;

                    // Create asset path
                    string newPath = Path.Combine(saveFolder, newName + ".anim");

                    // Ensure file doesn't overwrite
                    if (File.Exists(newPath))
                    {
                        Debug.LogWarning($"File already exists: {newPath}, skipping.");
                        continue;
                    }

                    AssetDatabase.CreateAsset(newClip, newPath);
                    Debug.Log($"Created copy: {newPath}");
                }
                else
                {
                    Debug.LogWarning($"Skipping '{originalClip.name}' — name does not match expected format (Part_ID_AnimType_Direction).");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}

