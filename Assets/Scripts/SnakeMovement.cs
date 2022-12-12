using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    //Snake specs
    public float forwardSpeed;
    public float sensitivity = 10;
    private Vector2 touchLastPos;
    private float sidewaysSpeed;
    private Camera mainCamera;
    private Rigidbody snakeRb;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        snakeRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            GetMousePosition();
        }
    }
    private void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            forwardSpeed = 11;

            if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 5 * Mathf.Sign(sidewaysSpeed);
            snakeRb.velocity = new Vector2(sidewaysSpeed * 13, forwardSpeed);

            sidewaysSpeed = 0;
        }
        else
        {
            forwardSpeed = 0;
        }
    }
    void GetMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
           

            sidewaysSpeed += delta.x * sensitivity;
            
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
