using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P
{
    public int x;
    public int y;
    public P(int _x, int _y) { x = _x; y = _y; }
    public P() { x = 0; y = 0; }
    public static P operator +(P a, P b)
    {
        P res = new P();
        res.x = a.x + b.x;
        res.y = a.y + b.y;
        return res;
    }
    public static P operator -(P a, P b)
    {
        P res = new P();
        res.x = a.x - b.x;
        res.y = a.y - b.y;
        return res;
    }

    public static bool operator ==(P a, P b)
    {
        if (a.x == b.x && a.y == b.y)
            return true;
        return false;
    }
    public static bool operator !=(P a, P b)
    {
        return !(a == b);
    }

    public int ManhattanDistance(P a)
    {
        return Math.Abs(a.x - this.x) + Math.Abs(a.y - this.y);
    }

    public Vector2 toVector2()
    {
        return new Vector2(x, y);
    }

    public string toString()
    {
        return "" + x + "," + y;
    }

}
