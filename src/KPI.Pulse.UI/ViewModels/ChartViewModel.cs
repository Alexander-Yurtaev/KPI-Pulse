using KPI.Pulse.UI.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPI.Pulse.UI.ViewModels
{
    public class ChartViewModel
    {
        public ISeries[] Series { get; init; }

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

        public ChartViewModel()
        {
            var uiService = Locator.Current.GetService<IUiService>() ??
                            throw new InvalidOperationException(nameof(IUiService));

            Series = uiService.CreateSeries();
        }
    }
}
