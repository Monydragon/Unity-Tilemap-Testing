using UnityEngine;

public interface I_Interactable
{
    /// <summary>
    /// Method invoked to handle interaction on objects.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    void Interact(GameObject source, GameObject target);
}

