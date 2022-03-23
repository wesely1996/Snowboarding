using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{

    Rigidbody2D rb2d;
    [SerializeField] float torqueAmount = 2f;
    [SerializeField] float boostSpeed = 40f;
    [SerializeField] float boostInterval = 4f;
    [SerializeField] float boostTimer = 3f;
    [SerializeReference] Slider boostSlider;
    float maxBoostTime;
    float baseSpeed;
    SurfaceEffector2D surfaceEffector2D;
    bool canMove = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector2D.speed;
        maxBoostTime = boostTimer;
    }

    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.Space) && boostTimer > 0)
        {
            if (surfaceEffector2D.speed <= boostSpeed)
                surfaceEffector2D.speed += (boostInterval * Time.deltaTime);
            else
                surfaceEffector2D.speed = boostSpeed;
            boostTimer -= Time.deltaTime;
            boostSlider.value = boostTimer / maxBoostTime;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
