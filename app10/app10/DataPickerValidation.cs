using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace app10
{
    public class DataPickerValidation : TriggerAction<DatePicker>
    {
        protected override void Invoke(DatePicker sender)
        {
            if (sender.Date > DateTime.Now.Date)
            {
               sender.TextColor = Color.Red;
            }
            else
            {
                sender.TextColor = Color.Default;
            }
        }
    }
}
