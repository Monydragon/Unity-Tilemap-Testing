using UnityEngine;

    /// <summary>
    /// This is the Abstract Intractable Object that inherits from <see cref="I_Interactable"/> can be used to customize behavior for interactions.
    /// </summary>
    public abstract class BaseInteractableObject : MonoBehaviour, I_Interactable
    {
        [SerializeField]protected bool _Interactable = true;
        /// <summary>
        /// Enables/Disables Object Interaction. When enabled <see cref="Interact"/> can be invoked properly if set to base defaults.
        /// </summary>
        public virtual bool Interactable { get { return _Interactable; } set { _Interactable = value; } }

        private void OnEnable()
        {
            EventManager.Event_ObjectInteracted += Interact;
        }

        private void OnDisable()
        {
            EventManager.Event_ObjectInteracted -= Interact;
        }

        /// <summary>
        /// Interaction call on the object that is called from the source to the target. You can override this to handle interaction differently. Source represents the gameobject that invokes the action. and Target represents the target gameobject.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public virtual void Interact(GameObject source, GameObject target)
        {
            if (source != null && target == this.gameObject)
            {
                if (_Interactable)
                {
                    HandleInteraction(source, target);
                }
            }
        }

        /// <summary>
        /// Used to invoke actions when <see cref="Interact"/> is called. Must be set so that interaction is handled by all inheriting objects.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public abstract void HandleInteraction(GameObject source, GameObject target);

    }

