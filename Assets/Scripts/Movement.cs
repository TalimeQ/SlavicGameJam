﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float RotateSpeed = 100; //only for rotating with arrows
    [SerializeField] float keyboardRotationSpeed = 3.0f;
    [SerializeField] float KeyboardSpeed = 8;
    [SerializeField] float PadSpeed = 8;

    Rigidbody2D rigid;
    private Vector2 direction;
    private AudioSource audioSource;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
     //   Debug.Log(Input.GetAxis("Horizontal_Joystick2"));
        if(MovementManager.singleton.Mode != MovementManager.MovementMode.pads)
            direction = new Vector2(Input.GetAxis("Horizontal_Keyboard"), Input.GetAxis("Vertical_Keyboard"));
        else
            direction = new Vector2(Input.GetAxis("Horizontal_Joystick2"), Input.GetAxis("Vertical_Joystick2"));

        if(direction.magnitude > 0 && !audioSource.isPlaying)
            audioSource.Play();

    }



    

    private void FixedUpdate()
    {
        float choosenSpeed;
        if (MovementManager.singleton.Mode != MovementManager.MovementMode.pads)
            choosenSpeed = KeyboardSpeed;
        else
            choosenSpeed = PadSpeed;

        rigid.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * KeyboardSpeed * Time.fixedDeltaTime);


        if(MovementManager.singleton.Mode != MovementManager.MovementMode.pads)
        {
            //  Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //diff.Normalize();
            //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            float rotationChange = Input.GetAxis("Vertical");
            Vector3 oldRot = transform.rotation.eulerAngles;
            oldRot += new Vector3(0, 0, 1) * rotationChange * RotateSpeed * Time.deltaTime * keyboardRotationSpeed;
            transform.rotation = Quaternion.Euler(oldRot);
        }
        else
            transform.Rotate(0, 0, Input.GetAxis("Rotate_Joystick2") * RotateSpeed * Time.deltaTime);
    }
}
