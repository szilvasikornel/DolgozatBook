namespace DolgozatBook
{
    /*Kereszt- és vezeték névvel, valamint GUID-al rendelkezik,
    a név részei egyenként minimum 3, maximum 32 karakter hosszúak. Egyetlen konstruktor van, ahol a vezeték és keresztnevét
    egyetlen szóközzel tagolt strinkbem kapja. A konstruktor generál neki létrehozáskor egy GUIDet és szétbontja a nevét a 
    megfelelő adattagokra.*/
    public class Author
    {
        private string _firstName;
        private string _lastName;
        public Guid AuthorId { get; private set; }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
        }

        public Author(string fullName)
        {
            var nameParts = fullName.Split(' ');

            if (nameParts.Length != 2)
                throw new ArgumentException("A teljes névnek pontosan két részből kell állnia: kereszt- és vezeték névből.");

            if (nameParts[0].Length < 3 || nameParts[0].Length > 32)
                throw new ArgumentException("A kereszt névnek 3 és 32 karakter között kell lennie.");

            if (nameParts[1].Length < 3 || nameParts[1].Length > 32)
                throw new ArgumentException("A vezeték névnek 3 és 32 karakter között kell lennie.");

            _firstName = nameParts[0];
            _lastName = nameParts[1];
            AuthorId = Guid.NewGuid();
        }
    }
}
