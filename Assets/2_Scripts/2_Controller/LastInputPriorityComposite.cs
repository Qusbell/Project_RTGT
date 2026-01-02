using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// InputAction에서 Last Input Priority Composite
/// </summary>
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class LastInputPriorityComposite : InputBindingComposite<Vector2>
{
    // 각 방향마다 키 Binding 허용
    [InputControl(layout = "Button")] public int up;
    [InputControl(layout = "Button")] public int down;
    [InputControl(layout = "Button")] public int left;
    [InputControl(layout = "Button")] public int right;

    private float lastX = 0f;
    private float lastY = 0f;

    public override Vector2 ReadValue(ref InputBindingCompositeContext context)
    {
        // ---- up / down ---- //
        bool isUpPressed = context.ReadValueAsButton(up);
        bool isDownPressed = context.ReadValueAsButton(down);
        float y = 0f;

        // 양쪽 키를 누른 경우
        if (isUpPressed && isDownPressed) { y = lastY; }
        // 한쪽 키만 누른 경우
        else if (isUpPressed) { y = 1f;  }
        else if (isDownPressed) { y = -1f; }
        // 어느 쪽 키도 누르지 않은 경우
        else { y = 0f; }
        lastY = y;

        // ---- left / right ---- //
        bool isLeftPressed = context.ReadValueAsButton(left);
        bool isRightPressed = context.ReadValueAsButton(right);
        float x = 0f;

        // 양쪽 키를 누른 경우
        if (isLeftPressed && isRightPressed) { x = lastX; }
        // 한쪽 키만 누른 경우
        else if (isRightPressed) { x = 1f; }
        else if (isLeftPressed) { x = -1f; }
        // 어느 쪽 키도 누르지 않은 경우
        else { x = 0f;}
        lastX = x;

        return new Vector2(x, y);
    }

    static LastInputPriorityComposite() { InputSystem.RegisterBindingComposite<LastInputPriorityComposite>(); }
}
