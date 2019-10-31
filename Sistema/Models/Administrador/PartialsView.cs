namespace Sistema.Models
{
    public class PartialsView
    {
        public PartialsView()
        {
           

        }
    }

    public class PartialsView_Subheader
    {
        public string app { get; set; }
        public int id { get; set; }
        public int id2 { get; set; }

        public PartialsView_Subheader(string app, int id, int id2)
        {
            this.app = app;
            this.id = id;
            this.id2 = id2;
        }
    }
}