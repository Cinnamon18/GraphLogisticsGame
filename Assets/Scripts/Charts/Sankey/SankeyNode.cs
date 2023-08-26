using System;
using System.Collections.Generic;
using Shapes;
using TMPro;
using UnityEngine;
using Utilities;

namespace Charts.Sankey {
	public class SankeyNode : MonoBehaviour, IComparable {
		[Header("Refs")]
		[SerializeField] private Rectangle rectangle;
		[SerializeField] private TMP_Text text;

		// [Header("Variables")]
		/// <summary>
		/// How much ~stuff~ does this node represent.
		/// </summary>
		public float Value {
			get {
				return value;
			} set {
				rectangle.Height = value;
				transform.position = Vector3.up * 0.5f * value + parentColumn.TopOfNodeBefore(this);
				UpdateOutboundLinkPositions();
				this.value = value;
			}
		}
		private float value;

		private SankeyChartColumn parentColumn;
		public List<SankeyLink> OutboundLinks = new List<SankeyLink>();
		public List<SankeyLink> InboundLinks = new List<SankeyLink>();

		public Vector2 Top => transform.position + (Vector3.up * Value * 0.5f);
		public Vector2 Bottom => transform.position - (Vector3.up * Value * 0.5f);

		public SankeyNode SetModel(string name, SankeyChartColumn parentColumn, float value = 0) {
			this.parentColumn = parentColumn;
			text.text = name;
			this.gameObject.name = name;
			Value = value;
			return this;
		}

		/// <summary>
		/// Called by The relevant SankeyLink, no need to call from other contexts.
		/// </summary>
		public void UpdateModelBasedOnNewInboundLink(SankeyLink newInboundLink) {
			InboundLinks.Add(newInboundLink);
		}

		public void UpdateOutboundLinkPositions() {
			int mySpacePreviousLinkTop = 0;
			foreach(var link in OutboundLinks) {
				link.UpdateView();
				mySpacePreviousLinkTop += (int) link.Thickness;
			}
		}

		public Vector2 TopOfOutboundLinkBelow(SankeyLink link) { return TopOfLinkBelow(link, true); }
		public Vector2 TopOfInboundLinkBelow(SankeyLink link) { return TopOfLinkBelow(link, false); }

		private Vector2 TopOfLinkBelow(SankeyLink link, bool isOutbound) {
			var linksListToCheck = isOutbound ? OutboundLinks : InboundLinks;
			var linkIndex = linksListToCheck.IndexOf(link);

			// Link is the first, so the "bottom" is this node's bottom
			if(linksListToCheck.Count == 0 || linkIndex == 0) {
				var offsetVec = isOutbound ? 
				Vector2.right * (GetComponent<Rectangle>().Width * 0.5f) :
				Vector2.left * (GetComponent<Rectangle>().Width * 0.5f);
				return this.Bottom + offsetVec;
			}

			if(linkIndex < 0) {
				throw new Exception("Tried to get top of link not attached to this node");
			}

			var linkBelow = linksListToCheck[linkIndex - 1];

			return isOutbound ? linkBelow.StartTop : linkBelow.EndTop;
		}

		public int CompareTo(object obj) {
			if (obj == null) return 1;

			SankeyChart otherNode = obj as SankeyChart;
			if (otherNode != null) {
				return this.name.CompareTo(otherNode.name);
			} else {
				throw new ArgumentException("Object is not a SankeyNode");
			}
		}
	}
}