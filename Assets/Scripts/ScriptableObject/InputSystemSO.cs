using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/InputSystem",order = 2)]
public class InputSystemSO : ScriptableObject
{
    public bool MoveLeft => Input.GetKeyDown(KeyCode.A);
    public bool MoveRight => Input.GetKeyDown(KeyCode.D);
    public bool HardDrop => Input.GetKeyDown(KeyCode.Space);
}
