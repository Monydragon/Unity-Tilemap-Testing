using UnityEngine;

public static class EventManager
{
    /// <summary>
    /// Event Handler for Object Interaction. Source represents the gameobject that invokes the action. and Target represents the target gameobject
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public delegate void ObjectInteractionEventHandler(GameObject source,GameObject target);
    public static event ObjectInteractionEventHandler Event_ObjectInteracted;


    /// <summary>
    /// <para>Used to call upon the <see cref="ObjectInteractionEventHandler"/>. Source represents the gameobject that invokes the action. and Target represents the target gameobject</para>
    /// <para>EXAMPLE: ObjectInteract(this.gameObject,othergameobject); This gameobject is the source and the othergameobjct is the object you are invoking the actions of. </para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void ObjectInteract(GameObject source, GameObject target)
    {
        if (Event_ObjectInteracted != null)
        {
            Event_ObjectInteracted(source,target);
        }
    }
}

