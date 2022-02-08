using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Helpers.Database.Dump
{
    public class DumpModificationColors : Dump
    {
        public DumpModificationColors(ApplicationDbContext context) : base(context) { }

        public override String Create()
        {
            XmlSerializer formatter = new(typeof(List<ModificationColors>));

            String fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffff")}backupModificationColors.xml";

            using (FileStream fs = new FileStream(Path + fileName, FileMode.OpenOrCreate))
                formatter.Serialize(fs, context.ModificationColors.ToList());

            String message = $"<a href='https://localhost:44306/storage/dumps/{fileName}' target='_top'>Download</a>";

            Notification.Email.Send(message, "backup modification colors", "makstyulyukov@gmail.com");

            return Path + fileName;
        }

        public override List<ModificationColors> Restore(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ModificationColors>));

            using (Stream reader = new FileStream(filePath, FileMode.Open))
                return (List<ModificationColors>)serializer.Deserialize(reader);
        }
    }
}
