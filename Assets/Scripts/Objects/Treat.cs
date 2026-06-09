using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Treat : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool isHolding;
    [SerializeField] private float floor;
    
    void Awake()
    {
        isHolding = true;
    }
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            isHolding = false;
        }

        if (isHolding)
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = new Vector3(MousePos.x, MousePos.y, 0);
        }

        if (isHolding == false)
        {
            if (transform.position.y >= floor) 
            {
                Vector3 posDelta = new Vector3(0, fallSpeed, 0);
                Vector3 pos = transform.position;
                transform.position = pos - (posDelta * Time.deltaTime);
            }

        }
        ;
    }
}
