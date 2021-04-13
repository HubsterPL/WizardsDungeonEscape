using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RailObject2D))]
public class RailObject2DForceTransfer : MonoBehaviour
{
    [HideInInspector]
    public RailObject2D ro2d;
    public float transferMulti = -1f;
    [SerializeField] RailObject2D target;
    public bool debug = false;

    Vector2 oldVelocity = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        ro2d = GetComponent<RailObject2D>();
    }

    private void FixedUpdate()
    {
        Vector2 deltaVelocity = ro2d.rb2d.velocity - oldVelocity;
        Vector2 deltaForce = deltaVelocity * ro2d.rb2d.mass * 10f * transferMulti;
        float deltaForceProjection = Vector2.Dot(ro2d.rail.transform.up, deltaForce);

        target.rb2d.AddForce(target.rail.transform.up * deltaForceProjection);
    }

    void Log(string message)
    {
        if (debug)
            Debug.Log("[ForceTransfer] : " + message);
    }
}
