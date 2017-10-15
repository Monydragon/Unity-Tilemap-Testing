using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public abstract class BaseMovementController : MonoBehaviour
{
	[SerializeField] protected TileMovement _TileMove;
	[SerializeField] protected Tilemap _Tilemap;

	protected Rigidbody2D _Rb;
	protected Collider2D _Collider;
	protected Animator _Animator;


	protected TileMovement TileMove
	{
		get { return _TileMove; }
		set { _TileMove = value; }
	}

	protected Tilemap Tilemap
	{
		get { return _Tilemap; }
		set { _Tilemap = value; }
	}

	protected virtual void Awake()
	{
		_Animator = GetComponent<Animator>();
		_Rb = GetComponent<Rigidbody2D>();
		_Collider = GetComponent<Collider2D>();
		if (_Tilemap == null)
		{
			_Tilemap = GetComponentInParent<Tilemap>() ?? GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
		}
	}

	protected abstract void Move();

	protected virtual void Update() { Move(); }

}
