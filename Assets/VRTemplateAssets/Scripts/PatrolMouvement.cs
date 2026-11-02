using UnityEngine;

public class PatrolMouvement : MonoBehaviour
{

    public enum MovementType { MoveTowards, Lerp, Smooth };

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float speed = 1f;
    [SerializeField] private MovementType mode = MovementType.MoveTowards;
    [SerializeField] private float arrivalThreshold = 0.05f;

    private Vector3 target;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = pointA.position;
        target = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(mode)
        {
            case MovementType.MoveTowards:
                MoveTowards();
                break;
            case MovementType.Lerp:
                Lerp();
                break;
            case MovementType.Smooth:
                Smooth();
                break;
        }
    }

    private void MoveTowards()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < arrivalThreshold)
        {
            target = (target == pointB.position) ? pointA.position : pointB.position;
        }
    }

    private void Lerp()
    {
        float t = Mathf.PingPong(Time.time * speed / Vector3.Distance(pointA.position, pointB.position), 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }

    private void Smooth()
    {
        float t = Mathf.PingPong(Time.time * speed / Vector3.Distance(pointA.position, pointB.position), 1f);
        float smoothT = Mathf.SmoothStep(0f, 1f, t);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, smoothT);
    }


}
