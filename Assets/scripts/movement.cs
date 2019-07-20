using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] string HorizontalInputAxis = "Horizontal";
    [SerializeField] string VerticalInputAxis = "Vertical";
    [SerializeField] float speed = 20;
    Rigidbody2D rigid;
    private Vector2 direction;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        direction = new Vector2(Input.GetAxis(HorizontalInputAxis), Input.GetAxis(VerticalInputAxis));
    }
    

    private void FixedUpdate()
    {
        rigid.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.fixedDeltaTime);


        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
    }
}
