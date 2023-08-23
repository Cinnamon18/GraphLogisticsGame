using UnityEngine;

namespace Charts.Sankey {
	public class SankeyLink : MonoBehaviour{
		public SankeyNode target;
		/// <summary>
		/// A (0-1] value inidicating how much of the source node this link feeds into its target node.
		/// </summary>
		public float percentOfInput;
	}
}