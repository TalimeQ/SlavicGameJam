using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement for second player
public class MovementSecond : MonoBehaviour
{
    [SerializeField] float RotateSpeed = 100; //only for rotating with arrows
    [SerializeField] float JoystickSpeed = 16;
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
        transform.Rotate(0, 0, Input.GetAxis("Rotate_Joystick1") * RotateSpeed * Time.deltaTime);
        direction = new Vector2(Input.GetAxis("Horizontal_Joystick1"), Input.GetAxis("Vertical_Joystick1"));

        if (direction.magnitude > 0 && !audioSource.isPlaying)
            audioSource.Play();
    }

    private void FixedUpdate()
    {
            rigid.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * JoystickSpeed * Time.fixedDeltaTime);
    }

}
