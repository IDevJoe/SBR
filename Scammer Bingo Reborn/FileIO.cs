using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scammer_Bingo_Reborn
{
    static class FileIO
    {
        public static void SaveFile(string path, Settings.SavedSettings toSave)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                MemoryStream ms = Serialize(toSave);
                ms.WriteTo(fs);
            }
        }

        public static Settings.SavedSettings LoadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Deserialize<Settings.SavedSettings>(fs);
            }
        }

        static private MemoryStream Serialize(object toSerialize)
        {
            if (toSerialize != null)
            {
                MemoryStream stream = new MemoryStream();
                XmlSerializer formatter = new XmlSerializer(toSerialize.GetType());
                formatter.Serialize(stream, toSerialize);
                stream.Position = 0;
                return stream;
            }
            else return new MemoryStream();
        }

        static private T Deserialize<T>(Stream stream)
        {
            if (stream.Length > 0)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                stream.Position = 0;
                return (T)formatter.Deserialize(stream); ;
            }
            else return default(T);
        }

        internal static void UpgradeSaveFile(string dir)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = File.OpenRead(dir + "config.ini"))
                {
                    var binformatter = new BinaryFormatter();
                    var xmlformatter = new XmlSerializer(typeof(Settings.SavedSettings));

                    xmlformatter.Serialize(ms, (Settings.SavedSettings)binformatter.Deserialize(fs));

                }

                using (FileStream fs = File.Create(dir + "config.xml"))
                {
                    ms.Position = 0;
                    ms.WriteTo(fs);
                }

                File.Delete(dir + "config.ini");
            }
            
        }
    }
}
