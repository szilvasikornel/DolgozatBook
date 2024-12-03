namespace DolgozatBook
{
    /*- ISBN azonosító (egyedi azonosító, 10 számjegyű számsor)
    - szerzők listája ([1-3] elemű Author osztály példányait tartalmazó lista)
    - cím (minimum 3, maximum 64 karakter hosszú karakterlánc)
    - kiadás éve (2007 és a jelen év közti egész szám)
    - nyelv (csak az angol, német és magyar az elfogadott érték)
    - készlet (egész szám, minimum 0)
    - ár (1000 és 10000 közötti, kerek 100as szám)

    A készleten kívül minden mező csak a konstruktorban állítható be (private set-es)

    két konstruktor:
    egyikben minden tulajdonságot meg kell adni, a szerzők esetében params-al, csak a neveiket tartalmazó stringeket, rendre utolsó paraméterként
    A másikban csak a címet, és egyetlen szerző nevét. Ebben az esetben a készlet 0, a nyelv magyar, az ár 4500,  az ISBN random, a kiadás éve 2024 - a szerzők listája pedig egy elemű.

    A ToString override-ban valósítsd meg, hogyha a "szerzők listája" egyetlen tagok tartalmaz, a property kiírása előtt ne "szerzők:", hanem "szerző:" legyen, valamint ha a készlet értéke nulla, akkor az "xy db" helyett a "beszerzés alatt" szöveg jelenjen meg.*/
    public class Book
    {
        private string _isbn;
        private string _title;
        private int _publicationYear;
        private string _language;
        private int _stock;
        private int _price;
        private List<Author> _authors;

        public string ISBN
        {
            get => _isbn;
            private set
            {
                _isbn = value;
            }
        }

        public List<Author> Authors
        {
            get => _authors;
            private set
            {
                _authors = value;
            }
        }

        public string Title
        {
            get
            {
                if (_title.Length < 3 || _title.Length > 64)
                    throw new ArgumentException("A szöveg 3 és 64 karakter hosszúságú lehet!");
                return _title;
            }
            private set
            {
                _title = value;
            }
        }

        public int PublicationYear
        {
            get
            {
                if (_publicationYear < 2007 || _publicationYear > DateTime.Now.Year)
                    throw new ArgumentException("Az év 2007 és a jelenlegi év közöttinek kell lennie!");
                return _publicationYear;
            }
            private set
            {
                _publicationYear = value;
            }
        }

        public string Language
        {
            get
            {
                if (!new[] { "angol", "német", "magyar" }.Contains(_language))
                    throw new ArgumentException("Nyelv csak ezek közül lehet: angol, német, magyar!");
                return _language;
            }
            private set
            {
                _language = value;
            }
        }

        public int Stock
        {
            get
            {
                if (_stock < 0)
                    throw new ArgumentException("Csak 0 vagy valamilyen pozitív szám lehet!");
                return _stock;
            }
            set
            {
                _stock = value;
            }
        }

        public int Price
        {
            get
            {
                if (_price < 1000 || _price > 10000 || _price % 100 != 0)
                    throw new ArgumentException("1000 és 10000 közötti, kerek 100as szám lehet csak!");
                return _price;
            }
            private set
            {
                _price = value;
            }
        }

        public Book(string title, int publicationYear, string language, int stock, int price, params string[] authorNames)
        {
            Title = title;
            PublicationYear = publicationYear;
            Language = language;
            Stock = stock;
            Price = price;

            Authors = authorNames.Select(name => new Author(name)).ToList();

            ISBN = GenerateRandomISBN();
        }

        public Book(string title, string authorName)
        {
            Title = title;
            PublicationYear = 2024;
            Language = "magyar";
            Stock = 0;
            Price = 4500;

            Authors = new List<Author> { new Author(authorName) };

            ISBN = GenerateRandomISBN();
        }

        public string GenerateRandomISBN()
        {
            var random = new Random();
            return string.Join("", Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
        }

        public override string ToString()
        {
            var authorLabel = Authors.Count == 1 ? "szerző:" : "szerzők:";
            var stockLabel = Stock == 0 ? "beszerzés alatt" : $"{Stock} db";
            var authorsList = string.Join(", ", Authors.Select(a => $"{a.FirstName} {a.LastName}"));

            return $"Cím: {Title}\n{authorLabel} {authorsList}\nKiadás éve: {PublicationYear}\nNyelv: {Language}\nKészlet: {stockLabel}\nÁr: {Price} Ft\nISBN: {ISBN}";
        }
    }
}
