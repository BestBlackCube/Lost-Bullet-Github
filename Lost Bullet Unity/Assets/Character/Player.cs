using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f; 

    [Header("Jump")]
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    [SerializeField] private float checkRadius = 0.1f;  //원 크기
    [SerializeField] private float groundOffset = -0.5f;   

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float currnetStamina = 0f;
    public float runStamina = 10f;      //달릴때 스태미나 소모값
    public float jumpStamina = 20f;     //점프할때 스태미나 소모값
    public float staminaRegen = 15f;    //스태미나 회복 값 15
    public float staminaDelay = 3f;     //스태미나 회복 시간
    public Slider staminaSlider;

    private Rigidbody2D rb;
    private bool isGrounded = false;    //점프 조건
    private float staminaTimer = 0f;    //스태미나 회복 타이머
    private bool isRunning = false;
    Animator animator;
    

    private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currnetStamina = maxStamina;                //시작 시 스태미나 최대치

        if (staminaSlider != null) staminaSlider.maxValue = maxStamina;
    }

    void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float Move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(Move * speed, rb.linearVelocityY);


        bool tryRun = Input.GetKey(KeyCode.LeftShift) && Move != 0;             //달리기 처리
        if(tryRun && currnetStamina > 0)
        {
            isRunning = true;
            speed = 8f;
            currnetStamina -= runStamina * Time.deltaTime;
            currnetStamina = Mathf.Max(0, currnetStamina);      //0이하로 내려가지 않게
            staminaTimer = 0f;
            animator.SetBool("isRun", true);
        }
        else
        {
            speed = 4f;
            animator.SetBool("isRun", false);
            isRunning = false;
        }

        if(currnetStamina <= 0)
        {
            speed = 4f;
            animator.SetBool("isRun", false);
        }

        if(Move > 0) //오른쪽 이동
        {
            //transform.localScale = new Vector3(2,2,2);
            animator.SetBool("isWalk", true);
            
        }else if (Move < 0) //왼쪽 이동
        {
            //transform.localScale = new Vector3(-2,2,2);
            animator.SetBool("isWalk", true);
        }else
            animator.SetBool("isWalk", false);
        

        // 마우스 인식 받은 후 플레이어 회전
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        if(mousePos.x < transform.position.x)
        {
            sprite.flipX = true;
        }else
            sprite.flipX = false;


        //점프 기능
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + groundOffset);
        isGrounded = Physics2D.OverlapCircle(checkPos, checkRadius, groundLayer);
        if((isGrounded && Input.GetKeyDown(KeyCode.Space)) && currnetStamina >= jumpStamina)       //? 점프가능 스테미너가 있을 경우 점프가 되게 추가
        {
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            
            currnetStamina -= jumpStamina;
            staminaTimer = 0f;
        }

        //? 스테미나 회복 
        if(!isRunning && isGrounded)            //달리거나 점프하지 않을때
        {
            staminaTimer += Time.deltaTime;

            if(staminaTimer >= staminaDelay)    //스태미너 타이머가 회복 시간보다 클 경우
            {
                currnetStamina += staminaRegen * Time.deltaTime;
                currnetStamina = Mathf.Min(maxStamina, currnetStamina);     //최대치 이상 회복 안됨
            }
        }

        if(staminaSlider != null) staminaSlider.value = currnetStamina;
        
        
        //Debug.Log($"current Stamina : {currnetStamina:F1} / {maxStamina}");    
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + groundOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(checkPos, checkRadius);
    }
}
