using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _mazeSize;
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);

    [SerializeField] float _chance;

    public Maze maze;

    private void Start()
    {
        GenerateMaze();        

    }

    private void GenerateMaze()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze(_mazeSize);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, 0, y * CellSize.z), Quaternion.identity);
                c.transform.parent = transform;

                var num = Random.Range(0, 100);
                maze.cells[x, y].WallLeft = num < _chance ? false : maze.cells[x, y].WallLeft;
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }
    }
}