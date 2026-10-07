// PROTOTYPE - NOT FOR PRODUCTION
// Question: Does real-time (no pause) control of 4 Chip-controlled agents feel good - split, regroup, focus-fire?
// Date: 2026-10-06

using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// Menu: Prototype > Squad Control > Create + Open Scene. Then press Play.
public static class SquadControlSceneBuilder
{
    const string ScenePath = "Assets/Prototypes/SquadControl/SquadControlProto.unity";

    [MenuItem("Prototype/Squad Control/Create + Open Scene")]
    public static void CreateAndOpen()
    {
        if (!File.Exists(ScenePath))
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            new GameObject("SquadPrototype").AddComponent<SquadPrototype>();
            EditorSceneManager.SaveScene(scene, ScenePath);
        }
        else EditorSceneManager.OpenScene(ScenePath);
    }
}
