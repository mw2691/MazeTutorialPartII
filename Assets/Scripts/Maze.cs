using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public CellVector size;

    public MazeCell CellPrefab;

    public MazeCellPassage PassagePrefab;

    public MazeCellWall WallPrefab;

    public MazeCellDoor DoorPrefab;

    [Range(0f, 1f)]
    public float doorProbability;

    public float CreationStepDelay;

    private MazeCell[,] cells;
    
    // systematic
    /*
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(this.CreationStepDelay);
        this.cells = new MazeCell[this.size.x, this.size.z];
        for (int x = 0; x < this.size.x; x++)
        {
            for (int z = 0; z < this.size.z; z++)
            {
                this.CreateCell(new CellVector(x, z));
                yield return delay;
            }
        }
        
    }
    */
    // purely random
    /*
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(this.CreationStepDelay);
        this.cells = new MazeCell[this.size.x, this.size.z];
        CellVector coordinates = this.RandomCoordinates;
        while (ContainsCoordinates(coordinates) && this.GetCellAt(coordinates) == null)
        {
            yield return delay;
            this.CreateCell(coordinates);
            coordinates += MazeDirections.RandomValue.ToCellVector();
        }
    }
    */
    // backtracking
    public IEnumerator Generate(GameObject PlayerPrefab = null, int numberOfExplorers = 1)
    {
        WaitForSeconds delay = new WaitForSeconds(this.CreationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        this.DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            //yield return delay;
            this.DoNextGenerationStep(activeCells);
        }

        if (PlayerPrefab != null)
        {
            for (int i = 0; i < numberOfExplorers; i++)
            {
                GameObject player = GameObject.Instantiate(PlayerPrefab) as GameObject;
                player.transform.position = this.GetCellAt(this.RandomCoordinates).transform.position;
            }
        }

        yield return null;
    }
    
    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(this.CreateCell(this.RandomCoordinates));
    }
    // random
    /*
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        CellVector coordinates = currentCell.coordinates + direction.ToCellVector();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = this.GetCellAt(coordinates);
            if (neighbor == null)
            {
                neighbor = this.CreateCell(coordinates);
                this.CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                this.CreateWall(currentCell, neighbor, direction);
                activeCells.RemoveAt(currentIndex);
            }
        }
        else
        {
            this.CreateWall(currentCell, null, direction);
            activeCells.RemoveAt(currentIndex);
        }
    }
    */
    // with connection check
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        //int currentIndex = activeCells.Count / 2;
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        CellVector coordinates = currentCell.coordinates + direction.ToCellVector();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = this.GetCellAt(coordinates);
            if (neighbor == null)
            {
                neighbor = this.CreateCell(coordinates);
                this.CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                this.CreateWall(currentCell, neighbor, direction);
                // No longer remove the cell here.
            }
        }
        else
        {
            this.CreateWall(currentCell, null, direction);
            // No longer remove the cell here.
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeCellPassage prefab = Random.value < doorProbability ? this.DoorPrefab : this.PassagePrefab;
        MazeCellPassage passage = Instantiate(prefab) as MazeCellPassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazeCellPassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeCellWall wall = Instantiate(this.WallPrefab) as MazeCellWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(this.WallPrefab) as MazeCellWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private MazeCell GetCellAt(CellVector coordinates)
    {
        return this.cells[coordinates.x, coordinates.z];
    }

    private MazeCell CreateCell(CellVector coordinates)
    {
        MazeCell newCell = Instantiate(this.CellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.coordinates = coordinates;
        newCell.transform.parent = this.transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - this.size.x * 0.5f + 0.5f, 0f, coordinates.z - this.size.z * 0.5f + 0.5f);
        return newCell;
    }

    public CellVector RandomCoordinates
    {
        get
        {
            return new CellVector(Random.Range(0, this.size.x), Random.Range(0, this.size.z));
        }
    }

    public bool ContainsCoordinates(CellVector coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < this.size.x && coordinate.z >= 0 && coordinate.z < this.size.z;
    }
}
