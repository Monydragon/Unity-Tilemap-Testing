using System;
using UnityEngine;

[Serializable]
public class TileMovement
{
    public MoveDirection _FacingDirection = MoveDirection.UP;
    public bool Use8Directional = true;
    public bool SmoothMovement = true;
    public float Speed = 3f;
    public LayerMask ColliderMask;
    public float RayCheckDistance = 0.75f;
    public Collider2D Up;
    public Collider2D Right;
    public Collider2D Down;
    public Collider2D Left;
    //8 Directional
    public Collider2D UpLeft;
    public Collider2D UpRight;
    public Collider2D DownRight;
    public Collider2D DownLeft;

    public void Move(Transform _transform, MoveDirection _direction, float _distance = -1.0f, bool _checkArea = true)
    {
        _FacingDirection = _direction;
        if (_checkArea) { CheckArea(_transform); }
        if ((int)_distance == -1) { _distance = Speed; }
        if (SmoothMovement) { _distance = _distance * Time.deltaTime; }
        
        Vector3 endPos = new Vector3(_transform.position.x,_transform.position.y,_transform.position.z);

        switch (_direction)
        {
            //4 Directional
            case MoveDirection.UP: if(!Up) endPos += Vector3.up * _distance; break;
            case MoveDirection.RIGHT: if (!Right) endPos += Vector3.right * _distance; break;
            case MoveDirection.DOWN: if (!Down) endPos +=  Vector3.down * _distance; break;
            case MoveDirection.LEFT: if (!Left) endPos +=  Vector3.left * _distance; break;
            //8 Directional
            case MoveDirection.UP_LEFT: if(!UpLeft && Use8Directional) endPos += (Vector3.up + Vector3.left) * _distance; break; //if (!UpLeft && Use8Directional) { _transform.position += (Vector3.up + Vector3.left) * _distance; } break;
            case MoveDirection.UP_RIGHT: if(!UpRight && Use8Directional) endPos += (Vector3.up + Vector3.right) * _distance; break; //if (!UpRight && Use8Directional) { _transform.position += (Vector3.up + Vector3.right) * _distance; } break;
            case MoveDirection.DOWN_RIGHT: if (!DownRight && Use8Directional) endPos += (Vector3.down + Vector3.right) * _distance; break; //if (!DownRight && Use8Directional) { _transform.position += (Vector3.down + Vector3.right) * _distance; } break;
            case MoveDirection.DOWN_LEFT: if (!DownLeft && Use8Directional) endPos += (Vector3.down + Vector3.left) * _distance; break; // if (!DownLeft && Use8Directional) { _transform.position += (Vector3.down + Vector3.left) * _distance; } break;
        }

        //if (Up != null) { if(Up.transform.position == endPos && !Up) { _transform.position = endPos; } }
        //if (Right != null) { if (Right.transform.position == endPos && !Right) { _transform.position = endPos; } }
        //if (Down != null) { if (Down.transform.position == endPos && !Down) { _transform.position = endPos; } }
        //if (UpLeft != null) { if (UpLeft.transform.position == endPos && !UpLeft) { _transform.position = endPos; } }
        //if (UpRight != null) { if (UpRight.transform.position == endPos && !UpRight) { _transform.position = endPos; } }
        //if (DownRight != null) { if (DownRight.transform.position == endPos && !DownRight) { _transform.position = endPos; } }
        //if (DownLeft != null) { if (DownLeft.transform.position == endPos && !DownLeft) { _transform.position = endPos; } }


        _transform.position = endPos;

        //if (Up?.transform.position == endPos ||
        //    Right?.transform.position == endPos ||
        //    Down?.transform.position == endPos ||
        //    Left?.transform.position == endPos ||
        //    UpLeft?.transform.position == endPos ||
        //    UpRight?.transform.position == endPos ||
        //    DownRight?.transform.position == endPos ||
        //    DownLeft?.transform.position == endPos) { Debug.Log($"Cannot move to occupying area pos: {endPos}"); return; }
        
        ;
        if (_checkArea) { CheckArea(_transform); }

    }

