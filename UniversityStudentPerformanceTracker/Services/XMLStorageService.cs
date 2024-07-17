using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace UniversityStudentPerformanceTrackerApi.Services
{
    public class XMLStorageService
    {
        public void SaveData<T>(string filePath, List<T> data)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public List<T> LoadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            var serializer = new XmlSerializer(typeof(List<T>));
            using (var reader = new StreamReader(filePath))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }
    }
}
