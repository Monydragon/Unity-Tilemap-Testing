using UnityEngine;

/// <summary>
/// Inherits from <see cref="BaseInteractableObject"/> With methods for basic Item Handling.
/// </summary>
public class Mono_BaseItemObject : BaseInteractableObject
{
    public bool m_Exmainable = true;
    public bool m_Usable = true;

    public string m_ExamineText;
        
    public override void HandleInteraction(GameObject source, GameObject target)
    {
        if (m_Exmainable)
        {
            Examine(source, target);
        }
        if (m_Usable)
        {
            Use(source,target);
        }

    }

    /// <summary>
    /// This is the Examine method. Used to examine an object the examine description provided by <see cref="m_ExamineText"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public virtual void Examine(GameObject source, GameObject target)
    {
        Debug.Log(target.name + ": " + m_ExamineText);
    }

    /// <summary>
    /// This is the Use method. Used to use an object.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public virtual void Use(GameObject source, GameObject target)
    {
        Debug.Log(source.name + " used " + target.name);
    }

}