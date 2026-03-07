using Xunit;
using ExamMovieTracker.Models;

namespace ExamMovieTrackertest
{
    public class MovieTests
    {
        [Fact]
        public void Movie_Initialization_Should_Set_Properties_Correctly()
        {
            // Arrange (Préparation des données)
            var expectedId = 123;
            var expectedTitle = "Inception";
            var expectedVote = 8.8;

            // Act (Exécution de l'action à tester)
            var movie = new Movie
            {
                Id = expectedId,
                Title = expectedTitle,
                VoteAverage = expectedVote
            };

            // Assert (Vérification du résultat)
            Assert.Equal(expectedId, movie.Id);
            Assert.Equal(expectedTitle, movie.Title);
            Assert.Equal(expectedVote, movie.VoteAverage);
        }
    }
}