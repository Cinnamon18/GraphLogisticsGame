using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Charts.Sankey {
	public class SankeyChartColumn : MonoBehaviour {
		// Padding between nodes in meters
		private const float Padding = 0.3f;

		public List<SankeyNode> MyNodes = new List<SankeyNode>();

		public void AddNode(SankeyNode node) {
			MyNodes.Add(node);
			node.transform.position = GetTopNodePos() + (Vector2.up * (node.Value / 2f));
		}

		public Vector2 GetTopNodePos() {
			var lastNode = MyNodes.LastOrDefault();
			if(lastNode == null) {
				return transform.position;
			}
			return lastNode.Top;
		}

		internal Vector3 TopOfNodeBefore(SankeyNode sankeyNode) {
			var nodeIndex = MyNodes.IndexOf(sankeyNode);

			// Node is the first, so the "bottom" is this column's origin.
			if(MyNodes.Count == 0 || nodeIndex == 0) {
				return transform.position;
			}

			if(nodeIndex < 0) {
				throw new Exception($"Tried to get top of {sankeyNode.name}, which is not in this column");
			}
			
			return MyNodes[nodeIndex - 1].Top + Vector2.up * Padding;

		}
	}
}