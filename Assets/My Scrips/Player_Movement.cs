using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float move_Speed = 2f;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Horizontal * move_Speed * Time.deltaTime, 0,0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocityY = 5f;
        }
    }
}
