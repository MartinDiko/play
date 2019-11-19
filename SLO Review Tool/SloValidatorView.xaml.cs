using SloReviewTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SloReviewTool
{
    /// <summary>
    /// Interaction logic for SloValidatorView.xaml
    /// </summary>
    public partial class SloValidatorView : Window
    {
        public SloValidatorView()
        {
            InitializeComponent();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                ThreadContext<SloParsingContext>.Set(new SloParsingContext(SloYamlText.Text));
                var slo = new SloDefinition(SloYamlText.Text);
                ResultsText.Text = "SLO Validation Succeeded";

            } catch (Exception ex) {
                ResultsText.Text = $"SLO Validation Failed: {ex.Message}";
            }
        }
    }
}
