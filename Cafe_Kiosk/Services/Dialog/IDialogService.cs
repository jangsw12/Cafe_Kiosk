using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.Dialog
{
    public interface IDialogService
    {
        void ShowMenuOptionDialog(CafeMenuItem menuItem);
        void CloseMenuOptionDialog();
    }
}