using Xunit;
using ExamMovieTracker.Models;

namespace ExamMovieTrackertest
{
    public class MovieTests
    {
        // Test 1 : Vérifie que le commentaire personnel (la modification locale de l'examen) fonctionne
        [Fact]
        public void Test_Ajout_Commentaire_Personnel()
        {
            // Arrange
            var movie = new Movie();
            var expectedComment = "Un chef d'oeuvre absolu !";

            // Act
            movie.PersonalComment = expectedComment;

            // Assert
            Assert.Equal(expectedComment, movie.PersonalComment);
        }

        // Test 2 : Vérifie que le titre s'enregistre bien
        [Fact]
        public void Test_Assignation_Titre()
        {
            // Arrange
            var movie = new Movie();
            var expectedTitle = "Interstellar";

            // Act
            movie.Title = expectedTitle;

            // Assert
            Assert.Equal(expectedTitle, movie.Title);
        }

        // Test 3 : Vérifie que la note accepte bien les nombres à virgule (le fameux type 'double' !)
        [Fact]
        public void Test_Assignation_Note_API()
        {
            // Arrange
            var movie = new Movie();
            double expectedVote = 8.5; 

            // Act
            movie.VoteAverage = expectedVote;

            // Assert
            Assert.Equal(expectedVote, movie.VoteAverage);
        }

        // Test 4 : Vérifie les valeurs par défaut d'un film quand il vient d'être créé
        [Fact]
        public void Test_Valeurs_Par_Defaut_Nouveau_Film()
        {
            // Arrange & Act
            var movie = new Movie();

            // Assert : Un ID non assigné doit toujours être 0 en C#
            Assert.Equal(0, movie.Id);
        }
    }
}