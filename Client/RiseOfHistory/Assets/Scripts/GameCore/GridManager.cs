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
        StartCoroutine(ProcessMatches());
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

                // �� tile �� Transform ��������Ϊ GameManager�����ֳ�������
                tile.transform.parent = transform;

                // ��������� Tile �ű�
                Tile tileScript = tile.AddComponent<Tile>();

                // ���ѡ��һ����ɫ ID
                int randomColorIndex = Random.Range(0, colors.Length);
                tileScript.colorId = randomColorIndex;

                // ��ȡ MeshRenderer ����������������ɫ
                MeshRenderer renderer = tile.GetComponent<MeshRenderer>();
                renderer.material.color = colors[randomColorIndex]; 
                
            }
        }
    }

    // ����������ƥ���Э�̣���������Ͳ���ѭ��
    IEnumerator ProcessMatches()
    {
        bool hasMatches = true;
        while (hasMatches)
        {
            hasMatches = FindAndRemoveMatches();
            if (hasMatches)
            {
                yield return new WaitForSeconds(0.5f); // �ӳ٣��Ա�۲�
                                                       // CollapseColumns();
                yield return new WaitForSeconds(0.5f);
                // RefillGrid();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    // ���������Ҳ��Ƴ�����ƥ��ķ���
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

                // ʹ�� colorId ���бȽ�
                int currentColorId = currentTile.GetComponent<Tile>().colorId;

                // ���ˮƽƥ��
                if (x < width - 2)
                {
                    int nextColorId = tiles[x + 1, y].GetComponent<Tile>().colorId;
                    int nextNextColorId = tiles[x + 2, y].GetComponent<Tile>().colorId;

                    // ʹ�� ID �Ƚ�
                    if (currentColorId == nextColorId && nextColorId == nextNextColorId)
                    {
                        matches.Add(currentTile);
                        matches.Add(tiles[x + 1, y]);
                        matches.Add(tiles[x + 2, y]);
                        hasMatches = true;
                    }
                }

                // ��鴹ֱƥ��
                if (y < height - 2)
                {
                    int nextColorId = tiles[x, y + 1].GetComponent<Tile>().colorId;
                    int nextNextColorId = tiles[x, y + 2].GetComponent<Tile>().colorId;

                    // ʹ�� ID �Ƚ�
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

        // �Ƴ�ƥ��ķ���
        foreach (GameObject match in matches)
        {
            if (match != null)
            {
                Destroy(match);
            }
        }

        return hasMatches;
    }

    // �������������䣬���λ
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
                    // ����������
                    tiles[x, y - emptySpot] = tiles[x, y];
                    tiles[x, y].transform.position -= new Vector3(0, emptySpot * tileScale, 0);
                    tiles[x, y] = null; // ��վ�λ��
                }
            }
        }
    }

    // �����������·��飬�������λ
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

                    // ���·������ Tile �ű��� colorId
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
