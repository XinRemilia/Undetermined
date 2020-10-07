using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rig;
    public float speed;
    float xVelocity, yVelocity;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        MoveControl();
    }
    // Update is called once per frame
    void Update()
    {
        xVelocity = Input.GetAxis("Horizontal");
        yVelocity = Input.GetAxis("Vertical");
    }
    void MoveControl()
    {
        int index = sprites.Length - 1;
        #region 方向コントロール
        if (xVelocity == 0)//↑0 ↓1
        {
            index = yVelocity == 0 ? sprites.Length - 1 : yVelocity > 0 ? 0 : 1; 
        }
        if (xVelocity > 0)//→2 ↗3 ↘4   
        {
            index = yVelocity == 0 ? 2 : yVelocity > 0 ? 3 : 4;
        }
        if (xVelocity < 0)//←5 ↖6 ↙7
        {
            index = yVelocity == 0 ? 5 : yVelocity > 0 ? 6 : 7;
        }
        #endregion
        rig.velocity = new Vector2(xVelocity * speed * Time.fixedDeltaTime, yVelocity * speed * Time.fixedDeltaTime);
        spriteRenderer.sprite = sprites[index];
    }
    /// <summary>
    /// 方向コントロール
    /// </summary>
    void FacingControl()
    {
        //facingRigh = !facingRigh;
        //transform.Rotate(0f, 180f, 0f);
    }
}
