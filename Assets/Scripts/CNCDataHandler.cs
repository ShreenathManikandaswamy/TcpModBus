using E2C;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNCDataHandler : MonoBehaviour
{

    [SerializeField]
    E2ChartData E2ChartDataBar = null;

    [SerializeField]
    E2Chart e2ChartBar = null;

    [SerializeField]
    E2ChartData E2ChartDataPie = null;

    [SerializeField]
    E2Chart e2ChartPie = null;

    [SerializeField]
    E2ChartData E2ChartDataline = null;

    [SerializeField]
    E2Chart e2Chartline = null;

    //[SerializeField]
    //E2ChartDataGenerator e2ChartDataGenerator = null;

    

    // Start is called before the first frame update
    void Start()
    {
        //e2ChartDataGenerator.seriesInfo[0].dataYMin = 0;
        List<float> values = new List<float>() { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f };
        //e2ChartDataGenerator.seriesInfo[0].dataYMax = 100;
        UpdateValues(values);
    }

    public void UpdateValues(List<float> chartValues)
    {
        E2ChartDataBar.series[0].dataY.Add(chartValues[0]);
        E2ChartDataBar.series[0].dataY.Add(chartValues[1]);
        E2ChartDataBar.series[0].dataY.Add(chartValues[2]);
        E2ChartDataBar.series[0].dataY.Add(chartValues[3]);
        e2ChartBar.UpdateChart();

        E2ChartDataPie.series[0].dataY.Add(chartValues[4]);
        E2ChartDataPie.series[0].dataY.Add(chartValues[5]);
        E2ChartDataPie.series[0].dataY.Add(chartValues[6]);
        e2ChartPie.UpdateChart();

        E2ChartDataline.series[0].dataY.Add(chartValues[7]);
        E2ChartDataline.series[0].dataY.Add(chartValues[8]);
        E2ChartDataline.series[0].dataY.Add(chartValues[9]);
        
        e2Chartline.UpdateChart();
    }

    /*E2ChartDataBar.series[0].dataY.Add(123);
        E2ChartDataBar.series[0].dataY.Add(23);
        E2ChartDataBar.series[0].dataY.Add(14);
        e2ChartBar.UpdateChart();

        E2ChartDataPie.series[0].dataY.Add(30);
        E2ChartDataPie.series[0].dataY.Add(25);
        E2ChartDataPie.series[0].dataY.Add(45);
        e2ChartPie.UpdateChart();

        E2ChartDataline.series[0].dataY.Add(48);
        E2ChartDataline.series[0].dataY.Add(33);
        E2ChartDataline.series[0].dataY.Add(66);*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
