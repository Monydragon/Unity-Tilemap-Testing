using System;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Stopwatch = System.Diagnostics.Stopwatch;

public class EnemyMovement : BaseMovementController
{
    Stopwatch sw = new Stopwatch();
    public long TimerSpeedMS = 500;
    public MoveDirection StartDirection = MoveDirection.UP;
    public MoveDirection CurrentDirection = MoveDirection.NONE;
    private bool rolled;

    void Start()
    {
        CurrentDirection = StartDirection;
    }
    protected override void Move()
    {

        //if(CurrentDirection == MoveDirection.NONE) { CurrentDirection = StartDirection; }

        if (rolled && (_TileMove.Up && _TileMove.Right && _TileMove.Down && _TileMove.Left))
        {
            var randRoll = UnityEngine.Random.Range(1, 5);
            CurrentDirection = (MoveDirection)randRoll;
            Debug.Log("ROLLED " + randRoll);
            rolled = true;
            sw.Restart();
            _TileMove.CheckArea(transform);

        }
            //{
            //    var randRoll = UnityEngine.Random.Range(1, 4);
            //    CurrentDirection = (MoveDirection)randRoll;
            //    Debug.Log("ROLLED " + randRoll);
            //    rolled = true;
            //}
        if (sw.ElapsedMilliseconds > TimerSpeedMS && rolled)
        {
            //if (_TileMove.Up || _TileMove.Right || _TileMove.Down || _TileMove.Left)
            //{
            //    if (!_TileMove.Up) { CurrentDirection = MoveDirection.UP; }
            //    if (!_TileMove.Right) { CurrentDirection = MoveDirection.RIGHT; }
            //    if (!_TileMove.Down) { CurrentDirection = MoveDirection.DOWN; }
            //    if (!_TileMove.Left) { CurrentDirection = MoveDirection.LEFT; }
            //    rolled = false;
            //}


            _TileMove.Move(transform, CurrentDirection, _TileMove.Speed);
            //if (!_TileMove.Right) { CurrentDirection = MoveDirection.RIGHT;}
            //if (!_TileMove.Down) { CurrentDirection = MoveDirection.DOWN; }
            //if (!_TileMove.Left) { CurrentDirection = MoveDirection.LEFT; }
            //if (!_TileMove.UpRight) { CurrentDirection = MoveDirection.UP_RIGHT; }
            //if (!_TileMove.UpLeft) { CurrentDirection = MoveDirection.UP_LEFT; }
            //if (!_TileMove.DownRight) { CurrentDirection = MoveDirection.DOWN_RIGHT; }
            //if (!_TileMove.DownLeft) { CurrentDirection = MoveDirection.DOWN_LEFT; }


        }
    }

    private void OnTriggerEmter2D(Collider2D collision)
    {
        sw.Restart();
            _TileMove.CheckArea(transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sw.Restart();
            _TileMove.CheckArea(transform);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            _TileMove.CheckArea(transform);
        if (!rolled && sw.ElapsedMilliseconds < TimerSpeedMS)
        {
            var randRoll = UnityEngine.Random.Range(1, 5);
            CurrentDirection = (MoveDirection)randRoll;
            Debug.Log("ROLLED " + randRoll);
            rolled = true;
            Debug.Log(collision.name);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
            _TileMove.CheckArea(transform);
         if (!rolled && sw.ElapsedMilliseconds < TimerSpeedMS)
        {
            var randRoll = UnityEngine.Random.Range(1, 5);
            CurrentDirection = (MoveDirection)randRoll;
            Debug.Log("ROLLED " + randRoll);
            rolled = true;
            Debug.Log(collision.gameObject.name);
        }

    }


}