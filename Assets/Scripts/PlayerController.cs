using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables
    [Header("Public Fields")]
    public float speed;
    public float dashDistance;
    public float jumpVelocity;
    public Transform frontCheck;
    public float wallSlidingSpeed;

    #endregion

    #region Private Variables
    private float m_MoveX;
    private float m_MoveY;
    private Vector3 m_LastDirection = Vector3.left;
    private int m_AirJumpCount;
    private int m_AirJumpCountMax;
    private bool m_MovePlayer;
    private bool m_IsTouchingFront;
    private bool m_WallSlidig;

    [Header("Private Fields")]
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private LayerMask platformsLayerMask;

    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        m_AirJumpCountMax = 1;
    }

    private void Update()
    {
        CheckForMovementInput();
    }

    private void FixedUpdate()
    {
        if (m_MovePlayer)
        {
            MovePlayer();
        }
    }

    #endregion

    #region Private Functions


    private void CheckForMovementInput()
    {
        if (IsGrounded())
        {
            m_AirJumpCount = 0;
        }
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_MovePlayer = true;
            m_MoveX = -1;
            m_MoveY = 0;
            TurnTowardsDirection(-1);
        }
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            m_MovePlayer = true;
            m_MoveX = 1;
            m_MoveY = 0;
            TurnTowardsDirection(1);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded()) Jump();
            else if (m_AirJumpCount < m_AirJumpCountMax)
            {
                Jump();
                m_AirJumpCount++;
            }
        }
    }

    private void MovePlayer()
    {
        var moveDirection = new Vector3( m_MoveX, m_MoveY).normalized;
        transform.position += moveDirection * (speed * Time.deltaTime);
        m_LastDirection = moveDirection;
        m_MovePlayer = false;
    }

    private void Dash()
    {
        transform.position += m_LastDirection * dashDistance;
    }

    private void Jump()
    {
        playerRigidbody.velocity = Vector2.up * jumpVelocity;
    }

    private bool IsGrounded()
    {
        var bounds = boxCollider2D.bounds;
        var raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }
    
    private void TurnTowardsDirection(int direction)
    {
        
    }

    #endregion

    #region Public Functions

    

    #endregion
}