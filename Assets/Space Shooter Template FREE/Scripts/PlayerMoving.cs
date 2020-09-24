using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    Camera mainCamera;
    bool controlIsActive = true;
    public float speed = 1f;
    public static PlayerMoving instance; //unique instance of the script for easy access to the script
    public bool tiltToMove = false;
    public float tiltSpeed = 10f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
       
    }

    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR    //if the current platform is not mobile, set pc settings

             float horiz = Input.GetAxis("Horizontal");
             float vert = Input.GetAxis("Vertical");
             Vector3 direction = new Vector3(horiz, vert, 0);
             direction.Normalize();
             transform.Translate(direction.x*speed*Time.deltaTime, direction.y*speed*Time.deltaTime, 0);
             
             
         
#endif

#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 
            if (tiltToMove) 
            {
                horiz = Input.acceleration.x;
                vert = Input.acceleration.y;
                transform.Translate(horiz*Time.deltaTime*tiltSpeed, vert*Time.deltaTime*tiltSpeed, 0);
            }

            else if (Input.touchCount == 1) // if there is a touch
            {
                Touch touch = Input.touches[0];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
                touchPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
            }
#endif
            //transform.position = new Vector3    if 'Player' crossed the movement borders, returning him back 
            //(
            //Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
            //Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
            //0
            //);

            Vector3 min = mainCamera.ViewportToWorldPoint(Vector2.zero);
            Vector3 size = mainCamera.ViewportToWorldPoint(Vector2.one) - min;
            Rect worldBounds = new Rect(min.x, min.y, size.x, size.y);

            Vector3 pos = transform.position;
            
            if (pos.x > worldBounds.xMax)
                pos.x -= worldBounds.width;
            if (pos.x < worldBounds.xMin)
                pos.x += worldBounds.width;
            if (pos.y > worldBounds.yMax)
                pos.y -= worldBounds.height;
            if (pos.y < worldBounds.yMin)
                pos.y += worldBounds.height;

            transform.position = pos;
        }
    }

    //setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }
}
