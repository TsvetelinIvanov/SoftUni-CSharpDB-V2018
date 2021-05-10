using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using VaporStore.Data;
using Newtonsoft.Json;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Export;

namespace VaporStore.DataProcessor
{
    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var genres = context.Genres.Where(g => genreNames.Any(gn => gn == g.Name))
                .Select(g => new
                {
                    g.Id,
                    Genre = g.Name,
                    Games = g.Games.Where(gm => gm.Purchases.Count >= 1)
                    .Select(gm => new
                    {
                        gm.Id,
                        Title = gm.Name,
                        Developer = gm.Developer.Name,
                        Tags = string.Join(", ", gm.GameTags.Select(gt => gt.Tag.Name)),
                        Players = gm.Purchases.Count
                    })
                    .OrderByDescending(gm => gm.Players)
                    .ThenBy(gm => gm.Id)
                    .ToArray(),
                    TotalPlayers = g.Games.Where(gm => gm.Purchases.Count >= 1).Sum(s => s.Purchases.Count)
                }).ToArray();

            string jsonString = JsonConvert.SerializeObject(genres, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            PurchaseType storeTypeEnum = Enum.Parse<PurchaseType>(storeType);
            ExportUserDto[] exportUserDtos = context.Users.Select(u => new ExportUserDto
            {
                Username = u.Username,
                Purchases = u.Cards.SelectMany(c => c.Purchases).Where(p => p.Type == storeTypeEnum)
                            .Select(p => new ExportPurchaseDto
                            {
                                Card = p.Card.Number,
                                Cvc = p.Card.Cvc,
                                Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                Game = new PurchaseGameDto
                                {
                                    Title = p.Game.Name,
                                    Genre = p.Game.Genre.Name,
                                    Price = p.Game.Price
                                }
                            })
                            .OrderBy(p => p.Date)
                            .ToArray(),
                TotalSpent = u.Cards.SelectMany(p => p.Purchases).Where(p => p.Type == storeTypeEnum)
                           .Sum(p => p.Game.Price)
            })
            .Where(u => u.Purchases.Any())
            .OrderByDescending(u => u.TotalSpent)
            .ThenBy(u => u.Username)
            .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));
            StringBuilder exportUserPurchasesBuilder = new StringBuilder();
            XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            serializer.Serialize(new StringWriter(exportUserPurchasesBuilder), exportUserDtos, serializerNamespaces);

            return exportUserPurchasesBuilder.ToString();
        }
	}
}