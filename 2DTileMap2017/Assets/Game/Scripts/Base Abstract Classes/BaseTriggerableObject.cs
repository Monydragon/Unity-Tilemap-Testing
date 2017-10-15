using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the Abstract Triggerable Object that inherits from <see cref="I_Triggerable"/> can be used to customize behavior for triggers.
/// </summary>
[RequireComponent(typeof(Collider))]
public abstract class BaseTriggerableObject : MonoBehaviour, I_Triggerable
{
    //Protected Fields
    /// <summary>
    /// protected UnityEventHandler handling for trigger actions.
    /// </summary>
    [SerializeField]
    protected UnityEvent _UnityEventHandler;

    /// <summary>
    /// This is the protected Gameobject that is currently in the trigger zone.
    /// </summary>
    [SerializeField] protected GameObject _CollidedGameObject;

    /// <summary>
    /// protected bool handling for trigger actions and input.
    /// </summary>
    [SerializeField]
    protected bool _Triggerable = true, _AutoTrigger = true, _UseEventHandler = true, _AllowMultiTrigger, _isTriggered;

    [SerializeField] protected int _TriggersAllowed, _CurrentTriggerCount;

    //Properties
    public UnityEvent UnityEventHandler { get { return _UnityEventHandler; } set { _UnityEventHandler = value; } } //TODO: Write a custom Event Handler?
    public virtual bool Triggerable { get { return _Triggerable; } set { _Triggerable = value; } }
    public virtual bool IsTriggered { get { return _isTriggered; } set { _isTriggered = value; } }
    public virtual bool AutoTrigger { get { return _AutoTrigger; } set { _AutoTrigger = value; } }
    public virtual bool AllowMultiTrigger { get { return _AllowMultiTrigger; } set { _AllowMultiTrigger = value; } }
    public int TriggersAllowed { get { return _TriggersAllowed; } set { _TriggersAllowed = value; } }
    public int CurrentTriggerCount { get { return _TriggersAllowed; } }
    public virtual bool UseEventHandler { get { return _UseEventHandler; } set { _UseEventHandler = value; } }
    public GameObject CollidedGameObject { get { return _CollidedGameObject; } set { _CollidedGameObject = value; } }

    //Abstract methods.
    public abstract void CustomTrigger(GameObject source, GameObject target);
    public abstract bool CustomTriggerInput();

    public virtual void HandleTrigger()
    {

        if (!_Triggerable || CollidedGameObject == null)
        {
            return;
        }

        _isTriggered = true;

        if ( _isTriggered)
        {

            if (UseEventHandler && _UnityEventHandler != null)
            {
                UnityEventHandler.Invoke();
            }

            CustomTrigger(gameObject, _CollidedGameObject);
            ++_CurrentTriggerCount;

            if (!AllowMultiTrigger)
            {
                _Triggerable = false;
            }

            if (_CurrentTriggerCount >= _TriggersAllowed)
            {
                if(_TriggersAllowed == 0) { return; }
                _Triggerable = false;
            }

        }

    }

    //Private methods
    private void Awake()
    {
        if (!GetComponent<Collider>().isTrigger)
        {
            Debug.LogWarning("Collider on " + gameObject.name + " not set to trigger. Setting to trigger now.");
            GetComponent<Collider>().isTrigger = true;
        }
    }

    //Virtual Methods
    /// <summary>
    /// By Default this update will call <see cref="HandleTrigger"/> if the Method <see cref="CustomTriggerInput"/> returns true.
    /// </summary>
    public virtual void Update()
    {
        if (CustomTriggerInput())
        {
            HandleTrigger();
        }
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != null)
        {
            _CollidedGameObject = col.gameObject;

            if (_AutoTrigger)
            {
                HandleTrigger();
            }
        }
    }

    public virtual void OnTriggerExit(Collider col)
    {
        _CollidedGameObject = null;

        if (_AutoTrigger && _isTriggered && _AllowMultiTrigger)
        {
            if (_TriggersAllowed == 0)
            {
                _isTriggered = false;
            }
            else if (_CurrentTriggerCount <= TriggersAllowed)
            {
                _isTriggered = false;
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != null)
        {
            _CollidedGameObject = col.gameObject;

            if (_AutoTrigger)
            {
                HandleTrigger();
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D col)
    {
        _CollidedGameObject = null;

        if (_AutoTrigger && _isTriggered && _AllowMultiTrigger)
        {
            if (_TriggersAllowed == 0)
            {
                _isTriggered = false;
            }
            else if (_CurrentTriggerCount <= TriggersAllowed )
            {
                _isTriggered = false;
            }
        }
    }
}