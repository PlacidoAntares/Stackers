
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeData : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int spriteID;
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
    public bool isActive;
    public Transform ObjTransform;
    public bool isMoving;
    public CubeData cubeData;
    RaycastHit2D Left_2DRay;
    RaycastHit2D Right_2DRay;
    RaycastHit2D Up_2DRay;
    RaycastHit2D Down_2DRay;
    void Start()
    {
        isMoving = true;
        ObjTransform = transform;
        BoxList = new GameObject[4];
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
        InvokeRepeating("RandomBox", 10.0f, 10.0f);
    }

    private void Update()
    {
        Draw_2DRays();
        C_2DRaycasts();
        CheckBottom();
    }

    void CheckBottom()
    {
        if (Down_2DRay.collider != null)
        {
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
        spriteID = Random.Range(0, BoxSprites.Length - 2);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BoxSprites[spriteID];
        this.gameObject.tag = BoxTags[spriteID];
    }
    public void SetSprite()
    {
        spriteID = Random.Range(0, (BoxSprites.Length));
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
        Ray_2DLeft = new Ray2D(this.gameObject.transform.position, (CubeRays[0]));
        Debug.DrawRay(this.gameObject.transform.position, CubeRays[0], Color.green);
        Ray_2DRight = new Ray2D(this.gameObject.transform.position, (CubeRays[1]));
        Debug.DrawRay(this.gameObject.transform.position, (CubeRays[1]), Color.green);
        Ray_2DUp = new Ray2D(this.gameObject.transform.position, (CubeRays[2]));
        Debug.DrawRay(this.gameObject.transform.position, (CubeRays[2]), Color.green);
        Ray_2DDown = new Ray2D(this.gameObject.transform.position, (CubeRays[3]));
        Debug.DrawRay(this.gameObject.transform.position, (CubeRays[3]), Color.green);
        Left_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[0]);
        Right_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[1]);
        Up_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[2]);
        Down_2DRay = Physics2D.Raycast(ObjTransform.position, (CubeRays[3]));
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

        else if (this.gameObject.tag == "White")
        {
            if (Left_2DRay.collider != null && Left_2DRay.collider.gameObject.tag != "Grey")
            {
                BoxList[0] = Left_2DRay.rigidbody.gameObject;
            }
            if (Right_2DRay.collider != null && Right_2DRay.collider.gameObject.tag != "Grey")
            {
                BoxList[1] = Right_2DRay.rigidbody.gameObject;
            }
            if (Up_2DRay.collider != null && Up_2DRay.collider.gameObject.tag != "Grey")
            {
                BoxList[2] = Up_2DRay.rigidbody.gameObject;
            }
            if (Down_2DRay.collider != null && Down_2DRay.collider.gameObject.tag != "Grey")
            {
                BoxList[3] = Down_2DRay.collider.gameObject;             
            }
        }

    }


}