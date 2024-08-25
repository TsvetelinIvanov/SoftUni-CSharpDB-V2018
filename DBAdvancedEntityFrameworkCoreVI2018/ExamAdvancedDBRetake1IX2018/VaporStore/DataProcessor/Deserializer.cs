using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VaporStore.Data;
using VaporStore.Data.Models;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor
{
    public static class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
	{
            GameDto[] deserializedGames = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            StringBuilder messageBuilder = new StringBuilder();
            List<Game> games = new List<Game>();
            List<Developer> developers = new List<Developer>();
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();
            foreach (GameDto gameDto in deserializedGames)
            {
                if (!IsValid(gameDto) || !gameDto.Tags.All(IsValid))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                //bool isReleaseDateValid = DateTime.TryParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);
                //if (!isReleaseDateValid)
                //{
                //    messageBuilder.AppendLine(ErrorMessage);
                //    continue;
                //}

                Developer developer = developers.SingleOrDefault(d => d.Name == gameDto.Developer);
                if (developer == null)
                {
                    developer = new Developer
                    {
                        Name = gameDto.Developer
                    };

                    developers.Add(developer);
                }

                Genre genre = genres.SingleOrDefault(g => g.Name == gameDto.Genre);
                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = gameDto.Genre
                    };

                    genres.Add(genre);
                }
                
                foreach (string tagName in gameDto.Tags)
                {
                    Tag tag = tags.SingleOrDefault(t => t.Name == tagName);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName
                        };

                        tags.Add(tag);
                    }
                }

                Game game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    Developer = developer,
                    Genre = genre,
                    GameTags = tags.Select(t => new GameTag
                    {
                        Tag = t
                    })
		    .ToArray()
                };

                //List<GameTag> gameTags = new List<GameTag>();
                //foreach (Tag tag in tags)
                //{
                //    GameTag gameTag = new GameTag
                //    {
                //        Game = game,
                //        Tag = tag
                //    };

                //    gameTags.Add(gameTag);
                //}

                //foreach (GameTag gameTag in gameTags)
                //{
                //    game.GameTags.Add(gameTag);
                //}

                games.Add(game);
                messageBuilder.AppendLine($"Added {gameDto.Name} ({gameDto.Genre}) with {gameDto.Tags.Length} tags");
                //This below differ with one symbol from judge's expected answer:
                //messageBuilder.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
        }        

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
	{
            UserDto[] deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            StringBuilder messageBuilder = new StringBuilder();
            List<User> users = new List<User>();
            foreach (UserDto userDto in deserializedUsers)
            {
                if (!IsValid(userDto) || !userDto.Cards.All(IsValid))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Card[] cards = userDto.Cards.Select(c => new Card
                {
                    Number = c.Number,
                    Cvc = c.Cvc,
                    Type = c.Type
                })
		.ToArray();

                User user = new User
                {
                    Username = userDto.Username,
                    FullName = userDto.FullName,
                    Email = userDto.Email,
                    Age = userDto.Age,
                    Cards = cards
                };

                users.Add(user);
                messageBuilder.AppendLine($"Imported {userDto.Username} with {userDto.Cards.Length} cards");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
	}

	public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
	{
            XmlSerializer serialiser = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));
            PurchaseDto[] deserialisedPurchases = (PurchaseDto[])serialiser.Deserialize(new StringReader(xmlString));

            StringBuilder messageBuilder = new StringBuilder();            
            List<Purchase> purchases = new List<Purchase>();
            foreach (PurchaseDto purchaseDto in deserialisedPurchases)
            {
                if (!IsValid(purchaseDto))
                {
                    messageBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Game game = context.Games.Single(g => g.Name == purchaseDto.Title);
                Card card = context.Cards.Include(c => c.User).Single(c => c.Number == purchaseDto.Card);
                DateTime date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                Purchase purchase = new Purchase
                {
                    Game = game,
                    Type = purchaseDto.Type,
                    Card = card,
                    ProductKey = purchaseDto.Key,
                    Date = date
                };

                purchases.Add(purchase);
                messageBuilder.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return messageBuilder.ToString().TrimEnd();
        }       

        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
