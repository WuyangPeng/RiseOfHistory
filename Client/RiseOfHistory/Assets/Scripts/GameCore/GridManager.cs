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

                // 获取 MeshRenderer 组件，并随机分配颜色
                MeshRenderer renderer = tile.GetComponent<MeshRenderer>();
                renderer.material.color = colors[Random.Range(0, colors.Length)];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
