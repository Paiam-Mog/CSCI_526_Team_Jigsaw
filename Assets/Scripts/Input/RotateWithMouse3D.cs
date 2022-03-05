using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse3D : RotateWithMouse
{
    public BoxCollider2D boxCollider2D;

    protected override bool CheckPoint()
    {
        if (boxCollider2D == null)
        {
            return false;
        }

        return InputHelper.CheckInputPositionInCollider(boxCollider2D);
    }
}
