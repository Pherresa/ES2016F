using UnityEngine;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System;

// http://www.mono-project.com/docs/database-access/providers/odbc/

public class Excel_Manage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OdbcConnection oCon = null;
        // Must be saved as excel 2003 workbook, not 2007, mono issue really
        string con = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790; Dbq='" + Application.dataPath + "/Resources/Book1.xls'" + ";";
        Debug.Log(con);
        string yourQuery = "SELECT * FROM [Sheet1$]";
        // our odbc connector
        try
        {
            oCon = new OdbcConnection(con);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        // our command object 
        OdbcCommand oCmd = new OdbcCommand(yourQuery, oCon);
        // table to hold the data 
        DataTable dtYourData = new DataTable("YourData");
        // open the connection 
        oCon.Open();
        // lets use a datareader to fill that table! 
        OdbcDataReader rData = oCmd.ExecuteReader();
        // now lets blast that into the table by sheer man power! 
        dtYourData.Load(rData);
        // close that reader! 
        rData.Close();
        // close your connection to the spreadsheet! 
        oCon.Close();

        /*
        OleDbConnection conexion = null;
        DataSet dataSet = null;
        OleDbDataAdapter dataAdapter = null;
        string hoja = "Isengard";
        string consultaHojaExcel = "Select * from [Sheet1$]";
        string archivo = "/Resources/Book1.xls";
        //string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Application.dataPath + archivo + "';Extended Properties=Excel 12.0;";
        string cadenaConexionArchivoExcel = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790; Dbq=" + Application.dataPath + archivo + ";";
        try
        {
            //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
            Debug.Log("Peta open");
            Debug.Log(cadenaConexionArchivoExcel);
            conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
            conexion.Open(); //abrimos la conexion
            Debug.Log("Pasa open");
            dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
            dataSet = new DataSet(); // creamos la instancia del objeto DataSet
            Debug.Log(dataSet.Tables.ToString());
            //dataAdapter.Fill(dataSet, hoja);//llenamos el dataset
            //dataGridView1.DataSource = dataSet.Tables[0]; //le asignamos al DataGridView el contenido del dataSet
            //conexion.Close();//cerramos la conexion
            //dataGridView1.AllowUserToAddRows = false;       //eliminamos la ultima fila del datagridview que se autoagrega
        }
        catch (Exception ex)
        {
            //en caso de haber una excepcion que nos mande un mensaje de error
            Debug.Log("Error, Verificar el archivo o el nombre de la hoja"+ ex.ToString());
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
