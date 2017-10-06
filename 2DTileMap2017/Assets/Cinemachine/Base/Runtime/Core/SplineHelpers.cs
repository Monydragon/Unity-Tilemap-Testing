using UnityEngine;

namespace Cinemachine.Utility
{
    internal static class SplineHelpers
    {
        public static Vector3 Bezier3(
            float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            t = Mathf.Clamp01(t);
            float d = 1f - t;
            return d * d * d * p0 + 3f * d * d * t * p1
                + 3f * d * t * t * p2 + t * t * t * p3;
        }

        public static Vector3 BezierTangent3(
            float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            t = Mathf.Clamp01(t);
            return (-3f * p0 + 9f * p1 - 9f * p2 + 3f * p3) * t * t
                +  (6f * p0 - 12f * p1 + 6f * p2) * t
                -  3f * p0 + 3f * p1;
        }

        public static float Bezier1(float t, float p0, float p1, float p2, float p3)
        {
            t = Mathf.Clamp01(t);
            float d = 1f - t;
            return d * d * d * p0 + 3f * d * d * t * p1
                + 3f * d * t * t * p2 + t * t * t * p3;
        }

        public static float BezierTangent1(
            float t, float p0, float p1, float p2, float p3)
        {
            t = Mathf.Clamp01(t);
            return (-3f * p0 + 9f * p1 - 9f * p2 + 3f * p3) * t * t
                +  (6f * p0 - 12f * p1 + 6f * p2) * t
                -  3f * p0 + 3f * p1;
        }

        public static void ComputeSmoothControlPoints(
            ref Vector4[] knot, ref Vector4[] ctrl1, ref Vector4[] ctrl2, bool looped)
        {
            int numPoints = knot.Length;

            // Special case for 2 points and not looped
            if (!looped && numPoints == 2)
            {
                ctrl1[0] = Vector4.Lerp(knot[0], knot[1], 0.33333f);
                ctrl2[0] = Vector4.Lerp(knot[0], knot[1], 0.66666f);
                return;
            }

            var a = new float[numPoints];
            var b = new float[numPoints];
            var c = new float[numPoints];
            var r = new float[numPoints];
            for (int axis = 0; axis < 4; ++axis)
            {
                int n = numPoints - 1;

                // Linear into the first segment if not looped
                if (!looped)
                {
                    a[0] = 0;
                    b[0] = 2;
                    c[0] = 1;
                    r[0] = knot[0][axis] + 2 * knot[1][axis];
                }

                // Internal segments
                int last = looped ? n + 1 : n - 1;
                for (int i = looped ? 0 : 1; i < last; ++i)
                {
                    int next = (i == n) ? 0 : i + 1;
                    a[i] = 1;
                    b[i] = 4;
                    c[i] = 1;
                    r[i] = 4 * knot[i][axis] + 2 * knot[next][axis];
                }

                // Linear out of the last segment if not looped
                if (!looped)
                {
                    a[n - 1] = 2;
                    b[n - 1] = 7;
                    c[n - 1] = 0;
                    r[n - 1] = 8 * knot[n - 1][axis] + knot[n][axis];
                }

                // Solve with Thomas algorithm
                for (int i = looped ? 0 : 1; i < (looped ? n+1 : n); ++i)
                {
                    int prev = (i == 0) ? n : i-1;
                    float m = a[i] / b[prev];
                    b[i] = b[i] - m * c[prev];
                    r[i] = r[i] - m * r[prev];
                }

                // Compute ctrl1
                int first = looped ? 0 : n-1;
                ctrl1[first][axis] = r[first] / b[first];
                for (int i = looped ? n : n - 2; i >= 0; --i)
                {
                    int next = (i == n) ? 0 : i + 1;
                    ctrl1[i][axis] = (r[i] - c[i] * ctrl1[next][axis]) / b[i];
                }

                // Compute ctrl2 from ctrl1
                for (int i = 0; i <= n; i++)
                {
                    int next = (i == n) ? 0 : i + 1;
                    ctrl2[i][axis] = 2 * knot[next][axis] - ctrl1[next][axis];
                }
                if (!looped)
                    ctrl2[n - 1][axis] = 0.5f * (knot[n][axis] + ctrl1[n - 1][axis]);
            }
        }
    }
}
