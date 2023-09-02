namespace BalloonCrusher.View
{
using UnityEngine;
using System.Collections.Generic;
using VContainer;
using Controller;
using Model;

public class RecordBoard : MonoBehaviour
{
    [SerializeField] private RecordView _recordView = default;
    [SerializeField] private Transform _recordListParent = default;

    private List<RecordView> _records = new List<RecordView>();
    private RecordRepository _recordRepository = null;
    
    [Inject] public void Inject(RecordRepository recordRepository) => _recordRepository = recordRepository;

    private void OnEnable()
    {
        List<Record> records = _recordRepository.GetRecords();

        CreateRecordList(records);
        
        for (int i = 0; i < records.Count; i++)
        {
            InitRecordView(records[i], _records[i]);
        }
    }

    private void CreateRecordList(List<Record> records)
    {
        if (_records.Count == 0)
        {
            records.ForEach(_ => CreateRecordView());
        }

        else
        {
            for (int i = 0; i < records.Count - _records.Count; i++)
            {
                CreateRecordView();
            }
        }
    }

    private void CreateRecordView() => _records.Add(Instantiate(_recordView, _recordListParent));

    private void InitRecordView(Record data, RecordView view)
    {
        view.Init(data);
        view.onSubmitRenaming += _recordRepository.Rewrite;
    }

    private void OnDisable() => _records.ForEach(record => record.onSubmitRenaming -= _recordRepository.Rewrite);
}
}