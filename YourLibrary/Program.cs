namespace YourLibrary
{
    class Program
    {
        private const string Value = "Select: ";

        static void Main(string[] args)
        {
            Library library = new();
            bool running = true;

            while (running)
            {
                // Display main menu options
                Console.WriteLine("\t" + "Welcome to Your Library");
                Console.WriteLine("\t" + "-----------------------");
                Console.WriteLine("\t" + "[1] - Add a new book");
                Console.WriteLine("\t" + "[2] - View all books");
                Console.WriteLine("\t" + "[3] - Delete a book");
                Console.WriteLine("\t" + "[4] - Find book");
                Console.WriteLine("\t" + "[5] - Close program");
                Console.WriteLine("\t" + "------------------------");
                Console.Write("\t" + Value);

                // Take user input for main menu choice
                if (int.TryParse(s: Console.ReadLine(), out int mainMenuChoice))
                {
                    Console.WriteLine();

                    switch (mainMenuChoice)
                    {
                        case 1:
                            library.AddNewBook();
                            break;

                        case 2:
                            library.Show();
                            break;

                        case 3:
                            library.DeleteBook();
                            break;

                        case 4:
                            library.SearchBook();
                            break;

                        case 5:
                            running = false;
                            break;

                        default:
                            Console.WriteLine("\t" + "Please, choose a number (1-5)");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\t" + "Invalid input. Please enter a number (1-5).");
                }
            }
        }
    }

    // Class representing the library and its operations
    class Library
    {
        private List<Book> books = new List<Book>();

        // Display all books in the library
        public void Show()
        {
            if (books.Count > 0)
            {
                foreach (Book book in books)
                {
                    Console.WriteLine("\t" + book);
                }
            }
            else
            {
                Console.WriteLine("\t" + "The library is empty");
                Console.WriteLine("\t" + "--------------------");
            }
        }

        // Add a new book to the library
        public void AddNewBook()
        {
            string title = GetInput("\t" + "Title of the book: ");
            string author = GetInput("\t" + "Author of the book: ");
            int year = GetIntInput("\t" + "Year of the book: ");
            string isbn = GetInput("\t" + "ISBN of the book: ");
            string lendingStatus = GetInput("\t" + "Is the book lent? (lent or Not): ");

            Console.WriteLine("\t" + "Is the book [1] Novel, [2] Magazine, [3] Collection");
            Console.Write("\t" + "Select: ");

            // Take user input for the type of book
            if (int.TryParse(Console.ReadLine(), out int bookTypeChoice))
            {
                switch (bookTypeChoice)
                {
                    case 1:
                        books.Add(new Novel(title, author, year, isbn, lendingStatus));
                        Console.WriteLine("\t" + "-----------");
                        Console.WriteLine("\t" + "Book added.");
                        Console.WriteLine("\t" + "-----------");
                        break;

                    case 2:
                        books.Add(new Magazine(title, author, year, isbn, lendingStatus));
                        Console.WriteLine("\t" + "-----------");
                        Console.WriteLine("\t" + "Book added.");
                        Console.WriteLine("\t" + "-----------");
                        break;

                    case 3:
                        books.Add(new Collection(title, author, year, isbn, lendingStatus));
                        Console.WriteLine("\t" + "-----------");
                        Console.WriteLine("\t" + "Book added.");
                        Console.WriteLine("\t" + "-----------");
                        break;

                    default:
                        Console.WriteLine("\t" + "-------------------------------");
                        Console.WriteLine("\t" + "Invalid choice. Book not added.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\t" + "------------------------------");
                Console.WriteLine("\t" + "Invalid input. Book not added.");
            }
        }

        // Search for a book in the library
        public void SearchBook()
        {
            if (books.Count > 0)
            {
                Console.WriteLine("\t" + "Choose a number to search for your book:");
                Console.WriteLine("\t" + "----------------------------------------");
                Console.WriteLine("\t" + "[1] - Title");
                Console.WriteLine("\t" + "[2] - Author");
                Console.WriteLine("\t" + "[3] - Year");
                Console.WriteLine("\t" + "------------");
                Console.Write("\t" + "Select: ");

                // Take user input for search 
                if (int.TryParse(Console.ReadLine(), out int searchMenuChoice))
                {
                    switch (searchMenuChoice)
                    {
                        case 1:
                            string titleSearch = GetInput("\t" + "Enter the title keyword: ");
                            var titleResults = books.FindAll(b => b.Title.Contains(titleSearch));
                            DisplaySearchResults(titleResults, titleSearch);
                            Console.WriteLine("\t" + "-------------------------");
                            break;

                        case 2:
                            string authorSearch = GetInput("\t" + "Enter the author keyword: ");
                            var authorResults = books.FindAll(b => b.Author.Contains(authorSearch));
                            DisplaySearchResults(authorResults, authorSearch);
                            Console.WriteLine("\t" + "-----------------------");
                            break;

                        case 3:
                            int yearSearch = GetIntInput("\t" + "Enter the year to search for: ");
                            var yearResults = books.FindAll(b => b.Year == yearSearch);
                            DisplaySearchResults(yearResults, yearSearch.ToString());
                            Console.WriteLine("\t" + "------------------------------");
                            break;

                        default:
                            Console.WriteLine("\t" + "Invalid choice.");
                            Console.WriteLine("\t" + "---------------");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\t" + "Invalid input.");
                    Console.WriteLine("\t" + "--------------");
                }
            }
            else
            {
                Console.WriteLine("\t" + "The library is empty");
                Console.WriteLine("\t" + "--------------------");
            }
        }

        // Delete a book from the library
        public void DeleteBook()
        {
            if (books.Count > 0)
            {
                Console.WriteLine("\t" + "[1] - Delete a selected book");
                Console.WriteLine("\t" + "[2] - Delete all books");
                Console.WriteLine("\t" + "[3] - Back to the main menu");
                Console.WriteLine("\t" + "----------------------------");
                Console.Write("\t" + "Select: ");

                // Take user input for delete operation
                if (int.TryParse(Console.ReadLine(), out int deleteMenuChoice))
                {
                    switch (deleteMenuChoice)
                    {
                        case 1:
                            DeleteSelectedBook();
                            break;

                        case 2:
                            books.Clear();
                            Console.WriteLine("\t" + "All books have been deleted.");
                            Console.WriteLine("\t" + "----------------------------");
                            break;

                        case 3:
                            Console.WriteLine("\t" + "Back to the main menu.");
                            Console.WriteLine("\t" + "----------------------");
                            break;

                        default:
                            Console.WriteLine("\t" + "Invalid choice.");
                            Console.WriteLine("\t" + "---------------");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\t" + "Invalid input.");
                    Console.WriteLine("\t" + "--------------");
                }
            }
            else
            {
                Console.WriteLine("\t" + "The library is empty.");
                Console.WriteLine("\t" + "---------------------");
            }
        }

        // Delete a selected book from the library
        private void DeleteSelectedBook()
        {
            Console.WriteLine("\t" + "Select the book to delete:");

            // Display the list of books for selection
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine("\t" + $"[{i + 1}] {books[i].Title}");
                Console.WriteLine("\t" + "---------------------------");
            }
            Console.Write("\t" + "Select: ");

            // Take user input for the book to be deleted
            if (int.TryParse(Console.ReadLine(), out int deleteChoice) && deleteChoice > 0 && deleteChoice <= books.Count)
            {
                books.RemoveAt(deleteChoice - 1);
                Console.WriteLine("\t" + "The book has been deleted.");
                Console.WriteLine("\t" + "--------------------------");
            }
            else
            {
                Console.WriteLine("\t" + "Invalid choice. Book not deleted.");
                Console.WriteLine("\t" + "---------------------------------");
            }
        }

        // Display search results
        private void DisplaySearchResults(List<Book> results, string searchCriteria)
        {
            if (results.Count > 0)
            {
                Console.WriteLine("\t" + $"Found books matching '{searchCriteria}':");
                foreach (Book book in results)
                {
                    Console.WriteLine(book);
                    Console.WriteLine("\t" + "----------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("\t" + "----------------------------------------");
                Console.WriteLine("\t" + $"No books found with '{searchCriteria}'.");
            }
        }

        // Get user input for string
        private string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        // Get user input for integer
        private int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("\t" + "Invalid input. Please enter a valid number.");
                Console.WriteLine("\t" + "-------------------------------------------");
                return GetIntInput(prompt);
            }
        }
    }

    // Class representing a generic book
    class Book
    {
        public string Title { get; }
        public string Author { get; }
        public int Year { get; }
        public string ISBN { get; }
        public string LendingStatus { get; }
        public string BookType { get; }

        // Constructor for the Book class
        public Book(string title, string author, int year, string isbn, string lendingStatus, string bookType)
        {
            Title = title;
            Author = author;
            Year = year;
            ISBN = isbn;
            LendingStatus = lendingStatus;
            BookType = bookType;
        }

        // Override ToString method for customized string representation
        public override string ToString()
        {
            return "\t" + $"Title: '{Title}', Author: {Author}, Year: {Year}, ISBN: {ISBN}, Lending Status: {LendingStatus}, Type: {BookType}"
                + "\n"+"---------------------------------------------------------------------------------------------------------------------";
        }
    }

    // Class representing a novel, inheriting from Book
    class Novel : Book
    {
        public Novel(string title, string author, int year, string isbn, string lendingStatus)
            : base(title, author, year, isbn, lendingStatus, "Novel")
        {
        }
    }

    // Class representing a magazine, inheriting from Book
    class Magazine : Book
    {
        public Magazine(string title, string author, int year, string isbn, string lendingStatus)
            : base(title, author, year, isbn, lendingStatus, "Magazine")
        {
        }
    }

    // Class representing a collection, inheriting from Book
    class Collection : Book
    {
        public Collection(string title, string author, int year, string isbn, string lendingStatus)
            : base(title, author, year, isbn, lendingStatus, "Collection")
        {
        }
    }
}
