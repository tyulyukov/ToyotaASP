using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Helpers.Database.Dump
{
    public class DumpModifications : Dump
    {
        public DumpModifications(ApplicationDbContext context) : base(context) { }

        public override String Create()
        {
            XmlSerializer formatter = new(typeof(List<Modification>));

            String fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffff")}backupModification.xml";

            using (FileStream fs = new FileStream(Path + fileName, FileMode.OpenOrCreate))
                formatter.Serialize(fs, context.Modifications.ToList());

            String message = $"<a href='https://localhost:44306/storage/dumps/{fileName}' target='_top'>Download</a>";

            Notification.Email.Send(message, "backup modifications", "makstyulyukov@gmail.com");

            return Path + fileName;
        }

        public override bool Restore(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Modification>));
            List<Modification> backup;

            using (Stream reader = new FileStream(filePath, FileMode.Open))
                backup = (List<Modification>)serializer.Deserialize(reader);

            return true;
        }
    }
}
