#define DONE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMap : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    //public GameObject Player;

    public int Size;//每边可以扩展的最大长度

    public int roomSize;//每个房间的尺寸

    public int maxNodeNumber_Up;//Up边可以行成的最多Node数
    public int maxNodeNumber_Down;//Down边可以行成的最多Node数
    public int maxNodeNumber_Right;//Right边可以行成的最多Node数
    public int maxNodeNumber_Left;//Left边可以行成的最多Node数

    private Node[,] map;
    private Node[] testNode;
    private int[] temp=new int[25];
    private int num;
    //private Node[] random;
    private int rang;

    struct Border
    {
        public List<Vector2> points;
    }

    //private int currentNodeNumber;
    // Use this for initialization
    void Start()
    {
        map = new Node[Size * 2, Size * 2];
        //currentNodeNumber = maxNodeNumber;
        rang = ChooseRandom();
        InitMap();
        CreateRoad();
        Create();
        //Instantiate(Player, new Vector3(0,0,0), Quaternion.identity);
    }

    void InitMap()
    {
        for (int i = 0; i < Size * 2; i++)
        {
            for (int j = 0; j < Size * 2; j++)
            {
                map[i, j] = new Node(new Vector2(i - Size, j - Size), Type.NULL);
                if (i == Size && j == Size)
                {
                    map[i, j].type = Type.NULL;
                    map[i, j].IsUp = false;
                    map[i, j].IsRight = false;
                    map[i, j].IsDown = false;
                    map[i, j].IsLeft = false;

                }
            }
        }
    }

    void CreateRoad()
    {

        map[Size, Size].childrenNodes.Add(map[Size + 1, Size]);
        map[Size, Size].IsRight = true;
        map[Size + 1, Size].pNode = map[Size, Size];
        map[Size + 1, Size].IsLeft = true;
        map[Size, Size].childrenNodes.Add(map[Size - 1, Size]);
        map[Size, Size].IsLeft = true;
        map[Size - 1, Size].pNode = map[Size, Size];
        map[Size - 1, Size].IsRight = true;
        map[Size, Size].childrenNodes.Add(map[Size, Size + 1]);
        map[Size, Size].IsUp = true;
        map[Size, Size + 1].pNode = map[Size, Size];
        map[Size, Size + 1].IsDown = true;
        map[Size, Size].childrenNodes.Add(map[Size, Size - 1]);
        map[Size, Size].IsDown = true;
        map[Size, Size - 1].pNode = map[Size, Size];
        map[Size, Size - 1].IsUp = true;
        map[Size, Size].pNode = map[Size, Size];

        Border upBorder = new Border();
        upBorder.points = new List<Vector2>();
        upBorder.points.Add(new Vector2(0, 0));
        upBorder.points.Add(new Vector2(Size, Size));
        upBorder.points.Add(new Vector2(-Size, Size));
        CreateLine(Vector2.up, upBorder, maxNodeNumber_Up);

        Border downBorder = new Border();
        downBorder.points = new List<Vector2>();
        downBorder.points.Add(new Vector2(0, 0));
        downBorder.points.Add(new Vector2(Size, -Size));
        downBorder.points.Add(new Vector2(-Size, -Size));
        CreateLine(Vector2.down, downBorder, maxNodeNumber_Down);

        Border rightBorder = new Border();
        rightBorder.points = new List<Vector2>();
        rightBorder.points.Add(new Vector2(0, 0));
        rightBorder.points.Add(new Vector2(Size, Size));
        rightBorder.points.Add(new Vector2(Size, -Size));
        CreateLine(Vector2.right, rightBorder, maxNodeNumber_Right);

        Border leftBorder = new Border();
        leftBorder.points = new List<Vector2>();
        leftBorder.points.Add(new Vector2(0, 0));
        leftBorder.points.Add(new Vector2(-Size, Size));
        leftBorder.points.Add(new Vector2(-Size, -Size));
        CreateLine(Vector2.left, leftBorder, maxNodeNumber_Left);
    }


    void CreateLine(Vector2 pos, Border border, int currentNodeNumber)
    {
        Node currentNode = map[(int)pos.x + Size, (int)pos.y + Size];

        while (currentNode != null)
        {
            Node testNode = HasAvailableNode(currentNode, border);
            if (testNode == null || currentNodeNumber <= 0)
            {
                //Debug.Log("叶子节点或者节点数已满");
                return;
            }
            else
            {
                testNode.pNode = currentNode;
                currentNode.childrenNodes.Add(testNode);
                if (testNode.pos.x == currentNode.pos.x && testNode.pos.y == currentNode.pos.y + 1)
                {
                    currentNode.IsUp = true;
                    testNode.IsDown = true;
                }
                else if (testNode.pos.x == currentNode.pos.x && testNode.pos.y == currentNode.pos.y - 1)
                {
                    currentNode.IsDown = true;
                    testNode.IsUp = true;
                }
                else if (testNode.pos.x == currentNode.pos.x + 1 && testNode.pos.y == currentNode.pos.y)
                {
                    currentNode.IsRight = true;
                    testNode.IsLeft = true;
                }
                else if (testNode.pos.x == currentNode.pos.x - 1 && testNode.pos.y == currentNode.pos.y)
                {
                    currentNode.IsLeft = true;
                    testNode.IsRight = true;
                }
                currentNodeNumber--;
                //currentNode = testNode;
                CreateLine(testNode.pos, border, currentNodeNumber);
                currentNodeNumber--;
            }
        }
    }

    Node HasAvailableNode(Node currentNode, Border border)
    {
        Node availableNode = null;
        int i;
        Vector2 testPos = Vector2.zero;
        bool available = false;
        bool allVisited = true;

        Vector2[] neighborPos = new Vector2[4];
        neighborPos[0] = new Vector2(1, 0);
        neighborPos[1] = new Vector2(-1, 0);
        neighborPos[2] = new Vector2(0, 1);
        neighborPos[3] = new Vector2(0, -1);

        int[] visited = new int[4];
        for (int n = 0; n < visited.Length; n++)
        {
            visited[n] = 0;
        }

        i = Random.Range(0, visited.Length);
        while (!available)
        {
            for (int n = 0; n < visited.Length; n++)
            {
                if (visited[n] == 0)
                {
                    allVisited = false;
                    break;
                }
            }
            if (!allVisited)
            {
                allVisited = true;
                testPos = new Vector2(currentNode.pos.x + neighborPos[i].x, currentNode.pos.y + neighborPos[i].y);

                if (testPos.x + Size >= Size * 2 || testPos.y + Size >= Size * 2 || testPos.x + Size < 0 || testPos.y + Size < 0)
                {
                    //在边界外
                    visited[i] = 1;
                    i = Random.Range(0, visited.Length);
                    continue;
                }
                else if ((map[(int)testPos.x + Size, (int)testPos.y + Size] != null) && (map[(int)testPos.x + Size, (int)testPos.y + Size].pNode != null))
                {
                    //有父节点的不考虑
                    visited[i] = 1;
                    i = Random.Range(0, visited.Length);
                    continue;
                }
                else if (!IsInRange(border, testPos))
                {
                    //在边界外的点不考虑
                    visited[i] = 1;
                    i = Random.Range(0, visited.Length);
                    continue;
                }
                else
                {
                    available = true;
                }
            }
            else
            {
                return null;
            }
        }
        if (available)
        {
            //Debug.Log("找到可用节点" + map[(int)testPos.x + Size, (int)testPos.y + Size].pos + currentNode.pos);
            availableNode = map[(int)testPos.x + Size, (int)testPos.y + Size];
            return availableNode;
        }
        return null;
    }

    //判断一个点是否在多边形内
    bool IsInRange(Border border, Vector2 pos)
    {
        bool inRange = false;
        List<Vector2> points = border.points;
        for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
        {
            if (Mathf.Abs(pos.x) == Mathf.Abs(pos.y)) continue;
            if (((points[i].y > pos.y) != (points[j].y > pos.y)) && (pos.x < (points[j].x - points[i].x) * (pos.y - points[i].y) / (points[j].y - points[i].y) + points[i].x))
            {
                inRange = !inRange;
            }
        }
        return inRange;
    }

    /*void CreateRoom()
    {
        for (int i = 0; i < Size * 2; i++)
        {
            for (int j = 0; j < Size * 2; j++)
            {
                if (map[i, j].pNode == null)
                {
                    continue;
                }
                GameObject go;
                if (i == positions[rang].x && j == positions[rang].y)
                   go = Instantiate(ChooseCrossPrefab(), map[i, j].pos * roomSize, Quaternion.identity);
                //GameObject temp = new GameObject();
                //temp = ChooseRoomPrefab(map[i, j]);
                //if(random[i,j,code]=)
                else go = Instantiate(roomPrefabs[14], map[i, j].pos * roomSize, Quaternion.identity);//生成房间的函数调用
                go.transform.SetParent(GameObject.Find("GO").transform);
            }
        }
    }*/

    void Create()
    {
        for( int k = 0;k <=num;k++ )
        {
            if (testNode[k] == null)
            {
                continue;
            }
            GameObject go = Instantiate(roomPrefabs[RoomPrefabCode(testNode[k])], testNode[k].pos * roomSize, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("GO").transform);

        }
    }


    int RoomPrefabCode(Node node)
    {
        //中心节点

        Debug.Log(node.IsUp + " " + node.IsRight + " " + node.IsDown + " " + node.IsLeft);

        if (node.IsUp == true && node.IsRight == true && node.IsDown == true && node.IsLeft == true)
        {
            return 14;
        }
        else if (node.IsUp == false && node.IsRight == true && node.IsDown == true && node.IsLeft == true)
        {
            return 13;
        }
        else if (node.IsUp == true && node.IsRight == false && node.IsDown == true && node.IsLeft == true)
        {
            return 12;
        }
        else if (node.IsUp == true && node.IsRight == true && node.IsDown == false && node.IsLeft == true)
        {
            return 11;
        }
        else if (node.IsUp == true && node.IsRight == true && node.IsDown == true && node.IsLeft == false)
        {
            return 10;
        }
        else if (node.IsUp == false && node.IsRight == false && node.IsDown == true && node.IsLeft == true)
        {
            return 9;
        }
        else if (node.IsUp == false && node.IsRight == true && node.IsDown == false && node.IsLeft == true)
        {
            return 8;
        }
        else if (node.IsUp == false && node.IsRight == true && node.IsDown == true && node.IsLeft == false)
        {
            return 7;
        }
        else if (node.IsUp == true && node.IsRight == false && node.IsDown == false && node.IsLeft == true)
        {
            return 6;
        }
        else if (node.IsUp == true && node.IsRight == false && node.IsDown == true && node.IsLeft == false)
        {
            return 5;
        }
        else if (node.IsUp == true && node.IsRight == true && node.IsDown == false && node.IsLeft == false)
        {
            return 4;
        }
        else if (node.IsUp == false && node.IsRight == false && node.IsDown == false && node.IsLeft == true)
        {
            return 3;
        }
        else if (node.IsUp == false && node.IsRight == false && node.IsDown == true && node.IsLeft == false)
        {
            return 2;
        }
        else if (node.IsUp == false && node.IsRight == true && node.IsDown == false && node.IsLeft == false)
        {
            return 1;
        }
        else if (node.IsUp == true && node.IsRight == false && node.IsDown == false && node.IsLeft == false)
        {
            return 0;
        }
        else
        {
            // Debug.Log("can't find this type of room!!!");
            return -1;
        }
    }//选择房间的代码


    int ChooseRandom()
    {
        return Random.Range(0, 4);
    }

    int ChooseRoom()
    {
        return 0;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(180, 120, 100, 150), "这是一个button按钮", "button"))
        {
            for (int i = 0; i < GameObject.Find("GO").transform.childCount; i++)
            {
                Destroy(GameObject.Find("GO").transform.GetChild(i).gameObject);
            }
            rang = ChooseRandom();
            InitMap();
            CreateRoad();
            Create();
        }
    }//设定按钮功能的函数

}
