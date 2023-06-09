using Environment.Grid;
using UnityEngine;

namespace Environment.Systems
{
    public class GridSystem
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private GridObject[,] gridObjectArray;
        
        public GridSystem(int width, int height, float cellSize)
        {
            this._width = width;
            this._height = height;
            this._cellSize = cellSize;

            gridObjectArray = new GridObject[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    gridObjectArray[x, z] = new GridObject(this, gridPosition);
                }
            }
        }
        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize),
                Mathf.RoundToInt(worldPosition.z / _cellSize)
            );
        }

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                   GridPosition gridPosition = new GridPosition(x, z);
                   Transform degubTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                   GridDebugObject gridDebugObject = degubTransform.GetComponent<GridDebugObject>();
                   gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return gridObjectArray[gridPosition.x, gridPosition.z];
        }

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            return gridPosition.x >= 0 &&
                   gridPosition.z >= 0 &&
                   gridPosition.x < _width &&
                   gridPosition.z < _height;
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetHeight()
        {
            return _height;
        }

        public float GetCellSize()
        {
            return _cellSize;
        }
    }    
}
