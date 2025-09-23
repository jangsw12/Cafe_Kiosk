using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.MenuData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cafe_Kiosk.Services
{
    public class MenuDataService : IMenuDataService
    {
        private const string filePath = @"Data/menu.xml";

        public List<CafeMenuItem> LoadCafeMenu()
        {
            if (!File.Exists(filePath))
                return new List<CafeMenuItem>();
        
            var serializer = new XmlSerializer(typeof(List<CafeMenuItem>));
            using var reader = new StreamReader(filePath);

            return (List<CafeMenuItem>)serializer.Deserialize(reader);
        }
    }
}