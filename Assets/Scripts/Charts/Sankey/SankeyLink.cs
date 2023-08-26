using System;
using Shapes;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Charts.Sankey {
	public class SankeyLink : SplineDrawer, IComparable {
		/// <summary>
		/// Soooo *twirls hair* how do we set our bezier control handles? The approach imma try for now setting it as a
		/// percentage of the distance between the source SankeyNode and the destination one.
		/// </summary>
		private const float BezierControlPointDistanceProportion = 0.2f;
		/// <summary>
		/// Haha... So... I'm thinking to gaurentee we have a point tooootally paralell for the first bit, we add an additional
		/// point in the inwards direction
		/// </summary>
		private const float FlatLineFriendshipPointProportion = 0.025f;
		public SankeyNode Source;
		public SankeyNode Target;
		/// <summary>
		/// A (0-1] value inidicating how much of the source node this link feeds into its target node.
		/// </summary>
		public float RatioOfInput;

		public Vector2 StartTop => (Vector3) (StartPoint) + (Vector3.up * (Thickness / 2f));
		public Vector2 StartBottom => (Vector3) (StartPoint) - (Vector3.up * (Thickness / 2f));
		public Vector2 EndTop => (Vector3) (EndPoint) + (Vector3.up * (Thickness / 2f));
		public Vector2 EndBottom => (Vector3) (EndPoint) - (Vector3.up * (Thickness / 2f));

		public SankeyLink SetModel(SankeyNode source = null, SankeyNode target = null, float ratioOfInput = 0) {
			Source = source;
			Target = target;
			RatioOfInput = ratioOfInput;
			return this;
		}

		/// <summary>
		/// Updates the positions of the for this link. Because Shapes doesn't support beziers as a component, we gotta use immediate mode
		/// mode and redraw these every frame ourselves.
		/// 
		/// The link itself handles per frame redrawing, but the user is responsible for calling this method whenever state upon
		/// which this link depends changes.
		/// </summary>
		public void UpdateView() {
			Thickness = Source.Value * RatioOfInput;

			StartPoint = Source.TopOfOutboundLinkBelow(this) + Vector2.up * (Thickness / 2);
			EndPoint = Target.TopOfInboundLinkBelow(this) + Vector2.up * (Thickness / 2);

			var xDistance = Vector2.right * (EndPoint.x - StartPoint.x);
			StartFriendshipPoint = StartPoint + (xDistance * FlatLineFriendshipPointProportion);
			EndFriendshipPoint = EndPoint - (xDistance * FlatLineFriendshipPointProportion);
			StartControlPoint = StartPoint + (xDistance * BezierControlPointDistanceProportion);
			EndControlPoint = EndPoint - (xDistance * BezierControlPointDistanceProportion);
		}

		public int CompareTo(object obj) {
			if (obj == null) return 1;

			SankeyLink otherLink = obj as SankeyLink;
			if (otherLink != null) {
				return this.name.CompareTo(otherLink.name);
			} else {
				throw new ArgumentException("Object is not a SankeyLink");
			}
		}
	}
}