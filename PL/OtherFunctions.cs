
using System.Windows;
using System.Windows.Controls;

namespace PL;

static internal class OtherFunctions
{
    /// <summary>
    /// help function that recive a ComboBox of categories and returns the appropriate category
    /// </summary>
    /// <param name="textBox"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    static public BO.Categories CategoryParse(ComboBox comboBox)
    {
        switch (comboBox.Text)
        {
            case "Simple":
                return BO.Categories.Simple;
            case "Suv":
                return BO.Categories.Suv;
            case "Luxury":
                return BO.Categories.Luxury;
            case "Sport":
                return BO.Categories.Sport;
            case "Electric":
                return BO.Categories.Electric;
            default:
                throw new System.Exception();
        }
    }
}
