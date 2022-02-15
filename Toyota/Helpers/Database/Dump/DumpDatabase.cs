using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Helpers.Database.Dump
{
    public class DumpDatabase : Dump
    {
        public DumpDatabase(ApplicationDbContext context) : base(context) { }
        public event Action<String> OnBackupCreated;

        public override String Create()
        {
            String fileName = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff")}-BackupFullDatabase.json";

            var db = context.Modifications
                     .Include(m => m.Model)
                     .Include(m => m.ModificationColors)
                     .ThenInclude(modColor => modColor.Color)
                     .ToList();

            var json = JsonSerializer.Serialize(db, new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve });

            File.WriteAllText(Path + fileName, json);

            String message = $"<a href='https://localhost:44306/storage/dumps/{fileName}' target='_top'>Download</a>";

            OnBackupCreated?.Invoke(message);

            return Path + fileName;
        }

        public override bool Restore(string filePath)
        {
            try
            {
                var backup = JsonSerializer.Deserialize<List<Modification>>(File.ReadAllText(filePath), new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve });

                ClearDatabase();
                LoadToDatabase(backup);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ClearDatabase()
        {
            var oldModifications = context.Modifications;
            var oldModels = context.Models;
            var oldColors = context.Colors;
            var oldModColors = context.ModificationColors;

            foreach (var color in oldColors)
                context.Remove(color);

            foreach (var modificaton in oldModifications)
                context.Remove(modificaton);

            foreach (var model in oldModels)
                context.Remove(model);

            foreach (var modColor in oldModColors)
                context.Remove(modColor);

            context.SaveChanges();
        }

        private void LoadToDatabase(List<Modification> backup)
        {
            foreach (var modification in backup)
                context.Add(modification);

            context.SaveChanges();
        }
    }
}
