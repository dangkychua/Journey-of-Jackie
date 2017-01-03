/*  Author: 
 *          JackLu
 *  Date:   
 *          28-Sep-2016
 *  Description: 
 *          Handle behavior of Character (moving,change state,setting healthy,.....)
 *    
*/

using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    private const float minLeft = -8.5f;
    //Properties
    [SerializeField]
    private float speedMovement = 2.0f;
    [SerializeField]
    private bool facingRight = true;
    // speed for character running
    private Rigidbody2D myRigbody2D;
    private Animator myAnimator;
    public bool isJumping = false;
    public GameObject Kunai;

    // Use this for initialization
    void Start()
    {
        myRigbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");

        FlipCharacter(horizontal);
        HandleMovement(horizontal);
        // attack,stading,jumping...
        CheckStateCharacter(horizontal);
    }

    void CheckStateCharacter(float horizontal)
    {
        if (Input.GetMouseButtonDown(0)) //left click -> attack
        {
            myAnimator.SetTrigger("attack");
        }
        if (Input.GetMouseButtonDown(1)) //right click -> throw
        {
            myAnimator.SetTrigger("throw");
            //Init prefab Kunai
            if (transform.Find("Kunai(Clone)") == null)
            {
                GameObject kunai = (GameObject)Instantiate(Kunai,transform);
                //kunai.transform.parent = gameObject.transform;

            }

        }
        if (Input.GetKeyDown("space")) //spacebar -> jump
        {
            //Jumping
            //myAnimator.SetTrigger("attack");
            if (!isJumping)
            {
                myAnimator.SetLayerWeight(0, 0);
                myAnimator.SetLayerWeight(1, 1);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("air", true);
                myRigbody2D.AddForce(new Vector2(0, speedMovement), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
    }

    void FlipCharacter(float horizontal)
    {
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Reset jumping status
        isJumping = false;
        myAnimator.SetLayerWeight(0, 1);
        myAnimator.SetLayerWeight(1, 0);
        myAnimator.SetBool("air", false);
    }

    void HandleMovement(float horizontal)
    {
        if (transform.localPosition.x < minLeft)
        {
            transform.localPosition = new Vector3(minLeft, transform.localPosition.y, transform.localPosition.z);

        }
        // is standing
        if (horizontal == 0)
        {
            myAnimator.SetInteger("speedMovement", 0);
            myRigbody2D.velocity = new Vector2(0, myRigbody2D.velocity.y);
        }
        else
        {
            myAnimator.SetInteger("speedMovement", 1);
            myRigbody2D.velocity = new Vector2(horizontal * speedMovement, myRigbody2D.velocity.y);
        }
    }
}
