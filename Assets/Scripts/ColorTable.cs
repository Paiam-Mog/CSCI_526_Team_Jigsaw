using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Red + Blue + 1 = Magenta
// Red + Green + 1 = Yellow
// Blue + Green + 1 = Cyan
// Red + Blue + Green + 1 = White
public enum ColorState { Red = 1, Blue = 2, Green = 3,
                        Magenta = 4, Yellow = 5, Cyan = 6, White = 7}

public class ColorTable : MonoBehaviour
{
    public ColorState GetColorState(Color color)
    {
        if (color == Color.red)
        {
            return ColorState.Red;
        }
        else if (color == Color.blue)
        {
            return ColorState.Blue;
        }
        else if (color == Color.green)
        {
            return ColorState.Green;
        }
        else if (color == Color.yellow)
        {
            return ColorState.Yellow;
        }
        else if (color == Color.magenta)
        {
            return ColorState.Magenta;
        }
        else if (color == Color.cyan)
        {
            return ColorState.Cyan;
        }
        else
        {
            return ColorState.White;
        }
    }

    public Color GetColor(ColorState color)
    {
        if (color == ColorState.Red)
        {
            return Color.red;
        }
        else if (color == ColorState.Blue)
        {
            return Color.blue;
        }
        else if (color == ColorState.Green)
        {
            return Color.green;
        }
        else if (color == ColorState.Yellow)
        {
            return Color.yellow;
        }
        else if (color == ColorState.Magenta)
        {
            return Color.magenta;
        }
        else if (color == ColorState.Cyan)
        {
            return Color.cyan;
        }
        else
        {
            return Color.white;
        }
    }

    public ColorState ChangeColor(ColorState inputColor, ColorState mirrorColor)
    {
        if (inputColor != mirrorColor)
        {
            int newColorCode = (int)inputColor + (int)mirrorColor + 1;
            if (newColorCode == 4)
            {
                return ColorState.Magenta;
            }
            else if (newColorCode == 5)
            {
                return ColorState.Yellow;
            }
            else if (newColorCode == 6)
            {
                return ColorState.Cyan;
            }
            else
            {
                return ColorState.White;
            }
        }
        else
        {
            return inputColor;
        }
    }
}
