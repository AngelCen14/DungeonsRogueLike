using System;
using UnityEngine;
using Utils;

namespace ExtensionMethods {
    public static class TransformExtension {
        public static void Flip(this Transform transform, Axis axis, float axisValue) {
            if (axisValue != 0) {
                transform.localScale = new Vector3(
                    axis == Axis.X ? Math.Sign(axisValue) : transform.localScale.x,
                    axis == Axis.Y ? Math.Sign(axisValue) : transform.localScale.y,
                    axis == Axis.Z ? Math.Sign(axisValue) : transform.localScale.z
                );
            }
        }
    }
}