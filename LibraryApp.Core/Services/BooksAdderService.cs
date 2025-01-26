using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Hosting;

namespace LibraryApp.Core.Services
{
    public class BooksAdderService : IBooksAdderService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IGenresRepository _genresRepository;

        private readonly IAuthorsGetterService _authorsGetterService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksAdderService(IBooksRepository booksRepository, IGenresRepository genresRepository, IAuthorsGetterService authorsGetterService, IWebHostEnvironment webHostEnvironment)
        {
            _booksRepository = booksRepository;
            _genresRepository = genresRepository;
            _authorsGetterService = authorsGetterService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Book> AddBook(BookDTO bookDTO)
        {
            string[] genreNames;
            Guid bookId = Guid.NewGuid();
            Book book = new Book();

            if (bookDTO.Genres.Contains(","))
            {
                genreNames = bookDTO.Genres.Replace(" ", "").Split(",");
            }
            else
            {
                genreNames = bookDTO.Genres.Split(" ");
            }

            List<BookGenre> genres = new List<BookGenre>();

            foreach (string genreName in genreNames)
            {
                Genre genreFromDb = await _genresRepository.GetGenre(genreName);

                if (genreFromDb != null)
                {
                    genres.Add(new BookGenre()
                    {
                        BookGenreId = Guid.NewGuid(),
                        BookId = bookId,
                        Book = book,
                        GenreId = genreFromDb.GenreId,
                        Genre = genreFromDb
                    });
                }
                else
                {
                    Genre newGenre = new Genre()
                    {
                        GenreId = Guid.NewGuid(),
                        Name = genreName
                    };

                    await _genresRepository.AddGenre(newGenre);

                    genres.Add(new BookGenre()
                    {
                        BookGenreId = Guid.NewGuid(),
                        BookId = bookId,
                        Book = book,
                        GenreId = newGenre.GenreId,
                        Genre = newGenre
                    });
                }
            }

            Author bookAuthor = await _authorsGetterService.GetAuthorByFullName(bookDTO.AuthorFirstname, bookDTO.AuthorLastname);


            string bookImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Book_Images");

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + bookDTO.Image.FileName;
            string imagePathToCreateFile = Path.Combine(bookImagesFolder, uniqueFileName);

            using (var fileStream = new FileStream(imagePathToCreateFile, FileMode.Create))
            {
                await bookDTO.Image.CopyToAsync(fileStream);
            }

            string imagePath = $"~/Images/Book_Images/{uniqueFileName}";


            book = new Book()
            {
                BookId = bookId,
                ImagePath = imagePath,
                Author = bookAuthor,
                Title = bookDTO.Title,
                PublicationYear = bookDTO.PublicationYear,
                Genres = genres,
                Rating = 0,
                Language = bookDTO.Language,
                Description = bookDTO.Description,
                Amount = 1,
                Holds = 0
            };

            await _booksRepository.AddBook(book);

            return book;
        }
    }
}
