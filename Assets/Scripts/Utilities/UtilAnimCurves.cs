using System.Collections.Generic;
using UnityEngine;

namespace Utilities {

	// TODO one day we can read the keyframe data from the frames at compile time and bake them in as static vars...
	// It'll take a bit of editorscripting tho...
	public class UtilAnimCurves : MonoBehaviour{
		public static UtilAnimCurves Instance {
			get {
				if(instance != null) {
				} else {
					var maybeAnimCurves = GameObject.FindAnyObjectByType<UtilAnimCurves>();
					if(maybeAnimCurves != null) {
						instance = maybeAnimCurves;
					} else {
						Debug.LogError("No UtilAnimCurves obj in scene :(");
					}
				}
				return instance;
			}
		} 
		private static UtilAnimCurves instance;

		public AnimationCurve WiggleAnimationCurve;
		public AnimationCurve OvershootAnimationCurve;
	}
}