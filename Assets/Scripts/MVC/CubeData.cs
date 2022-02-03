
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
    void Start()
    {
        isMoving = true;
        ObjTransform = transform;
        BoxList = new GameObject[4];
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
        InvokeRepeating("RandomBox",10.0f,10.0f);
    }

    private void Update()
    {
        Draw_2DRays();
        C_2DRaycasts();
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
        spriteRenderer.sprite = BoxSprites[spriteID];
        this.gameObject.tag = BoxTags[spriteID];
    }
    public void SetSprite()
    {
        spriteID = Random.Range(0, (BoxSprites.Length));
        //spriteID = Random.Range(0, 2);
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
        Debug.DrawRay(this.gameObject.transform.position,(CubeRays[1]),Color.green);
        Ray_2DUp = new Ray2D(this.gameObject.transform.position, (CubeRays[2]));
        Debug.DrawRay(this.gameObject.transform.position,(CubeRays[2]),Color.green);
        Ray_2DDown = new Ray2D(this.gameObject.transform.position, (CubeRays[3]));
        Debug.DrawRay(this.gameObject.transform.position, (CubeRays[3]), Color.green);
    }

    void C_2DRaycasts()
    {
        RaycastHit2D Left_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[0], 1.0f);
        if (Left_2DRay.rigidbody != null && Left_2DRay.rigidbody.gameObject.tag == "Red" && this.gameObject.tag == "Red" && Left_2DRay.rigidbody.gameObject.tag != "Grey")
        {
            if (isMoving != true)
            {
                BoxList[0] = Left_2DRay.rigidbody.gameObject;
            }
        }
        RaycastHit2D Right_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[1], 1.0f);
        if (Right_2DRay.rigidbody != null && Right_2DRay.rigidbody.gameObject.tag == "Red" && this.gameObject.tag == "Red" && Right_2DRay.rigidbody.gameObject.tag != "Grey")
        {
            if (isMoving != true)
            {
                BoxList[1] = Right_2DRay.rigidbody.gameObject;
            }
        }
        //
        RaycastHit2D Up_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[2], 1.0f);
        if (Up_2DRay.rigidbody != null && Up_2DRay.rigidbody.gameObject.tag == "Blue" && this.gameObject.tag == "Blue" && Up_2DRay.rigidbody.gameObject.tag != "Grey")
        {
            if (isMoving != true)
            {
                BoxList[2] = Up_2DRay.rigidbody.gameObject;
            }
        }
        //
        RaycastHit2D Down_2DRay = Physics2D.Raycast(ObjTransform.position, CubeRays[3], 1.0f);
        if (Down_2DRay.collider != null)
        {
            if (isMoving != true && Down_2DRay.collider.tag == "Blue" && this.gameObject.tag == "Blue" && Down_2DRay.collider.tag != "Grey")
            {
                BoxList[3] = Down_2DRay.collider.gameObject;
            }
            isMoving = false;
        }

    }


}
