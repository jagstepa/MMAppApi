using Microsoft.AspNetCore.Mvc;
using MMAppApi.Controllers;
using MMAppApi.DTO;
using MMAppApi.Interfaces;
using MMAppApi.Models;
using Moq;

namespace MMAppApi.Tests
{
    public class GenreControllerTests
    {
        private readonly Mock<IRepository<Genre>> _repositoryMock;
        private readonly GenreController _controller;

        private List<Genre> GetGenreList()
        {
            List<Genre> genres = new List<Genre>();
            genres.Add(new Genre
            {
                GenreId = 1,
                Name = "Test One"
            });
            genres.Add(new Genre
            {
                GenreId = 2,
                Name = "Test Two"
            });
            genres.Add(new Genre
            {
                GenreId = 3,
                Name = "Test Three"
            });

            return genres;
        }
        

        public GenreControllerTests()
        {
            _repositoryMock = new Mock<IRepository<Genre>>();
            _controller = new GenreController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetGenres_ShouldReturnAllItems()
        {
            // Arrange
            var expectedGenres = GetGenreList();

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedGenres);

            var result = await _controller.GetGenres();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GenreDto>>(okResult.Value);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task GetGenre_ReturnsNotFound_WhenGenreDoesNotExist()
        {
            // Arrange: Setup the mocked service to return null
            _repositoryMock.Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genre?)null);

            // Act: Call the controller method
            var result = await _controller.GetGenre(1);

            // Assert: Verify the result type
            Assert.IsType<NotFoundResult>(result); // Use result.Result when controller returns ActionResult<T>
        }

        [Fact]
        public async Task GetGenre_ReturnsOk_WhenGenreExists()
        {
            // Arrange: Setup the mocked service to return a sample product
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            // Act: Call the controller method
            var result = await _controller.GetGenre(1);

            // Assert: Verify the result type and the returned data
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedGenre = Assert.IsType<GenreDto>(okResult.Value);
            Assert.Equal(genre.GenreId, returnedGenre.GenreId);
        }

        [Fact]
        public async Task GenrePost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var genreDto = new GenreDto { GenreId = genre.GenreId, Name = genre.Name };

            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.CreateGenre(genreDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GenrePost_ReturnsOkObjectResult_WhenModelStateIsValid()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var genreDto = new GenreDto { GenreId = genre.GenreId, Name = genre.Name };

            var result = await _controller.CreateGenre(genreDto);

            var badRequestResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GenrePut_ReturnsNoContentResult()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var genreDto = new GenreDto { GenreId = genre.GenreId, Name = genre.Name };

            var result = await _controller.UpdateGenre(1, genreDto);

            var badRequestResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GenrePut_ReturnsBadRequest()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var genreDto = new GenreDto { GenreId = genre.GenreId, Name = genre.Name };

            var result = await _controller.UpdateGenre(11, genreDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GenrePut_ReturnsNotFoundResult()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var genreDto = new GenreDto { GenreId = 11, Name = genre.Name };

            var result = await _controller.UpdateGenre(11, genreDto);

            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GenreDelete_ReturnsNotFoundResult()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var result = await _controller.DeleteGenre(11);

            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GenreDelete_ReturnsNoContentResult()
        {
            var genre = GetGenreList()[0];
            _repositoryMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(genre);

            var result = await _controller.DeleteGenre(1);

            var badRequestResult = Assert.IsType<NoContentResult>(result);
        }
    }
}
