using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// primary color + primary color = secondary color
// (Red + Blue) % 9 = Magenta
// (Red + Green) % 9 = Yellow
// (Blue + Green) % 9 = Cyan

// seconday color + secondary color = shared primary color
// (Magenta + Yellow) % 9 = Red
// (Magenta + Cyan) % 9 = Blue
// (Yellow + Cyan) % 9 = Green

// primary color + secondary color = other primary color if input colors adjacent else white
// secondary color - primary color 

// (white + color) % 9 = white
// (Red + Blue + Green) % 9 = White
public enum ColorState { White = 0, Red = 1, Blue = 3, Green = 5,
                        Magenta = 4, Yellow = 6, Cyan = 8}

public class ColorTable : MonoBehaviour
{
    private List<ColorState> PrimaryColors = new List<ColorState> {ColorState.Red, ColorState.Blue, ColorState.Green};
    private List<ColorState> SecondaryColors = new List<ColorState> {ColorState.Magenta, ColorState.Yellow, ColorState.Cyan};

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
            int inputColorCode = (int)inputColor;
            int mirrorColorCode = (int)mirrorColor;
            int newColorCode;

            // primary + secondary = other primary if colors adjacent else white
            if (PrimaryColors.Contains(inputColor) && SecondaryColors.Contains(mirrorColor))
            {
                newColorCode = mirrorColorCode - inputColorCode;
                
                // no secondary is made up of two copies of same primary
                if (inputColorCode == newColorCode)
                {
                    newColorCode = 0;
                }
            }

            else if (SecondaryColors.Contains(inputColor) && PrimaryColors.Contains(mirrorColor))
            {
                newColorCode = inputColorCode - mirrorColorCode;
                if (mirrorColorCode == newColorCode)
                {
                    newColorCode = 0;
                }
            }

            // handles:
            // 1. primary + primary = secondary
            // 2. secondary + secondary = common primary
            // 3. white + color = color
            else
            {
                newColorCode = (inputColorCode + mirrorColorCode) % 9;
            }
            
            // get the color based on the code
            if (newColorCode == 1)
            {
                return ColorState.Red;
            }
            else if (newColorCode == 3)
            {
                return ColorState.Blue;
            }
            else if (newColorCode == 5)
            {
                return ColorState.Green;
            }
            else if (newColorCode == 4)
            {
                return ColorState.Magenta;
            }
            else if (newColorCode == 6)
            {
                return ColorState.Yellow;
            }
            else if (newColorCode == 8)
            {
                return ColorState.Cyan;
            }
            else
            {
                return ColorState.White;
            }
        }

        // if input color is the same as mirror color
        else
        {
            return inputColor;
        }
    }
}
