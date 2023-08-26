using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
	public class Palette {
		public List<Color> colors;
	}

	public class PaletteManager {
		// public void SetPalette(Palette) {

		// }
		// public Color GetColor() {

		// }

		// TODO i think i'm engineering this too early. for now just gonna throw down some static vars lol. 
		public static Color BaseColor =     new Color(50 / 255f,  120 / 255f, 225 / 255f);
		public static Color BaseColorTint = new Color(95 / 255f,  150 / 255f, 231 / 255f);
		public static Color BaseDark =      new Color(237 / 255f, 243 / 255f, 252 / 255f);
		public static Color BaseLight =     new Color(6 / 255f,   18 / 255f,  35 / 255f);

		public static Color GetRandomColor() {
			return new Color(Random.value, Random.value, Random.value);
		}
	}
}