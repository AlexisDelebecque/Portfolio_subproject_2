using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.FunctionDomain;

namespace WebApi.Services.FunctionService
{
    public class FunctionService
    {
        private readonly PortfolioContext _ctx; 
        //Skal bare indsætte _ctx i alle nedenstående, samt tingene i contexten og domæneobjekterne.
        public FunctionService(string connectionString = "")
        {
            _ctx = new PortfolioContext(connectionString);
        }

        //Connectionstring is host, db, uid & pwd
        //all methods where private static in earlier program, also had to change ctx from readonly, as we need to access it. 
        //namebasics.rating does not exist. This function will probably only work when rating is present. Maybe alter table in start or handle exception. 
        public List<SearchResultsPopularActorsCoPlayers> SearchForPopularActorsCoPlayers(string actorId)
        {
            Console.WriteLine("Search Results Popular Coplayers by Id");
            //var ctx = new NorthwindContex(connectionString);

            var result = _ctx.SearchResultsPopularActorsCoPlayers.FromSqlInterpolated($"select * from popular_actors_co_players({actorId})");


            List<SearchResultsPopularActorsCoPlayers> searchResultsPopularActorsCoPlayers = new List<SearchResultsPopularActorsCoPlayers>();

            foreach (var searchResult in result)
            {
                Console.WriteLine($"{searchResult.Id}, {searchResult.PrimaryName}, {searchResult.Rating}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsPopularActorsCoPlayers.Add(searchResult);
            }
            return searchResultsPopularActorsCoPlayers;
        }
        public List<SearchResultRecommended> Recommended(string titleId)
        {
            Console.WriteLine("Search Results Recommended");

            var result = _ctx.SearchResultRecommendeds.FromSqlInterpolated($"select * from recommended({titleId})");


            List<SearchResultRecommended> searchResultsCoPlayers = new List<SearchResultRecommended>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.PrimaryTitle}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsCoPlayers.Add(searchResult);
            }
            return searchResultsCoPlayers;
        }

        public void FindRating(string actorId) //Shoudl return result probably, but does not return proper value right now.
        {
            Console.WriteLine("Search Results from Find Rating");
            
            //only structured string search needéd user=null for some reason. 
            var result = _ctx.Database.ExecuteSqlInterpolated($"select * from find_rating({actorId})");
            
            Console.WriteLine(result);
            //return result; //dont remember why its void, need to check if output is faulty.
        }