    public void CheckArea(Transform _transform)
    {
        //4 Directional
        Vector3 UpDir = _transform.TransformDirection(Vector2.up * RayCheckDistance);
        Vector3 RightDir = _transform.TransformDirection(Vector2.right * RayCheckDistance);
        Vector3 DownDir = _transform.TransformDirection(Vector2.down * RayCheckDistance);
        Vector3 LeftDir = _transform.TransformDirection(Vector2.left * RayCheckDistance);

#if DEBUG || UNITY_EDITOR
        Debug.DrawRay(_transform.position, UpDir, Color.blue);
        Debug.DrawRay(_transform.position, RightDir, Color.blue);
        Debug.DrawRay(_transform.position, DownDir, Color.blue);
        Debug.DrawRay(_transform.position, LeftDir, Color.blue);
#endif

        RaycastHit2D U_hit = Physics2D.Raycast(_transform.position, UpDir, RayCheckDistance, ColliderMask);
        RaycastHit2D R_hit = Physics2D.Raycast(_transform.position, RightDir, RayCheckDistance, ColliderMask);
        RaycastHit2D D_hit = Physics2D.Raycast(_transform.position, DownDir, RayCheckDistance, ColliderMask);
        RaycastHit2D L_hit = Physics2D.Raycast(_transform.position, LeftDir, RayCheckDistance, ColliderMask);

        Up = U_hit.collider;
        Right = R_hit.collider;
        Down = D_hit.collider;
        Left = L_hit.collider;

        if (Use8Directional)
        {
            //8 Directional
            Vector3 UpLeftDir = _transform.TransformDirection((Vector2.left + Vector2.up) * RayCheckDistance);
            Vector3 UpRightDir = _transform.TransformDirection((Vector2.up + Vector2.right) * RayCheckDistance);
            Vector3 DownRightDir = _transform.TransformDirection((Vector2.down + Vector2.right) * RayCheckDistance);
            Vector3 DownLeftDir = _transform.TransformDirection((Vector2.down + Vector2.left) * RayCheckDistance);

#if DEBUG || UNITY_EDITOR
            Debug.DrawRay(_transform.position, UpLeftDir, Color.blue);
            Debug.DrawRay(_transform.position, UpRightDir, Color.blue);
            Debug.DrawRay(_transform.position, DownRightDir, Color.blue);
            Debug.DrawRay(_transform.position, DownLeftDir, Color.blue);
#endif

            RaycastHit2D UL_hit = Physics2D.Raycast(_transform.position, UpLeftDir, RayCheckDistance, ColliderMask);
            RaycastHit2D UR_hit = Physics2D.Raycast(_transform.position, UpRightDir, RayCheckDistance, ColliderMask);
            RaycastHit2D DR_hit = Physics2D.Raycast(_transform.position, DownRightDir, RayCheckDistance, ColliderMask);
            RaycastHit2D DL_hit = Physics2D.Raycast(_transform.position, DownLeftDir, RayCheckDistance, ColliderMask);

            UpLeft = UL_hit.collider;
            UpRight = UR_hit.collider;
            DownRight = DR_hit.collider;
            DownLeft = DL_hit.collider;
        }
    }

    public GameObject Interact()
    {
        GameObject go = null;
        switch (_FacingDirection)
        {
            case MoveDirection.UP: go = Up?.gameObject; break;
            case MoveDirection.RIGHT: go = Right?.gameObject; break;
            case MoveDirection.DOWN: go = Down?.gameObject; break;
            case MoveDirection.LEFT: go = Left?.gameObject; break;
            case MoveDirection.UP_LEFT: go = UpLeft?.gameObject; break;
            case MoveDirection.UP_RIGHT: go = UpRight?.gameObject; break;
            case MoveDirection.DOWN_RIGHT: go = DownRight?.gameObject; break;
            case MoveDirection.DOWN_LEFT: go = DownLeft?.gameObject; break;
        }

        return go;
    }
}