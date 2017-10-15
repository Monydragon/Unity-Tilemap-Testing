using UnityEngine;
using UnityEngine.Events;

public interface I_Triggerable
{
    /// <summary>
    /// This is a Unity Event Handler that is invoked when called by the <see cref="CustomTrigger"/> Method if <see cref="UseEventHandler"/> is enabled.  
    /// </summary>
    UnityEvent UnityEventHandler { get; set; }

    /// <summary>
    /// Bool that handles if the trigger can be activated..
    /// </summary>
    bool Triggerable { get; set; }

    /// <summary>
    /// Bool that handles the trigger status.
    /// </summary>
    bool IsTriggered { get; set; }

    /// <summary>
    /// When Enabled <see cref="IsTriggered"/> will be set when the player first enters the trigger area.
    /// </summary>
    bool AutoTrigger { get; set; }

    /// <summary>
    /// When enabled can be triggered multiple times. if <see cref="AutoTrigger"/> is enabled the trigger will be reset and upon reentry of a collider it will trigger again. 
    /// </summary>
    bool AllowMultiTrigger { get; set; }

    /// <summary>
    /// When <see cref="AllowMultiTrigger"/> is enabled you can specify the amount of triggers allowed. 0 (Default) is unlimited. Any other number will be limited to that number.
    /// </summary>
    int TriggersAllowed { get; set; }

    /// <summary>
    /// The Current trigger count.
    /// </summary>
    int CurrentTriggerCount { get; }

    /// <summary>
    /// When enabled the <see cref="UnityEventHandler"/> will be able to be invoked by the <see cref="CustomTrigger"/> Method
    /// </summary>
    bool UseEventHandler { get; set; }

    /// <summary>
    /// This is the Gameobject that is currently in the trigger zone.
    /// </summary>
    GameObject CollidedGameObject { get; set; }

    /// <summary>
    /// Method invoked to handle trigger actions on objects.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    void CustomTrigger(GameObject source, GameObject target);

    /// <summary>
    /// <para>When this method returns true <see cref="HandleTrigger"/> will be executed in the Update loop.</para>
    /// <para>EXAMPLE:</para>
    /// <para> if (Input.GetKeyDown(KeyCode.T)) return true; </para> 
    /// </summary>
    /// <returns></returns>
    bool CustomTriggerInput();

    /// <summary>
    /// This handles the triggering action. Checking all set conditions and executing accordingly.
    /// </summary>
    void HandleTrigger();

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger (3D physics only)
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col);

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger (3D physics only)
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col);

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col);

    /// <summary>
    /// OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit2D(Collider2D col);
}
