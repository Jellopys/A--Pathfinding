using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridGenerator : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;
    private Grid<StringGridObject> stringGrid;
    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    
    void Start()
    {
        int x = 20;
        int y = 20;
        int size = 5;
        grid = new Grid<HeatMapGridObject>( x , y , size , new Vector3(-x * 2, -y * 2), (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        stringGrid = new Grid<StringGridObject>( x , y , size , new Vector3(-x * 2, -y * 2), (Grid<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));
        // heatMapGenericVisual.SetGrid(grid);
        // heatMapVisual.SetGrid(grid);
        // heatMapBoolVisual.SetGrid(grid);
    }

    private void Update()
    {
        Vector3 position = UtilsClass.GetMouseWorldPosition();

        // if (Input.GetMouseButtonDown(0)){
        //     HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
        //     if (heatMapGridObject != null) {
        //         heatMapGridObject.AddValue(5);
        //     }
        // }


        if (Input.GetKeyDown(KeyCode.A)) { stringGrid.GetGridObject(position)?.AddLetter("A"); }
        if (Input.GetKeyDown(KeyCode.B)) { stringGrid.GetGridObject(position)?.AddLetter("B"); }
        if (Input.GetKeyDown(KeyCode.C)) { stringGrid.GetGridObject(position)?.AddLetter("C"); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { stringGrid.GetGridObject(position)?.AddNumber("1"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { stringGrid.GetGridObject(position)?.AddNumber("2"); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { stringGrid.GetGridObject(position)?.AddNumber("3"); }
    }
}

public class HeatMapGridObject {
    private const int MIN = 0;
    private const int MAX = 100;
    public int value;
    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;


    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }


    public void AddValue(int addValue) {
        value += addValue;
        value = Mathf.Clamp(value , MIN , MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized() {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class StringGridObject {
    private Grid<StringGridObject> grid;
    private int x;
    private int y;
    private string letters;
    private string numbers;

    public StringGridObject(Grid<StringGridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter) {
        letters += letter;
        grid.TriggerGridObjectChanged(x,y);
    }

    public void AddNumber(string number) {
        numbers += number;
        grid.TriggerGridObjectChanged(x,y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
