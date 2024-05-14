using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    // Nhân vật
    public CharacterController controller;
    // Độ mượt xoay
    float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;
    // Tốc độ
    public float speed = 10f;
    // Camera
    public Transform cam;
    // Trọng lực
    public float gravity = - 9.81f;
    Vector3 velocity;
    // Kiểm tra mặt đất
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGround;
    // Chuyển động
    public Animator anim;
    // Năng lượng
    public float mana = 10;

    void Update()
    {
        // Tiếp dất
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Di chuyển
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 derection = new Vector3(horizontal, 0f, vertical).normalized;
        if (derection.magnitude >= 0.1f)
        {
            anim.SetBool("Walk", true);
            if (Input.GetKey(KeyCode.LeftShift) && mana > 0) {
                speed = 15;
                anim.SetBool("Run", true);
                mana -= Time.deltaTime;
            } else {
                speed = 10;
                anim.SetBool("Run", false);
                if (mana < 100)
                    mana += Time.deltaTime;
            }
            float targetAngle = Mathf.Atan2(derection.x, derection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            controller.Move(moveDir * speed * Time.deltaTime);
        } 
        else 
        {
            anim.SetBool("Walk", false);
        }

        // Các hiệu ứng tấn công
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("Defent");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Skill");
        }
    }
}