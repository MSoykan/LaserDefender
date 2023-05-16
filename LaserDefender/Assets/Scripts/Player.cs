using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Vector2 rawInput;

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        transform.position += delta;
        //Vector2 palyerVelocity = new Vector2(rawInput.x*moveSpeed*Time.deltaTime, rawInput.y*moveSpeed*Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);    
    }
}
