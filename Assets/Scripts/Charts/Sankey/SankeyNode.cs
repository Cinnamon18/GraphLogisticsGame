using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Charts.Sankey {
	public class SankeyNode : MonoBehaviour{
		/// <summary>
		/// There's a series of columns moving from left to right on a Sankey graph. Which one does this occupy?
		/// </summary>
		public int Column;
		/// <summary>
		/// How much ~stuff~ does this node represent.
		/// </summary>
		public int Value;
		public List<SankeyLink> links;

		public void DoInitialDraw(float xPos) {
			// TODO Set pixel height to value.
			transform.position = transform.position.SetX(xPos);
		}

		public void DoInitialDrawOfLinks() {

		}
	}
}