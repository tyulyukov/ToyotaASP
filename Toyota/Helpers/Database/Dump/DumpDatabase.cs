using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Helpers.Database.Dump
{
    public class DumpDatabase : Dump
    {
        public DumpDatabase(ApplicationDbContext context) : base(context) { }

        public override String Create()
        {
            

            String fileName = Guid.NewGuid() + ".json";

            var db = context.Modifications
                     .Include(m => m.Model)
                     .Include(m => m.ModificationColors)
                     .ThenInclude(modColor => modColor.Color)
                     .ToList();

            var json = JsonSerializer.Serialize(db, new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve });

            File.WriteAllText(Path + fileName, json);

            String message = $"<a href='https://localhost:44306/storage/dumps/{fileName}' target='_top'>Download</a>";

            Notification.Email.Send(message, "backup database", "makstyulyukov@gmail.com");

            return Path + fileName;
        }

        public override List<Modification> Restore(string filePath)
        {
            return JsonSerializer.Deserialize<List<Modification>>(File.ReadAllText(filePath), new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve });
        }
    }
}
