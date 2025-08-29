using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public Color[] colors;

    private GameObject[,] tiles;

    // 新增：方块的缩放比例
    [Range(0.1f, 1.5f)] // 在 Inspector 中添加滑块，方便调整
    public float tileScale = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        StartCoroutine(ProcessMatches());
    }

    private void GenerateGrid()
    {
        tiles = new GameObject[width, height];

        // 计算偏移量，使网格居中
        float offsetX = (width - 1) * tileScale / 2.0f;
        float offsetY = (height - 1) * tileScale / 2.0f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 调整位置，使其居中
                Vector3 spawnPosition = new Vector3(x * tileScale - offsetX, y * tileScale - offsetY, 0);

                // 创建一个方块实例
                GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                tile.name = $"Tile ({x}, {y})";
                tiles[x, y] = tile;

                // 将 tile 的 Transform 父级设置为 GameManager，保持场景整洁
                tile.transform.parent = transform;

                // 给方块添加 Tile 脚本
                Tile tileScript = tile.AddComponent<Tile>();

                // 随机选择一个颜色 ID
                int randomColorIndex = Random.Range(0, colors.Length);
                tileScript.colorId = randomColorIndex;

                // 获取 MeshRenderer 组件，并随机分配颜色
                MeshRenderer renderer = tile.GetComponent<MeshRenderer>();
                renderer.material.color = colors[randomColorIndex]; 
                
            }
        }
    }

    // 新增：处理匹配的协程，允许下落和补充循环
    IEnumerator ProcessMatches()
    {
        bool hasMatches = true;
        while (hasMatches)
        {
            hasMatches = FindAndRemoveMatches();
            if (hasMatches)
            {
                yield return new WaitForSeconds(0.5f); // 延迟，以便观察
                                                       // CollapseColumns();
                yield return new WaitForSeconds(0.5f);
                // RefillGrid();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    // 新增：查找并移除所有匹配的方块
    private bool FindAndRemoveMatches()
    {
        bool hasMatches = false;
        List<GameObject> matches = new List<GameObject>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject currentTile = tiles[x, y];
                if (currentTile == null) continue;

                // 使用 colorId 进行比较
                int currentColorId = currentTile.GetComponent<Tile>().colorId;

                // 检查水平匹配
                if (x < width - 2)
                {
                    int nextColorId = tiles[x + 1, y].GetComponent<Tile>().colorId;
                    int nextNextColorId = tiles[x + 2, y].GetComponent<Tile>().colorId;

                    // 使用 ID 比较
                    if (currentColorId == nextColorId && nextColorId == nextNextColorId)
                    {
                        matches.Add(currentTile);
                        matches.Add(tiles[x + 1, y]);
                        matches.Add(tiles[x + 2, y]);
                        hasMatches = true;
                    }
                }

                // 检查垂直匹配
                if (y < height - 2)
                {
                    int nextColorId = tiles[x, y + 1].GetComponent<Tile>().colorId;
                    int nextNextColorId = tiles[x, y + 2].GetComponent<Tile>().colorId;

                    // 使用 ID 比较
                    if (currentColorId == nextColorId && nextColorId == nextNextColorId)
                    {
                        matches.Add(currentTile);
                        matches.Add(tiles[x, y + 1]);
                        matches.Add(tiles[x, y + 2]);
                        hasMatches = true;
                    }
                }
            }
        }

        // 移除匹配的方块
        foreach (GameObject match in matches)
        {
            if (match != null)
            {
                Destroy(match);
            }
        }

        return hasMatches;
    }

    // 新增：方块下落，填补空位
    private void CollapseColumns()
    {
        for (int x = 0; x < width; x++)
        {
            int emptySpot = 0;
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == null)
                {
                    emptySpot++;
                }
                else if (emptySpot > 0)
                {
                    // 将方块下移
                    tiles[x, y - emptySpot] = tiles[x, y];
                    tiles[x, y].transform.position -= new Vector3(0, emptySpot * tileScale, 0);
                    tiles[x, y] = null; // 清空旧位置
                }
            }
        }
    }

    // 新增：生成新方块，填补顶部空位
    private void RefillGrid()
    {
        float offsetX = (width - 1) * tileScale / 2.0f;
        float offsetY = (height - 1) * tileScale / 2.0f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == null)
                {
                    Vector3 spawnPosition = new Vector3(x * tileScale - offsetX, y * tileScale - offsetY, 0);

                    GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                    newTile.name = $"Tile ({x}, {y})";
                    tiles[x, y] = newTile;

                    // 给新方块添加 Tile 脚本和 colorId
                    Tile tileScript = newTile.AddComponent<Tile>();
                    int randomColorIndex = Random.Range(0, colors.Length);
                    tileScript.colorId = randomColorIndex;

                    newTile.transform.localScale = new Vector3(tileScale, tileScale, tileScale);
                    MeshRenderer renderer = newTile.GetComponent<MeshRenderer>();          
                    renderer.material.color = colors[randomColorIndex];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
