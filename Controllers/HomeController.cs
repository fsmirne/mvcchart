using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using System.IO;
using System.Drawing;

namespace mvcchart.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Chart()
		{
			var chart = buildChart();
			StringBuilder result = new StringBuilder();
			result.Append(getChartImage(chart));
			result.Append(chart.GetHtmlImageMap("ImageMap"));
			return Content(result.ToString());
		}

		private Chart buildChart()
		{
			// Build Chart
			var chart = new Chart();

			chart.Width = 800;
			chart.Height = 600;

			// Create chart here
			chart.Titles.Add(CreateTitle());
			chart.Legends.Add(CreateLegend());
			chart.ChartAreas.Add(CreateChartArea());
			chart.Series.Add(CreateSeries());

			return chart;
		}

		private string getChartImage(Chart chart)
		{
			using (var stream = new MemoryStream())
			{
				string img = "<img src='data:image/png;base64,{0}' alt='' usemap='#ImageMap'>";
				chart.SaveImage(stream, ChartImageFormat.Png);
				string encoded = Convert.ToBase64String(stream.ToArray());
				return String.Format(img, encoded);
			}
		}

		private Title CreateTitle()
		{
			Title title = new Title();
			title.Text = "Result Chart";
			title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
			title.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
			title.ShadowOffset = 3;
			title.ForeColor = Color.FromArgb(26, 59, 105);
			return title;
		}

		private Legend CreateLegend()
		{
			Legend legend = new Legend();
			legend.Enabled = true;
			legend.ShadowColor = Color.FromArgb(32, 0, 0, 0);
			legend.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
			legend.ShadowOffset = 3;
			legend.ForeColor = Color.FromArgb(26, 59, 105);
			legend.Title = "Legend";
			return legend;
		}

		private ChartArea CreateChartArea()
		{
			ChartArea chartArea = new ChartArea();
			chartArea.Name = "Result Chart";
			chartArea.BackColor = Color.Transparent;
			chartArea.AxisX.IsLabelAutoFit = false;
			chartArea.AxisY.IsLabelAutoFit = false;
			chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
			chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
			chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
			chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
			chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
			chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
			chartArea.AxisX.Interval = 1;
			return chartArea;
		}

		public Series CreateSeries()
		{
			Series seriesDetail = new Series();
			seriesDetail.Name = "Result Chart";
			seriesDetail.IsValueShownAsLabel = false;
			seriesDetail.Color = Color.FromArgb(198, 99, 99);
			seriesDetail.ChartType = SeriesChartType.Bar;
			seriesDetail.BorderWidth = 2;

			for (int i = 1; i < 10; i++)
			{
				var p = seriesDetail.Points.Add(i);
				p.Label = String.Format("Point {0}", i);
				p.LabelMapAreaAttributes = String.Format("href=\"javascript:void(0)\" onclick=\"myfunction('{0}')\"", i);
				p["BarLabelStyle"] = "Center";
			}

			seriesDetail.ChartArea = "Result Chart";
			return seriesDetail;
		}
	}
}
