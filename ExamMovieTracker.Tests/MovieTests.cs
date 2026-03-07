using Xunit;
using ExamMovieTracker.Models;

namespace ExamMovieTrackertest
{
    public class MovieTests
    {
        // L'attribut [Fact] indique à xUnit que c'est une méthode de test
        [Fact]
        public void Movie_Should_Store_Personal_Comment_Correctly()
        {
            // 1. Arrange : On prépare nos données de test
            var expectedTitle = "Interstellar";
            var expectedComment = "Chef d'oeuvre absolu !";
            var movie = new Movie();

            // 2. Act : On exécute l'action (assigner les valeurs)
            movie.Title = expectedTitle;
            movie.PersonalComment = expectedComment;

            // 3. Assert : On vérifie que le résultat correspond à ce qui est attendu
            Assert.Equal(expectedTitle, movie.Title);
            Assert.Equal(expectedComment, movie.PersonalComment);
        }
    }
}