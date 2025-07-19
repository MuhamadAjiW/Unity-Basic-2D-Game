using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel; // For IReadOnlyDictionary
using System.Reflection; // For PropertyInfo
using System; // For Action
using System.Linq.Expressions; // For Expression

public class StoryManager : MonoBehaviour
{
  public static StoryManager Instance { get; private set; }

  public StoryEvents Events { get; private set; }


  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      InitializeStoryEvents();
    }
  }

  private void InitializeStoryEvents()
  {
    Events = new StoryEvents();
  }

  /// <summary>
  /// Sets the state of a specific story event using a string path (e.g., "chapter1.EventA").
  /// </summary>
  /// <param name="eventPath">The dot-separated path to the event property (e.g., "chapter1.EventA").</param>
  /// <param name="state">The new state of the event (true for completed/active, false for pending/inactive).</param>
  public void SetEventState(string eventPath, bool state)
  {
    string[] pathParts = eventPath.Split('.');
    if (pathParts.Length < 2)
    {
      Debug.LogWarning($"Invalid event path format: '{eventPath}'. Expected format: 'Category.EventName'.");
      return;
    }

    object currentObject = Events;
    PropertyInfo property = null;

    for (int i = 0; i < pathParts.Length; i++)
    {
      property = currentObject.GetType().GetProperty(pathParts[i]);
      if (property == null)
      {
        Debug.LogWarning($"Story path part '{pathParts[i]}' not found in '{currentObject.GetType().Name}'. Full path: '{eventPath}'.");
        return;
      }

      if (i < pathParts.Length - 1) // Not the last part of the path
      {
        currentObject = property.GetValue(currentObject);
        if (currentObject == null)
        {
          Debug.LogWarning($"Story path part '{pathParts[i]}' is null. Full path: '{eventPath}'.");
          return;
        }
      }
    }

    if (property != null && property.PropertyType == typeof(bool))
    {
      property.SetValue(currentObject, state);
      Debug.Log($"Story Event '{eventPath}' set to: {state}");
    }
    else
    {
      Debug.LogWarning($"Story Event '{eventPath}' not found or is not a boolean property.");
    }
  }

  /// <summary>
  /// Gets the current state of a specific story event using a string path (e.g., "chapter1.EventA").
  /// </summary>
  /// <param name="eventPath">The dot-separated path to the event property (e.g., "chapter1.EventA").</param>
  /// <returns>True if the event is completed/active, false if pending/inactive, or false if not found.</returns>
  public bool GetEventState(string eventPath)
  {
    string[] pathParts = eventPath.Split('.');
    if (pathParts.Length < 2)
    {
      Debug.LogWarning($"Invalid event path format: '{eventPath}'. Expected format: 'Category.EventName'. Returning false.");
      return false;
    }

    object currentObject = Events;
    PropertyInfo property = null;

    for (int i = 0; i < pathParts.Length; i++)
    {
      property = currentObject.GetType().GetProperty(pathParts[i]);
      if (property == null)
      {
        Debug.LogWarning($"Story path part '{pathParts[i]}' not found in '{currentObject.GetType().Name}'. Full path: '{eventPath}'. Returning false.");
        return false;
      }

      if (i < pathParts.Length - 1) // Not the last part of the path
      {
        currentObject = property.GetValue(currentObject);
        if (currentObject == null)
        {
          Debug.LogWarning($"Story path part '{pathParts[i]}' is null. Full path: '{eventPath}'. Returning false.");
          return false;
        }
      }
    }

    if (property != null && property.PropertyType == typeof(bool))
    {
      return (bool)property.GetValue(currentObject);
    }
    else
    {
      Debug.LogWarning($"Story Event '{eventPath}' not found or is not a boolean property. Returning false.");
      return false;
    }
  }

  /// <summary>
  /// Checks if a specific story event is true using a string path.
  /// </summary>
  public bool IsEventTrue(string eventPath)
  {
    return GetEventState(eventPath);
  }

  /// <summary>
  /// Checks if a specific story event is false using a string path.
  /// </summary>
  public bool IsEventFalse(string eventPath)
  {
    return !GetEventState(eventPath);
  }
  /// <summary>
  /// Enumerates all available story event paths (e.g., "chapter1.EventA").
  /// </summary>
  /// <returns>A list of all valid story event paths.</returns>
  public List<string> GetAllStoryEventPaths()
  {
    List<string> paths = new List<string>();
    
    // Get all public properties of StoryEvents (e.g., chapter1, chapter2)
    foreach (var chapterProperty in Events.GetType().GetProperties())
    {
      object chapterInstance = chapterProperty.GetValue(Events);
      if (chapterInstance != null)
      {
        // Iterate through all boolean properties within each chapter (e.g., EventA, EventB)
        foreach (var eventProperty in chapterInstance.GetType().GetProperties())
        {
          if (eventProperty.PropertyType == typeof(bool))
          {
            paths.Add($"{chapterProperty.Name}.{eventProperty.Name}");
          }
        }
      }
    }
    return paths;
  }
}