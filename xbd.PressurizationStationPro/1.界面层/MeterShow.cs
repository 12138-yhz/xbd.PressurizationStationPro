using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbd.PressurizationStationPro
{
    public partial class MeterShow : UserControl
    {
        public MeterShow()
        {
            InitializeComponent();
        }

        private string parmName = "出水管温度";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取变量名称")]
        public string ParmName
        {
            get { return parmName; }
            set { parmName = value; 
                this.lbl_ParmName.Text = value;
            }
        }


        private float parmValue = 0.00f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取变量数值")]
        public float ParmValue
        {
            get { return parmValue; }
            set {
                if (parmValue != value)
                {
                    parmValue = value;
                    this.lbl_ParmValue.Text = value.ToString("f2") + " " + Unit;
                }
            }
        }

        private string unit = "℃";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取变量单位")]
        public string Unit
        {
            get { return unit; }
            set { unit = value;
                this.lbl_ParmValue.Text = ParmValue.ToString("f2") + " " + Unit;
            }
        }


        private float meterMaxValue = 100.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取仪表盘最大值")]
        public float MeterMaxValue
        {
            get { return meterMaxValue; }
            set
            {
                meterMaxValue = value;
                this.meter_Parm.MaxValue = MeterMaxValue;
            }
        }

        private float meterMinValue = 0.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取仪表盘最小值")]
        public float MeterMinValue
        {
            get { return meterMinValue; }
            set
            {
                meterMinValue = value;
                this.meter_Parm.MinValue = MeterMinValue;
            }
        }
    }
}
