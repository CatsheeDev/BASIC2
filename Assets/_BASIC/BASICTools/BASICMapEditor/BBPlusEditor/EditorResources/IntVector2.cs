using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
[Serializable]
public struct IntVector2
{
    // Token: 0x0600067B RID: 1659 RVA: 0x0002281E File Offset: 0x00020A1E
    public IntVector2(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    // Token: 0x0600067C RID: 1660 RVA: 0x0002282E File Offset: 0x00020A2E
    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        a.x += b.x;
        a.z += b.z;
        return a;
    }

    // Token: 0x0600067D RID: 1661 RVA: 0x00022853 File Offset: 0x00020A53
    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        a.x -= b.x;
        a.z -= b.z;
        return a;
    }

    // Token: 0x0600067E RID: 1662 RVA: 0x00022878 File Offset: 0x00020A78
    public static IntVector2 operator *(IntVector2 a, int b)
    {
        a.x *= b;
        a.z *= b;
        return a;
    }

    // Token: 0x0600067F RID: 1663 RVA: 0x0002289C File Offset: 0x00020A9C
    public static Vector2 operator *(IntVector2 a, float b)
    {
        return new Vector2
        {
            x = (float)a.x * b,
            y = (float)a.z * b
        };
    }

    // Token: 0x06000680 RID: 1664 RVA: 0x000228D2 File Offset: 0x00020AD2
    public static bool operator ==(IntVector2 a, IntVector2 b)
    {
        return a.x == b.x && a.z == b.z;
    }

    // Token: 0x06000681 RID: 1665 RVA: 0x000228F2 File Offset: 0x00020AF2
    public static bool operator !=(IntVector2 a, IntVector2 b)
    {
        return a.x != b.x || a.z != b.z;
    }

    // Token: 0x06000682 RID: 1666 RVA: 0x00022915 File Offset: 0x00020B15
    public IntVector2 Scale(IntVector2 toScale)
    {
        toScale.x *= this.x;
        toScale.z *= this.z;
        return toScale;
    }

    // Token: 0x06000683 RID: 1667 RVA: 0x0002293A File Offset: 0x00020B3A
    public static IntVector2 ControlledRandomPosition(int minX, int maxX, int minZ, int maxZ, System.Random rng)
    {
        return new IntVector2(rng.Next(minX, maxX), rng.Next(minZ, maxZ));
    }

    // Token: 0x06000684 RID: 1668 RVA: 0x00022953 File Offset: 0x00020B53
    public static IntVector2 RandomPosition(int rangeX, int rangeZ)
    {
        return new IntVector2(UnityEngine.Random.Range(0, rangeX), UnityEngine.Random.Range(0, rangeZ));
    }

    // Token: 0x06000685 RID: 1669 RVA: 0x00022968 File Offset: 0x00020B68
    public static Vector2 ToVector2(IntVector2 iv2)
    {
        return new Vector2((float)iv2.x, (float)iv2.z);
    }

    // Token: 0x06000686 RID: 1670 RVA: 0x00022980 File Offset: 0x00020B80
    public static IntVector2 GetGridPosition(Vector3 position)
    {
        return new IntVector2
        {
            x = Mathf.FloorToInt(position.x / 10f),
            z = Mathf.FloorToInt(position.z / 10f)
        };
    }

    // Token: 0x06000687 RID: 1671 RVA: 0x000229C8 File Offset: 0x00020BC8
    public static IntVector2 CombineLowest(IntVector2 vectorA, IntVector2 vectorB)
    {
        IntVector2 intVector = default(IntVector2);
        if (vectorA.x > vectorB.x)
        {
            intVector.x = vectorB.x;
        }
        else
        {
            intVector.x = vectorA.x;
        }
        if (vectorA.z > vectorB.z)
        {
            intVector.z = vectorB.z;
        }
        else
        {
            intVector.z = vectorA.z;
        }
        return intVector;
    }

    // Token: 0x06000688 RID: 1672 RVA: 0x00022A34 File Offset: 0x00020C34
    public static IntVector2 CombineGreatest(IntVector2 vectorA, IntVector2 vectorB)
    {
        IntVector2 intVector = default(IntVector2);
        if (vectorA.x > vectorB.x)
        {
            intVector.x = vectorA.x;
        }
        else
        {
            intVector.x = vectorB.x;
        }
        if (vectorA.z > vectorB.z)
        {
            intVector.z = vectorA.z;
        }
        else
        {
            intVector.z = vectorB.z;
        }
        return intVector;
    }

    public new string ToString()
    {
        return string.Format("{0},{1}", this.x, this.z);
    }

    public override int GetHashCode()
    {
        return x + z * 999; 
    }

    public override bool Equals(object o)
    {
        IntVector2 vec = (IntVector2)o;

        return vec.x == x && vec.z == z;
    }

    // Token: 0x040006DB RID: 1755
    public int x;

    // Token: 0x040006DC RID: 1756
    public int z;
}
