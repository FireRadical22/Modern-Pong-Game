using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public float ballSpeedMultiplier;

    public GameObject ObjectTracking;
    public GameObject ObjectTrackingPrefab;
    public GameObject Detector;
    public static int difficulty = 2;
    protected Vector2 ballPos;
    protected static float lowerBound = -3.5f;
    protected static float upperBound = 3.5f;
    private static float reactiontime = 0.3f;
    private float time;
    private AIAbilityHolder holder;

    delegate void Move();
    Move move;

    //protected virtual void Move() { }

    void Start()
    {
        
        holder = gameObject.GetComponent<AIAbilityHolder>();
        time = 0.0f;
        startPosition = transform.position;
   
        switch (difficulty)
        {
            case 0:
               move = EasyMove;
               break;
            case 1:
               move = MediumMove;
               break;
            case 2:
               move = HardMove;
               break;
        }
    }

    void Update()
    {
        ballPos = ObjectTracking.transform.position;
        if (holder.isActivated() && holder.currentAbilityInUse is InvisBall)
        {
            switch (difficulty)
            {
                case 0:
                    move = EasyMoveOnActivate;
                    break;
                case 1:
                    move = MediumMoveOnActivate;
                    break;
                case 2:
                    move = HardMoveOnActivate;
                    break;
            }
        }
        else
        {
            switch (difficulty)
            {
                case 0:
                    move = EasyMove;
                    break;
                case 1:
                    move = MediumMove;
                    break;
                case 2:
                    move = HardMove;
                    break;
            }
        }
        move();
        
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCollisionWithBall = collision.gameObject.CompareTag("Ball");
        
        if (isCollisionWithBall)
        {
            if (difficulty == 2)
            {
                ObjectTrackingPrefab.GetComponent<InvisibleBallAI>().ActivatePreFab(ObjectTracking);
            }

            if (Detector.GetComponent<BallAIDetector>().HasReached())
            {
                Detector.GetComponent<BallAIDetector>().Reset();
            }

            collision.GetComponent<Ball>().lastHitByPlayer1 = false;
            Ball ballScript = collision.GetComponent<Ball>();
            Rigidbody2D rb = ObjectTracking.GetComponent<Rigidbody2D>();
            
            if (ballScript.IsBelowSpeedLimit())
            {
                rb.velocity = new Vector2(rb.velocity.x * ballSpeedMultiplier, rb.velocity.y * ballSpeedMultiplier);
            }
            else
            {
                float XSpeedLimit = rb.velocity.x < 0 ? -10.0f : 10.0f;
                float YSpeedLimit = rb.velocity.y < 0 ? -10.0f : 10.0f;
                rb.velocity = new Vector2(XSpeedLimit, YSpeedLimit);
                ballScript.speedLimitReached = true;
            }
        } 

    }

    void EasyMove()
    {
        ballPos = ObjectTracking.transform.position;
        if (ObjectTracking.GetComponent<Ball>().lastHitByPlayer1)
        {
            if (time <= reactiontime)
            {
                transform.localPosition += Vector3.zero;
                time += Time.deltaTime;
            }
            else
            {
                Track();
            }
        } else
        {
            time = 0.0f;
            Track();
        }
    }
    void MediumMove()
    {
        ballPos = ObjectTracking.transform.position;
        //if (ObjectTracking.GetComponent<Ball>().lastHitByPlayer1)
        //{
        Track();
        //}
        //else
        //{
        //    transform.localPosition += Vector3.zero;
        //}
    }

    void HardMove()
    {
        BallAIDetector detector = Detector.GetComponent<BallAIDetector>();
        if (detector.HasReached())
        {
            ballPos = detector.GetFinalDest();
        } else
        {
            ballPos = ObjectTracking.transform.position;
        }
        Track();
    }

    void EasyMoveOnActivate()
    {
        ballPos = ObjectTracking.transform.position;
        if (ObjectTracking.GetComponent<Ball>().lastHitByPlayer1)
        {
            transform.localPosition += Vector3.zero;
        } else
        {
            Track();
        }
    }

    void MediumMoveOnActivate()
    {

        transform.localPosition += Vector3.zero;
    }

    void HardMoveOnActivate()
    {
        if (!(ObjectTracking.GetComponent<Ball>().lastHitByPlayer1))
        {
            transform.localPosition += Vector3.zero;
        } else
        {
            HardMove();
        }
    }

    private void Track()
    {
        if (transform.localPosition.y > ballPos.y && transform.localPosition.y > lowerBound)
        {
            transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
        }
        else if (transform.localPosition.y < ballPos.y && transform.localPosition.y < upperBound)
        {
            transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else
        {
            transform.localPosition += Vector3.zero;
        }
    }

    
}
