using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private CharacterController controller;
    public Animator anim;
    public Transform LookAtDir, PlayerHead;

    private float MouseX, MouseY;
    private float InputX, InputY;
    private bool Ismoving = false;
    [SerializeField]
    private float P_Speed = 10f;
    [SerializeField]
    private float Rotate_Speed = 100f;
    private Vector3 Movement;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

    }


    // Update is called once per frame
    void Update()
    {
        Player_Movement();
        CameraLook();
    }
   
    void Player_Movement()
    {
        InputX = Input.GetAxis("Horizontal");
        InputY = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
        {
            Ismoving = true;
        }
        else
        {
            Ismoving = false;
        }

        anim.SetBool("Walk", Ismoving);

        Movement = transform.forward * InputY + transform.right * InputX;
      
        if (Movement != Vector3.zero)
        {
              RotateTowardCamDir();
              Quaternion Look = Quaternion.LookRotation(Movement, Vector3.up);
              LookAtDir.rotation = Quaternion.RotateTowards(LookAtDir.rotation, Look, Rotate_Speed * Time.deltaTime);
        }


        controller.Move(Movement * P_Speed * Time.deltaTime);


    }
    void RotateTowardCamDir()
    {
        Vector3 CamDir = Camera.main.transform.forward;
        CamDir.y = 0;
        Quaternion rot = Quaternion.LookRotation(CamDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
    }
    void CameraLook()
    {
        MouseX += Input.GetAxis("Mouse X");
        MouseY -= Input.GetAxis("Mouse Y");
        PlayerHead.transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);

    }
}
