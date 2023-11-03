using UnityEngine;

namespace Utils.Extensions
{
  public static class MathExtensions
  {
    public static Vector3 ClampPosition(this Vector3 position, Rect borders)
    {
      position.x = Mathf.Max(position.x, borders.xMin);
      position.y = Mathf.Max(position.y, borders.yMin);
      position.x = Mathf.Min(position.x, borders.xMax);
      position.y = Mathf.Min(position.y, borders.yMax);
      return position;
    }

    public static float ClampPosition(this float position, Vector2 borders)
    {
      position = Mathf.Min(position, borders.Max());
      position = Mathf.Max(position, borders.Min());
      return position;
    }

    public static float Min(this Vector2 value) =>
      value.x;

    public static float Max(this Vector2 value) =>
      value.y;

    public static int GetClearDirection(this float direction)
    {
      int i = 0;
      if (direction != 0)
        i = direction > 0 ? 1 : -1;

      return i;
    }
  }
}