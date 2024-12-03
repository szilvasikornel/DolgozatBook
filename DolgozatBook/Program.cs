namespace DolgozatBook
{
    /*100 ismétlést hajtás végre az alábbi folyamatból:
    egy vásárló keres egy bizonyos könyvet (kiválasztunk egyet véletlenszerűen a listaról)
    ha van készleten, akkor csökkentjük a készlet számát és elszámoljuk a bevételt (a könyv árát)
    ha nincs készleten, akkor megpróbáljuk beszerezni:
    50% eséllyel növeljük a készlet mennyiséget random [1-10] darabbal, 50% eséllyel elfogyott a nagykerben is: eltávolítjuk a könyvet a listáról.

    az emulácio után írjuk ki az eladásokbó származó (bruttó) bevételt, hogy hány könyv fogyott ki a nagykerben, illetve hogy mennyit változott a raktárkeszletünk számossaga a kiindulási állapothoz képest (hány db könyv volt kezdetben készleten, mennyi van most, és mennyi ennek az előjeles különbsége)*/
    class Program
    {
        static void Main(string[] args)
        {
            var books = BookGenerator.GenerateBooks();
            var initialStock = books.Sum(b => b.Stock);
            var random = new Random();
            var totalRevenue = 0;
            var outOfStockBooks = 0; 
            var initialBooksCount = books.Count;

            for (int i = 0; i < 100; i++)
            {
                var book = books[random.Next(books.Count)];

                if (book.Stock > 0)
                {
                    book.Stock--;
                    totalRevenue += book.Price;
                }
                else 
                {
                    var restockChance = random.NextDouble();
                    if (restockChance < 0.5)
                    {
                        var newStock = random.Next(1, 11);
                        book.Stock += newStock;
                    }
                    else
                    {
                        books.Remove(book);
                        outOfStockBooks++;
                    }
                }
            }

            var finalStock = books.Sum(b => b.Stock);
            var stockChange = finalStock - initialStock;

            var soldBooksCount = initialBooksCount - books.Count;
            Console.WriteLine($"Összes bevétel: {totalRevenue} Ft");
            Console.WriteLine($"Készlethiányos könyvek száma: {outOfStockBooks}");
            Console.WriteLine($"Készletváltozás: {stockChange} db");
            Console.WriteLine($"Eladott könyvek száma: {soldBooksCount}");
            Console.WriteLine($"Kezdeti készlet: {initialStock} db");
            Console.WriteLine($"Végső készlet: {finalStock} db");

            Console.WriteLine("\nKönyvek listája:\n\n\n");
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
                Console.WriteLine("\n\n");
            }
        }
    }
}
