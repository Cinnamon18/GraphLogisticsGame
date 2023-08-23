using Charts.Sankey;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay {
	public class GameManager {

		[SerializeField] private SankeyChart sankeyChart;
		void Start() {
			sankeyChart.DoInitialDraw();
		}
	}
}