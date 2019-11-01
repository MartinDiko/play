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

namespace SLO_Review_Tool
{
    /// <summary>
    /// Interaction logic for SloView.xaml
    /// </summary>
    public partial class SloView : Window
    {
        SloRecord slo_;

        public SloView(SloRecord slo)
        {
            slo_ = slo;
            this.DataContext = slo_;
            InitializeComponent();

            //YamlText.Text = slo_.YamlValue;
        }

        public SloRecord Slo {
            get {
                return slo_;
            }
        }
    }
}
