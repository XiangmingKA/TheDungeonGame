using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    BOSSROOM,
    NORMALROOM,
    STARTROOM,
    NULL
}

public class Node  {
    public Vector2 pos;//在世界中的位置

    public Node pNode;//父节点

    public List<Node> childrenNodes = new List<Node>();

    public bool IsUp;

    public bool IsRight;

    public bool IsDown;

    public bool IsLeft;

    public Type type;

    public Node(Vector2 pos,Type type)
    {
        pNode = null;
        this.pos = pos;
        this.type = type;
    }
}
