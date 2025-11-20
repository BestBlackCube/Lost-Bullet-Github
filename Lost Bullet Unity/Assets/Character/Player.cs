using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f; 
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = false;    //점프 조건

    public LayerMask groundLayer;
    [SerializeField] private float checkRadius = 0.1f;  //원 크기
    [SerializeField] private float groundOffset = -0.5f;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float Move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(Move * speed, rb.linearVelocityY);

        if(Move > 0) //오른쪽 이동
        {
            transform.localScale = new Vector3(2,2,2);
        }else if (Move < 0) //왼쪽 이동
        {
            transform.localScale = new Vector3(-2,2,2);
        }
        
        if(Input.GetKey(KeyCode.LeftShift)) //달리기
        {
            speed = 8f;
        }else
        {
            speed = 4f;
        }
    
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + groundOffset);
        isGrounded = Physics2D.OverlapCircle(checkPos, checkRadius, groundLayer);
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + groundOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(checkPos, checkRadius);
    }
}
