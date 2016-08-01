using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

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
                IFormatter formatter = new BinaryFormatter();
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
                IFormatter formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                object o = formatter.Deserialize(stream);
                return (T)o;
            }
            else return default(T);
        }
    }
}
