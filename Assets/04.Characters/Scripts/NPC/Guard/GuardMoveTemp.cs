using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuardMoveTemp : MonoBehaviour
{

    private Rigidbody2D rig;
    public List<Transform> guardMovePoints = new List<Transform>();

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (guardMovePoints.Count <= 0)
            return;

        Transform tempTransform = guardMovePoints.Last();//获取最后一个元素

        switch (guardMovePoints.Count)
        {
            case 2:
                rig.velocity = new Vector2(rig.velocity.x, -speed);
                if (transform.position.y <= tempTransform.position.y)
                {
                    transform.position = tempTransform.position;
                    guardMovePoints.Remove(tempTransform);
                }
                break;
            case 1:
            case 3:
                rig.velocity = new Vector2(speed, rig.velocity.y);
                if (transform.position.x >= tempTransform.position.x)
                {
                    transform.position = tempTransform.position;
                    guardMovePoints.Remove(tempTransform);
                }
                break;
        }

        Debug.Log(guardMovePoints.Count);
        Debug.Log(tempTransform.name);
    }

}
