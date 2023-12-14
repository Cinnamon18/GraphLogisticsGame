using UnityEngine;
using Shapes;
using Utilities;
#pragma warning disable 0162 // Unrecahble code warning due to preprocessor statement.
[ExecuteAlways] public class SplineDrawer : ImmediateModeShapeDrawer {
	public const int PointCount = 24;
	public const float Opacity = 0.75f;
	public float Thickness = 0.05f;
	public Color Color = Color.white;
	public Vector2 StartPoint = new Vector2( 0, 0);
	public Vector2 StartFriendshipPoint = new Vector2( 0, 0);
	public Vector2 StartControlPoint = new Vector2( -2, 1);
	public Vector2 EndControlPoint = new Vector2( 2, 1);
	public Vector2 EndFriendshipPoint = new Vector2( 2, 1);
	public Vector2 EndPoint = new Vector2( 0, 0);

	// Code from miss freya holmer herself, see https://shapes.userecho.com/communities/1/topics/42-add-spline-primitive
	public override void DrawShapes( Camera cam ){
		using( Draw.Command( cam, UnityEngine.Rendering.CameraEvent.BeforeForwardAlpha) ){
			// set up static parameters. these are used for all following Draw.Line calls
			// Draw.LineGeometry = LineGeometry.Billboard;
			Draw.PolylineGeometry = PolylineGeometry.Billboard;
			Draw.ThicknessSpace = ThicknessSpace.Meters;
			Draw.Opacity = Opacity;
			Draw.BlendMode = ShapesBlendMode.Transparent;

			Draw.Color = Color;

			// set static parameter to draw in the local space of this object
			Draw.Matrix = transform.localToWorldMatrix;

			using( var path = new PolylinePath() ) {
				path.AddPoint( StartPoint );
				path.AddPoint( StartFriendshipPoint);
				path.BezierTo( StartControlPoint, EndControlPoint, EndFriendshipPoint, PointCount );
				path.AddPoint( EndPoint );
				Draw.Polyline( path, Thickness, PolylineJoins.Simple);
			}

			// DEBUG
			return;
			using( var path = new PolylinePath() ) {
				path.AddPoint( StartPoint );
				path.AddPoint( StartFriendshipPoint);
				path.BezierTo( StartControlPoint, EndControlPoint, EndFriendshipPoint, PointCount );
				path.AddPoint( EndPoint );
				Draw.Polyline( path, 0.05f, PolylineJoins.Simple, Color.black);
			}
		}

	}

	// static Vector3 GetBezierPt( Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t ) {
	// 	float omt = 1f - t;
	// 	float omt2 = omt * omt;
	// 	return a * ( omt2 * omt ) + b * ( 3f * omt2 * t ) + c * ( 3f * omt * t * t ) + d * ( t * t * t );
	// }

}
#pragma warning restore 0162