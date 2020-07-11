using UnityEngine;

public class SheepController : MonoBehaviour 
{
    public float baseWanderingTime = 2f; // how long sheep will go in the same direction
    public float baseMaxspeed = 5f;
    public float panicWanderingTime = 1.5f;
    public float panicMaxspeed = 7f;
    public float baseAcceleration = 2f;
    public float panicAcceleration = 16f;

    public int pauseMovementFrames = 30; // how long to stop the sheep before changing direction

    private float timeLeft = 2f; // tracking time before direction switches
    private Vector2 movementDir;
    private Vector2 prevDir = new Vector2(1f, 0.1f);
    private Rigidbody2D sheepRb;
    private SpriteRenderer sheepRenderer;
    private int pauseFrameCount; // Used to pause fixedupdate

    public bool isPanicked = false;

    void Start() 
    {
        sheepRb = this.GetComponent<Rigidbody2D>();
        sheepRenderer = this.GetComponent<SpriteRenderer>();
    }

	private void OnCollisionEnter2D(Collision2D collision) // collision refers to other object
	{
        if (collision.gameObject.CompareTag("Boundary")) //switch direction if we hit edge of map
        {
            if (Mathf.Abs(collision.relativeVelocity.x) > Mathf.Abs(collision.relativeVelocity.y)) // if hitting a side wall
            {
                sheepRb.velocity = new Vector2(-sheepRb.velocity.x, sheepRb.velocity.y);
                movementDir = new Vector2(-movementDir.x, movementDir.y);
            }
            else // if hitting a top/bot wall
            {
                sheepRb.velocity = new Vector2(sheepRb.velocity.x, -sheepRb.velocity.y);
                movementDir = new Vector2(movementDir.x, -movementDir.y);
            }
        }
	}

	void Update()
    {
        timeLeft -= Time.deltaTime;

        if (!isPanicked) // Chill sheep
        {
            if (timeLeft <= 0) // Switch direction
            {
                sheepRb.velocity = new Vector2(0, 0); // TODO: Maybe implement slower deceleration later
                pauseFrameCount = pauseMovementFrames; // Pause sheep in place before switching dir
                prevDir = movementDir;
                movementDir = GenerateMovementDir();
                timeLeft += baseWanderingTime;
            }
        }
        else if (isPanicked) // Panic sheep
        {
            if (timeLeft <= 0) // Switch direction
            {
                //sheepRb.velocity = new Vector2(0, 0); // TODO: Maybe implement slower deceleration later
                prevDir = movementDir;
                movementDir = GenerateMovementDir();
                timeLeft += panicWanderingTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (pauseFrameCount <= 0)
        {
            if (!isPanicked)
            {
                if (sheepRb.velocity.magnitude < baseMaxspeed)
                { // if sheep hasn't reached maxspeed yet, accelerate it
                    sheepRb.AddForce(movementDir * baseAcceleration);
                }
            }
            else if (isPanicked)
            {
                if (sheepRb.velocity.magnitude < panicMaxspeed)
                {
                    sheepRb.AddForce(movementDir * panicAcceleration);
                }
            }
        }
        else
        {
            pauseFrameCount -= 1;
        }
    }

    void AnimateSheep()
    {
        // Set animations based on movement vector, idle, color
    }


    private int innerBoundary_x = 8;
    private int innerBoundary_y = 4;

    Vector2 GenerateMovementDir()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        if (y > innerBoundary_y && x < -innerBoundary_x) //near topleft
        {
            return (new Vector2(Random.Range(0.5f, 1f), Random.Range(-1f, -0.5f))).normalized;
        }
        else if (y > innerBoundary_y && x > innerBoundary_x) //near topright
        {
            return (new Vector2(Random.Range(-1f, -0.5f), Random.Range(-1f, -0.5f))).normalized;
        }
        else if (y < -innerBoundary_y && x < -innerBoundary_x) //near botleft
        {
            return (new Vector2(Random.Range(1f, 0.5f), Random.Range(0.5f, 1f))).normalized;
        }
        else if (y < -innerBoundary_y && x > innerBoundary_x) //near botright
        {
            return (new Vector2(Random.Range(-1f, -0.5f), Random.Range(0.5f, 1f))).normalized;
        }

        else if (y > innerBoundary_y) //near top
        {
            return (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, -0.5f))).normalized;
        }
        else if (y < -innerBoundary_y) //near bot
        {
            return (new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f))).normalized;
        }
        else if (x < -innerBoundary_x) //near left
        {
            return (new Vector2(Random.Range(0.5f, 1f), Random.Range(-1f, 1f))).normalized;
        }
        else if (x > innerBoundary_x) //near right
        {
            return (new Vector2(Random.Range(-1f, -0.5f), Random.Range(1f, 0.5f))).normalized;
        }


        return (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
    }

    void Panic()
    {
    }

    void HaveBabies()
    {
    }
}
