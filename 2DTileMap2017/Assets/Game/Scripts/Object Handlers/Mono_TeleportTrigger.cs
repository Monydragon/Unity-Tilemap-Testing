using UnityEngine;


public class Mono_TeleportTrigger : Mono_BaseTriggerObject
{
    public bool m_Teleport = true;
    public bool m_DisplayMessage = true;
    public string m_DisplayMessageText;
    public Vector3 m_TeleportLocation;

    public override void CustomTrigger(GameObject source, GameObject target)
    {
        if (m_Teleport)
        {
//                var location = m_TeleportLocation[Random.Range(0, m_TeleportLocation.Length)]; 
            Teleport(source, target, m_TeleportLocation);
        }

        if (m_DisplayMessage)
        {
            DisplayMessage(source, target, m_DisplayMessageText);
        }

    }

    public virtual void Teleport(GameObject source, GameObject target, Vector3 teleport_location)
    {
        Debug.Log(source.name + " Teleports to: " + teleport_location); //TODO: RemoveField later after debugging.
        target.transform.position = teleport_location;
    }

    public virtual void DisplayMessage(GameObject source, GameObject target, string message_text)
    {
        Debug.Log(target.name + ": " + message_text); //TODO: RemoveField later after debugging.
    }

}
