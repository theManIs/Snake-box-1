﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake_box
{
    public sealed class InputController : IExecute
    {
        #region Private Data

        private KeyCode _left = KeyCode.A;
        private KeyCode _right = KeyCode.D;
        private KeyCode _up = KeyCode.W;
        private KeyCode _down = KeyCode.S;
        private KeyCode _e = KeyCode.E;


        #endregion

        private readonly CharacterData _characterData;
#if UNITY_IOS || UNITY_ANDROID
        private float _minDistanceForSwipe = 20;

        private Vector2 _fingerDownPosition;
        private Vector2 _fingerUpPosition;

        private Vector2 _fingerMovement => _fingerUpPosition - _fingerDownPosition;
#endif

        public InputController()
        {
            _characterData = Data.Instance.Character;           
        }

        #region IExecute

        public void Execute()
        {
            Direction direction = Direction.None;
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR || UNITY_WSA
            if (Input.GetKeyDown(_left))
            {
                direction = Direction.Left;
            }
            if (Input.GetKeyDown(_right))
            {
                direction = Direction.Right;
            }
            if (Input.GetKeyDown(_up))
            {
                direction = Direction.Up;
            }
            if (Input.GetKeyDown(_down))
            {
                direction = Direction.Down;
            }
            if (Input.GetKeyDown(_e))
            {
                var point = Services.Instance.LevelService.CharacterBehaviour.GetPoint();
                if (point != null)
                {
                    Data.Instance.TurretData.AddNewWithParent(point);
                } 
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        _fingerDownPosition = touch.position;
                        _fingerUpPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Ended:
                        _fingerUpPosition = touch.position;
                        if(_fingerMovement.sqrMagnitude >= _minDistanceForSwipe * _minDistanceForSwipe)
                        {
                            if(Mathf.Abs(_fingerMovement.x) > Mathf.Abs(_fingerMovement.y))
                            {
                                if (_fingerMovement.x > 0)
                                    direction = Direction.Right;
                                else
                                    direction = Direction.Left;
                            }
                            else
                            {
                                if (_fingerMovement.y > 0)
                                    direction = Direction.Up;
                                else
                                    direction = Direction.Down;
                            }
                        }
                        break;
                }
            }
#endif
            Services.Instance.LevelService.CharacterBehaviour.InputMove(direction);
            if (Input.GetKey(AxisManager.ESCAPE))
            {
                SceneManager.LoadScene(0);
            }                   
        }

#endregion
    }
}
