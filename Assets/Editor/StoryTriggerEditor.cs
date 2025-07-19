using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(StoryTrigger))]
public class StoryTriggerEditor : Editor
{
    private SerializedProperty storyEventPathProp;
    private SerializedProperty triggerOnceProp;

    private List<string> storyEventPaths;
    private int selectedPathIndex = -1;

    private void OnEnable()
    {
        storyEventPathProp = serializedObject.FindProperty("storyEventPath");
        triggerOnceProp = serializedObject.FindProperty("triggerOnce");

        // Populate the list of story event paths
        if (StoryManager.Instance != null)
        {
            storyEventPaths = StoryManager.Instance.GetAllStoryEventPaths();
            // Add an empty option for no selection or invalid path
            storyEventPaths.Insert(0, "None (Select an Event)");
        }
        else
        {
            storyEventPaths = new List<string> { "StoryManager not found!" };
        }

        // Find the current index of the selected path
        selectedPathIndex = storyEventPaths.IndexOf(storyEventPathProp.stringValue);
        if (selectedPathIndex == -1)
        {
            selectedPathIndex = 0; // Default to "None" if current path is not found
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Always start with this

        // Draw the dropdown for storyEventPath
        EditorGUI.BeginChangeCheck();
        selectedPathIndex = EditorGUILayout.Popup("Story Event Path", selectedPathIndex, storyEventPaths.ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            if (selectedPathIndex > 0) // Avoid "None" option
            {
                storyEventPathProp.stringValue = storyEventPaths[selectedPathIndex];
            }
            else
            {
                storyEventPathProp.stringValue = ""; // Clear the string if "None" is selected
            }
        }

        // Draw other properties as usual
        EditorGUILayout.PropertyField(triggerOnceProp);

        serializedObject.ApplyModifiedProperties(); // Always end with this
    }
}