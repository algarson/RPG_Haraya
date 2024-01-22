using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controll : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(speedX == 0 && speedY == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetTrigger( name: "isSwordMode");
        };
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetTrigger(name: "isNormalMode");
        }


        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }
}
