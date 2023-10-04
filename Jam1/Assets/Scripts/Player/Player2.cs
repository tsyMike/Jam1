using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
   


    //Basic Movement variables
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float SlowMoveSpeed = 0.5f; //Si estamos atacando la velocidad se multiplica por este parametro.
    

    //Basic Animation Variables
    private Animator animator;
    private SpriteRenderer sr;

    //Colision System variables
    public float colisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    //------Dash System Variables
    private Vector2 lastMovedDirection;
    private bool canDash = true;
    private bool isDashing = false;
    //dash sound
    public AudioClip soundToplay;
    public float volume=1f;

    [SerializeField]
    private float dashDistance = 1f;
    //public int dashCount=2;
    [SerializeField]
    private LayerMask obstacleLayer;

    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    private float dashCD = 0.3f;

    // [SerializeField]
    // private TrailRenderer tr;

    //Other dash system variables
    //public float dashSpeed=10f;
    //public float dashDuration=1f;

    //public GameObject GameoverScreen;
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
        if (isDashing)
        {
            return;
        }

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
    
    void OnDash()
    {
        if (canDash)
        {
            //StartCoroutine(Dash(dashDuration));
            StartCoroutine(Dash2());
        }
    }

    public Camera cam;
    void OnAttack()
    {

        Vector2 objPos = rb.transform.position;//gets player position
        Vector2 mousePos = Input.mousePosition;//gets mouse postion
        mousePos = cam.ScreenToWorldPoint(mousePos);
        float mousePosX = mousePos.x - objPos.x;//gets the distance between object and mouse position for x
        float mousePosY = mousePos.y - objPos.y;//gets the distance between object and mouse position for y  i
        
        setDirection(new Vector2(mousePosX,0f));
        animator.SetFloat(AnimationStrings.mouseClickX, mousePosX);
        animator.SetFloat(AnimationStrings.mouseClickY, mousePosY);

        animator.SetTrigger(AnimationStrings.attackTrigger);
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

    //Dash system using velocity intead of transfor position (requires a dinamic rigidbody to work)
    /*IEnumerator Dash(float currentDashTime){
      canDash=false;
      isDashing=true;
      movementFilter.useLayerMask=true;
      rb.velocity= new Vector2(lastMovedDirection.x*dashSpeed,lastMovedDirection.y*dashSpeed);
    Debug.Log("Dash!");
      yield return new WaitForSeconds(currentDashTime);
      movementFilter.useLayerMask=false;  
      rb.velocity=new Vector2(0f,0f);
      isDashing=false;
      
      yield return new WaitForSeconds(dashCD);

      
      canDash=true;

    Debug.Log("CanDash");
    }*/

    IEnumerator Dash2()
    {
        canDash = false;
        isDashing = true;

        //tr.emitting = true;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, lastMovedDirection, dashDistance * colisionOffset * 1.3f, obstacleLayer);
        if (hit)
        {
            if (hit.fraction > 0.09f)
            {
                transform.position += (Vector3)(lastMovedDirection) * dashDistance * hit.fraction * 0.9f;
            }

        }
        else
        {
            transform.position += (Vector3)(lastMovedDirection) * dashDistance;
            AudioSource.PlayClipAtPoint(soundToplay,animator.gameObject.transform.position,volume);
        }

        //Debug.Log("Dash!");

        isDashing = false;

        yield return new WaitForSeconds(dashCD);

        //tr.emitting = false;

        canDash = true;
    }
    
    //Verifica si se esta ejecutando la animacion de ataque para reducir la velocidad del jugador
    public bool IsSlow()
    {   
        return animator.GetBool(AnimationStrings.isAttacking);
        
    }
    public bool isAlive{
        get{
            return animator.GetBool(AnimationStrings.isAlive);
        }  
    }


}


