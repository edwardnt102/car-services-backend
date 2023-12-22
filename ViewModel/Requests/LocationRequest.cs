namespace ViewModel.Requests
{
    public class LocationRequest
    {
        public string ParentId { get; set; }
        public string SearchText { get; set; }
    }

    public class LocationNewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Slug { get; set; }
        public string Name_With_Type { get; set; }
        public string Path { get; set; }
        public string Path_With_Type { get; set; }
        public string Parent_Code { get; set; }
    }
}
