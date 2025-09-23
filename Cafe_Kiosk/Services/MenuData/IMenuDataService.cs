using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.MenuData
{
    public interface IMenuDataService
    {
        List<CafeMenuItem> LoadCafeMenu();
    }
}