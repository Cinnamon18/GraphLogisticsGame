using UnityEngine;
using System;

namespace Gameplay {
	public class ClickableHitbox {
		[Header("Refs")]
		[SerializeField] private BoxCollider2D hitBox;

		public event Action OnClick;


		void OnMouseDown() {
			OnClick.Invoke();
		}

	}
}