        public List<SearchResultStructuredActorSearch> StructuredActorSearch(string str1, string str2, string str3, string str4)
        {
            Console.WriteLine("Search Results from Structured Actor Search");
            
            //only structured string search needéd user=null for some reason. 
            var result = _ctx.SearchResultStructuredActorSearches.FromSqlInterpolated($"select * from structured_actors_search({str1}, {str2}, {str3}, {str4})");


            List<SearchResultStructuredActorSearch> searchResultsStructuredActorSearches = new List<SearchResultStructuredActorSearch>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.NameId}, {searchResult.PrimaryName}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsStructuredActorSearches.Add(searchResult);
            }
            return searchResultsStructuredActorSearches;
        }

        public List<SearchResultStructuredStringSearch> StructuredStringSearch(string userName, string str1, string str2, string str3, string str4)
        {
            Console.WriteLine("Search Results from Structured String Search");
            


            var result = _ctx.SearchResultStructuredStringSearches.FromSqlInterpolated($"select * from structured_string_search({userName}, {str1}, {str2}, {str3}, {str4})");


            List<SearchResultStructuredStringSearch> searchResultsStringSearches = new List<SearchResultStructuredStringSearch>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.TitleId}, {searchResult.PrimaryTitle}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsStringSearches.Add(searchResult);
            }
            return searchResultsStringSearches;
        }

        //Måske bare starte med void funktioner.
        public List<SearchResultsCoPlayers> FindCoPlayersByID(string id)
        {
            Console.WriteLine("Search Results Coplayers by Id");
            
            var actorsId = id;

            var result = _ctx.SearchResultsCoPlayers.FromSqlInterpolated($"select * from find_co_players_by_id({actorsId})");


            List<SearchResultsCoPlayers> searchResultsCoPlayers = new List<SearchResultsCoPlayers>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.CoPlayerId}, {searchResult.PrimaryName}, {searchResult.Frequency}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsCoPlayers.Add(searchResult);
            }
            return searchResultsCoPlayers;
        }

        public List<SearchResultsCoPlayers> FindCoPlayers(string actorName)
        {
            Console.WriteLine("Search Results Coplayers");
            
            var actorsName = actorName;
            var result = _ctx.SearchResultsCoPlayers.FromSqlInterpolated($"select * from find_co_players({actorsName})"); ;

            List<SearchResultsCoPlayers> searchResultsCoPlayers = new List<SearchResultsCoPlayers>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.CoPlayerId}, {searchResult.PrimaryName}, {searchResult.Frequency}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsCoPlayers.Add(searchResult);
            }
            return searchResultsCoPlayers;
        }

        public List<SearchResultsPopularActorsInMovie> PopularActorsInMovieSearch(string titleId)
        {
            Console.WriteLine("Popular Actors In Movie " + titleId);
            var movieTitleId = titleId;
            
            var result = _ctx.SearchResultsPopularActorsInMovies.FromSqlInterpolated($"select * from popular_actors_in_movie({movieTitleId})"); ;

            List<SearchResultsPopularActorsInMovie> searchResultsPopularActorsInMovies = new List<SearchResultsPopularActorsInMovie>();

            foreach (var searchResult in result) //Cannot handle null values. Added question mark, so okay. Now does not print properly.
            {
                Console.WriteLine($"{searchResult.Id}, {searchResult.Primaryname}, {searchResult.Rating}"); //only shows firsts letters for some reason. Just had to change from char to string in object.
                searchResultsPopularActorsInMovies.Add(searchResult);
            }
            return searchResultsPopularActorsInMovies;
        }

        public List<SearchResultExactMatch> ExactMatchSearch(params string[] words) //First input is connection, rest are list of input elements
        {
            Console.WriteLine("Exact Match");
            var query = "select * from exact_match('" + words[0] + "'";

            for (int i = 1; i < words.Length; i++) //Måske out of bounds hvis der kun er 1? Kan lige prøve. Nope. Det fungerer.
            {
                query += ", '" + words[i] + "'";
            }
            query += ")";

            Console.WriteLine(query);
            
            var result = _ctx.SearchResultsExactMatches.FromSqlRaw(query); //SQL injections could be a danger, but had to construct string as we did not know number of arguments. 


            List<SearchResultExactMatch> searchResultExactMatches = new List<SearchResultExactMatch>();

            foreach (var searchResult in result)
            {
                Console.WriteLine($"{searchResult.TitleId}, {searchResult.PrimaryTitle}"); //Skal returne i liste af objekter.
                searchResultExactMatches.Add(searchResult);
                //Console.Read();
            }

            return searchResultExactMatches;
        }

        public List<SearchResultBestMatch> BestMatchSearch(params string[] words)
        {
            Console.WriteLine("Best Match");
            var query = "select * from best_match('" + words[0] + "'";

            for (int i = 1; i < words.Length; i++) //Måske out of bounds hvis der kun er 1? Kan lige prøve. Nope. Det fungerer.
            {
                query += ", '" + words[i] + "'";
            }
            query += ")";

            Console.WriteLine(query);
            

            //var apple = "apple";

            //var result = ctx.SearchResults.FromSqlRaw("select * from search({0})", "%ab%");
            //var result = ctx.SearchResultsBestMatches.FromSqlInterpolated($"select * from best_match({"apple"}, {"mads"}, {"mikkelsen"})");
            var result = _ctx.SearchResultsBestMatches.FromSqlRaw(query);

            List<SearchResultBestMatch> searchResultBestMatches = new List<SearchResultBestMatch>();

            foreach (var searchResult in result)
            {
                Console.WriteLine($"{searchResult.TitleId}, {searchResult.PrimaryTitle}, {searchResult.Rank}");
                searchResultBestMatches.Add(searchResult);
                //Console.Read();
            }

            return searchResultBestMatches; //return list of search results.
        }


        public void AddUser(string usersname, string userPassword, bool isUserAdmin)
        {
            Console.WriteLine("ADO from Entity Framework");

            var username = usersname;
            var password = userPassword;
            bool isAdmin = isUserAdmin;


            _ctx.Database.ExecuteSqlInterpolated($"select registerUser({username},{password},{isAdmin})");
            Console.WriteLine("user added, also deez nuts");
        }

        public void AddRating(string titleId, int vote, string user, string comment) //ratings function from sql
        {
            Console.WriteLine("Adding rating");
            
            _ctx.Database.ExecuteSqlInterpolated($"select ratings({titleId}, {vote}, {user}, {comment})");
            Console.WriteLine("rating added, also deez nuts");
        }

    }
}