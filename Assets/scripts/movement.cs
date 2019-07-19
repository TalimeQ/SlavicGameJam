using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] string HorizontalInputAxis = "Horizontal";
    [SerializeField] string VerticalInputAxis = "Vertical";
    [SerializeField] float speed = 20;
    Rigidbody2D rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Vector2 direction = new Vector2(Input.GetAxis(HorizontalInputAxis), Input.GetAxis(VerticalInputAxis));
        rigid.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime);
    }
}
