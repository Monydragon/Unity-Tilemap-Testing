using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChestScript : BaseInteractableObject
{
	public AudioClip sfx; //TODO: add Audio Manager to play audio clips.
	private Animator anim;
	public bool Open = false;
	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void OpenChest(GameObject source)
	{
		if (source.tag == "Player")
		{
			Debug.Log($"{source.name} Opens {name}");
			anim.SetTrigger(Open ? "Close" : "Open");
			Open = !Open;
		}
	}

	public override void HandleInteraction(GameObject source, GameObject target) => OpenChest(source);

}
