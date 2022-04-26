    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight;
    public float speed = 7.0f;
    public float maxSpeed = 15f;
    public float jumpForce;
    public float gravity = -20f;
    public float GroundDistance = 0.1f;
    public LayerMask Ground;
    public GameObject[] projectiles;
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float projectileSpeed = 1000;

    private CharacterController myCharacterController;
    private bool jump = false;
    private bool roll = false;
    private bool attack = false;
    private bool canMove = true;
    private Vector3 direction = Vector3.zero;
    private int line = 1;
    private int targetLine = 1;
    private Animator myAnimator;
    private Transform _groundChecker;
    private bool _isGrounded = true;

    


    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        _groundChecker = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxSpeed)
        {
            speed += 0.1f * Time.deltaTime;

        }
        direction.z = speed;

        //turnLeft = Input.GetKeyDown(KeyCode.A);
        //turnRight = Input.GetKeyDown(KeyCode.D);

        jump = Input.GetKeyDown(KeyCode.Space);
        roll = Input.GetKeyDown(KeyCode.S);
        attack = Input.GetKeyDown(KeyCode.Mouse0);

        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        //if (turnLeft)
        //    transform.Rotate(new Vector3(0f, -90f, 0f));
        //else if (turnRight)
        //    transform.Rotate(new Vector3(0f, 90f, 0f));
        direction.y += gravity * Time.deltaTime;
        

        if (_isGrounded)
        {
           
            if (jump)
            {
                Jump();              
            }
            else if (roll)
                StartCoroutine(Slide());
            else if (attack)
                Shoot();
        }
        


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
    }

   private void Shoot()
    {
        myAnimator.Play("Attack");
        GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileSpeed);
    }

    private IEnumerator Slide()
    {
        myAnimator.Play("Roll");
        myCharacterController.center = new Vector3(0, 0.5f, 0);
        myCharacterController.height = 1;
        yield return new WaitForSeconds(0.9f);

        myCharacterController.center = new Vector3(0, 1f, 0);
        myCharacterController.height = 2;
    }

   
}
