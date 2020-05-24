using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isAlive = true;
    private bool canExit = false;

    public float survivalTime = 0.0f;
    public float timeNeededToWin = 30.0f;

    [Header("SURVIVAL METERS")]
    [Range(0.0f, 100.0f)] public float food = 100f;
    [SerializeField] private float foodDecreaseRate = 0.05f;

    [Range(0.0f,100.0f)] public float water = 100f;
    [SerializeField] private float waterDecreaseRate = 0.5f;

    [Range(0.0f, 100.0f)] public float entertainment = 100f;
    [SerializeField] private float entertainmentDecreaseRate = 0.25f;

    [Range(0.0f, 100.0f)] public float mood = 100f;
    [SerializeField] private float moodDecreaseRate = 0.25f;


    public Image fadeOutImage;

    private Vector3 startPos;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        fadeOutImage.enabled = false;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        survivalTime += Time.deltaTime;

        DecreaseFood();
        DecreaseWater();
        DecreaseEntertainment();
        DecreaseMood();

        if (survivalTime >= timeNeededToWin && canExit == false)
        {
            print("You can leave now!");

            canExit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EndGameTrigger")
        {
            if(canExit && isAlive)
            {
                // REPLACE WITH END GAME COROUTINE
                print("You win!");
            }
            else
            {
                // DIE SHOULD BE A COROUTINE
                StartCoroutine("Die");
            }
        }
    }

    IEnumerator Die()
    {
        isAlive = false;
        print("You died!");

        fadeOutImage.enabled = true;

        // DISABLE PLAYER MOVEMENT
        rb.isKinematic = true;

        yield return new WaitForSeconds(1f);

        // MOVE THEM BACK TO THEIR ORIGINAL POSITION
        transform.position = startPos;

        // RESET STATS
        food = 100f;
        water = 100f;
        entertainment = 100f;
        mood = 100f;

       
        yield return new WaitForSeconds(4f);

        fadeOutImage.enabled = false;

        // Makes player move again
        rb.isKinematic = false;

        isAlive = true;
    }

    void DecreaseFood()
    {
        food -= Time.deltaTime * foodDecreaseRate;
    }
    void DecreaseWater()
    {
        water -= Time.deltaTime * waterDecreaseRate;
    }
    void DecreaseEntertainment()
    {
        entertainment -= Time.deltaTime * entertainmentDecreaseRate;
    }
    void DecreaseMood()
    {
        mood -= Time.deltaTime * moodDecreaseRate;
    }
}
