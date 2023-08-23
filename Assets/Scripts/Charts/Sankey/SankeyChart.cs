using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Charts.Sankey {
	public class SankeyChart : MonoBehaviour{
		public List<SankeyNode> nodes;

		private float width = 1600;
		private float height = 900;

		public void DoInitialDraw() {
			// Oh god I've missed u linq. so expressive. so readable. preformance considerations so negligable in a one off context.
			int columnCount = nodes.OrderByDescending(node => node.Column).First().Column;

			foreach(var node in nodes) {
				var nodeXPos = node.Column * (width / columnCount);
				node.DoInitialDraw(nodeXPos);
			}

			// We gotta do two passes becacuse I need all the nodes initialized to draw the links between them.
			foreach(var node in nodes) {
				node.DoInitialDrawOfLinks();
			}
		}
	}
}