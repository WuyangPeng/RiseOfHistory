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

    // ��������������ű���
    [Range(0.1f, 1.5f)] // �� Inspector ����ӻ��飬�������
    public float tileScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tiles = new GameObject[width, height];

        // ����ƫ������ʹ�������
        float offsetX = (width - 1) * tileScale / 2.0f;
        float offsetY = (height - 1) * tileScale / 2.0f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // ����λ�ã�ʹ�����
                Vector3 spawnPosition = new Vector3(x * tileScale - offsetX, y * tileScale - offsetY, 0);

                // ����һ������ʵ��
                GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                tile.name = $"Tile ({x}, {y})";
                tiles[x, y] = tile;

                // ��ȡ MeshRenderer ����������������ɫ
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
