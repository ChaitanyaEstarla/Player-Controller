using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables
    [Header("Public Fields")]
    public float speed;
    public float dashDistance;
    public float jumpVelocity;
    
    #endregion

    #region Private Variables
    private float m_MoveX;
    private float m_MoveY;
    private Vector3 m_LastDirection = Vector3.left;
    private bool m_CanJumpInAir;
    
    [Header("Private Fields")]
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private LayerMask platformsLayerMask;

    #endregion

    #region Unity Event Functions
    private void Update()
    {
        CheckForMovementInput();
    }

    #endregion

    #region Private Functions


    private void CheckForMovementInput()
    {
        if (!m_CanJumpInAir && IsGrounded())
        {
            m_CanJumpInAir = true;
        }
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayer(-1,0);
        }
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayer(1,0);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded()) Jump();
            else if (m_CanJumpInAir)
            {
                Jump();
                m_CanJumpInAir = false;
            }
        }
    }

    private void MovePlayer(float x, float y)
    {
        m_MoveX = x;
        m_MoveY = y;
        var moveDirection = new Vector3(m_MoveX, m_MoveY).normalized;
        transform.position += moveDirection * (speed * Time.deltaTime);
        m_LastDirection = moveDirection;
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

    #endregion

    #region Public Functions

    

    #endregion
}
