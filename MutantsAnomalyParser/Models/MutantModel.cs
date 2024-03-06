namespace MutantsAnomalyParser.Models
{
    public class MutantModel
    {
        public required string MapName { get; set; }
        public required List<decimal> CameraPosition { get; set; }
        public required int Year { get; set; }
        public required int Month { get; set;}
        public required int Day { get; set; }
        public required int Hour { get; set; }
        public required int Minute { get; set; }
        public required int Second { get; set; }
        public required decimal Overcast0 { get; set; }
        public required decimal Fog0 { get; set; }
        public required decimal Rain0 { get; set; }
        public required List<EditorObject> EditorObjects { get; set; }
        public class EditorObject
        {
            public required string Uuid { get; set; }
            public required string Type { get; set; }
            public required string DisplayName { get; set; }
            public required List<decimal> Position { get; set; }
            public required List<decimal> Orientation { get; set; }
            public required decimal Scale { get; set; }
            public required int Flags { get; set;}
            public required int M_Id { get; set; }
        }
    }
}
