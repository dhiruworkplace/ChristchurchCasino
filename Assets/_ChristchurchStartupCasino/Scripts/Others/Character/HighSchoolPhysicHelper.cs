using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighSchoolPhysicHelper
{
    public static Vector3 GetAngleThrow_SkewThrow(Vector3 position, Vector3 target, float v, Vector3 up, bool getLowAnge = true) {

        float angle1, angle2;

        Vector3 targetVec = position - target;

        //Vertical distance
        float h = targetVec.y;

        //Reset y so we can get the horizontal distance x
        targetVec.y = 0f;

        //Horizontal distance
        float l = targetVec.magnitude;

        //Gravity
        float g = 9.81f;


        //Calculate the angles

        float vSqr = v * v;

        float delta = (vSqr * vSqr) - g * (g * l * l + 2 * h * vSqr);

        //Check if we are within range
        if (delta >= 0f) {
            float sqrtDelta = Mathf.Sqrt(delta);
            float top1 = vSqr + sqrtDelta;
            float top2 = vSqr - sqrtDelta;

            float bottom = g * l;

            angle1 = Mathf.Atan2(top1, bottom) * Mathf.Rad2Deg;
            angle2 = Mathf.Atan2(top2, bottom) * Mathf.Rad2Deg;

            float angle = 0;
            if(getLowAnge) {
                angle = angle2;
            }
            else {
                angle = angle1;
            }

            return (Quaternion.AngleAxis(angle, Vector3.Cross((target - position), up)) * (target - position)).normalized * v;
        }
        return Vector3.zero;
        
    }

    public static Vector3 GetVelocity_SkewThrow(Vector3 position, Vector3 target, float angle, Vector3 up) {
        target.y = position.y;
        Vector3 targetVec = target - position;

        //Vertical distance
       // float h = Mathf.Abs(targetVec.y);
        float h = targetVec.y;
        //Reset y so we can get the horizontal distance x
        targetVec.y = 0f;

        //Horizontal distance
        float l = targetVec.magnitude;

        //Gravity
        //float g = 50f;
        float g = -Physics.gravity.y;
        float tan = Mathf.Tan(angle * Mathf.Deg2Rad);
        //float v = Mathf.Sqrt(g * l * tan * tan / (h + tan));
        float v = Mathf.Sqrt(l * g * (1 + h * 0) / Mathf.Sin(angle * 2 * Mathf.Deg2Rad));
        float v1 = Mathf.Sqrt(l * g * (1 + h * 1) / Mathf.Sin(angle * 2 * Mathf.Deg2Rad));
        targetVec = target - position;

        return (Quaternion.AngleAxis(angle, Vector3.Cross(targetVec, up)) * targetVec).normalized * v;
    }

    public static Vector3 RandomPositionOnPlaneCircle(Vector3 orgin, float radius, Vector3 normal) {
        Vector3 randomPoint;
        do {
            randomPoint = Vector3.Cross(Random.insideUnitSphere, normal);
        } while (randomPoint == Vector3.zero);

        randomPoint.Normalize();
        randomPoint *= radius;
        randomPoint += orgin;

        return randomPoint;
    }

    public static Vector3 RandomPositionInsideOnPlaneCircle(Vector3 orgin, float radius, Vector3 normal) {
        Vector3 randomPoint;
        do {
            randomPoint = Vector3.Cross(Random.insideUnitSphere, normal);
        } while (randomPoint == Vector3.zero);

        randomPoint.Normalize();
        randomPoint *= Random.Range(0.1f, radius);
        randomPoint += orgin;

        return randomPoint;
    }

    public static Vector3 RotationVector(Vector3 direction, float angle, Vector3 normal) {
        Vector3 newDirection = (Quaternion.AngleAxis(angle, normal) * direction).normalized * direction.magnitude;
        return newDirection;
    }

    public static int RandomWithRate(float[] percents) {
        float random = Random.Range(0f, 100f);
        float current = 0f;
        for(int i = 0; i < percents.Length; ++i) {
            current += percents[i];
            if(random <= current) {
                return i;
            }
        }
        return -1;
    }

}
