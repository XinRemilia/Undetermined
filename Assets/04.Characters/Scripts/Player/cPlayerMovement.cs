using UnityEngine;
public class cPlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rig;
    BoxCollider2D m_coll;
    Animator m_anima;
    [Header("移動")]//move関係のパラメータ
    public float speed = 10f;//move speed
    public float crouchSpeedDivisor = 3f;//しゃがむ speed down
    bool m_facingRigh = true;//方向

    [Header("ジャンプ")]//jump関係のパラメータ
    public float jumpForce = 10f;//jump力
    public float jumpHoldForce = 1.9f;//jump key長い時間押す時の力
    public float jumpHoldDuration = 0.1f;//jump key長い時間押すの続く時間
    public float crouchJumpBoost = 15f;//しゃがむ後でjumpするとのjump力up

    float m_jumpTime;//jumpの時間計算、jump key長い時間押す

    [Header("状態")]
    public bool isCrouch;//しゃがむの判定
    public bool isOnGround;//roleが地面に立つ判定
    public bool isJump;//jump判定
    public bool isHeadBlocked;//head判定

    [Header("環境チェック")]//環境チェック
    public float xFootOffset;//Ray x
    public float yFootOffset;//Ray y

    public float headClerance = 0.32f;//頭距離チェック
    public float groundDistance = 0.2f;//地面距離チェック 
    public LayerMask groundLayer;//地面

    float m_xVelocity;//移動判定

    //keyの設定
    bool m_jumpHeld;//jump key　長押し
    bool m_crouchHeld;//しゃがむ key　長押し
    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        m_rig = GetComponent<Rigidbody2D>();
        m_coll = GetComponent<BoxCollider2D>();
        m_anima = GetComponent<Animator>();
        xFootOffset = m_coll.size.x / 2 - m_coll.offset.x;
        yFootOffset = m_coll.size.y / 2 - m_coll.offset.y;


    }
    /// <summary>
    /// 入力チェック
    /// </summary>
    void Update()
    {
        m_jumpHeld = Input.GetButton("Jump");
        
        m_crouchHeld = Input.GetButton("Crouch");
    }
    float tempFloat = 0;
    /// <summary>
    /// 物理更新関係
    /// </summary>
    void FixedUpdate()
    {
        PhysicsCheck();//チェック
        tempFloat += Time.fixedDeltaTime;
        RoleMovement();//実行
    }

    /// <summary>
    /// キャラクター移動関係
    /// </summary>
    void RoleMovement()
    {
        JumpControl();
        MoveControl();
        CrouchControl();
    }
    /// <summary>
    /// ジャンプコントロール
    /// </summary>
    void JumpControl()
    {
        if (isOnGround && m_jumpHeld && !isJump && !isHeadBlocked)
        {
            float tempJumpForce = 0;
            if (isCrouch)
            {
                isCrouch = false;
                m_anima.SetBool("Crouch", isCrouch);
                //rig.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
                //rig.velocity = new Vector2(0f, crouchJumpBoost);
                tempJumpForce = crouchJumpBoost;
            }

            //jumpする時、状態変更
            isOnGround = false;//地面を離れる
            isJump = true;//今はjumpする

            m_jumpTime = Time.time + jumpHoldDuration;//jump key長押す時の時間
            m_rig.velocity = new Vector2(0f, jumpForce + tempJumpForce);
        }
        else if (isJump)
        {
            if (m_jumpHeld)//jump key長押すともっと高いjump
            {
                m_rig.velocity = new Vector2(0f, jumpForce);
            }
            if (m_jumpTime < Time.time)//jump key長押す時の時間 < 今の時間
            {
                isJump = false;
            }
        }
    }
    /// <summary>
    /// しゃがむコントロール
    /// </summary>
    void CrouchControl()
    {
        if (isOnGround && m_crouchHeld)
        {
            isCrouch = true;
            m_anima.SetFloat("Run", 0f);
        }
        else if(!isHeadBlocked)
            isCrouch = false;

        m_anima.SetBool("Crouch", isCrouch);
    }
    /// <summary>
    /// 移動コントロール
    /// </summary>
    void MoveControl()
    {
        m_xVelocity = Input.GetAxis("Horizontal");

        if (isCrouch)
            m_xVelocity /= crouchSpeedDivisor;

        m_rig.velocity = new Vector2(m_xVelocity * speed, m_rig.velocity.y);

        #region 方向コントロール
        if (m_xVelocity < 0 && m_facingRigh)
            FacingControl();
        if (m_xVelocity > 0 && !m_facingRigh)
            FacingControl();
        #endregion

        m_anima.SetFloat("Run", Mathf.Abs(m_xVelocity));
        
        //if(m_xVelocity != 0 && tempFloat > 0.5f)
        //{
        //    cSourceManager.instance.MoveAudio();
        //    tempFloat = 0;
        //}
    }
    /// <summary>
    /// 方向コントロール
    /// </summary>
    void FacingControl()
    {
        m_facingRigh = !m_facingRigh;
        transform.Rotate(0f, 180f, 0f);
    }

    /// <summary>
    /// 物理チェック
    /// </summary>
    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-xFootOffset, -yFootOffset), Vector2.down, groundDistance, groundLayer)//左脚
            , righCheck = Raycast(new Vector2(xFootOffset, -yFootOffset), Vector2.down, groundDistance, groundLayer)//右脚
            , leftheadCheck = Raycast(new Vector2(-xFootOffset, yFootOffset), Vector2.up, headClerance, groundLayer)//頭
            , righheadCheck = Raycast(new Vector2(xFootOffset, yFootOffset), Vector2.up, headClerance, groundLayer);//頭

        isOnGround = leftCheck || righCheck ? true : false;

        isHeadBlocked = leftheadCheck || righheadCheck ? true : false;
    }
    /// <summary>
    /// Rayチェック
    /// </summary>
    /// <param name="_offset">オフセット</param>
    /// <param name="_rayDiraction">方向</param>
    /// <param name="_length">長さ</param>
    /// <param name="_layer">レイヤー</param>
    /// <returns>ヒット</returns>
    RaycastHit2D Raycast(Vector2 _offset, Vector2 _rayDiraction, float _length, LayerMask _layer)
    {
        Vector2 tempPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(tempPos + _offset, _rayDiraction, _length, _layer);

        Color rayColor = hit ? Color.red : Color.yellow;
        Debug.DrawRay(tempPos + _offset, _rayDiraction * _length, rayColor);
        return hit;
    }
}
