using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovementController
{
    public bool isMoving = false;
    private Vector3 _MovementPath;

    void HandleAnimation()
    {
        if (_Animator != null)
        {
            _Animator.SetBool("moving", isMoving);

            if (isMoving)
            {
                _Animator.SetFloat("input_x", _MovementPath.x);
                _Animator.SetFloat("input_y", _MovementPath.y);
            }
        }
        else
        {
            Debug.LogWarning("NO Animator to animate!");
        }
    }
    protected override void Move()
    {
        HandleAnimation();
        if (TileMove.SmoothMovement)
        {
            _MovementPath.x = Input.GetAxisRaw("Horizontal");
            _MovementPath.y = Input.GetAxisRaw("Vertical");

            isMoving  = (_MovementPath != Vector3.zero) ? true : false;

            if (isMoving)
            {
                transform.position += _MovementPath * Time.deltaTime * _TileMove.Speed;
            }
            _TileMove.CheckArea(transform);

            //_Collider.enabled = true;
            //_Collider.isTrigger = true;
            //if (Input.GetKey(KeyCode.UpArrow)) { transform.position += Vector3.up * Time.deltaTime * _Speed; }
            //if (Input.GetKey(KeyCode.RightArrow)) { transform.position += Vector3.right * Time.deltaTime * _Speed; }
            //if (Input.GetKey(KeyCode.DownArrow)) { transform.position += Vector3.down * Time.deltaTime * _Speed; }
            //if (Input.GetKey(KeyCode.LeftArrow)) { transform.position += Vector3.left * Time.deltaTime * _Speed; }
        }
        else
        {
            //_Collider.isTrigger = false;
            //_Collider.enabled = false;
            if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.DownArrow)) { _TileMove.Move(transform, MoveDirection.UP, _TileMove.Speed); }
            if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.LeftArrow)) { _TileMove.Move(transform, MoveDirection.RIGHT, _TileMove.Speed); }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.UpArrow)) { _TileMove.Move(transform, MoveDirection.DOWN, _TileMove.Speed); }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.RightArrow)) { _TileMove.Move(transform, MoveDirection.LEFT, _TileMove.Speed); }
            //UpLeft/UpRight/DownRight/DownLeft
            if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.DownArrow)) { _TileMove.Move(transform, MoveDirection.UP_LEFT, _TileMove.Speed); }
            if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.DownArrow)) { _TileMove.Move(transform, MoveDirection.UP_RIGHT, _TileMove.Speed); }
            if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.UpArrow)) { _TileMove.Move(transform, MoveDirection.DOWN_RIGHT, _TileMove.Speed); }
            if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.UpArrow)) { _TileMove.Move(transform, MoveDirection.DOWN_LEFT, _TileMove.Speed); }
        }

        if (Input.GetKeyDown(KeyCode.Space)) { Debug.Log(_TileMove.Up?.tag); }
    }
}