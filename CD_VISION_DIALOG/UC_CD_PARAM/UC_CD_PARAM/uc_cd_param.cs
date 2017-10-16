using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CD_Figure;
using DEF_PARAMS;

namespace CD_Paramter
{
    public partial class pnl_parameter: UserControl
    {
        List<string> listMeasurement = new List<string>();

        public pnl_parameter()
        {
            InitializeComponent();

            listMeasurement = IFX_ALGORITHM.ToList();

        }

        public void SetParam_RC_PAIR_DIG(PROPERTY_PairRct single){property.SelectedObject = single;}
        public void SetParam_Circle/***/(PROPERTY_PairCir single){property.SelectedObject = single;}
        public void SetParam_Overlay/**/(PROPERTY_PairOvl single) { property.SelectedObject = single; }

        private void property_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyGrid pg = (PropertyGrid)s;

            object objPrev = property.SelectedObject;
            object objCurr = pg.SelectedObjects[0];

            property.SelectedObject = objCurr;
          

        }
        public object GetCurrentData()
        {
            return property.SelectedObject;
        }

        public void ClearData()
        {
            property.SelectedObject = null;
        }

        public void SetSize(int width, int height)
        {
            this.Size = new Size(width, height);

        }
        
    }
}
