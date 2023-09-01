namespace BalloonCrusher.Controller
{
using Model;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Linq;

public class RecordRepository
{
    private const string FILE_NAME = "records";
    private const string FILE_EXTENSION = ".balloonCrusher";
    private string _filePath = "";
    
    private List<Record> _records = new List<Record>();

    private string GetFilePath()
    {
        if (string.IsNullOrEmpty(_filePath))
        {
            _filePath = Application.companyName + FILE_NAME + FILE_EXTENSION;
        }

        return _filePath;
    }

    public List<Record> GetRecords()
    {
        Read();
        return _records;
    }

    public void Rewrite(string recordID, string newName)
    {
        Record record = _records.Find(record => record.ID == recordID);
        
        if (record != null)
        {
            record.Name = newName;
            Write();
        }
    }

    public void Write(Record newRecord = null)
    {
        if (newRecord != null)
        {
            Read();
            _records.Add(newRecord);
        }

        string json = JsonConvert.SerializeObject(_records.OrderByDescending(record => record.Score), Formatting.Indented);

        if (!File.Exists(GetFilePath()))
        {
            File.Create(GetFilePath());
        }

        File.WriteAllText(GetFilePath(), json);
    }

    private void Read()
    {
        if (!File.Exists(GetFilePath()))
        {
            return;
        }

        string json = File.ReadAllText(GetFilePath());
        _records = JsonConvert.DeserializeObject<List<Record>>(json);
    }
}
}
