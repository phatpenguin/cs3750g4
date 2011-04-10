using System;
using System.Windows;
using System.Windows.Controls;

namespace Controls
{
    public class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            DataObject.AddPastingHandler(this, CheckPasteFormat);
        }

        private Boolean CheckFormat(string text)
        {
            Decimal val;
            return Decimal.TryParse(text, out val);
        }

        private void CheckPasteFormat(object sender, DataObjectPastingEventArgs e)
        {
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.Text, true);
            if (isText)
            {
                var text = e.SourceDataObject.GetData(DataFormats.Text) as string;
                if (CheckFormat(text))
                {
                    return;
                }
            }
            e.CancelCommand();
        }

        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!CheckFormat(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                base.OnPreviewTextInput(e);
            }
        }
    }
}
