using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float LookAtTarget2D(Transform self, Vector3 targetPosition)
    {
        Vector3 relative = self.InverseTransformPoint(targetPosition);
        var angle = -Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        return angle;
    }
}
