using System;
using DynamicData;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KPI.Pulse.UI.ViewModels.Tree;

public class Node : BaseTreeItem
{
    public Node(string icon, string title) : base(icon, title)
    {
        Children = [];
        XAxes = new ObservableCollection<Axis>(GetXAxes());
        YAxes = [];
        Series = [];
    }

    public List<BaseTreeItem> Children { get; set; }

    private readonly ObservableCollection<StackedColumnSeries<double>> _series;
    public ObservableCollection<StackedColumnSeries<double>> Series
    {
        get => _series;
        init => this.RaiseAndSetIfChanged(ref _series, value);
    }

    public ObservableCollection<Axis> XAxes { get; init; }
    public ObservableCollection<Axis> YAxes { get; init; }

    public void InitSeries()
    {
        Series.Clear();
        var series = GetSeries();
        YAxes.AddRange(GetYAxes(0, GetMax(series)));
        Series.AddRange(series);
    }

    private StackedColumnSeries<double>[] GetSeries()
    {
        var leaves = Children.OfType<Leaf>().ToArray();
        if (leaves.Any())
        {
            var series = leaves.Select(l => l.Series)
                .ToLookup(k => k.Name);

            return series.Select(Sum).ToArray();
        }

        var nodes = Children.OfType<Node>().ToArray();
        if (nodes.Any())
        {
            var series = nodes.SelectMany(n => n.GetSeries())
                .ToLookup(k => k.Name);

            return series.Select(Sum).ToArray();
        }

        return [];
    }

    private StackedColumnSeries<double> Sum(IGrouping<string?, StackedColumnSeries<double>> series)
    {
        var values = series.First().Values?.ToArray() ?? [];
        var result = new StackedColumnSeries<double>
        {
            Name = series.Key,
            Values = new ReadOnlyCollection<double>(values),
            Fill = series.First().Fill
        };

        foreach (var ser in series.ToArray().Skip(1))
        {
            var currentValues = ser.Values?.ToArray() ?? [];
            for (int i = 1; i < values.Length; i++)
            {
                values[i] += currentValues[i];
            }
        }
        return result;
    }

    private Axis[] GetXAxes()
    {
        Axis[] result = [
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
        ];

        return result;
    }

    private Axis[] GetYAxes(int? min = 0, int? max = 0)
    {
        Axis[] result = [
            new Axis
            {
                MinLimit = min,
                MaxLimit = max
            }
        ];

        return result;
    }

    private int GetMax(StackedColumnSeries<double>[] series)
    {
        var dict = series.ToDictionary(k => k.Name!, v => v.Values?.ToArray() ?? [0,0,0,0,0,0,0]);
        double[] max = [0,0,0,0,0,0,0];
        foreach (var key in dict.Keys)
        {
            var v = dict[key];
            for (int i = 0; i < v.Length; i++)
            {
                max[i] += v[i];
            }
        }
        return (int)Math.Ceiling(max.Max() * 1.1);
    }
}