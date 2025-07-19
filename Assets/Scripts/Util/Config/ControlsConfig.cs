using UnityEngine;

[CreateAssetMenu(fileName = "ControlsConfig", menuName = "ScriptableObjects/Controls Config", order = 1)]
public class ControlsConfig : ScriptableObject
{
    [Header("Player Movement")]
    public KeyCode MoveUp = KeyCode.W;
    public KeyCode MoveLeft = KeyCode.A;
    public KeyCode MoveRight = KeyCode.D;
    public KeyCode MoveDown = KeyCode.S;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Sprint = KeyCode.LeftShift;
    public KeyCode Stance = KeyCode.LeftControl; // Assuming this is the "stance" control mentioned

    [Header("Player Actions")]
    public KeyCode Attack = KeyCode.Mouse0; // Left Mouse Button
    public KeyCode Interact = KeyCode.E;
    public KeyCode Dash = KeyCode.F;

    [Header("UI Controls")]
    public KeyCode ToggleConsole = KeyCode.Slash; // The '/' key for console activation
    public KeyCode Confirm = KeyCode.Return; // For general confirmation/dialogue progression
    public KeyCode Cancel = KeyCode.Escape; // For pausing/cancelling

    [Header("Console Navigation")]
    public KeyCode HistoryUp = KeyCode.UpArrow;
    public KeyCode HistoryDown = KeyCode.DownArrow;
}