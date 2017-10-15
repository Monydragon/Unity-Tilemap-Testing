using UnityEngine;

/// <summary>
/// Inherits from <see cref="BaseTriggerableObject"/> With methods for basic trigger Handling.
/// </summary>
public class Mono_BaseTriggerObject : BaseTriggerableObject
{
    public bool m_AllowButtonTrigger;
    public KeyCode m_CustomKeyCode;

    public override void CustomTrigger(GameObject source, GameObject target)
    {
        Debug.Log("Custom Trigger!"); //TODO: RemoveField later after debugging.

    }

    public override bool CustomTriggerInput()
    {
        if (m_AllowButtonTrigger && Input.GetKeyDown(m_CustomKeyCode))
        {
            return true;
        }

        return false;
    }

}
