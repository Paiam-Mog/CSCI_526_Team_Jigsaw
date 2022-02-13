using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable3DObject : DraggableObject
{
    public BoxCollider2D boxCollider2D;

    protected override bool CheckPoint() {
        if (boxCollider2D == null) {
            return false;
        }

        return InputHelper.CheckInputPositionInCollider(boxCollider2D);
    }

    protected override Vector2 InputMoveDelta(InputHelper.InputData inputData) {
        return inputData.inputPositionDeltaWorldVector;
    }
}
