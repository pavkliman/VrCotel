using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetInformationAboutObject : XRGrabInteractable
{
    private TextMeshProUGUI textNameModel;
    private TextMeshProUGUI textInfoModel;
    private GameObject gameObjSelect;
    private Material mOriginal;
    private Material mHover;
    private MeshRenderer objMR;

    private void Start()
    {
        textNameModel = GameObject.Find("TextNameModel").GetComponent<TextMeshProUGUI>();
        textInfoModel = GameObject.Find("TextInfoModel").GetComponent<TextMeshProUGUI>();
        gameObjSelect = GameObject.Find($"{gameObject.name}"+"_Select");
        mOriginal = gameObjSelect.GetComponent<MeshRenderer>().material;
        objMR = gameObjSelect.GetComponent<MeshRenderer>();
        mHover = Resources.Load("MRedR", typeof(Material)) as Material;
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        string path = Application.dataPath + "/StreamingAssets/test.db";
        string sqlCommandText = $"Select infomodel, namemodel FROM informationmodel WHERE namepartmodel = '{gameObject.name}'";
        string nameModel = "";
        string infoModel = "";
        SqliteConnection connection = new SqliteConnection("Data Source =" + path);
        connection.Open();
        if (connection.State == ConnectionState.Open)
        {
            SqliteCommand sqliteCommand = new SqliteCommand();
            sqliteCommand.Connection = connection;
            sqliteCommand.CommandText = sqlCommandText;
            SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();

            while (sqliteDataReader.Read())
            {
                var id = sqliteDataReader.GetValue(0);

                //object id = sqliteDataReader[0];
                //object id = sqliteDataReader["id"];
                //string id = sqliteDataReader["id"].ToString();
                string infomodel = sqliteDataReader["infomodel"].ToString();
                string namepartmodel = sqliteDataReader["namemodel"].ToString();
                infoModel = infomodel;
                nameModel = namepartmodel;
            }
        }
        connection.Close();
        Debug.Log("Object name: " + gameObject.name);
        textNameModel.text = nameModel;
        textInfoModel.text = infoModel;
        objMR.material = mHover;
        base.OnSelectEntered(interactor);
    }
    protected override void OnSelectExited(SelectExitEventArgs interactor)
    {
        textNameModel.text = null;
        textInfoModel.text = "";
        objMR.material = mOriginal;
        base.OnSelectExited(interactor);
    }
}
