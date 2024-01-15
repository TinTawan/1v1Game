using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance 
    {  
        get 
        { 
            return instance; 
        } 
    }
    
    private PlayerControls pc;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }


        pc = new PlayerControls();
    }

    void OnEnable()
    {
        pc.Enable();
    }

    void OnDisable()
    {
        pc.Disable();
    }


    public Vector2 GetPlayerMovement()
    {
        return pc.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return pc.Player.Look.ReadValue<Vector2>();
    }

    public bool GetPlayerJumped()
    {
        return pc.Player.Jump.triggered;
    }
}
