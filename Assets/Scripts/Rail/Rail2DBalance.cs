using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail2DBalance : MonoBehaviour
{
    [SerializeField] LogicalExtension turnOnIfBalance;
    [SerializeField] float balanceErrorMargin = 1f;

    [SerializeField] float correctionEnforcement = 100f;
    [SerializeField] float centerEnforcement = 100f;
    public RailObject2D left, right;
    public Transform leftTargetPoint, rightTargetPoint;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 leftRailCenter = left.rail.transform.position;
        Vector2 rightRailCenter = right.rail.transform.position;

        Vector2 leftPosition = left.transform.position;
        Vector2 rightPosition = right.transform.position;

        Vector2 leftOffset = leftPosition - leftRailCenter;
        Vector2 rightOffset = rightPosition - rightRailCenter;

        float leftDot = Vector2.Dot(left.rail.transform.up, leftOffset);
        float rightDot = Vector2.Dot(right.rail.transform.up, rightOffset);

        float diff = leftDot - rightDot; // positive if left Up and right Down

        float resultTargetOffset = diff / 2;

        Vector2 leftCorrect = leftRailCenter + (Vector2)left.rail.transform.up * resultTargetOffset;
        Vector2 rightCorrect = rightRailCenter - (Vector2)right.rail.transform.up * resultTargetOffset;

        leftTargetPoint.position = leftCorrect;
        rightTargetPoint.position = rightCorrect;

        Vector2 leftCorrection = leftCorrect - leftPosition;
        Vector2 rightCorrection = rightCorrect - rightPosition;

        left.rb2d.AddForce(leftCorrection.normalized * leftCorrection.sqrMagnitude * correctionEnforcement);
        right.rb2d.AddForce(rightCorrection.normalized * rightCorrection.sqrMagnitude * correctionEnforcement);

        left.rb2d.AddForce(-leftOffset.normalized * Mathf.Clamp(leftOffset.sqrMagnitude, 0f, 1f) * centerEnforcement);
        right.rb2d.AddForce(-rightOffset.normalized * Mathf.Clamp(rightOffset.sqrMagnitude, 0f, 1f) * centerEnforcement);

        if(turnOnIfBalance != null)
        {
            if (Mathf.Abs(resultTargetOffset) < balanceErrorMargin)
                turnOnIfBalance.TurnOn();
            else
                turnOnIfBalance.TurnOff();
        }
    }
}
