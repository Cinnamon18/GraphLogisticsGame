using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Utilities {
	public class Palette {
		public List<Color> Colors;
		public Color BaseDark;
		public Color BaseLight;

		private System.Random rng = new System.Random();

		public void Shuffle() {
			var newColors = Colors.OrderBy(color => rng.Next()).ToList();
			Colors = newColors;
		}

		public Color this[int key] {
			get => Colors[key];
			set => Colors[key] = value;
		}
	}

	public static class PaletteManager {
		private static int currentColorIndex = 0;

		private static Palette palette = DefaultPalette;
		public static Palette Palette {
			get {
				return palette;
			} set {
				currentColorIndex = 0;
				palette = value;
				palette.Shuffle();
			}
		}

		public static Color GetNextColor() {
			currentColorIndex++;
			currentColorIndex %= Palette.Colors.Count;
			return Palette[currentColorIndex];
		}

		public static Palette DefaultPalette = new Palette {
			Colors = new List<Color>() {
				new Color(50 / 255f,  120 / 255f, 225 / 255f),
				new Color(239 / 255f, 71 / 255f, 111 / 255f),
				new Color(255 / 255f, 209 / 255f, 102 / 255f),
				new Color(6 / 255f, 214 / 255f, 160 / 255f),
				new Color(155 / 255f, 93 / 255f, 229 / 255f),
				new Color(241 / 255f, 91 / 255f, 181 / 255f),
				new Color(0 / 255f, 187 / 255f, 249 / 255f),
				new Color(248 / 255f, 102 / 255f, 36 / 255f),
				new Color(37 / 255f, 206 / 255f, 209 / 255f),
			},
			BaseDark = new Color(237 / 255f, 243 / 255f, 252 / 255f),
			BaseLight = new Color(6 / 255f,   18 / 255f,  35 / 255f)
		};
	}

}