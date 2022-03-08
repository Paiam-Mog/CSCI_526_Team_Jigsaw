using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorColorAssigner : MonoBehaviour
{
    public ColorState mirrorColor = ColorState.White;
    public SpriteRenderer spriteRenderer;
	public ColorTable colorTable;

	private void Awake() {
		if (spriteRenderer && colorTable) {
			spriteRenderer.color = colorTable.GetColor(mirrorColor);
		}
	}
}
