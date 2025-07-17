using System.Data;

namespace Graphite.Source.Infrastructure.Wrappers
{
    public class SkippingInvalidRowsDataReader : IDataReader
    {
        private readonly IDataReader _inner;
        private readonly int _dataframeIdOrdinal;
        private readonly int _valueOrdinal;
        private readonly int _donorIdOrdinal;
        private readonly int _dateOrdinal;

        public SkippingInvalidRowsDataReader(IDataReader inner)
        {
            _inner = inner;
            _dataframeIdOrdinal = _inner.GetOrdinal("DataframeId");
            _valueOrdinal = _inner.GetOrdinal("Value");
            _donorIdOrdinal = _inner.GetOrdinal("DonorId");
            _dateOrdinal = _inner.GetOrdinal("Date");
        }

        public bool Read()
        {
            while (_inner.Read())
            {
                if (!IsInvalidRow())
                    return true;
            }
            return false;
        }

        private bool IsInvalidRow()
        {
            if (_inner.IsDBNull(_dataframeIdOrdinal)) return true;
            if (_inner.IsDBNull(_valueOrdinal)) return true;
            var value = _inner.GetValue(_valueOrdinal);
            if (value is string s && string.IsNullOrWhiteSpace(s)) return true;
            if (_inner.IsDBNull(_donorIdOrdinal)) return true;
            if (_inner.IsDBNull(_dateOrdinal)) return true;
            return false;
        }

        public int FieldCount => _inner.FieldCount;
        public object GetValue(int i) => _inner.GetValue(i);
        public string GetName(int i) => _inner.GetName(i);
        public int GetOrdinal(string name) => _inner.GetOrdinal(name);
        public bool IsDBNull(int i) => _inner.IsDBNull(i);
        public void Close() => _inner.Close();
        public DataTable GetSchemaTable() => _inner.GetSchemaTable();
        public bool NextResult() => _inner.NextResult();
        public int Depth => _inner.Depth;
        public bool IsClosed => _inner.IsClosed;
        public int RecordsAffected => _inner.RecordsAffected;
        public void Dispose() => _inner.Dispose();
        public bool GetBoolean(int i) => _inner.GetBoolean(i);
        public byte GetByte(int i) => _inner.GetByte(i);
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => _inner.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        public char GetChar(int i) => _inner.GetChar(i);
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => _inner.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        public DateTime GetDateTime(int i) => _inner.GetDateTime(i);
        public decimal GetDecimal(int i) => _inner.GetDecimal(i);
        public double GetDouble(int i) => _inner.GetDouble(i);
        public float GetFloat(int i) => _inner.GetFloat(i);
        public Guid GetGuid(int i) => _inner.GetGuid(i);
        public short GetInt16(int i) => _inner.GetInt16(i);
        public int GetInt32(int i) => _inner.GetInt32(i);
        public long GetInt64(int i) => _inner.GetInt64(i);
        public string GetString(int i) => _inner.GetString(i);
        public int GetValues(object[] values) => _inner.GetValues(values);
        public IDataReader GetData(int i) => _inner.GetData(i);
        public string GetDataTypeName(int i) => _inner.GetDataTypeName(i);
        public Type GetFieldType(int i) => _inner.GetFieldType(i);
        public object this[int i] => _inner[i];
        public object this[string name] => _inner[name];
    }
}
