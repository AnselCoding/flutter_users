namespace certExam.Common
{
    public class TableColumn
    {
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public double DataLength { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return ColumnName;
        }
    }
}