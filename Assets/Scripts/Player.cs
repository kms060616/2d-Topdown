using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 2f;

    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] Sprite spriteRight;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > 0.01f)  //키가 눌렸을때만
        {
            
            
                if(Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    if (input.x > 0)
                        sR.sprite = spriteRight;
                    else if (input.x < 0)
                        sR.sprite = spriteLeft;
                }
                else
                {
                    if (input.y > 0)
                        sR.sprite = spriteUp;
                    else
                        sR.sprite = spriteDown;
                }
            

        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.CompareTag("Item"))
        {
            score += collision.GetComponent<ItemObject>().GetPoint();
            scoreText.text = score.ToString();
            Destroy(collision.gameObject);
            
        }
    }
}
