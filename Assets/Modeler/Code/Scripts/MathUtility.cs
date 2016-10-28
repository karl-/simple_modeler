using UnityEngine;
using System;

namespace Modeler
{
	public static class MathUtility
	{
		/**
		 * Calculate the normal of 3 points:  B-A x C-A
		 */
		public static Vector3 Normal(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			Vector3 cross = Vector3.Cross(p1 - p0, p2 - p0);
			cross.Normalize();
			return cross;
		}

		/**
		 * Returns true if a raycast intersects a triangle.
		 * http://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
		 * http://www.cs.virginia.edu/~gfx/Courses/2003/ImageSynthesis/papers/Acceleration/Fast%20MinimumStorage%20RayTriangle%20Intersection.pdf
		 */
		public static bool RayIntersectsTriangle(Ray InRay, Vector3 InTriangleA,  Vector3 InTriangleB,  Vector3 InTriangleC)
		{
			// @todo lot of unnecessary garbage is generated

			Vector3 e1, e2;
			Vector3 P, Q, T;
			float det, inv_det, u, v;
			float t;

			// find vectors for two edges sharing V1
			e1 = InTriangleB - InTriangleA;
			e2 = InTriangleC - InTriangleA;

			// begin calculating determinant - also used to calculate `u` parameter
			P = Vector3.Cross(InRay.direction, e2);

			// if determinant is near zero, ray lies in plane of triangle
			det = Vector3.Dot(e1, P);

			if(det > -Mathf.Epsilon && det < Mathf.Epsilon)
				return false;

			inv_det = 1f / det;

			// calculate distance from V1 to ray origin
			T = InRay.origin - InTriangleA;

			// calculate u parameter and test bound
			u = Vector3.Dot(T, P) * inv_det;

			// the intersection lies outside of the triangle
			if(u < 0f || u > 1f)
				return false;

			// prepare to test v parameter
			Q = Vector3.Cross(T, e1);

			// calculate V parameter and test bound
			v = Vector3.Dot(InRay.direction, Q) * inv_det;

			// the intersection lies outside of the triangle
			if(v < 0f || u + v  > 1f)
				return false;

			t = Vector3.Dot(e2, Q) * inv_det;

			return t > Mathf.Epsilon;
		}

		/**
		 * Returns the nearest point on each line to the other line.
		 *
		 * http://wiki.unity3d.com/index.php?title=3d_Math_functions
		 * Two non-parallel lines which may or may not touch each other have a point on each line which are closest
		 * to each other. This function finds those two points. If the lines are not parallel, the function
		 * outputs true, otherwise false.
		 */
		public static bool ClosestPointsOnTwoLines(Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2, out Vector3 closestPointLine1, out Vector3 closestPointLine2)
		{
			closestPointLine1 = Vector3.zero;
			closestPointLine2 = Vector3.zero;

			float a = Vector3.Dot(lineVec1, lineVec1);
			float b = Vector3.Dot(lineVec1, lineVec2);
			float e = Vector3.Dot(lineVec2, lineVec2);

			float d = a*e - b*b;

			//lines are not parallel
			if(d != 0.0f)
			{
				Vector3 r = linePoint1 - linePoint2;
				float c = Vector3.Dot(lineVec1, r);
				float f = Vector3.Dot(lineVec2, r);

				float s = (b*f - c*e) / d;
				float t = (a*f - c*b) / d;

				closestPointLine1 = linePoint1 + lineVec1 * s;
				closestPointLine2 = linePoint2 + lineVec2 * t;

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
