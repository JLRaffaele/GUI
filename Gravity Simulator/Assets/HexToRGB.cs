using UnityEngine;
using System.Collections;

public class HexToRGB : MonoBehaviour 
{
	// Taken from unity community. Credit to MVI.

	// Note that Color32 and Color implicitly convert to each other. You may pass a Color object to this method without first casting it.
	public static string TryParseColorToHex(Color32 color)
	{
		return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}

	public static bool TryParseHexToColor(string hex, out Color output)
	{
		try
		{
			output = new Color32
				(
				byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
				byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
				byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
				255
				);
			return true;
		}
		catch (System.FormatException)
		{
			output = new Color(0, 0, 0, 0);
			return false;
		}
		catch (System.ArgumentOutOfRangeException)
		{
			output = new Color(0, 0, 0, 0);
			return false;
		}
	}
}
