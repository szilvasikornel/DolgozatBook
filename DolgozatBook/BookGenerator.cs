namespace DolgozatBook
{
    /*A könyv nyelve 80%al magyar, 20% angol.
    hozz létre egy listát, benne 15 "random" könyvvel:
    A szerzők nevei és a címek megadásához használhatsz random generáló weboldalakat, AI-t vagy tetszőleges faker módszereket. Próbálj meg ügyelni rá, hogy a cím nyelve reflektáljon a könyv nyelvére.
    az ISBN kód legyen teljesen véletlenszerű, ügyelj rá, hogy a listában semmiképp ne legyen két azonos. (.Any()-vel a legegyszerűbb ellenőrizni hozzáadás előtt)
    A készlet 30% eséllyel 0, egyébként egy 5 és 10 közötti egész szám.
    A könyvnek 70% eséllyel egyetlen szerzője van, egyébként azonos eséllyel 2 vagy 3*/
    public class BookGenerator
    {
        private static Random Random = new Random();
        private static List<string> FirstNames = new List<string>
        {
            "Eli", "Miguel", "Samantha", "Kenny", "Johnny", "Daniel", "Terry", "Demetri", "Kreese", "Tory","Robby"
        };

        private static List<string> LastNames = new List<string>
        {
            "Larusso", "John", "Lawrence", "Nichols", "Diaz", "Payne", "Silver", "Alexopoulos", "Moskowitz","Keene"
        };

        private static List<string> Titles = new List<string>
        {
            "Harry Potter és a bölcsek köve", "Elveszett évek", "A vihar után", "Trónok harca (A tűz és jég dala)", "A világ peremén",
            "A csendes erdő", "Az utolsó pillanat", "Harry Potter és a titkok kamrája", "Sötét árnyak", "A csillagok alatt"
        };

        private static List<string> EnglishTitles = new List<string>
        {
            "Harry Potter and the Sorcerer's Stone", "Lost Years", "After the Storm", "Game of Thrones (A Song of Ice and Fire)", "On the Edge of the World",
            "The Silent Forest", "The Last Moment", "Harry Potter and the secret of chamber", "Dark Shadows", "Beneath the Stars"
        };

        public static List<Book> GenerateBooks()
        {
            List<Book> books = new();
            HashSet<string> isbnSet = new();

            for (int i = 0; i < 15; i++)
            {
                string language = Random.NextDouble() < 0.8 ? "magyar" : "angol";

                string title = language == "magyar"
                    ? Titles[Random.Next(Titles.Count)]
                    : EnglishTitles[Random.Next(EnglishTitles.Count)];

                int year = Random.Shared.Next(2007, 2025);

                int numAuthors = Random.NextDouble() < 0.7 ? 1 : (Random.NextDouble() < 0.5 ? 2 : 3);
                var authorNames = new List<string>();

                for (int j = 0; j < numAuthors; j++)
                {
                    var firstName = FirstNames[Random.Next(FirstNames.Count)];
                    var lastName = LastNames[Random.Next(LastNames.Count)];
                    authorNames.Add($"{firstName} {lastName}");
                }

                var stock = Random.Next(0, 10); 
                                               
                var price = 100 * (Random.Next(10, 101));

                var book = new Book(title, year, language, stock, price, authorNames.ToArray());
                books.Add(book);
            }
            return books;
        }
    }
}
