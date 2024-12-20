using UnityEngine;
using UniRan = UnityEngine.Random;

public class FliesMovement_Test : MonoBehaviour
{
    [Range(0, 1)]
    public float upSpeed;
    [Range(0, 1)]
    public float YAmplitude;
    [Range(0, 1)]
    public float rotateSpeed;

    public Transform rotateTarget;

    public int rand;
    public float dir;
    private void Awake()
    {
        rand = UniRan.Range(0, 2);
    }
    private void Update()
    {
        if (Time.timeScale != 0f)
            Movement();
    }
    private void Movement()
    {
        if (rand == 0)
        {
            dir = Mathf.Sin(Time.time * upSpeed) * YAmplitude * 0.1f;
        }
        if (rand == 1)
        {
            dir = Mathf.Cos(Time.time * upSpeed) * YAmplitude * 0.1f;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + dir, transform.position.z);
        transform.RotateAround(rotateTarget.position, Vector3.down, rotateSpeed);
    }



}
