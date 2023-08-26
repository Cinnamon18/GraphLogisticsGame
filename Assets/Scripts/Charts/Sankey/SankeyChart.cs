using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Charts.Sankey {
	public class SankeyChart : MonoBehaviour {
		private const float SpaceBetweenColumns = 4;
		private const float AspectRatio = 16/9f;

		[Header("Refs")]
		[SerializeField] private SankeyChartColumn sankeyChartColumnPrefab;
		[SerializeField] private SankeyNode sankeyNodePrefab;
		[SerializeField] private SankeyLink sankeyLinkPrefab;	

		[Header("Vars")]
		public List<SankeyChartColumn> columnsAndNodes;
		public List<SankeyLink> links;

		void Start() {
			// Demo SankeyChart def
			var Sun = AddNode("Sun", 0, 4);
			var Grass = AddNode("Grass", 1);
			var Trees = AddNode("Trees", 1);
			var Corn = AddNode("Corn", 1);
			var Cows = AddNode("Cows", 2);
			var Crows = AddNode("Crows", 2);
			var People = AddNode("People", 3);
			var Bears = AddNode("Bears", 3);
			var Jackalope = AddNode("Jackalope", 3);

			
			AddLink(Sun, Grass, 0.2f);
			AddLink(Sun, Trees, 0.8f);
			AddLink(Sun, Corn, 0.1f);
			
			AddLink(Grass, Cows, 0.8f);
			AddLink(Corn, Cows, 0.7f);
			
			AddLink(Corn, Crows, 0.3f);
			AddLink(Grass, Crows, 0.2f);
			AddLink(Trees, Crows, 0.1f);
			
			AddLink(Cows, People, 0.4f);
			AddLink(Crows, People, 0.6f);
			
			AddLink(Cows, Bears, 0.5f);
			AddLink(Crows, Bears, 0.2f);
			
			AddLink(Cows, Jackalope, 0.1f);
			AddLink(Crows, Jackalope, 0.2f);
		}

		// Sidestepping my circlular dependency problem with a very particular initalization order. I'm like the guy in the matrix but for the consequences of my mediocre design 
		public SankeyNode AddNode(string name, int columnIdx, float value = 0) {
			var column = GetOrCreateColumn(columnIdx);
			var newNode = Instantiate(sankeyNodePrefab, transform);
			column.AddNode(newNode);
			newNode.SetModel(name, column, value);;
			return newNode;
		}

		public SankeyChartColumn GetOrCreateColumn(int columnNum) {
			while (columnsAndNodes.Count <= columnNum) {
				var newColumn = Instantiate(sankeyChartColumnPrefab, transform);
				newColumn.transform.position = new Vector3(columnsAndNodes.Count * SpaceBetweenColumns, 0, 0);
				columnsAndNodes.Add(newColumn);
			}
			return columnsAndNodes[columnNum];
		}

		public void AddLink(SankeyNode sourceNode, SankeyNode destinationNode, float ratioOfInput) {
			var newLink = Instantiate(sankeyLinkPrefab, transform).SetModel(sourceNode, destinationNode, ratioOfInput);
			sourceNode.OutboundLinks.Add(newLink);
			destinationNode.InboundLinks.Add(newLink);
			destinationNode.Value += sourceNode.Value * ratioOfInput;
			newLink.UpdateView();
			links.Add(newLink);
		}

		// public void DoInitialDraw() {
		// 	// Oh god I've missed u linq. so expressive. so readable. preformance considerations so negligable in a one off context.
		// 	int columnCount = nodes.OrderByDescending(node => node.Column).First().Column;

		// 	foreach(var node in nodes) {
		// 		var nodeXPos = node.Column * (width / columnCount);
		// 		if(node.Column != 0) {
		// 			node.ResetModel();
		// 		}
		// 		node.UpdateView(nodeXPos);
		// 	}

		// 	// We gotta do two passes becacuse I need all the nodes initialized to draw the links between them. Or at least it'll make my life easier.
		// 	foreach(var node in nodes) {
		// 		node.UpdateOutboundLinkPositions();
		// 	}
		// }

	}
}