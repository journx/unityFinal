using UnityEngine;

public class SimpleFollowNPC : MonoBehaviour
{
    public GameObject targetPlayer;
    Vector3 dirToPlayer;
    Rigidbody myRB;
    public float moveSpeed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPlayer = wasd3D._instance.gameObject;
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer;
        distanceToPlayer = targetPlayer.transform.position - transform.position;
        Debug.DrawRay(transform.position, distanceToPlayer, Color.red);

        dirToPlayer = distanceToPlayer.normalized;
    }

    void FixedUpdate()
    {
        myRB.AddForce(dirToPlayer * moveSpeed * Time.fixedDeltaTime);
    }
}
