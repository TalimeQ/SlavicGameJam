using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement for second player
public class MovementSecond : MonoBehaviour
{
    [SerializeField] string HorizontalInputAxis = "Horizontal_2";
    [SerializeField] string VerticalInputAxis = "Vertical_2";
    [SerializeField] string MouseAxis = "Mouse_2";
    [SerializeField] float RotateSpeed = 100; //only for rotating with arrows
    [SerializeField] float speed = 20;
    Rigidbody2D rigid;
    private Vector2 direction;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(0, 0, Input.GetAxis(MouseAxis)* RotateSpeed * Time.deltaTime);

        direction = new Vector2(Input.GetAxis(HorizontalInputAxis), Input.GetAxis(VerticalInputAxis));
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.fixedDeltaTime);
    }

}
