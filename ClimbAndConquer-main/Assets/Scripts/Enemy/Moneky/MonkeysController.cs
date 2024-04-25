using UnityEngine;
using UnityEngine.AI;

public class MonkeysController : MonoBehaviour
{
    public GameObject monkeyPrefab;
    public Transform character;
    public float monkeyCreationCooldownTimeSecond = 10.0f;
    public float monkeyDespawnDistance = 20.0f;
    public int maxNumberOfMonkeysInGame = 5;

    private int currentNumberOfMonekys = 0;
    private DestinationPredictor characterDestinationPredictor;
    private float timer;
    private string monkeyTag = "Enemy";

    void Start()
    {
        characterDestinationPredictor = gameObject.GetComponent<DestinationPredictor>();
    }

    void Update()
    {
        if (character)
        {
            timer += Time.deltaTime;

            if (timer >= monkeyCreationCooldownTimeSecond && currentNumberOfMonekys < maxNumberOfMonkeysInGame)
            {
                InstantiateMonkey();
                currentNumberOfMonekys += 1;
                timer = 0f;
            }

            //CheckToDespawnMonkeys();
        }
        else
        {
            Debug.Log("User Character not attached");
        }
    }

    void InstantiateMonkey()
    {

        float randomXOffset = Random.Range(-1f, 1f);
        float randomXOffset2 = Random.Range(-1f, 1f);
        Vector3 finalPosition = characterDestinationPredictor.GetPrediction() + new Vector3(randomXOffset + (5 * Mathf.Sign(randomXOffset)), 2f, randomXOffset2 + (5 * Mathf.Sign(randomXOffset2)));

        GameObject newMonkey = Instantiate(monkeyPrefab, finalPosition, Quaternion.identity);
        JumpOut(newMonkey);
    }


    void CheckToDespawnMonkeys()
    {
        GameObject[] monkeys = GameObject.FindGameObjectsWithTag(monkeyTag);

        foreach (GameObject monkey in monkeys)
        {
            if (Vector3.Distance(monkey.transform.position, character.position) > monkeyDespawnDistance || monkey.GetComponent<EnemyDamage>().IsHit)
            {
                currentNumberOfMonekys -= 1;
                Destroy(monkey);
                timer = 0f;
            }
        }
    }

    private void JumpOut(GameObject monkey)
    {
        Rigidbody monkeyRigidbody = monkey.GetComponent<Rigidbody>();

        if (monkeyRigidbody)
        {
            Vector3 jumpDirection = (characterDestinationPredictor.GetPrediction() - monkey.transform.position).normalized;
            float jumpForce = 5f; // Adjust the force as needed
            monkeyRigidbody.AddForce(Vector3.up * jumpForce * 2, ForceMode.Impulse);
            //monkeyRigidbody.AddForce(Vector3.left * jumpForce * Mathf.Sign(randomXOffset), ForceMode.Impulse);
            monkeyRigidbody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
    }
}
