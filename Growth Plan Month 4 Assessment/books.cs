namespace Growth_Plan_Month_4_Assessment
{
    public class books
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public books(int id,string author, string title, decimal price, string imageUrl)
        {
            Id= id;
            Author= author;
            Title= title;
            Price= price;
            ImageUrl= imageUrl;
             
        }

    }

}
