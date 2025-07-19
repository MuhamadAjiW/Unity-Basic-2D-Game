using UnityEngine;
using static ConfigurationManager;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI; // Added for ScrollRect
using System.Reflection; // Added for PropertyInfo

public class DeveloperConsole : MonoBehaviour
{
    public static DeveloperConsole Instance { get; private set; }

    [SerializeField] private GameObject consolePanel;
    [SerializeField] private TextMeshProUGUI consoleOutputText;
    [SerializeField] private TMP_InputField commandInputField;

    private bool consoleActive = false;
    private List<string> commandHistory = new List<string>();
    private int historyIndex = -1;
    private ScrollRect consoleScrollRect; // New field to store ScrollRect reference

    private delegate void ConsoleCommand(string[] args);
    private Dictionary<string, ConsoleCommand> commands = new Dictionary<string, ConsoleCommand>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Application.logMessageReceived += HandleLog; // Subscribe to Unity's log messages
        }

        consolePanel.SetActive(false);
        RegisterCommands();

        // Get and store the ScrollRect reference from consolePanel
        consoleScrollRect = consolePanel.GetComponent<ScrollRect>();
        if (consoleScrollRect != null && consoleOutputText != null)
        {
            consoleScrollRect.content = consoleOutputText.rectTransform;
        }
        else
        {
            Debug.LogError("DeveloperConsole: ScrollRect or ConsoleOutputText not found for content assignment on consolePanel.");
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog; // Unsubscribe when destroyed
    }

    private void Update()
    {
        if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.ToggleConsole))
        {
            ToggleConsole();
        }

        if (consoleActive)
        {
            // Pause game input when console is active
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }

            if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.Confirm))
            {
                ProcessCommand(commandInputField.text);
                commandInputField.text = "";
                commandInputField.ActivateInputField(); // Keep input field focused
            }
            else if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.HistoryUp))
            {
                NavigateHistory(1);
            }
            else if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.HistoryDown))
            {
                NavigateHistory(-1);
            }
        }
        else
        {
            // Resume game input when console is inactive
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }

    private void ToggleConsole()
    {
        consoleActive = !consoleActive;
        consolePanel.SetActive(consoleActive);

        if (consoleActive)
        {
            commandInputField.ActivateInputField();
            commandInputField.Select();
        }
        else
        {
            commandInputField.DeactivateInputField();
        }
    }

    private void RegisterCommands()
    {
        commands.Add("story", CmdStory);
        commands.Add("debug", CmdDebug);
        commands.Add("help", CmdHelp);
        // Add more commands here
    }

    private void ProcessCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        LogOutput(">" + input);
        commandHistory.Insert(0, input); // Add to history
        if (commandHistory.Count > 20) commandHistory.RemoveAt(commandHistory.Count - 1); // Limit history size
        historyIndex = -1; // Reset history index

        string[] parts = input.Split(' ');
        string commandName = parts[0].ToLower();
        string[] args = parts.Skip(1).ToArray();

        if (commands.ContainsKey(commandName))
        {
            commands[commandName].Invoke(args);
        }
        else
        {
            LogOutput("Unknown command: " + commandName);
        }
    }

    private void NavigateHistory(int direction)
    {
        if (commandHistory.Count == 0) return;

        historyIndex += direction;
        historyIndex = Mathf.Clamp(historyIndex, 0, commandHistory.Count - 1);

        commandInputField.text = commandHistory[historyIndex];
        commandInputField.MoveTextEnd(false); // Move cursor to end
    }

    private void LogOutput(string message)
    {
        consoleOutputText.text += message + "\n";
        // Scroll to bottom
        consoleOutputText.rectTransform.ForceUpdateRectTransforms();
        Canvas.ForceUpdateCanvases(); // Corrected: Static call

        if (consoleScrollRect != null && consoleScrollRect.content != null) // Use the stored reference
        {
            consoleScrollRect.verticalNormalizedPosition = 0f;
        }
        else
        {
            Debug.LogWarning("DeveloperConsole: Stored ScrollRect or its content is not assigned. Cannot scroll output.");
        }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string formattedLog = "";
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                formattedLog = $"<color=red>ERROR: {logString}</color>";
                break;
            case LogType.Warning:
            case LogType.Log:
            default:
                formattedLog = logString;
                break;
        }
        LogOutput(formattedLog);
    }

    // --- Console Commands ---

    private void CmdStory(string[] args)
    {
        if (StoryManager.Instance == null)
        {
            LogOutput("Error: StoryManager not found.");
            return;
        }

        if (args.Length == 0)
        {
            LogOutput("--- All Story Conditions ---");
            // Iterate through all properties of StoryEvents (e.g., chapter1, chapter2)
            // Iterate through all properties of StoryEvents (e.g., chapter1, chapter2)
            foreach (var chapterProperty in StoryManager.Instance.Events.GetType().GetProperties())
            {
                object chapterInstance = chapterProperty.GetValue(StoryManager.Instance.Events);
                if (chapterInstance != null)
                {
                    LogOutput($"Category: {chapterProperty.Name}");
                    // Iterate through all boolean properties within each chapter (e.g., eventA, eventB)
                    foreach (var eventProperty in chapterInstance.GetType().GetProperties())
                    {
                        if (eventProperty.PropertyType == typeof(bool))
                        {
                            string eventPath = $"{chapterProperty.Name}.{eventProperty.Name}";
                            bool state = StoryManager.Instance.GetEventState(eventPath);
                            LogOutput($"  - {eventProperty.Name}: {state}");
                        }
                    }
                }
            }
        }
        else if (args.Length == 2)
        {
            string eventPath = $"{args[0]}.{args[1]}"; // Construct the full path
            bool state = StoryManager.Instance.GetEventState(eventPath);
            LogOutput($"Story Event '{eventPath}': {state}");
        }
        else if (args.Length == 3)
        {
            string eventPath = $"{args[0]}.{args[1]}"; // Construct the full path
            bool state;
            if (!bool.TryParse(args[2], out state))
            {
                LogOutput("Invalid state. Use 'true' or 'false'.");
                return;
            }
            StoryManager.Instance.SetEventState(eventPath, state);
        }
        else
        {
            LogOutput("Usage: /story [category] [event] [true/false]");
            LogOutput("       /story [category] [event]");
            LogOutput("       /story");
        }
    }

    private void CmdDebug(string[] args)
    {
        if (args.Length == 0)
        {
            LogOutput("Usage: /debug [message]");
            return;
        }
        LogOutput("DEBUG: " + string.Join(" ", args));
    }

    private void CmdHelp(string[] args)
    {
        LogOutput("--- Available Commands ---");
        foreach (var cmd in commands.Keys)
        {
            LogOutput("/" + cmd);
        }
        LogOutput("------------------------");
    }

    // Example of a command that could use piping (conceptual)
    // private void CmdPipeExample(string[] args)
    // {
    //     // This would involve more complex parsing and execution
    //     // e.g., "/command1 | /command2"
    //     LogOutput("Piping is a conceptual feature and not fully implemented.");
    // }
}