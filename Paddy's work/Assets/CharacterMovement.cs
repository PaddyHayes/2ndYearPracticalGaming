using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    enum My_Char_Animations { Idle, Walk, Sprint, Backwards, Backwards_Sprint}
    My_Char_Animations my_current_Animation = My_Char_Animations.Idle;
    My_Char_Animations my_last_animation = My_Char_Animations.Idle;

    
    bool is_idling = true;
    private float current_speed = 1;
    private float turning_speed = 30;
    CameraControl my_camera;
    Animator my_animation;

    // Start is called before the first frame update
    void Start()
    {
        my_animation = GetComponent<Animator>();
        my_camera = FindObjectOfType<CameraControl>();

        my_camera.follow(this);
    }

    // Update is called once per frame
    void Update()
    {
        my_current_Animation = My_Char_Animations.Idle;

        if (should_walk_forward()) walk_forward();
        turn(Input.GetAxis("Horizontal"));
        if (should_walk_backward()) walk_backward();
        if (should_run_backward()) run_backward();
        if (should_disconnect_camera()) disconnect_camera();
        if (should_sprint()) sprint_forward();

        if (my_current_Animation != my_last_animation)
            switch(my_current_Animation)
            {

                case My_Char_Animations.Idle:
                    my_animation.SetBool("is_backwards_run", false);
                    my_animation.SetBool("is_backwards_walk", false);
                    my_animation.SetBool("is_idle", true);
                    my_animation.SetBool("is_walking", false);
                    my_animation.SetBool("is_sprinting", false);
                    break;

                case My_Char_Animations.Walk:
                    my_animation.SetBool("is_backwards_run", false);
                    my_animation.SetBool("is_backwards_walk", false);
                    my_animation.SetBool("is_idle", false);
                    my_animation.SetBool("is_walking", true);
                    my_animation.SetBool("is_sprinting", false);
                    break;

                case My_Char_Animations.Sprint:
                    my_animation.SetBool("is_backwards_run", false);
                    my_animation.SetBool("is_backwards_walk", false);
                    my_animation.SetBool("is_idle", false);
                    my_animation.SetBool("is_walking", false);
                    my_animation.SetBool("is_sprinting", true);
                    break;

                case My_Char_Animations.Backwards:
                    my_animation.SetBool("is_backwards_run", false);
                    my_animation.SetBool("is_backwards_walk", true);
                    my_animation.SetBool("is_idle", false);
                    my_animation.SetBool("is_walking", false);
                    my_animation.SetBool("is_sprinting", false);
                    break;

                case My_Char_Animations.Backwards_Sprint:
                    my_animation.SetBool("is_backwards_run", true);
                    my_animation.SetBool("is_backwards_walk", false);
                    my_animation.SetBool("is_idle", false);
                    my_animation.SetBool("is_walking", false);
                    my_animation.SetBool("is_sprinting", false);
                    break;

            }
        my_last_animation = my_current_Animation;
    }

     private void run_backward()
    {
        
        my_current_Animation = My_Char_Animations.Backwards_Sprint;
        transform.position -= current_speed * 2 * transform.forward * Time.deltaTime;
    }

    private bool should_run_backward()
    {
        return (Input.GetKey(KeyCode.S)) && !Input.GetKey(KeyCode.LeftShift); 
            
    }

    private void stop_walking()
    {
        my_animation.SetBool("is_walking", false);
    }

    private bool should_disconnect_camera()
    {
        return Input.GetKey(KeyCode.Space);
    }
    private void disconnect_camera()
    {
        //Make code to check if connected first
        transform.parent = null;
    }

    private void turn(float horz)
    {
        transform.Rotate(Vector3.up, horz*turning_speed * Time.deltaTime);
    }


    private void walk_backward()
    {
        
        my_current_Animation = My_Char_Animations.Backwards;
        transform.position -= current_speed * transform.forward * Time.deltaTime;
    }

    private bool should_walk_backward()
    {
        return (Input.GetKey(KeyCode.S)
        && Input.GetKey(KeyCode.LeftShift));
    }

    private void walk_forward()
    {
        
        my_current_Animation = My_Char_Animations.Walk;
        transform.position += current_speed * transform.forward * Time.deltaTime;
    }

    private bool should_walk_forward()
    {
        return (Input.GetKey(KeyCode.W)
        && Input.GetKey(KeyCode.LeftShift));


    }

    private void sprint_forward()
    {
        
        my_current_Animation = My_Char_Animations.Sprint;
        transform.position += current_speed * 2 * transform.forward * Time.deltaTime;
    }

    private bool should_sprint()
    {
        return (Input.GetKey(KeyCode.W)) && !Input.GetKey(KeyCode.LeftShift);
             
    }

  
}
