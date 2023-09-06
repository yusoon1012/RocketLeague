using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Rewired;

public class PlayerMove :MonoBehaviour
{
  
 
    public float speed;
    public float jumpForce;
    public float maxSpeed;
    public int playerId = 0;
  
    Rigidbody playerRigid;
    AudioSource playerStepSound;
    public Animator animator;
    public Transform cameraArm;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private float airbornTimer = 0.0f;
    private float airbornRate = 1f;
    Player player;
    private Vector3 moveVector;
    private bool fire;
    private bool isWalking;
    private bool isGround;
    private bool fallDamage;
    private bool isAttack = false;
    Vector3 direction;
  


    // Start is called before the first frame update
    void Start()
    {
        fire = false;
        playerRigid = GetComponent<Rigidbody>();

        player=ReInput.players.GetPlayer(0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
       
    }




    private void GetInput()
    {
       

        //moveVector.x = player.GetAxis("Move Horizontal");
        //moveVector.z = player.GetAxis("Move Vertical");
       

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
           
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = false;
        }
    }
   
    private void ProcessInput()
    {

        if (fire)
        {
            if(isAttack==false)
            {
                isAttack=true;
         
            
            // Set vibration in all Joysticks assigned to the Player
            int motorIndex = 0; // the first motor
            float motorLevel = 0.05f; // full motor speed
            float duration = 0.2f; // 2 seconds
         

            }
        }
        



    }
    private void LateUpdate()
    {
   
        

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        cameraArm.position = smoothedPosition;


    }
    private void LookAround()
    {
       

        Vector2 mouseDelta = new Vector2(player.GetAxis("Camera Horizontal"), player.GetAxis("Camera Vertical"));
        mouseDelta *= 0.2f;
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 40f);
        } 
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    // Update is called once per frame
    void Update()
    {
        
        LookAround();
        float moveHorizontal = player.GetAxis("Move Horizontal");
        float moveVertical = player.GetAxis("Move Vertical");


        Vector3 cameraForward = cameraArm.forward; // ī�޶� ���� �ִ� ���� ���͸� ������
        Vector3 cameraRight = cameraArm.right; // ī�޶��� ������ ���� ���͸� ������
        cameraForward.y = 0f; // y �� ���� 0���� �����Ͽ� ���� �̵��� �����ϰ� ��
        cameraRight.y = 0f; // y �� ���� 0���� �����Ͽ� ���� �̵��� �����ϰ� ��
        cameraForward.Normalize(); // ���� ����ȭ
        cameraRight.Normalize(); // ���� ����ȭ
        Vector3 movement = (cameraForward * moveVertical + cameraRight * moveHorizontal) * speed;

        float movementSpeed = Mathf.Clamp01(Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical));
        //animator.SetFloat("MoveSpeed", movementSpeed);


        transform.LookAt(transform.position + movement);

        // Rigidbody�� �ӵ��� ���� �����Ͽ� �̵���Ŵ
        playerRigid.velocity = new Vector3(movement.x, playerRigid.velocity.y, movement.z);

        if (playerRigid.velocity.magnitude > maxSpeed)
        {
            playerRigid.velocity = playerRigid.velocity.normalized * maxSpeed;
        }

        isWalking = movement.magnitude != 0;

        




       
        if (isGround)
        {
           

        }
        GetInput();
        ProcessInput();
    }


    private void OnTriggerEnter(Collider other)
    {    
      //PhotonView photonView_=other.gameObject.GetComponent<PhotonView>();

      //  if (photonView_ != null)
      //  {
            
      //  int viewId = photonView_.ViewID;
      //      if (other.CompareTag("Player"))
      //      {
      //          photonView.RPC("HealthUpdate", RpcTarget.Others, viewId, 1f);
      //          photonView.RPC("ParticlePlay", RpcTarget.AllBuffered, other.transform.position);
                
      //      }
      //      else
      //      {
      //      photonView.RPC("AttackForce", RpcTarget.MasterClient, viewId,transform.position,other.transform.position);

      //      }


      //  }
    }
 

  
    private void HealthUpdate(int viewId_,float minusHp)
    {
       
        
       
    }

  
    private void AttackForce(int viewId_,Vector3 playerPosition_,Vector3 otherPosition)
    {
        
      

    


    }
   
    
}

