using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSimpleInteractableWithDataBase : XRSimpleInteractable
{
    private TextMeshProUGUI textNameModel;
    private TextMeshProUGUI textInfoModel;
    private Material mOriginal;
    private Material mHover;
    private MeshRenderer objMR;

    private void Start()
    {
        textNameModel = GameObject.Find("TextNameModel").GetComponent<TextMeshProUGUI>();
        textInfoModel = GameObject.Find("TextInfoModel").GetComponent<TextMeshProUGUI>();
        mOriginal = GetComponent<MeshRenderer>().material;
        objMR = GetComponent<MeshRenderer>();
        mHover = Resources.Load("MRedR", typeof(Material)) as Material;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs interactor)
    {
        string path = Application.dataPath + "/StreamingAssets/test.db";
        string sqlCommandText = $"Select infomodel, namepartmodel FROM informationmodel WHERE namemodel = '{gameObject.name}'";
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
                string namepartmodel = sqliteDataReader["namepartmodel"].ToString();
                infoModel = infomodel;
                nameModel = namepartmodel;
            }
        }
        connection.Close();
        Debug.Log("Object name: " + gameObject.name);
        textNameModel.text = nameModel;
        textInfoModel.text = infoModel;
        objMR.material = mHover;
        base.OnHoverEntered(interactor);
    }

    protected override void OnHoverExited(HoverExitEventArgs interactor)
    {
        textNameModel.text = null;
        textInfoModel.text = "";
        objMR.material = mOriginal;
        base.OnHoverExited(interactor);
    }
}
