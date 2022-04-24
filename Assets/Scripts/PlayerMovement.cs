using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight;
    public float speed = 7.0f;
    public float jumpForce;
    public float gravity = -20;

    private CharacterController myCharacterController;
    private bool jump = false;
    private bool roll = false;
    private bool attack = false;
    private bool canMove = true;
    private Vector3 direction = Vector3.zero;
    private int line = 1;
    private int targetLine = 1;
    private Animator myAnimator;
    private bool isOnGround = true;
    

    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = speed;
        direction.y += gravity * Time.deltaTime;

        //turnLeft = Input.GetKeyDown(KeyCode.A);
        //turnRight = Input.GetKeyDown(KeyCode.D);

        jump = Input.GetKeyDown(KeyCode.Space);
        roll = Input.GetKeyDown(KeyCode.S);
        attack = Input.GetKeyDown(KeyCode.Mouse0);

        //if (turnLeft)
        //    transform.Rotate(new Vector3(0f, -90f, 0f));
        //else if (turnRight)
        //    transform.Rotate(new Vector3(0f, 90f, 0f));
       
        if (jump && isOnGround)
            Jump();
        else if (roll)
            myAnimator.Play("Roll");
        else if (attack)
            myAnimator.Play("Attack");


        Vector3 pos = gameObject.transform.position;
        if (!line.Equals(targetLine))
        {
            if (targetLine == 0 && pos.x < - 2)
            {
                gameObject.transform.position = new Vector3(-2, pos.y, pos.z);
                line = targetLine;
                direction.x = 0;
                canMove = true;
            }
            else if (targetLine == 1 && (pos.x > 0 || pos.x < 0)){
                if (line == 0 && pos.x > 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, pos.z);
                    line = targetLine;
                    direction.x = 0;
                    canMove = true;
                }
                else if (line == 2 && pos.x < 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, pos.z);
                    line = targetLine;
                    direction.x = 0;
                    canMove = true;
                }
            }else if (targetLine == 2 && pos.x > 2)
            {
                gameObject.transform.position = new Vector3(2, pos.y, pos.z);
                line = targetLine;
                direction.x = 0;
                canMove = true;
            }
        }
        checkInputs();
        myCharacterController.Move(direction * Time.deltaTime);
    }

    void checkInputs()
    {
        if (Input.GetKeyDown(KeyCode.A) && canMove && line > 0)
        {
            targetLine--;
            canMove = false;
            direction.x = -4f;
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove && line < 2){
            targetLine++;
            canMove = false;
            direction.x = 4f;
        }
    }

    private void Jump()
    {
        myAnimator.Play("Jump");
        direction.y = jumpForce;
        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
