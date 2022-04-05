using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeData : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int spriteID;
    private int spriteRoll;
    public int spawnerID;
    public Sprite[] BoxSprites;
    public string[] BoxTags;
    public GameObject[] BoxList;

    public int BoxID;
    Ray2D Ray_2DLeft;
    Ray2D Ray_2DRight;
    Ray2D Ray_2DUp;
    Ray2D Ray_2DDown;
    public Vector2[] CubeRays;
    public Vector2[] RayOffset;
    public float rayDistance;
    public bool isActive;
    public Transform ObjTransform;
    public bool isMoving;
    RaycastHit2D Left_2DRay;
    RaycastHit2D Right_2DRay;
    RaycastHit2D Up_2DRay;
    RaycastHit2D Down_2DRay;
    void Start()
    {
        isMoving = true;
        ObjTransform = this.gameObject.transform;
        BoxList = new GameObject[4];
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
        InvokeRepeating("RandomBox", 10.0f, 10.0f);
    }

    private void Update()
    {
        Draw_2DRays();
        C_2DRaycasts();
        
    }

    private void FixedUpdate()
    {
        CheckBottom();
    }
    void CheckBottom()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,rayDistance);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider);
            isMoving = false;
        }
    }
    public void RandomBox()
    {
        if (this.gameObject.tag == "Grey")
        {
            GreyBox();
        }
    }
    public void GreyBox()
    {
        //spriteID = Random.Range(0, BoxSprites.Length - 2);
        spriteRoll = Random.Range(0, 100);
        if (spriteRoll > 0 && spriteRoll <= 20) //20% chance
        {
            spriteID = 3;
        }
        else if(spriteRoll > 20 && spriteRoll <= 50)//40% chance
        {
            spriteID = 1;
        }
        else if (spriteRoll > 50 && spriteRoll <= 100)//40% chance
        {
            spriteID = 1;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BoxSprites[spriteID];
        this.gameObject.tag = BoxTags[spriteID];
    }
    public void SetSprite()
    {
        spriteRoll = Random.Range(0, 100);
        if (spriteRoll > 0 && spriteRoll <= 20) //20% chance
        {
            spriteID = 3;
        }
        else if (spriteRoll > 20 && spriteRoll <= 40)//20% chance
        {
            spriteID = 2;
        }
        else if (spriteRoll > 40 && spriteRoll <= 70)//30% chance
        {
            spriteID = 1;
        }
        else if (spriteRoll > 70 && spriteRoll <= 100)//30% chance
        {
            spriteID = 0;
        }
            //spriteID = Random.Range(0, (BoxSprites.Length));
            //spriteID = Random.Range(0, 2);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BoxSprites[spriteID];
        this.gameObject.tag = BoxTags[spriteID];
    }

    public void CleanBoxList()
    {
        System.Array.Clear(BoxList, 0, BoxList.Length);
    }
    void Draw_2DRays()
    {
        Ray_2DLeft = new Ray2D(ObjTransform.position - (Vector3)RayOffset[0], (CubeRays[0]));
        Debug.DrawRay(ObjTransform.position - (Vector3)RayOffset[0], CubeRays[0], Color.green);

        Ray_2DRight = new Ray2D(ObjTransform.position + (Vector3)RayOffset[1], (CubeRays[1]));
        Debug.DrawRay(ObjTransform.position + (Vector3)RayOffset[1], (CubeRays[1]), Color.green);

        Ray_2DUp = new Ray2D(ObjTransform.position + (Vector3)RayOffset[2], (CubeRays[2]));
        Debug.DrawRay(ObjTransform.position + (Vector3)RayOffset[2], (CubeRays[2]), Color.green);

        Ray_2DDown = new Ray2D(ObjTransform.position - (Vector3)RayOffset[3], (CubeRays[3]));
        Debug.DrawRay(ObjTransform.position - (Vector3)RayOffset[3], (CubeRays[3]), Color.green);

        Left_2DRay = Physics2D.Raycast(ObjTransform.position - (Vector3)RayOffset[0], CubeRays[0],rayDistance);
        Right_2DRay = Physics2D.Raycast(ObjTransform.position + (Vector3)RayOffset[1], CubeRays[1],rayDistance);
        Up_2DRay = Physics2D.Raycast(ObjTransform.position + (Vector3)RayOffset[2], CubeRays[2],rayDistance);
        Down_2DRay = Physics2D.Raycast(ObjTransform.position - (Vector3)RayOffset[3], (CubeRays[3]),rayDistance);

    }

    void C_2DRaycasts()
    {
        if (this.gameObject.tag == "Red")
        {
            if (Left_2DRay.collider != null && Left_2DRay.collider.gameObject.tag == "Red")
            {
                BoxList[0] = Left_2DRay.rigidbody.gameObject;
            }
            if (Right_2DRay.collider != null && Right_2DRay.collider.gameObject.tag == "Red")
            {
                BoxList[1] = Right_2DRay.rigidbody.gameObject;
            }
        }

        else if (this.gameObject.tag == "Blue")
        {
            if (Up_2DRay.collider != null && Up_2DRay.collider.gameObject.tag == "Blue")
            {
                BoxList[2] = Up_2DRay.rigidbody.gameObject;
            }
            if (Down_2DRay.collider != null && Down_2DRay.collider.gameObject.tag == "Blue")
            {
                BoxList[3] = Down_2DRay.rigidbody.gameObject;
            }
        }

    }


}