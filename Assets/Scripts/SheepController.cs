using UnityEngine;

public class SheepController : MonoBehaviour 
{
    public float baseWanderingTime = 2f; // how long sheep will go in the same direction
    public float baseMaxspeed = 5f;
    public float panicWanderingTime = 1.5f;
    public float panicMaxspeed = 7f;
    public float baseAcceleration = 2f;
    public float panicAcceleration = 16f;
    public bool isPanicked = false;
    public int pauseMovementFrames = 30; // how long to stop the sheep before changing direction

    private float timeLeft = 2f; // tracking time before direction switches
    private Vector2 movementDir;
    private Vector2 prevDir = new Vector2(1f, 0.1f);
    private Rigidbody2D sheepRb;
    private SpriteRenderer sheepRenderer;
    private int pauseFrameCount; // Used to pause fixedupdate

    private float innerBoundary_x; // 8 // when do the sheep start turning towards the center?
    private float innerBoundary_y; // 4 

    public Animator animator;


    void Start() 
    {
        sheepRb = this.GetComponent<Rigidbody2D>();
        sheepRenderer = this.GetComponent<SpriteRenderer>();

        GameObject boundaryObj = GameObject.Find("Boundaries");
        BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

        innerBoundary_x = boundary.playerBoundary_x - 6;
        innerBoundary_y = boundary.playerBoundary_y - 5;

        animator = this.GetComponent<Animator>();
    }

	private void OnCollisionEnter2D(Collision2D collision) // collision refers to other object
	{
        if (collision.gameObject.CompareTag("Sheep"))
        {
            SheepController otherSheep = collision.gameObject.GetComponent<SheepController>();
            if (isPanicked) //if this sheep is spooked
            {
                if (otherSheep) { otherSheep.PanicSheep(); } // Panic the other sheep
            }
            GenerateMovementDir(); // change direction of this sheep
            if (otherSheep) { otherSheep.GenerateMovementDir(); } // change direction of other sheep
        }

        else if (collision.gameObject.CompareTag("Boundary")) //switch direction if we hit edge of map
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
                GenerateMovementDir();
                timeLeft += baseWanderingTime;
            }
        }
        else if (isPanicked) // Panic sheep
        {
            if (timeLeft <= 0) // Switch direction
            {
                //sheepRb.velocity = new Vector2(0, 0); // TODO: Maybe implement slower deceleration later
                prevDir = movementDir;
                GenerateMovementDir();
                timeLeft += panicWanderingTime;
            }
        }

        AnimateSheep();
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
        if (isPanicked)
        {
            animator.SetBool("isPanicked", true);
        }
        else
        {
            animator.SetBool("isPanicked", false);
        }

        // Animate based on movement
    }

    public void UnpanicSheep()
    {
        isPanicked = false;
    }

    public void PanicSheep()
    {
        isPanicked = true;
    }

    public void HaveBabies() // spawn sheep from each other?
    {
    }

    public void GenerateMovementDir()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        if (y > innerBoundary_y && x < -innerBoundary_x) //near topleft
        {
            movementDir = (new Vector2(Random.Range(0.5f, 1f), Random.Range(-1f, -0.5f))).normalized;
        }
        else if (y > innerBoundary_y && x > innerBoundary_x) //near topright
        {
            movementDir = (new Vector2(Random.Range(-1f, -0.5f), Random.Range(-1f, -0.5f))).normalized;
        }
        else if (y < -innerBoundary_y && x < -innerBoundary_x) //near botleft
        {
            movementDir = (new Vector2(Random.Range(1f, 0.5f), Random.Range(0.5f, 1f))).normalized;
        }
        else if (y < -innerBoundary_y && x > innerBoundary_x) //near botright
        {
            movementDir = (new Vector2(Random.Range(-1f, -0.5f), Random.Range(0.5f, 1f))).normalized;
        }

        else if (y > innerBoundary_y) //near top
        {
            movementDir = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, -0.5f))).normalized;
        }
        else if (y < -innerBoundary_y) //near bot
        {
            movementDir = (new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f))).normalized;
        }
        else if (x < -innerBoundary_x) //near left
        {
            movementDir = (new Vector2(Random.Range(0.5f, 1f), Random.Range(-1f, 1f))).normalized;
        }
        else if (x > innerBoundary_x) //near right
        {
            movementDir = (new Vector2(Random.Range(-1f, -0.5f), Random.Range(1f, 0.5f))).normalized;
        }
        else
        {
            movementDir = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        }
    }
}
