using System;
using System.Collections.Generic;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace KPI.Pulse.UI.ViewModels
{
    public class ChartViewModel
    {
        public ISeries[] Series { get; set; } = CreateSeries();

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Labels = ["Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"],
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                ForceStepToMin = true,
                MinStep = 1
            }
        };

        private static ISeries[] CreateSeries()
        {
            var random = new Random();

            var result = new List<ISeries>
            {
                new ColumnSeries<double>
                {
                    Name = "Выручка",
                    Values = Enumerable.Range(1, 7).Select(_ => 100 * random.NextDouble()).ToArray(),
                    Fill = new SolidColorPaint(new SKColor(0x66, 0x7e, 0xea)) //#667eea
                },
                new ColumnSeries<double>
                {
                    Name = "Прибыль",
                    Values = Enumerable.Range(1, 7).Select(_ => 100 * random.NextDouble()).ToArray(),
                    Fill = new SolidColorPaint(new SKColor(0x48, 0xbb, 0x78)) //#48bb78
                },
                new ColumnSeries<double>
                {
                    Name = "Клиенты",
                    Values = Enumerable.Range(1, 7).Select(_ => 100 * random.NextDouble()).ToArray(),
                    Fill = new SolidColorPaint(new SKColor(0xf6, 0xad, 0x55)) //#f6ad55
                }
            };

            return result.ToArray();
        }
    }
}
