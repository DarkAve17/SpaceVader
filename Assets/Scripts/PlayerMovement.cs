using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform orientation;

    [Header("Movement")]
    public float movespeed;
    public float groundDrag;

    [Header("Groundcheck")]
    public float PlayerHeight;
    public LayerMask whatisGround;
    //public bool isgrounded;
    

    private float HorizontalInput;
    private float VerticalInput;

    private Vector3 MovementDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
    void Update()
    {
        //isgrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, whatisGround);
        myInputs();
        rb.drag = groundDrag;
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void myInputs()
    {
        HorizontalInput = Input.GetAxisRaw("Vertical");
        VerticalInput = Input.GetAxisRaw("Horizontal");
    }

    private void MovePlayer()
    {
        MovementDirection = orientation.forward * VerticalInput + orientation.right* (-HorizontalInput);

        rb.AddForce(MovementDirection.normalized * movespeed * 10f * Time.deltaTime,ForceMode.Force);
    }

    // Update is called once per frame
    
}
