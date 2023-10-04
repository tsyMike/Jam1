using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    //Basic Movement variables
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float SlowMoveSpeed = 0.5f; //Si relentizados la velocidad se multiplica por este parametro.

     //Basic Animation Variables
    private Animator animator;
    private SpriteRenderer sr;

    //Colision System variables
    public float colisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    [SerializeField] private LayerMask obstacleLayer;

    // public GameObject GameoverScreen;

    //------Dash System Variables
    private Vector2 lastMovedDirection;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        // if(!isAlive){
        //     if (!GameoverScreen.active)
        //     {
        //         GameoverScreen.SetActive(true);
        //     }
            
        //     return;
        // }
        

        if (moveInput != Vector2.zero)
        {
            bool success = MovePlayer(moveInput);

            //try moving up or down when facing a wall
            if (!success)
            {
                success = MovePlayer(new Vector2(moveInput.x, 0));

                if (!success)
                {
                    success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }
            animator.SetBool(AnimationStrings.isMoving, success);
        }
        else
        {
            animator.SetBool(AnimationStrings.isMoving, false);
        }



    }

    void OnMove(InputValue value)
    {       
            moveInput = value.Get<Vector2>();
            if (moveInput != Vector2.zero)
            {
                animator.SetFloat(AnimationStrings.dirctionInputX, moveInput.x);
                animator.SetFloat(AnimationStrings.dirctionInputY, moveInput.y);

                setDirection(moveInput);
            }

            lastMovedDirection = moveInput;
        
    }
    public bool _isFacingRight = true;
    public bool isFacingRight { get {return _isFacingRight;} private set{
        if(_isFacingRight != value){
            transform.localScale *= new Vector2(-1,1);
        }
        _isFacingRight = value;
    }} //variables para saber donde ve el personaje para reflejar el objeto
    void setDirection(Vector2 moveInput){
       
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight=true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {   
            
            isFacingRight=false;
        }
    }
    public bool MovePlayer(Vector2 direction)
    {
        int count = rb.Cast(
            direction, //x and y values between -1 and1 that represent the diretion from the body to look for collision
            movementFilter,//The settings that determine where a colission can occur on such layers ot colide with
            castCollisions,//List of collisions to store the found collisions after the cast
            moveSpeed * Time.fixedDeltaTime * colisionOffset//The amount to cast equal to the movement plus 
        );
        Vector2 moveVector=Vector2.zero;
        if (count == 0)
        {   
            
            if (IsSlow()) //----------Si estamos atacando la velocidad de movimiento se divide por la mitad!
            {
                moveVector = direction * moveSpeed* SlowMoveSpeed * Time.fixedDeltaTime;
            }else{
                moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            }
            
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            //print in console the hit object(optional)
            /* foreach (RaycastHit2D hit in castCollisions)
             {
                 print(hit.ToString());
             }
             */
            return false;
        }


    }
    //Verifica si se eta ejecutando la animacion de ataque para reducir la velocidad del jugador
        public bool IsSlow()
        {   
            return animator.GetBool(AnimationStrings.isAttacking);
            
        }
        public bool isAlive
        {
            get{
                return animator.GetBool(AnimationStrings.isAlive);
            }  
        }
}
