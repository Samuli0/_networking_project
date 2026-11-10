using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Controller : MonoBehaviour
{
    [Range(0.01f, 0.1f)]
    public float MoveDistance = 1.0f;

    [Range(1.0f, 10.0f)]
    public float JumpForce = 5.0f;

    private Rigidbody _rb;

    private ICommand _commandW;
    private ICommand _commandA;
    private ICommand _commandD;
    private ICommand _commandS;
    private ICommand _commandSpaceBar;
    private ICommand _commandUnDo;
    private ICommand _place;
    private ICommand _DoNothing;

    private ICommand _last; // last command
    private ICommand _next;
    private IEnumerator s;

    private Stack<ICommand> _coms = new Stack<ICommand>();
    private Stack<ICommand> _recom = new Stack<ICommand>();

    public int playerID; // assing from server

    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    private bool spawn = false;

    private void HandleInput()
    {

        //_last = null;// no error every update with this here.

        if (Keyboard.current.wKey.isPressed) // W
        {
            //transform.Translate(MoveDistance*Vector3.forward, Space.World);
            _commandW.Execute();
            _last = _commandW;
            _coms.Push(_commandW);
            //Debug.Log("W-Pressed");
            _recom.Clear();
        }
        if (Keyboard.current.aKey.isPressed) // a
        {
            //transform.Translate(MoveDistance * Vector3.left, Space.World);
            _commandA.Execute();
            _last = _commandA;
            _coms.Push(_commandA);
            //Debug.Log("A-Pressed");
            _recom.Clear();
        }
        if (Keyboard.current.dKey.isPressed) // d
        {
            //transform.Translate(MoveDistance * Vector3.right, Space.World);
            _commandD.Execute();
            _last = _commandD;
            _coms.Push(_commandD);
            //Debug.Log("D-Pressed");
            _recom.Clear();
        }
        if (Keyboard.current.sKey.isPressed) // s
        {
            //transform.Translate(MoveDistance * Vector3.back, Space.World);
            _commandS.Execute();
            _last = _commandS;
            _coms.Push(_commandS);
            //Debug.Log("S-Pressed");
            _recom.Clear();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame) // space
        {
            //_rb.AddForce(Vector3.up*JumpForce, ForceMode.Impulse); // jump
            _commandSpaceBar.Execute();
            _last = _commandSpaceBar;
            _coms.Push(_commandSpaceBar);
            //Debug.Log("Space-Pressed");
            _recom.Clear();
        }
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (_coms.Count > 0)
            {
                _last = _coms.Pop();
                _last.Undo();
                _recom.Push(_last);
                //_last = null;
            }
            //Debug.Log("Z-Pressed");
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            if (_recom.Count > 0)
            {
                _last = _recom.Pop();
                _last.Execute(); // w8.. im never using the dam undo commad script here.. oh well..
            }
        }

        SubmitPositionRequestRpc();
    }
    [Rpc(SendTo.Server)]
    private void SubmitPositionRequestRpc(RpcParams rpcParams = default)
    {
        if (!spawn)
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
            spawn = true;
        }
        else
        {
            Position.Value = transform.position + new Vector3(0f, 0f, 1f);
        }
        //Debug.Log("kek");
    }
    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(UnityEngine.Random.Range(-3f, 3f), 1f, UnityEngine.Random.Range(-3f, 3f));
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //*/// -------------- assing camera for player with ID // ID from separate script
        playerID = 0;
        //*///




        _rb = GetComponent<Rigidbody>();

        _commandW = new MoveForwardCommand(transform, MoveDistance); // W
        _commandA = new MoveLeftCommand(transform, MoveDistance); // A
        _commandS = new MoveBackCommand(transform, MoveDistance); // S
        _commandD = new MoveRightCommand(transform, MoveDistance); // D

        _commandSpaceBar = new JumpCommand(_rb, JumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput(); //handle input && move player
    }
}
